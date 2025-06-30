using System;
using System.Drawing;
using System.Windows.Forms;

namespace BacklinkBot
{
    partial class DogrulaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // FORM AYARLARI
            this.Text = "🔍 Link Doğrulama ve Analiz Sistemi";
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);

            // ÜST PANEL - KONTROLLER
            this.topPanel = new Panel();
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1160, 120);
            this.topPanel.Location = new System.Drawing.Point(20, 20);
            this.topPanel.BackColor = System.Drawing.Color.White;
            this.topPanel.BorderStyle = BorderStyle.FixedSingle;
            this.topPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // BAŞLIK
            this.headerLabel = new Label();
            this.headerLabel.Text = "🔍 LİNK DOĞRULAMA VE ANALİZ SİSTEMİ";
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 14F, FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.headerLabel.Location = new System.Drawing.Point(20, 15);
            this.headerLabel.Size = new System.Drawing.Size(500, 30);
            this.headerLabel.TextAlign = ContentAlignment.MiddleLeft;

            // ANA SAYFAYA DÖN BUTONU
            this.btnGoBack = new Button();
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Text = "🏠 Ana Sayfaya Dön";
            this.btnGoBack.Location = new System.Drawing.Point(950, 15);
            this.btnGoBack.Size = new System.Drawing.Size(150, 35);
            this.btnGoBack.BackColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnGoBack.ForeColor = System.Drawing.Color.White;
            this.btnGoBack.FlatStyle = FlatStyle.Flat;
            this.btnGoBack.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnGoBack.FlatAppearance.BorderSize = 0;
            this.btnGoBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // URL GİRİŞ LABEL
            this.urlInputLabel = new Label();
            this.urlInputLabel.Text = "URL Listesi (Her satıra bir link) :";
            this.urlInputLabel.Location = new System.Drawing.Point(20, 60);
            this.urlInputLabel.Size = new System.Drawing.Size(200, 20);
            this.urlInputLabel.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);

            // URL GİRİŞ TEXTBOX
            this.txtUrlInput = new TextBox();
            this.txtUrlInput.Name = "txtUrlInput";
            this.txtUrlInput.Location = new System.Drawing.Point(230, 55);
            this.txtUrlInput.Size = new System.Drawing.Size(400, 50);
            this.txtUrlInput.Multiline = true;
            this.txtUrlInput.ScrollBars = ScrollBars.Vertical;
            this.txtUrlInput.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtUrlInput.BorderStyle = BorderStyle.FixedSingle;
            this.txtUrlInput.Font = new System.Drawing.Font("Consolas", 9F);

            // DOSYADAN YÜKLE BUTONU
            this.btnLoadFile = new Button();
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Text = "📁 Dosyadan Yükle";
            this.btnLoadFile.Location = new System.Drawing.Point(650, 55);
            this.btnLoadFile.Size = new System.Drawing.Size(120, 30);
            this.btnLoadFile.BackColor = System.Drawing.Color.FromArgb(168, 85, 247);
            this.btnLoadFile.ForeColor = System.Drawing.Color.White;
            this.btnLoadFile.FlatStyle = FlatStyle.Flat;
            this.btnLoadFile.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnLoadFile.FlatAppearance.BorderSize = 0;

            // DOĞRULAMA BAŞLAT BUTONU
            this.btnStartValidation = new Button();
            this.btnStartValidation.Name = "btnStartValidation";
            this.btnStartValidation.Text = "🚀 Doğrulamayı Başlat";
            this.btnStartValidation.Location = new System.Drawing.Point(780, 55);
            this.btnStartValidation.Size = new System.Drawing.Size(140, 30);
            this.btnStartValidation.BackColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.btnStartValidation.ForeColor = System.Drawing.Color.White;
            this.btnStartValidation.FlatStyle = FlatStyle.Flat;
            this.btnStartValidation.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnStartValidation.FlatAppearance.BorderSize = 0;

            // DURDUR BUTONU
            this.btnStop = new Button();
            this.btnStop.Name = "btnStop";
            this.btnStop.Text = "⏹️ Durdur";
            this.btnStop.Location = new System.Drawing.Point(930, 55);
            this.btnStop.Size = new System.Drawing.Size(80, 30);
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.FlatStyle = FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.Enabled = false;

            // KAYDET BUTONU
            this.btnSaveResults = new Button();
            this.btnSaveResults.Name = "btnSaveResults";
            this.btnSaveResults.Text = "💾 Sonuçları Kaydet";
            this.btnSaveResults.Location = new System.Drawing.Point(1020, 55);
            this.btnSaveResults.Size = new System.Drawing.Size(120, 30);
            this.btnSaveResults.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnSaveResults.ForeColor = System.Drawing.Color.White;
            this.btnSaveResults.FlatStyle = FlatStyle.Flat;
            this.btnSaveResults.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSaveResults.FlatAppearance.BorderSize = 0;
            this.btnSaveResults.Enabled = false;
            this.btnSaveResults.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // ÜST PANEL KONTROL EKLEMELERİ
            this.topPanel.Controls.Add(this.headerLabel);
            this.topPanel.Controls.Add(this.btnGoBack);
            this.topPanel.Controls.Add(this.urlInputLabel);
            this.topPanel.Controls.Add(this.txtUrlInput);
            this.topPanel.Controls.Add(this.btnLoadFile);
            this.topPanel.Controls.Add(this.btnStartValidation);
            this.topPanel.Controls.Add(this.btnStop);
            this.topPanel.Controls.Add(this.btnSaveResults);

            // SOL PANEL - SONUÇLAR
            this.leftPanel = new Panel();
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(580, 620);
            this.leftPanel.Location = new System.Drawing.Point(20, 160);
            this.leftPanel.BackColor = System.Drawing.Color.White;
            this.leftPanel.BorderStyle = BorderStyle.FixedSingle;
            this.leftPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // SONUÇLAR BAŞLIK
            this.resultsHeaderLabel = new Label();
            this.resultsHeaderLabel.Text = "📊 DOĞRULAMA SONUÇLARI";
            this.resultsHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Bold);
            this.resultsHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.resultsHeaderLabel.Location = new System.Drawing.Point(15, 15);
            this.resultsHeaderLabel.Size = new System.Drawing.Size(550, 25);
            this.resultsHeaderLabel.BackColor = System.Drawing.Color.FromArgb(240, 253, 244);
            this.resultsHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;

            // SONUÇLAR LISTVIEW
            this.lvResults = new ListView();
            this.lvResults.Name = "lvResults";
            this.lvResults.Location = new System.Drawing.Point(20, 50);
            this.lvResults.Size = new System.Drawing.Size(540, 550);
            this.lvResults.View = View.Details;
            this.lvResults.FullRowSelect = true;
            this.lvResults.GridLines = true;
            this.lvResults.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.lvResults.Font = new System.Drawing.Font("Consolas", 8F);
            this.lvResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // LISTVIEW KOLONLARI
            this.lvResults.Columns.Add("Durum", 60);
            this.lvResults.Columns.Add("URL", 300);
            this.lvResults.Columns.Add("Sonuç", 80);
            this.lvResults.Columns.Add("Yorum Alanı", 80);
            this.lvResults.Columns.Add("Detay", 200);

            // SOL PANEL KONTROL EKLEMELERİ
            this.leftPanel.Controls.Add(this.resultsHeaderLabel);
            this.leftPanel.Controls.Add(this.lvResults);

            // SAĞ PANEL - İSTATİSTİK VE LOG
            this.rightPanel = new Panel();
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(560, 620);
            this.rightPanel.Location = new System.Drawing.Point(620, 160);
            this.rightPanel.BackColor = System.Drawing.Color.White;
            this.rightPanel.BorderStyle = BorderStyle.FixedSingle;
            this.rightPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

            // İSTATİSTİK BAŞLIK
            this.statsHeaderLabel = new Label();
            this.statsHeaderLabel.Text = "📈 CANLI İSTATİSTİKLER";
            this.statsHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Bold);
            this.statsHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.statsHeaderLabel.Location = new System.Drawing.Point(15, 15);
            this.statsHeaderLabel.Size = new System.Drawing.Size(530, 25);
            this.statsHeaderLabel.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            this.statsHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;

            // TOPLAM TEST EDİLEN
            this.lblTotalTested = new Label();
            this.lblTotalTested.Name = "lblTotalTested";
            this.lblTotalTested.Text = "📊 Toplam Test Edilen: 0";
            this.lblTotalTested.Location = new System.Drawing.Point(20, 60);
            this.lblTotalTested.Size = new System.Drawing.Size(250, 25);
            this.lblTotalTested.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotalTested.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.lblTotalTested.BackColor = System.Drawing.Color.FromArgb(239, 246, 255);
            this.lblTotalTested.TextAlign = ContentAlignment.MiddleCenter;

            // AKTİF LİNKLER
            this.lblActiveLinks = new Label();
            this.lblActiveLinks.Name = "lblActiveLinks";
            this.lblActiveLinks.Text = "✅ Aktif Linkler: 0";
            this.lblActiveLinks.Location = new System.Drawing.Point(290, 60);
            this.lblActiveLinks.Size = new System.Drawing.Size(250, 25);
            this.lblActiveLinks.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblActiveLinks.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.lblActiveLinks.BackColor = System.Drawing.Color.FromArgb(240, 253, 244);
            this.lblActiveLinks.TextAlign = ContentAlignment.MiddleCenter;

            // YORUM ALANI OLAN
            this.lblCommentArea = new Label();
            this.lblCommentArea.Name = "lblCommentArea";
            this.lblCommentArea.Text = "💬 Yorum Alanı Var: 0";
            this.lblCommentArea.Location = new System.Drawing.Point(20, 95);
            this.lblCommentArea.Size = new System.Drawing.Size(250, 25);
            this.lblCommentArea.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCommentArea.ForeColor = System.Drawing.Color.FromArgb(168, 85, 247);
            this.lblCommentArea.BackColor = System.Drawing.Color.FromArgb(250, 245, 255);
            this.lblCommentArea.TextAlign = ContentAlignment.MiddleCenter;

            // ÖLÜL LİNKLER
            this.lblDeadLinks = new Label();
            this.lblDeadLinks.Name = "lblDeadLinks";
            this.lblDeadLinks.Text = "❌ Ölü Linkler: 0";
            this.lblDeadLinks.Location = new System.Drawing.Point(290, 95);
            this.lblDeadLinks.Size = new System.Drawing.Size(250, 25);
            this.lblDeadLinks.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDeadLinks.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.lblDeadLinks.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            this.lblDeadLinks.TextAlign = ContentAlignment.MiddleCenter;

            // BAŞARI ORANI
            this.lblSuccessRate = new Label();
            this.lblSuccessRate.Name = "lblSuccessRate";
            this.lblSuccessRate.Text = "🎯 Başarı Oranı: %0";
            this.lblSuccessRate.Location = new System.Drawing.Point(20, 130);
            this.lblSuccessRate.Size = new System.Drawing.Size(520, 30);
            this.lblSuccessRate.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblSuccessRate.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.lblSuccessRate.BackColor = System.Drawing.Color.FromArgb(240, 253, 244);
            this.lblSuccessRate.TextAlign = ContentAlignment.MiddleCenter;

            // LOG BAŞLIK
            this.logHeaderLabel = new Label();
            this.logHeaderLabel.Text = "📝 İŞLEM GÜNLÜĞÜ";
            this.logHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 11F, FontStyle.Bold);
            this.logHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.logHeaderLabel.Location = new System.Drawing.Point(15, 180);
            this.logHeaderLabel.Size = new System.Drawing.Size(530, 25);
            this.logHeaderLabel.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.logHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;

            // LOG RICHTEXTBOX
            this.rtbLog = new RichTextBox();
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Location = new System.Drawing.Point(20, 215);
            this.rtbLog.Size = new System.Drawing.Size(520, 350);
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.rtbLog.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 8F);
            this.rtbLog.ReadOnly = true;
            this.rtbLog.BorderStyle = BorderStyle.FixedSingle;
            this.rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // LOG TEMİZLE BUTONU
            this.btnClearLog = new Button();
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Text = "🧹 Günlük Temizle";
            this.btnClearLog.Location = new System.Drawing.Point(20, 575);
            this.btnClearLog.Size = new System.Drawing.Size(520, 30);
            this.btnClearLog.BackColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnClearLog.ForeColor = System.Drawing.Color.White;
            this.btnClearLog.FlatStyle = FlatStyle.Flat;
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnClearLog.FlatAppearance.BorderSize = 0;
            this.btnClearLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // SAĞ PANEL KONTROL EKLEMELERİ
            this.rightPanel.Controls.Add(this.statsHeaderLabel);
            this.rightPanel.Controls.Add(this.lblTotalTested);
            this.rightPanel.Controls.Add(this.lblActiveLinks);
            this.rightPanel.Controls.Add(this.lblCommentArea);
            this.rightPanel.Controls.Add(this.lblDeadLinks);
            this.rightPanel.Controls.Add(this.lblSuccessRate);
            this.rightPanel.Controls.Add(this.logHeaderLabel);
            this.rightPanel.Controls.Add(this.rtbLog);
            this.rightPanel.Controls.Add(this.btnClearLog);

            // ANA FORMA PANELLERİ EKLEME
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.rightPanel);

            this.ResumeLayout(false);
        }

        #endregion

        // FORM KONTROL DECLARATIONS
        private Panel topPanel;
        private Label headerLabel;
        private Button btnGoBack;
        private Label urlInputLabel;
        private TextBox txtUrlInput;
        private Button btnLoadFile;
        private Button btnStartValidation;
        private Button btnStop;
        private Button btnSaveResults;

        private Panel leftPanel;
        private Label resultsHeaderLabel;
        private ListView lvResults;

        private Panel rightPanel;
        private Label statsHeaderLabel;
        private Label lblTotalTested;
        private Label lblActiveLinks;
        private Label lblCommentArea;
        private Label lblDeadLinks;
        private Label lblSuccessRate;
        private Label logHeaderLabel;
        private RichTextBox rtbLog;
        private Button btnClearLog;
    }
}