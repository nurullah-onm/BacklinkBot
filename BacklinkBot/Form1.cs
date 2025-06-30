using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
using HAP = HtmlAgilityPack;

namespace BacklinkBot
{
    public partial class Form1 : Form
    {
        // Ana değişkenler - GELİŞTİRİLMİŞ VERSİYON
        private static readonly HttpClient httpClient = new HttpClient();
        private CancellationTokenSource cancellationTokenSource;
        private bool isRunning = false;
        private int totalSites = 0;
        private int successCount = 0;
        private int failedCount = 0;
        private int currentSiteIndex = 0;
        private ConcurrentBag<string> processedSites = new ConcurrentBag<string>();

        // AYARLANIR PERFORMANS PARAMETRELERİ
        private const int MAX_PARALLEL_REQUESTS = 3;
        private int currentTimeoutSeconds = 30;
        private int currentSpeedLevel = 5;
        private const int RETRY_COUNT = 2;

        // HIZ SEVİYESİNE GÖRE GECİKME HESAPLAMALARI
        private int MinDelay => Math.Max(100, 2000 - (currentSpeedLevel * 180));
        private int MaxDelay => Math.Max(300, 4000 - (currentSpeedLevel * 360));

        // Threading ve animasyon
        private readonly object lockObject = new object();
        private SemaphoreSlim semaphore;
        private Random random = new Random();
        private System.Windows.Forms.Timer fadeTimer;

        static Form1()
        {
            // STATIC HTTP CLIENT AYARLARI
            httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "tr-TR,tr;q=0.8,en-US;q=0.5,en;q=0.3");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

            // HIZLI CONNECTION SETTINGS
            ServicePointManager.DefaultConnectionLimit = 20;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public Form1()
        {
            InitializeComponent();
            SetupFormAnimation();
            InitializeBot();
            SetupEventHandlers();
            LoadDefaultValues();
        }

        private void SetupFormAnimation()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;

            // FADE-IN ANİMASYON
            this.Opacity = 0.0;
            fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 50;
            fadeTimer.Tick += FadeTimer_Tick;

            this.KeyDown += Form1_KeyDown;
            this.Load += Form1_Load;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                ToggleFullScreen();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape && this.FormBorderStyle == FormBorderStyle.None)
            {
                ToggleFullScreen();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                BtnStart_Click(sender, e);
                e.Handled = true;
            }
        }

        private void ToggleFullScreen()
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Maximized;
                FastLog("🖥️ Normal mod", Color.Blue);
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                FastLog("🎮 Tam ekran mod (ESC ile çık)", Color.Blue);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fadeTimer.Start();
            FastLog("⚡ Form yüklendi - F11: Tam ekran, Ctrl+S: Başlat", Color.Gray);
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1.0)
            {
                this.Opacity += 0.1;
            }
            else
            {
                fadeTimer.Stop();
            }
        }

        private void InitializeBot()
        {
            semaphore = new SemaphoreSlim(MAX_PARALLEL_REQUESTS, MAX_PARALLEL_REQUESTS);
            UpdateHttpClientTimeout();

            FastLog("🚀 BacklinkBot Pro v1.0 başlatıldı!", Color.FromArgb(34, 197, 94));
            FastLog($"⚡ {MAX_PARALLEL_REQUESTS}x paralel işlem", Color.FromArgb(59, 130, 246));
            FastLog($"🏎️ Başlangıç timeout: {currentTimeoutSeconds}s, Hız: {currentSpeedLevel}/10", Color.FromArgb(168, 85, 247));
        }

        private void UpdateHttpClientTimeout()
        {
            httpClient.Timeout = TimeSpan.FromSeconds(currentTimeoutSeconds);
            FastLog($"⏱️ Timeout güncellendi: {currentTimeoutSeconds} saniye", Color.Orange);
        }

        private void SetupEventHandlers()
        {
            if (startButton != null) startButton.Click += BtnStart_Click;
            if (addButton != null) addButton.Click += BtnAdd_Click;
            if (loadFileButton != null) loadFileButton.Click += BtnLoadFile_Click;
            if (clearButton != null) clearButton.Click += BtnClear_Click;
            if (reportButton != null) reportButton.Click += BtnReport_Click;
            if (exitButton != null) exitButton.Click += BtnExit_Click;
            if (clearLogButton != null) clearLogButton.Click += BtnClearLog_Click;

            if (urlTextBox != null)
            {
                urlTextBox.KeyPress += TxtUrl_KeyPress;
                SetupUrlPlaceholder();
            }

            if (siteListBox != null)
            {
                siteListBox.SelectedIndexChanged += LstSites_SelectedIndexChanged;
            }

            if (speedTrackBar != null)
            {
                speedTrackBar.ValueChanged += speedTrackBar_ValueChanged;
            }

            if (timeoutNumericUpDown != null)
            {
                timeoutNumericUpDown.ValueChanged += timeoutNumericUpDown_ValueChanged;
            }
        }

        private void speedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (speedTrackBar != null && speedValueLabel != null)
            {
                currentSpeedLevel = speedTrackBar.Value;
                string speedText = GetSpeedText(currentSpeedLevel);
                speedValueLabel.Text = $"{speedText} ({currentSpeedLevel})";
                FastLog($"⚡ Hız seviyesi: {speedText} ({currentSpeedLevel}/10)", Color.Blue);
            }
        }

        private string GetSpeedText(int speed)
        {
            switch (speed)
            {
                case 1: return "Çok Yavaş";
                case 2: return "Yavaş";
                case 3: return "Düşük";
                case 4: return "Düşük-Orta";
                case 5: return "Orta";
                case 6: return "Orta-Hızlı";
                case 7: return "Hızlı";
                case 8: return "Çok Hızlı";
                case 9: return "Turbo";
                case 10: return "Maximum";
                default: return "Orta";
            }
        }

        private void timeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (timeoutNumericUpDown != null)
            {
                currentTimeoutSeconds = (int)timeoutNumericUpDown.Value;
                UpdateHttpClientTimeout();
            }
        }

        private void SetupUrlPlaceholder()
        {
            if (urlTextBox != null)
            {
                urlTextBox.Text = "https://";
                urlTextBox.ForeColor = Color.Gray;

                urlTextBox.Enter += (s, e) => {
                    if (urlTextBox.Text == "https://")
                    {
                        urlTextBox.Text = "";
                        urlTextBox.ForeColor = Color.Black;
                    }
                };

                urlTextBox.Leave += (s, e) => {
                    if (string.IsNullOrWhiteSpace(urlTextBox.Text))
                    {
                        urlTextBox.Text = "https://";
                        urlTextBox.ForeColor = Color.Gray;
                    }
                };
            }
        }

        private void LoadDefaultValues()
        {
            if (nameTextBox != null) nameTextBox.Text = "Ahmet Yılmaz";
            if (emailTextBox != null) emailTextBox.Text = "ahmet@example.com";
            if (locationTextBox != null) locationTextBox.Text = "İstanbul";
            if (webTextBox != null) webTextBox.Text = "https://example.com";
            if (messageTextBox != null) messageTextBox.Text = "Merhaba! Harika bir siteniz var. Ziyaret etmekten keyif aldım. Teşekkürler!";

            if (speedTrackBar != null)
            {
                speedTrackBar.Value = 5;
                currentSpeedLevel = 5;
            }

            if (timeoutNumericUpDown != null)
            {
                timeoutNumericUpDown.Value = 30;
                currentTimeoutSeconds = 30;
            }

            if (speedValueLabel != null)
            {
                speedValueLabel.Text = "Orta (5)";
            }

            UpdateCounters();
            UpdateStatusLabels();
            if (statusLabel != null) statusLabel.Text = "✨ Hazır - İşlem bekleniyor...";
        }

        // =============== BUTTON EVENT HANDLERS ===============

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                StopBot();
                return;
            }

            if (!QuickValidate()) return;

            UpdateHttpClientTimeout();
            await StartAdvancedBot();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (urlTextBox == null || siteListBox == null) return;

            string url = urlTextBox.Text.Trim();

            if (string.IsNullOrEmpty(url) || url == "https://") return;
            if (!IsValidUrl(url))
            {
                FastLog("⚠️ Geçersiz URL formatı!", Color.Orange);
                return;
            }
            if (siteListBox.Items.Contains(url))
            {
                FastLog("⚠️ URL zaten listede!", Color.Orange);
                return;
            }

            siteListBox.Items.Add(url);
            urlTextBox.Text = "https://";
            urlTextBox.ForeColor = Color.Gray;
            urlTextBox.Focus();

            UpdateCounters();
            FastLog("➕ " + url, Color.Green);
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Text Files|*.txt|All Files|*.*";
                dialog.Title = "Site listesi seç";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Task.Run(() => LoadFileAsync(dialog.FileName));
                }
            }
        }

        private void LoadFileAsync(string fileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
                int added = 0;

                this.Invoke(new MethodInvoker(delegate
                {
                    foreach (string line in lines)
                    {
                        string url = line.Trim();
                        if (IsValidUrl(url) && siteListBox != null && !siteListBox.Items.Contains(url))
                        {
                            siteListBox.Items.Add(url);
                            added++;
                        }
                    }
                    UpdateCounters();
                }));

                FastLog($"📁 {added} site yüklendi", Color.Purple);
            }
            catch (Exception ex)
            {
                FastLog("❌ Dosya hatası: " + ex.Message, Color.Red);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (siteListBox == null || siteListBox.Items.Count == 0) return;

            var result = MessageBox.Show("Tüm siteleri listeden kaldırmak istediğinizden emin misiniz?",
                                       "Liste Temizle",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                siteListBox.Items.Clear();
                UpdateCounters();
                FastLog("🗑️ Liste temizlendi", Color.Red);
            }
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            Task.Run(() => GenerateAdvancedReport());
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                var result = MessageBox.Show("İşlem devam ediyor. Çıkmak istediğinizden emin misiniz?",
                                           "Çıkış",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;
            }

            Application.Exit();
        }

        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            if (logTextBox == null) return;
            logTextBox.Clear();
            FastLog("🧹 Günlük temizlendi", Color.Gray);
        }

        private void TxtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnAdd_Click(sender, e);
                e.Handled = true;
            }
        }

        private void LstSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (siteListBox != null && siteListBox.SelectedItem != null)
            {
                FastLog("🎯 Seçilen: " + siteListBox.SelectedItem.ToString(), Color.Blue);
            }
        }

        // =============== GELIŞTIRILMIŞ BOT ENGINE ===============

        private bool QuickValidate()
        {
            if (nameTextBox == null || string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                FastLog("⚠️ Ad alanı gerekli!", Color.Orange);
                if (nameTextBox != null) nameTextBox.Focus();
                return false;
            }
            if (emailTextBox == null || string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                FastLog("⚠️ E-mail alanı gerekli!", Color.Orange);
                if (emailTextBox != null) emailTextBox.Focus();
                return false;
            }
            if (siteListBox == null || siteListBox.Items.Count == 0)
            {
                FastLog("⚠️ Site listesi boş! Önce site ekleyin.", Color.Orange);
                if (urlTextBox != null) urlTextBox.Focus();
                return false;
            }
            return true;
        }

        private async Task StartAdvancedBot()
        {
            isRunning = true;
            cancellationTokenSource = new CancellationTokenSource();

            UpdateButton(startButton, "⏹️ DURDUR", Color.FromArgb(239, 68, 68));

            totalSites = siteListBox != null ? siteListBox.Items.Count : 0;
            successCount = 0;
            failedCount = 0;
            currentSiteIndex = 0;
            processedSites = new ConcurrentBag<string>();

            SetProgress(0, totalSites);
            FastLog($"🚀 GELİŞTİRİLMİŞ BOT BAŞLADI!", Color.Green);
            FastLog($"📊 Toplam site: {totalSites}", Color.Blue);
            FastLog($"⚡ Hız seviyesi: {GetSpeedText(currentSpeedLevel)} ({currentSpeedLevel}/10)", Color.Blue);
            FastLog($"⏱️ Timeout: {currentTimeoutSeconds} saniye", Color.Blue);

            try
            {
                var sites = new List<string>();
                for (int i = 0; i < totalSites; i++)
                {
                    sites.Add(siteListBox.Items[i].ToString());
                }

                for (int i = 0; i < sites.Count; i += MAX_PARALLEL_REQUESTS)
                {
                    if (cancellationTokenSource.Token.IsCancellationRequested) break;

                    var batch = sites.Skip(i).Take(MAX_PARALLEL_REQUESTS);
                    var batchTasks = batch.Select(site => ProcessSiteWithAdvancedRetry(site, cancellationTokenSource.Token));

                    await Task.WhenAll(batchTasks);

                    int batchDelay = random.Next(MinDelay, MaxDelay);
                    await Task.Delay(batchDelay, cancellationTokenSource.Token);

                    UpdateStatus($"⚡ İşlenen: {Math.Min(i + MAX_PARALLEL_REQUESTS, totalSites)}/{totalSites} - Başarılı: {successCount}");
                    UpdateStatusLabels();
                }

                FastLog("🎉 TÜM SİTELER TAMAMLANDI!", Color.Green);
                UpdateStatus($"✨ İşlem tamamlandı - {successCount} başarılı, {failedCount} başarısız");

                double successRate = totalSites > 0 ? (double)successCount / totalSites * 100 : 0;
                FastLog($"📈 Başarı oranı: %{successRate:F1}", Color.Green);
            }
            catch (OperationCanceledException)
            {
                FastLog("⏹️ İşlem kullanıcı tarafından durduruldu", Color.Orange);
            }
            catch (Exception ex)
            {
                FastLog("💥 Genel hata: " + ex.Message, Color.Red);
            }
            finally
            {
                StopBot();
            }
        }

        private async Task<bool> ProcessSiteWithAdvancedRetry(string siteUrl, CancellationToken token)
        {
            await semaphore.WaitAsync(token);

            try
            {
                Interlocked.Increment(ref currentSiteIndex);

                this.Invoke(new MethodInvoker(delegate
                {
                    SetProgress(currentSiteIndex, totalSites);
                    UpdateStatus($"⚡ İşleniyor: {currentSiteIndex}/{totalSites} - {siteUrl}");
                }));

                FastLog($"🎯 [{currentSiteIndex}] {siteUrl}", Color.Blue);

                for (int attempt = 1; attempt <= RETRY_COUNT; attempt++)
                {
                    try
                    {
                        using (var cts = CancellationTokenSource.CreateLinkedTokenSource(token))
                        {
                            cts.CancelAfter(TimeSpan.FromSeconds(currentTimeoutSeconds));

                            bool success = await ProcessSiteAdvanced(siteUrl, cts.Token);

                            if (success)
                            {
                                Interlocked.Increment(ref successCount);
                                processedSites.Add(siteUrl);
                                FastLog($"✅ [{currentSiteIndex}] BAŞARILI", Color.Green);
                                return true;
                            }
                            else if (attempt < RETRY_COUNT)
                            {
                                FastLog($"🔄 [{currentSiteIndex}] Deneme {attempt}/{RETRY_COUNT} başarısız, tekrar denenecek...", Color.Orange);
                                await Task.Delay(1000, token);
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        FastLog($"⏱️ [{currentSiteIndex}] Zaman aşımı ({currentTimeoutSeconds}s)", Color.Orange);
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (attempt >= RETRY_COUNT)
                        {
                            string errorMsg = ex.Message.Length > 50 ? ex.Message.Substring(0, 50) + "..." : ex.Message;
                            FastLog($"❌ [{currentSiteIndex}] HATA: {errorMsg}", Color.Red);
                        }
                    }
                }

                Interlocked.Increment(ref failedCount);
                return false;
            }
            finally
            {
                semaphore.Release();
                int dynamicDelay = random.Next(MinDelay, MaxDelay);
                await Task.Delay(dynamicDelay, token);
            }
        }

        private async Task<bool> ProcessSiteAdvanced(string siteUrl, CancellationToken token)
        {
            try
            {
                string html = await httpClient.GetStringAsync(siteUrl);
                if (string.IsNullOrEmpty(html))
                {
                    FastLog("⚠️ Boş sayfa döndü", Color.Orange);
                    return false;
                }

                var doc = new HAP.HtmlDocument();
                doc.LoadHtml(html);

                string actionUrl = FindFormActionAdvanced(doc, siteUrl);
                if (string.IsNullOrEmpty(actionUrl))
                {
                    FastLog("⚠️ Form bulunamadı", Color.Orange);
                    return false;
                }

                string captcha = SolveCaptchaAdvanced(doc);
                var formData = PrepareFormDataAdvanced(captcha, doc);
                bool result = await SubmitFormAdvanced(actionUrl, formData, token);

                return result;
            }
            catch (Exception ex)
            {
                FastLog($"⚠️ İşlem hatası: {ex.Message}", Color.Orange);
                return false;
            }
        }

        private string FindFormActionAdvanced(HAP.HtmlDocument doc, string baseUrl)
        {
            try
            {
                var forms = doc.DocumentNode.SelectNodes("//form[" +
                    "(.//input[contains(@name,'name') or contains(@name,'ad') or contains(@name,'isim')] and " +
                    ".//input[contains(@name,'email') or contains(@name,'mail') or contains(@name,'eposta')] and " +
                    "(.//input[contains(@name,'message') or contains(@name,'mesaj') or contains(@name,'msg')] or .//textarea))" +
                    " or " +
                    "(.//input[@type='text'] and .//input[@type='email'] and (.//textarea or .//input[contains(@name,'message')]))" +
                    "]");

                if (forms != null && forms.Count > 0)
                {
                    var form = forms[0];
                    string action = form.GetAttributeValue("action", "");

                    if (string.IsNullOrEmpty(action) || action == "#")
                        return baseUrl;

                    if (action.StartsWith("http"))
                        return action;

                    if (action.StartsWith("/"))
                    {
                        var uri = new Uri(baseUrl);
                        return $"{uri.Scheme}://{uri.Host}{action}";
                    }

                    return $"{baseUrl.TrimEnd('/')}/{action.TrimStart('/')}";
                }
            }
            catch (Exception ex)
            {
                FastLog($"⚠️ Form arama hatası: {ex.Message}", Color.Orange);
            }
            return null;
        }

        private string SolveCaptchaAdvanced(HAP.HtmlDocument doc)
        {
            try
            {
                var captchaSelectors = new string[]
                {
                    "//span[contains(@style, 'BACKGROUND-COLOR: #000080')]",
                    "//span[@id='captcha']",
                    "//span[@class='captcha']",
                    "//*[contains(@class, 'captcha')]",
                    "//div[contains(@class, 'security')]//span",
                    "//label[contains(text(), 'güvenlik') or contains(text(), 'security')]/..//span",
                    "//span[contains(@style, 'background-color') and contains(@style, 'color')]"
                };

                foreach (var selector in captchaSelectors)
                {
                    var captchaNode = doc.DocumentNode.SelectSingleNode(selector);
                    if (captchaNode != null)
                    {
                        string text = captchaNode.InnerText;
                        if (!string.IsNullOrEmpty(text))
                        {
                            text = text.Replace("&nbsp;", "").Replace("\n", "").Replace("\r", "").Trim();
                            if (!string.IsNullOrEmpty(text) && text.Length > 0)
                            {
                                FastLog($"🔐 Captcha bulundu: {text}", Color.Yellow);
                                return text;
                            }
                        }
                    }
                }

                return "yok";
            }
            catch (Exception ex)
            {
                FastLog($"⚠️ Captcha çözme hatası: {ex.Message}", Color.Orange);
                return "yok";
            }
        }

        private Dictionary<string, string> PrepareFormDataAdvanced(string captcha, HAP.HtmlDocument doc)
        {
            var formData = new Dictionary<string, string>();

            try
            {
                var basicFields = new Dictionary<string, string>
                {
                    ["name"] = nameTextBox?.Text?.Trim() ?? "",
                    ["ad"] = nameTextBox?.Text?.Trim() ?? "",
                    ["isim"] = nameTextBox?.Text?.Trim() ?? "",
                    ["email"] = emailTextBox?.Text?.Trim() ?? "",
                    ["mail"] = emailTextBox?.Text?.Trim() ?? "",
                    ["eposta"] = emailTextBox?.Text?.Trim() ?? "",
                    ["location"] = locationTextBox?.Text?.Trim() ?? "",
                    ["yer"] = locationTextBox?.Text?.Trim() ?? "",
                    ["memleket"] = locationTextBox?.Text?.Trim() ?? "",
                    ["web"] = webTextBox?.Text?.Trim() ?? "",
                    ["website"] = webTextBox?.Text?.Trim() ?? "",
                    ["url"] = webTextBox?.Text?.Trim() ?? "",
                    ["message"] = messageTextBox?.Text?.Trim() ?? "",
                    ["mesaj"] = messageTextBox?.Text?.Trim() ?? "",
                    ["msg"] = messageTextBox?.Text?.Trim() ?? "",
                    ["yorum"] = messageTextBox?.Text?.Trim() ?? "",
                    ["captcha"] = captcha,
                    ["seccode"] = captcha,
                    ["security"] = captcha,
                    ["kod"] = captcha
                };

                foreach (var field in basicFields)
                {
                    formData[field.Key] = field.Value;
                }

                var hiddenInputs = doc.DocumentNode.SelectNodes("//input[@type='hidden']");
                if (hiddenInputs != null)
                {
                    foreach (var input in hiddenInputs)
                    {
                        string name = input.GetAttributeValue("name", "");
                        string value = input.GetAttributeValue("value", "");
                        if (!string.IsNullOrEmpty(name) && !formData.ContainsKey(name))
                        {
                            formData[name] = value;
                        }
                    }
                }

                FastLog($"📝 {formData.Count} alan hazırlandı", Color.Cyan);
            }
            catch (Exception ex)
            {
                FastLog($"⚠️ Form data hazırlama hatası: {ex.Message}", Color.Orange);
            }

            return formData;
        }

        private async Task<bool> SubmitFormAdvanced(string actionUrl, Dictionary<string, string> formData, CancellationToken token)
        {
            try
            {
                using (var content = new FormUrlEncodedContent(formData))
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, actionUrl)
                    {
                        Content = content
                    };

                    var baseUri = new Uri(actionUrl);
                    request.Headers.Add("Referer", $"{baseUri.Scheme}://{baseUri.Host}");

                    var response = await httpClient.SendAsync(request, token);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();

                        var successKeywords = new string[]
                        {
                            "teşekkür", "thank", "success", "başarı", "kaydedildi",
                            "saved", "submitted", "gönderildi", "added", "eklendi",
                            "mesajınız", "message sent", "başarıyla", "successfully"
                        };

                        bool isSuccess = false;
                        string lowerResponse = responseText.ToLowerInvariant();
                        foreach (var keyword in successKeywords)
                        {
                            if (lowerResponse.Contains(keyword.ToLowerInvariant()))
                            {
                                isSuccess = true;
                                break;
                            }
                        }

                        if (isSuccess)
                        {
                            FastLog("✅ Form başarıyla gönderildi", Color.Green);
                            return true;
                        }
                        else
                        {
                            FastLog("⚠️ Belirsiz sonuç - başarı mesajı bulunamadı", Color.Orange);
                            return false;
                        }
                    }
                    else
                    {
                        FastLog($"❌ HTTP hatası: {response.StatusCode}", Color.Red);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                FastLog($"❌ Submit hatası: {ex.Message}", Color.Red);
                return false;
            }
        }

        private void StopBot()
        {
            isRunning = false;
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
            UpdateButton(startButton, "🚀 BAŞLAT", Color.FromArgb(34, 197, 94));
            FastLog("⏹️ Bot durduruldu", Color.Orange);
        }

        // =============== LINK DOĞRULAYICI ===============

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Link doğrulayıcı formu burada açılacak
                MessageBox.Show("Link Doğrulayıcı henüz aktif değil. Geliştirme aşamasında...",
                               "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============== HELPER FUNCTIONS ===============

        private bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return false;

            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result) &&
                   (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }

        private void FastLog(string message, Color color)
        {
            if (logTextBox != null && logTextBox.InvokeRequired)
            {
                logTextBox.BeginInvoke(new Action(() => FastLog(message, color)));
                return;
            }

            if (logTextBox != null)
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                logTextBox.SelectionStart = logTextBox.TextLength;
                logTextBox.SelectionColor = color;
                logTextBox.AppendText($"[{timestamp}] {message}\n");
                logTextBox.ScrollToCaret();

                if (logTextBox.Lines.Length > 1000)
                {
                    var lines = logTextBox.Lines.Skip(200).ToArray();
                    logTextBox.Clear();
                    logTextBox.Lines = lines;
                    logTextBox.AppendText("\n[LOG OTOMATİK TEMİZLENDİ - PERFORMANS İÇİN]\n");
                }
            }
        }

        private void UpdateStatus(string status)
        {
            if (statusLabel != null && statusLabel.InvokeRequired)
            {
                statusLabel.BeginInvoke(new Action(() => UpdateStatus(status)));
                return;
            }
            if (statusLabel != null) statusLabel.Text = status;
        }

        private void UpdateCounters()
        {
            if (countLabel != null && countLabel.InvokeRequired)
            {
                countLabel.BeginInvoke(new Action(UpdateCounters));
                return;
            }
            if (countLabel != null && siteListBox != null)
            {
                countLabel.Text = $"📊 Toplam: {siteListBox.Items.Count} site";
            }
        }

        private void UpdateStatusLabels()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateStatusLabels));
                return;
            }

            if (successLabel != null)
                successLabel.Text = $"✅ Başarılı: {successCount}";

            if (failedLabel != null)
                failedLabel.Text = $"❌ Başarısız: {failedCount}";

            if (totalLabel != null)
                totalLabel.Text = $"📈 Toplam: {totalSites}";
        }

        private void SetProgress(int value, int max)
        {
            if (progressBar != null && progressBar.InvokeRequired)
            {
                progressBar.BeginInvoke(new Action(() => SetProgress(value, max)));
                return;
            }
            if (progressBar != null)
            {
                progressBar.Maximum = max > 0 ? max : 1;
                progressBar.Value = Math.Min(Math.Max(value, 0), progressBar.Maximum);
            }
        }

        private void UpdateButton(Button btn, string text, Color color)
        {
            if (btn != null && btn.InvokeRequired)
            {
                btn.BeginInvoke(new Action(() => UpdateButton(btn, text, color)));
                return;
            }
            if (btn != null)
            {
                btn.Text = text;
                btn.BackColor = color;
            }
        }

        private void GenerateAdvancedReport()
        {
            try
            {
                string fileName = $"BacklinkBot_Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                var report = new StringBuilder();

                report.AppendLine("🚀 BACKLINKBOT PRO v1.0 - GELİŞMİŞ RAPOR");
                report.AppendLine("=" + new string('=', 50));
                report.AppendLine($"📅 Tarih: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                report.AppendLine($"👤 Kullanıcı: {nameTextBox?.Text ?? "Bilinmiyor"}");
                report.AppendLine($"📧 E-mail: {emailTextBox?.Text ?? "Bilinmiyor"}");
                report.AppendLine();

                report.AppendLine("⚙️ AYARLAR:");
                report.AppendLine($"   ⚡ Hız Seviyesi: {GetSpeedText(currentSpeedLevel)} ({currentSpeedLevel}/10)");
                report.AppendLine($"   ⏱️ Timeout: {currentTimeoutSeconds} saniye");
                report.AppendLine($"   🚀 Gecikme Aralığı: {MinDelay}-{MaxDelay}ms");
                report.AppendLine($"   🔄 Paralel İşlem: {MAX_PARALLEL_REQUESTS}x");
                report.AppendLine();

                report.AppendLine("📊 SONUÇLAR:");
                report.AppendLine($"   📈 Toplam Site: {totalSites}");
                report.AppendLine($"   ✅ Başarılı: {successCount}");
                report.AppendLine($"   ❌ Başarısız: {failedCount}");

                double successRate = totalSites > 0 ? (double)successCount / totalSites * 100 : 0;
                report.AppendLine($"   🎯 Başarı Oranı: %{successRate:F1}");
                report.AppendLine();

                if (processedSites.Count > 0)
                {
                    report.AppendLine("✅ BAŞARILI SİTELER:");
                    foreach (string site in processedSites.OrderBy(s => s))
                    {
                        report.AppendLine($"   • {site}");
                    }
                    report.AppendLine();
                }

                report.AppendLine("💡 PERFORMANS ÖNERİLERİ:");
                if (successRate < 30)
                {
                    report.AppendLine("   ⚠️ Düşük başarı oranı. Hız seviyesini düşürmeyi deneyin.");
                }
                else if (successRate > 80)
                {
                    report.AppendLine("   🎉 Mükemmel performans! Hız seviyesini artırabilirsiniz.");
                }
                else
                {
                    report.AppendLine("   👍 İyi performans. Mevcut ayarlar optimal görünüyor.");
                }

                report.AppendLine();
                report.AppendLine("🔧 BacklinkBot Pro v1.0 - Gelişmiş Ayarlar ile Güçlendirildi");

                File.WriteAllText(fileName, report.ToString(), Encoding.UTF8);
                FastLog($"📊 Detaylı rapor oluşturuldu: {fileName}", Color.Blue);

                var result = MessageBox.Show($"Rapor başarıyla oluşturuldu:\n{fileName}\n\nRaporu şimdi açmak istiyor musunuz?",
                                           "Rapor Oluşturuldu",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("notepad.exe", fileName);
                }
            }
            catch (Exception ex)
            {
                FastLog($"❌ Rapor oluşturma hatası: {ex.Message}", Color.Red);
                MessageBox.Show($"Rapor oluşturulurken hata oluştu:\n{ex.Message}",
                              "Hata",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        // =============== FORM LIFECYCLE ===============

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (cancellationTokenSource != null) cancellationTokenSource.Cancel();
                if (fadeTimer != null) fadeTimer.Stop();

                // HTTP client'ı temizle - static olduğu için burada dispose etmeyin
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Form kapanış hatası: {ex.Message}");
            }
            finally
            {
                base.OnFormClosing(e);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.WindowState == FormWindowState.Minimized)
            {
                FastLog("📱 Form küçültüldü", Color.Gray);
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                FastLog("🖥️ Form maksimize edildi", Color.Gray);
            }
        }
    }
}