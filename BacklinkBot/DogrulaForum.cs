using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BacklinkBot
{
    public partial class DogrulaForm : Form
    {
        private CancellationTokenSource cancellationTokenSource;
        private bool isValidationRunning = false;
        private int totalTested = 0;
        private int activeLinks = 0;
        private int commentAreaLinks = 0;
        private int deadLinks = 0;
        private ConcurrentBag<ValidatedLink> validLinksWithComments;
        private ConcurrentBag<ValidatedLink> allResults;

        // SÜPER HIZLI HTTP CLIENT POOL
        private static readonly HttpClient httpClient = new HttpClient(new HttpClientHandler()
        {
            MaxConnectionsPerServer = 50, // Aynı anda 50 bağlantı
            UseCookies = false,
            UseDefaultCredentials = false
        })
        {
            Timeout = TimeSpan.FromSeconds(8), // 8 saniye timeout
            DefaultRequestHeaders =
            {
                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36" }
            }
        };

        // PARALELİZM AYARLARI
        private readonly ParallelOptions parallelOptions;
        private readonly SemaphoreSlim semaphore;

        // YORUM ALANI TESPİT REGEXLERI - SÜPER HIZLI
        private static readonly Regex[] CommentPatterns = {
            new Regex(@"<form[^>]*>[\s\S]*?<textarea[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex(@"<form[^>]*>[\s\S]*?<input[^>]*type=[""']text[""'][^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex(@"<textarea[^>]*name=[""']?(comment|yorum|mesaj|message)[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex(@"<input[^>]*name=[""']?(comment|yorum|mesaj|message|name|email)[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex(@"(comment|yorum|mesaj|reply|yanıt|cevap)[\s]*form", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex(@"class=[""'][^""']*comment[^""']*[""']", RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex(@"id=[""'][^""']*comment[^""']*[""']", RegexOptions.IgnoreCase | RegexOptions.Compiled)
        };

        public class ValidatedLink
        {
            public string Url { get; set; }
            public bool IsActive { get; set; }
            public bool HasCommentArea { get; set; }
            public int StatusCode { get; set; }
            public string ErrorMessage { get; set; }
            public TimeSpan ResponseTime { get; set; }
        }

        public DogrulaForm()
        {
            InitializeComponent();
            InitializeForm();

            // CONCURRENT BAG INITIALIZE
            validLinksWithComments = new ConcurrentBag<ValidatedLink>();
            allResults = new ConcurrentBag<ValidatedLink>();

            // PARALELİZM SETUP - CPU CORE SAYISINA GÖRE
            int maxDegreeOfParallelism = Environment.ProcessorCount * 4; // 4x parallelism
            semaphore = new SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism);

            parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };
        }

        public DogrulaForm(string[] urls) : this()
        {
            LoadUrls(urls);
        }

        private void InitializeForm()
        {
            // Event handlers
            this.btnGoBack.Click += BtnGoBack_Click;
            this.btnLoadFile.Click += BtnLoadFile_Click;
            this.btnStartValidation.Click += BtnStartValidation_Click;
            this.btnStop.Click += BtnStop_Click;
            this.btnSaveResults.Click += BtnSaveResults_Click;
            this.btnClearLog.Click += BtnClearLog_Click;

            AddLog("🚀 SÜPER HIZLI Link Doğrulama Sistemi hazır...");
            AddLog($"⚡ {Environment.ProcessorCount * 4} paralel işlem desteği aktif");
            AddLog("💡 İpucu: Binlerce URL'yi saniyeler içinde test eder!");
        }

        private void LoadUrls(string[] urls)
        {
            txtUrlInput.Clear();
            foreach (string url in urls)
            {
                txtUrlInput.AppendText(url + Environment.NewLine);
            }
            AddLog($"📥 {urls.Length} URL yüklendi");
        }

        private void BtnGoBack_Click(object sender, EventArgs e) => this.Close();

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "URL Listesi Dosyası Seçin";
                    openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var lines = File.ReadAllLines(openFileDialog.FileName, Encoding.UTF8);
                        txtUrlInput.Clear();

                        int validUrls = 0;
                        foreach (string line in lines)
                        {
                            string trimmedLine = line.Trim();
                            if (!string.IsNullOrEmpty(trimmedLine) && IsValidUrl(trimmedLine))
                            {
                                txtUrlInput.AppendText(trimmedLine + Environment.NewLine);
                                validUrls++;
                            }
                        }

                        AddLog($"📁 Dosya yüklendi: {validUrls} geçerli URL bulundu");
                        MessageBox.Show($"🚀 BAŞARILI!\n{validUrls} geçerli URL yüklendi\nSüper hızlı doğrulama için hazır!",
                                       "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog($"❌ Dosya yükleme hatası: {ex.Message}");
                MessageBox.Show($"Dosya yüklenirken hata oluştu:\n{ex.Message}",
                               "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnStartValidation_Click(object sender, EventArgs e)
        {
            if (isValidationRunning) return;

            string[] urls = GetUrlsFromInput();
            if (urls.Length == 0)
            {
                MessageBox.Show("⚠️ Lütfen önce URL listesi girin veya dosyadan yükleyin!",
                               "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await StartSuperFastValidation(urls);
        }

        private async Task StartSuperFastValidation(string[] urls)
        {
            isValidationRunning = true;
            cancellationTokenSource = new CancellationTokenSource();
            parallelOptions.CancellationToken = cancellationTokenSource.Token;

            // UI durumunu güncelle
            btnStartValidation.Enabled = false;
            btnStop.Enabled = true;
            btnSaveResults.Enabled = false;

            // Sonuçları temizle
            ResetStats();
            lvResults.Items.Clear();
            validLinksWithComments = new ConcurrentBag<ValidatedLink>(); // YENİ INSTANCE
            allResults = new ConcurrentBag<ValidatedLink>(); // YENİ INSTANCE

            var startTime = DateTime.Now;
            AddLog($"🚀 SÜPER HIZLI DOĞRULAMA BAŞLADI!");
            AddLog($"📊 {urls.Length} URL paralel olarak test ediliyor...");

            try
            {
                try
                {
                    // .NET FRAMEWORK UYUMLU PARALELLİ İŞLEM
                    await Task.Run(() =>
                    {
                        Parallel.ForEach(urls.Select((url, index) => new { url, index }),
                            parallelOptions,
                            item =>
                            {
                                semaphore.Wait(cancellationTokenSource.Token);
                                try
                                {
                                    // Async metodları Task.Run ile çalıştır
                                    var task = Task.Run(async () => await ValidateUrlSuperFast(item.url, cancellationTokenSource.Token));
                                    task.Wait(cancellationTokenSource.Token);
                                    var result = task.Result;

                                    allResults.Add(result);

                                    // Thread-safe UI güncelleme
                                    Invoke(new Action(() =>
                                    {
                                        AddResultToListView(result, item.index + 1);
                                        UpdateStatsRealTime();
                                    }));

                                    if (result.IsActive && result.HasCommentArea)
                                    {
                                        validLinksWithComments.Add(result);
                                    }
                                }
                                catch (OperationCanceledException)
                                {
                                    // İptal edildi, normal
                                }
                                catch (Exception ex)
                                {
                                    Invoke(new Action(() => AddLog($"❌ {item.url}: {ex.Message}")));
                                }
                                finally
                                {
                                    semaphore.Release();
                                }
                            });
                    });

                    if (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        var endTime = DateTime.Now;
                        var duration = endTime - startTime;

                        AddLog($"✅ DOĞRULAMA TAMAMLANDI!");
                        AddLog($"⚡ Süre: {duration.TotalSeconds:F2} saniye");
                        AddLog($"🎯 Hız: {urls.Length / duration.TotalSeconds:F1} URL/saniye");
                        AddLog($"💬 Yorum alanı olan: {validLinksWithComments.Count} site");

                        btnSaveResults.Enabled = validLinksWithComments.Count > 0;

                        MessageBox.Show($"🎉 SÜPER HIZLI DOĞRULAMA TAMAMLANDI!\n\n" +
                                       $"📊 Toplam: {urls.Length} URL\n" +
                                       $"✅ Aktif: {activeLinks} site\n" +
                                       $"💬 Yorum alanı: {commentAreaLinks} site\n" +
                                       $"⚡ Süre: {duration.TotalSeconds:F2} saniye\n" +
                                       $"🚀 Hız: {urls.Length / duration.TotalSeconds:F1} URL/saniye",
                                       "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                catch (OperationCanceledException)
                {
                    AddLog("⏹️ Doğrulama kullanıcı tarafından durduruldu");
                }
                catch (Exception ex)
                {
                    AddLog($"❌ HATA: {ex.Message}");
                }
                finally
                {
                    StopValidation();
                }
            }
            catch (OperationCanceledException)
            {
                AddLog("⏹️ Doğrulama kullanıcı tarafından durduruldu");
            }
            catch (Exception ex)
            {
                AddLog($"❌ HATA: {ex.Message}");
            }
            finally
            {
                StopValidation();
            }
        }
private async Task<ValidatedLink> ValidateUrlSuperFast(string url, CancellationToken cancellationToken)
        {
            var result = new ValidatedLink { Url = url };
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                    request.Headers.Add("Accept-Language", "tr-TR,tr;q=0.9,en;q=0.8");
                    request.Headers.Add("Cache-Control", "no-cache");

                    using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken))
                    {
                        result.StatusCode = (int)response.StatusCode;
                        result.ResponseTime = stopwatch.Elapsed;

                        if (response.IsSuccessStatusCode)
                        {
                            result.IsActive = true;

                            // SÜPER HIZLI HTML ANALİZİ
                            string content = await response.Content.ReadAsStringAsync();
                            result.HasCommentArea = HasCommentAreaSuperFast(content);

                            if (result.HasCommentArea)
                            {
                                Interlocked.Increment(ref commentAreaLinks);
                            }

                            Interlocked.Increment(ref activeLinks);
                        }
                        else
                        {
                            result.IsActive = false;
                            result.ErrorMessage = $"HTTP {result.StatusCode}";
                            Interlocked.Increment(ref deadLinks);
                        }
                    }
                }

                Interlocked.Increment(ref totalTested);
            }
            catch (TaskCanceledException)
            {
                result.ErrorMessage = "Timeout";
                result.IsActive = false;
                Interlocked.Increment(ref deadLinks);
                Interlocked.Increment(ref totalTested);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message.Length > 50 ? ex.Message.Substring(0, 50) + "..." : ex.Message;
                result.IsActive = false;
                Interlocked.Increment(ref deadLinks);
                Interlocked.Increment(ref totalTested);
            }

            stopwatch.Stop();
            result.ResponseTime = stopwatch.Elapsed;
            return result;
        }

        private bool HasCommentAreaSuperFast(string htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent) || htmlContent.Length < 100)
                return false;

            // SÜPER HIZLI REGEX KONTROLÜ
            foreach (var pattern in CommentPatterns)
            {
                if (pattern.IsMatch(htmlContent))
                    return true;
            }

            // EKSTRA HIZLI ANAHTAR KELİME KONTROLÜ
            var contentLower = htmlContent.ToLowerInvariant();
            return (contentLower.Contains("<form") && contentLower.Contains("<textarea")) ||
                   (contentLower.Contains("<form") && contentLower.Contains("type=\"text\"")) ||
                   (contentLower.Contains("comment") && (contentLower.Contains("<input") || contentLower.Contains("<textarea"))) ||
                   (contentLower.Contains("yorum") && (contentLower.Contains("<input") || contentLower.Contains("<textarea")));
        }

        private void AddResultToListView(ValidatedLink result, int index)
        {
            var item = new ListViewItem();

            if (result.IsActive)
            {
                item.SubItems[0].Text = "✅";
                item.SubItems.Add(result.Url);
                item.SubItems.Add("Aktif");
                item.SubItems.Add(result.HasCommentArea ? "✅ Var" : "❌ Yok");
                item.SubItems.Add($"HTTP {result.StatusCode} ({result.ResponseTime.TotalMilliseconds:F0}ms)");
                item.ForeColor = result.HasCommentArea ? Color.Green : Color.Orange;
                if (result.HasCommentArea)
                    item.BackColor = Color.LightGreen;
            }
            else
            {
                item.SubItems[0].Text = "❌";
                item.SubItems.Add(result.Url);
                item.SubItems.Add("Ölü");
                item.SubItems.Add("❌ Yok");
                item.SubItems.Add(result.ErrorMessage ?? "Bilinmeyen hata");
                item.ForeColor = Color.Red;
            }

            lvResults.Items.Add(item);
        }

        private void UpdateStatsRealTime()
        {
            lblTotalTested.Text = $"📊 Toplam Test Edilen: {totalTested}";
            lblActiveLinks.Text = $"✅ Aktif Linkler: {activeLinks}";
            lblCommentArea.Text = $"💬 Yorum Alanı Var: {commentAreaLinks}";
            lblDeadLinks.Text = $"❌ Ölü Linkler: {deadLinks}";

            if (totalTested > 0)
            {
                double successRate = (double)commentAreaLinks / totalTested * 100;
                lblSuccessRate.Text = $"🎯 Başarı Oranı: %{successRate:F1}";
            }
        }

        private string[] GetUrlsFromInput()
        {
            return txtUrlInput.Text
                .Split(new[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .Where(line => IsValidUrl(line))
                .ToArray();
        }

        private bool IsValidUrl(string url)
        {
            return !string.IsNullOrEmpty(url) &&
                   (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                    url.StartsWith("https://", StringComparison.OrdinalIgnoreCase));
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            AddLog("⏹️ Doğrulama durduruldu");
        }

        private void StopValidation()
        {
            isValidationRunning = false;
            btnStartValidation.Enabled = true;
            btnStop.Enabled = false;
        }

        private void BtnSaveResults_Click(object sender, EventArgs e)
        {
            if (validLinksWithComments.Count == 0)
            {
                MessageBox.Show("💡 Kaydedilecek geçerli link bulunamadı!\n" +
                               "Yorum alanı olan aktif linkler kaydetme için gereklidir.",
                               "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Geçerli Linkleri Kaydet";
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                    saveFileDialog.FileName = $"BASARILI_LINKLER_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var validLinks = validLinksWithComments.OrderBy(x => x.Url).ToList();
                        var sb = new StringBuilder();

                        sb.AppendLine("# =====================================================");
                        sb.AppendLine("# BAŞARILI LİNKLER - YORUM ALANI MEVCUT");
                        sb.AppendLine($"# Tarih: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                        sb.AppendLine($"# Toplam: {validLinks.Count} aktif link (yorum alanı mevcut)");
                        sb.AppendLine($"# Doğrulama Süresi: {validLinks.Average(x => x.ResponseTime.TotalMilliseconds):F0}ms ortalama");
                        sb.AppendLine("# =====================================================");
                        sb.AppendLine();

                        foreach (var link in validLinks)
                        {
                            sb.AppendLine($"{link.Url} # HTTP{link.StatusCode} - {link.ResponseTime.TotalMilliseconds:F0}ms");
                        }

                        sb.AppendLine();
                        sb.AppendLine("# =====================================================");
                        sb.AppendLine("# Bu linkler aktif ve yorum alanı bulunmaktadır");
                        sb.AppendLine("# BacklinkBot Pro tarafından doğrulanmıştır");
                        sb.AppendLine("# =====================================================");

                        File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);

                        AddLog($"💾 {validLinks.Count} başarılı link kaydedildi");
                        MessageBox.Show($"🎉 BAŞARIYLA KAYDEDİLDİ!\n\n" +
                                       $"📁 Dosya: {Path.GetFileName(saveFileDialog.FileName)}\n" +
                                       $"✅ Başarılı Link: {validLinks.Count}\n" +
                                       $"💬 Tümünde yorum alanı mevcut\n" +
                                       $"🚀 Backlink için hazır!",
                                       "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog($"❌ Kaydetme hatası: {ex.Message}");
                MessageBox.Show($"Kaydetme sırasında hata oluştu:\n{ex.Message}",
                               "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
            AddLog("🧹 Günlük temizlendi");
        }

        private void ResetStats()
        {
            totalTested = 0;
            activeLinks = 0;
            commentAreaLinks = 0;
            deadLinks = 0;
        }

        private void AddLog(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AddLog(message)));
                return;
            }

            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            rtbLog.AppendText($"[{timestamp}] {message}\n");
            rtbLog.ScrollToCaret();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cancellationTokenSource?.Cancel();
            base.OnFormClosing(e);
        }
    }


}