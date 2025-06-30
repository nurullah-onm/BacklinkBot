using System.Drawing;
using System.Windows.Forms;

namespace BacklinkBot
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.leftPanel = new System.Windows.Forms.Panel();
            this.headerLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.locationLabel = new System.Windows.Forms.Label();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.webLabel = new System.Windows.Forms.Label();
            this.webTextBox = new System.Windows.Forms.TextBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.TextBox();

            // YENİ: Hız ve Timeout ayarları
            this.speedLabel = new System.Windows.Forms.Label();
            this.speedTrackBar = new System.Windows.Forms.TrackBar();
            this.speedValueLabel = new System.Windows.Forms.Label();
            this.timeoutLabel = new System.Windows.Forms.Label();
            this.timeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.timeoutUnitLabel = new System.Windows.Forms.Label();

            this.startButton = new System.Windows.Forms.Button();
            this.reportButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.urlHeaderLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.countLabel = new System.Windows.Forms.Label();
            this.listPanel = new System.Windows.Forms.Panel();
            this.listHeaderLabel = new System.Windows.Forms.Label();
            this.siteListBox = new System.Windows.Forms.ListBox();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.statsHeaderLabel = new System.Windows.Forms.Label();
            this.successLabel = new System.Windows.Forms.Label();
            this.failedLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.logHeaderLabel = new System.Windows.Forms.Label();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).BeginInit();
            this.leftPanel.SuspendLayout();
            this.centerPanel.SuspendLayout();
            this.listPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.White;
            this.leftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftPanel.Controls.Add(this.button1);
            this.leftPanel.Controls.Add(this.headerLabel);
            this.leftPanel.Controls.Add(this.exitButton);
            this.leftPanel.Controls.Add(this.nameLabel);
            this.leftPanel.Controls.Add(this.nameTextBox);
            this.leftPanel.Controls.Add(this.emailLabel);
            this.leftPanel.Controls.Add(this.emailTextBox);
            this.leftPanel.Controls.Add(this.locationLabel);
            this.leftPanel.Controls.Add(this.locationTextBox);
            this.leftPanel.Controls.Add(this.webLabel);
            this.leftPanel.Controls.Add(this.webTextBox);
            this.leftPanel.Controls.Add(this.messageLabel);
            this.leftPanel.Controls.Add(this.messageTextBox);
            this.leftPanel.Controls.Add(this.speedLabel);
            this.leftPanel.Controls.Add(this.speedTrackBar);
            this.leftPanel.Controls.Add(this.speedValueLabel);
            this.leftPanel.Controls.Add(this.timeoutLabel);
            this.leftPanel.Controls.Add(this.timeoutNumericUpDown);
            this.leftPanel.Controls.Add(this.timeoutUnitLabel);
            this.leftPanel.Controls.Add(this.startButton);
            this.leftPanel.Controls.Add(this.reportButton);
            this.leftPanel.Controls.Add(this.statusLabel);
            this.leftPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.leftPanel.Location = new System.Drawing.Point(20, 20);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(320, 650);
            this.leftPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            this.headerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.headerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerLabel.Location = new System.Drawing.Point(15, 15);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(290, 25);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "👤 KİŞİSEL BİLGİLER";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.nameLabel.Location = new System.Drawing.Point(20, 60);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(46, 25);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Ad :";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(100, 57);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(200, 31);
            this.nameTextBox.TabIndex = 2;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.emailLabel.Location = new System.Drawing.Point(20, 95);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(76, 25);
            this.emailLabel.TabIndex = 3;
            this.emailLabel.Text = "E-Mail :";
            // 
            // emailTextBox
            // 
            this.emailTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.emailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.emailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emailTextBox.Location = new System.Drawing.Point(100, 92);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(200, 31);
            this.emailTextBox.TabIndex = 4;
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.locationLabel.Location = new System.Drawing.Point(20, 130);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(107, 25);
            this.locationLabel.TabIndex = 5;
            this.locationLabel.Text = "Memleket :";
            // 
            // locationTextBox
            // 
            this.locationTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.locationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.locationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.locationTextBox.Location = new System.Drawing.Point(130, 127);
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.Size = new System.Drawing.Size(170, 31);
            this.locationTextBox.TabIndex = 6;
            // 
            // webLabel
            // 
            this.webLabel.AutoSize = true;
            this.webLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.webLabel.Location = new System.Drawing.Point(20, 165);
            this.webLabel.Name = "webLabel";
            this.webLabel.Size = new System.Drawing.Size(61, 25);
            this.webLabel.TabIndex = 7;
            this.webLabel.Text = "Web :";
            // 
            // webTextBox
            // 
            this.webTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.webTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.webTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webTextBox.Location = new System.Drawing.Point(100, 162);
            this.webTextBox.Name = "webTextBox";
            this.webTextBox.Size = new System.Drawing.Size(200, 31);
            this.webTextBox.TabIndex = 8;
            this.webTextBox.Text = "https://";
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.messageLabel.Location = new System.Drawing.Point(20, 200);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(72, 25);
            this.messageLabel.TabIndex = 9;
            this.messageLabel.Text = "Mesaj :";
            // 
            // messageTextBox
            // 
            this.messageTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.messageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTextBox.Location = new System.Drawing.Point(20, 220);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(280, 80);
            this.messageTextBox.TabIndex = 10;
            this.messageTextBox.Text = "Merhaba! Harika bir siteniz var. Ziyaret etmekten keyif aldım.";

            // 
            // speedLabel - YENİ HIZ AYARI
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.speedLabel.Location = new System.Drawing.Point(20, 320);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(90, 25);
            this.speedLabel.TabIndex = 11;
            this.speedLabel.Text = "⚡ İşlem Hızı :";

            // 
            // speedTrackBar - YENİ HIZ TRACKBAR
            // 
            this.speedTrackBar.BackColor = System.Drawing.Color.White;
            this.speedTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.speedTrackBar.Location = new System.Drawing.Point(20, 345);
            this.speedTrackBar.Minimum = 1;
            this.speedTrackBar.Maximum = 10;
            this.speedTrackBar.Value = 5;
            this.speedTrackBar.Name = "speedTrackBar";
            this.speedTrackBar.Size = new System.Drawing.Size(200, 56);
            this.speedTrackBar.TabIndex = 12;
            this.speedTrackBar.TickFrequency = 1;
            this.speedTrackBar.ValueChanged += new System.EventHandler(this.speedTrackBar_ValueChanged);

            // 
            // speedValueLabel - HIZ DEĞER ETİKETİ
            // 
            this.speedValueLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.speedValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.speedValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speedValueLabel.Location = new System.Drawing.Point(230, 320);
            this.speedValueLabel.Name = "speedValueLabel";
            this.speedValueLabel.Size = new System.Drawing.Size(70, 20);
            this.speedValueLabel.TabIndex = 13;
            this.speedValueLabel.Text = "Orta (5)";
            this.speedValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // timeoutLabel - YENİ TIMEOUT AYARI
            // 
            this.timeoutLabel.AutoSize = true;
            this.timeoutLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.timeoutLabel.Location = new System.Drawing.Point(20, 400);
            this.timeoutLabel.Name = "timeoutLabel";
            this.timeoutLabel.Size = new System.Drawing.Size(110, 25);
            this.timeoutLabel.TabIndex = 14;
            this.timeoutLabel.Text = "⏱️ Zaman Aşımı :";

            // 
            // timeoutNumericUpDown - TIMEOUT SAYISAL KONTROL
            // 
            this.timeoutNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.timeoutNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeoutNumericUpDown.Location = new System.Drawing.Point(140, 398);
            this.timeoutNumericUpDown.Minimum = 5;
            this.timeoutNumericUpDown.Maximum = 120;
            this.timeoutNumericUpDown.Value = 30;
            this.timeoutNumericUpDown.Name = "timeoutNumericUpDown";
            this.timeoutNumericUpDown.Size = new System.Drawing.Size(80, 31);
            this.timeoutNumericUpDown.TabIndex = 15;

            // 
            // timeoutUnitLabel - TIMEOUT BİRİM ETİKETİ
            // 
            this.timeoutUnitLabel.AutoSize = true;
            this.timeoutUnitLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.timeoutUnitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.timeoutUnitLabel.Location = new System.Drawing.Point(225, 400);
            this.timeoutUnitLabel.Name = "timeoutUnitLabel";
            this.timeoutUnitLabel.Size = new System.Drawing.Size(55, 25);
            this.timeoutUnitLabel.TabIndex = 16;
            this.timeoutUnitLabel.Text = "saniye";

            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.startButton.FlatAppearance.BorderSize = 0;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.startButton.ForeColor = System.Drawing.Color.White;
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startButton.Location = new System.Drawing.Point(20, 450);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(130, 45);
            this.startButton.TabIndex = 17;
            this.startButton.Text = "🚀 BAŞLAT";
            this.startButton.UseVisualStyleBackColor = false;
            // 
            // reportButton
            // 
            this.reportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.reportButton.FlatAppearance.BorderSize = 0;
            this.reportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reportButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.reportButton.ForeColor = System.Drawing.Color.White;
            this.reportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reportButton.Location = new System.Drawing.Point(170, 450);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(130, 45);
            this.reportButton.TabIndex = 18;
            this.reportButton.Text = "📊 RAPOR";
            this.reportButton.UseVisualStyleBackColor = false;

            // 
            // button1 - LİNK DOĞRULAYICI
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(20, 505);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(280, 34);
            this.button1.TabIndex = 19;
            this.button1.Text = "🔗 Link Doğrulayıcı (deneysel)";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.exitButton.ForeColor = System.Drawing.Color.White;
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Location = new System.Drawing.Point(100, 550);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(121, 38);
            this.exitButton.TabIndex = 20;
            this.exitButton.Text = "❌ ÇIKIŞ";
            this.exitButton.UseVisualStyleBackColor = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.Location = new System.Drawing.Point(15, 595);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(290, 44);
            this.statusLabel.TabIndex = 21;
            this.statusLabel.Text = "✨ 0 site geçen yanıt verdi...";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // centerPanel
            // 
            this.centerPanel.BackColor = System.Drawing.Color.White;
            this.centerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.centerPanel.Controls.Add(this.urlHeaderLabel);
            this.centerPanel.Controls.Add(this.addressLabel);
            this.centerPanel.Controls.Add(this.urlTextBox);
            this.centerPanel.Controls.Add(this.addButton);
            this.centerPanel.Controls.Add(this.loadFileButton);
            this.centerPanel.Controls.Add(this.clearButton);
            this.centerPanel.Controls.Add(this.countLabel);
            this.centerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.centerPanel.Location = new System.Drawing.Point(360, 20);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(320, 200);
            this.centerPanel.TabIndex = 1;
            // 
            // urlHeaderLabel
            // 
            this.urlHeaderLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.urlHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.urlHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(85)))), ((int)(((byte)(247)))));
            this.urlHeaderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urlHeaderLabel.Location = new System.Drawing.Point(15, 15);
            this.urlHeaderLabel.Name = "urlHeaderLabel";
            this.urlHeaderLabel.Size = new System.Drawing.Size(290, 25);
            this.urlHeaderLabel.TabIndex = 0;
            this.urlHeaderLabel.Text = "🌐 HEDEF SİTE YÖNETİMİ";
            this.urlHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addressLabel
            // 
            this.addressLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.addressLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.addressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addressLabel.Location = new System.Drawing.Point(20, 55);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(280, 15);
            this.addressLabel.TabIndex = 1;
            this.addressLabel.Text = "Adres ( ör : http://www.example.com/zd )";
            // 
            // urlTextBox
            // 
            this.urlTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.urlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.urlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urlTextBox.Location = new System.Drawing.Point(20, 75);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(280, 31);
            this.urlTextBox.TabIndex = 2;
            this.urlTextBox.Text = "https://";
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(20, 110);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(85, 30);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "➕ Ekle";
            this.addButton.UseVisualStyleBackColor = false;
            // 
            // loadFileButton
            // 
            this.loadFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(85)))), ((int)(((byte)(247)))));
            this.loadFileButton.FlatAppearance.BorderSize = 0;
            this.loadFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadFileButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.loadFileButton.ForeColor = System.Drawing.Color.White;
            this.loadFileButton.Location = new System.Drawing.Point(115, 110);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(90, 30);
            this.loadFileButton.TabIndex = 4;
            this.loadFileButton.Text = "📁 Dosyadan";
            this.loadFileButton.UseVisualStyleBackColor = false;
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.clearButton.FlatAppearance.BorderSize = 0;
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.clearButton.ForeColor = System.Drawing.Color.White;
            this.clearButton.Location = new System.Drawing.Point(215, 110);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(85, 30);
            this.clearButton.TabIndex = 5;
            this.clearButton.Text = "🗑️ Temizle";
            this.clearButton.UseVisualStyleBackColor = false;
            // 
            // countLabel
            // 
            this.countLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.countLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.countLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.countLabel.Location = new System.Drawing.Point(20, 155);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(280, 20);
            this.countLabel.TabIndex = 6;
            this.countLabel.Text = "📊 Toplam: 0 site";
            this.countLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listPanel
            // 
            this.listPanel.BackColor = System.Drawing.Color.White;
            this.listPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listPanel.Controls.Add(this.listHeaderLabel);
            this.listPanel.Controls.Add(this.siteListBox);
            this.listPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listPanel.Location = new System.Drawing.Point(360, 240);
            this.listPanel.Name = "listPanel";
            this.listPanel.Size = new System.Drawing.Size(320, 430);
            this.listPanel.TabIndex = 2;
            // 
            // listHeaderLabel
            // 
            this.listHeaderLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.listHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.listHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.listHeaderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listHeaderLabel.Location = new System.Drawing.Point(15, 15);
            this.listHeaderLabel.Name = "listHeaderLabel";
            this.listHeaderLabel.Size = new System.Drawing.Size(290, 25);
            this.listHeaderLabel.TabIndex = 0;
            this.listHeaderLabel.Text = "📋 SİTE LİSTESİ";
            this.listHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // siteListBox
            // 
            this.siteListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.siteListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.siteListBox.Font = new System.Drawing.Font("Consolas", 8F);
            this.siteListBox.ItemHeight = 19;
            this.siteListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.siteListBox.Location = new System.Drawing.Point(20, 50);
            this.siteListBox.Name = "siteListBox";
            this.siteListBox.Size = new System.Drawing.Size(280, 365);
            this.siteListBox.TabIndex = 1;
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.Color.White;
            this.rightPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightPanel.Controls.Add(this.statsHeaderLabel);
            this.rightPanel.Controls.Add(this.successLabel);
            this.rightPanel.Controls.Add(this.failedLabel);
            this.rightPanel.Controls.Add(this.totalLabel);
            this.rightPanel.Controls.Add(this.logHeaderLabel);
            this.rightPanel.Controls.Add(this.logTextBox);
            this.rightPanel.Controls.Add(this.clearLogButton);
            this.rightPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightPanel.Location = new System.Drawing.Point(700, 20);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(300, 650);
            this.rightPanel.TabIndex = 3;
            // 
            // statsHeaderLabel
            // 
            this.statsHeaderLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.statsHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.statsHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.statsHeaderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statsHeaderLabel.Location = new System.Drawing.Point(15, 15);
            this.statsHeaderLabel.Name = "statsHeaderLabel";
            this.statsHeaderLabel.Size = new System.Drawing.Size(270, 25);
            this.statsHeaderLabel.TabIndex = 0;
            this.statsHeaderLabel.Text = "📊 İSTATİSTİKLER";
            this.statsHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // successLabel
            // 
            this.successLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.successLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.successLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.successLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.successLabel.Location = new System.Drawing.Point(20, 60);
            this.successLabel.Name = "successLabel";
            this.successLabel.Size = new System.Drawing.Size(260, 25);
            this.successLabel.TabIndex = 1;
            this.successLabel.Text = "✅ Başarılı: 0";
            this.successLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // failedLabel
            // 
            this.failedLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.failedLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.failedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.failedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.failedLabel.Location = new System.Drawing.Point(20, 95);
            this.failedLabel.Name = "failedLabel";
            this.failedLabel.Size = new System.Drawing.Size(260, 25);
            this.failedLabel.TabIndex = 2;
            this.failedLabel.Text = "❌ Başarısız: 0";
            this.failedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalLabel
            // 
            this.totalLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.totalLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.totalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.totalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalLabel.Location = new System.Drawing.Point(20, 130);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(260, 25);
            this.totalLabel.TabIndex = 3;
            this.totalLabel.Text = "📈 Toplam: 0";
            this.totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logHeaderLabel
            // 
            this.logHeaderLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.logHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.logHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.logHeaderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logHeaderLabel.Location = new System.Drawing.Point(15, 175);
            this.logHeaderLabel.Name = "logHeaderLabel";
            this.logHeaderLabel.Size = new System.Drawing.Size(270, 25);
            this.logHeaderLabel.TabIndex = 4;
            this.logHeaderLabel.Text = "📝 İŞLEM GÜNLÜĞÜ";
            this.logHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.logTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 8F);
            this.logTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.logTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextBox.Location = new System.Drawing.Point(20, 219);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(260, 380);
            this.logTextBox.TabIndex = 5;
            this.logTextBox.Text = "📱 BacklinkBot hazır...\n⚡ İşlem bekleniyor...\n";
            // 
            // clearLogButton
            // 
            this.clearLogButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.clearLogButton.FlatAppearance.BorderSize = 0;
            this.clearLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearLogButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.clearLogButton.ForeColor = System.Drawing.Color.White;
            this.clearLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clearLogButton.Location = new System.Drawing.Point(20, 610);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(260, 30);
            this.clearLogButton.TabIndex = 6;
            this.clearLogButton.Text = "🧹 Günlük Temizle";
            this.clearLogButton.UseVisualStyleBackColor = false;
            // 
            // progressBar
            // 
            this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(20, 685);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(980, 15);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 4;

            // 
            // Form1 - ANA FORM
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1020, 720);
            this.MinimumSize = new System.Drawing.Size(1020, 720);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.centerPanel);
            this.Controls.Add(this.listPanel);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.progressBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BacklinkBot Pro v1.0 🚀 - Gelişmiş Ayarlar";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;

            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).EndInit();
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.centerPanel.ResumeLayout(false);
            this.centerPanel.PerformLayout();
            this.listPanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Panel leftPanel;
        private Label headerLabel;
        private Label nameLabel;
        private TextBox nameTextBox;
        private Label emailLabel;
        private TextBox emailTextBox;
        private Label locationLabel;
        private TextBox locationTextBox;
        private Label webLabel;
        private TextBox webTextBox;
        private Label messageLabel;
        private TextBox messageTextBox;

        // YENİ ALANLAR - HIZ VE TIMEOUT
        private Label speedLabel;
        private TrackBar speedTrackBar;
        private Label speedValueLabel;
        private Label timeoutLabel;
        private NumericUpDown timeoutNumericUpDown;
        private Label timeoutUnitLabel;

        private Button startButton;
        private Button reportButton;
        private Button exitButton;
        private Label statusLabel;
        private Panel centerPanel;
        private Label urlHeaderLabel;
        private Label addressLabel;
        private TextBox urlTextBox;
        private Button addButton;
        private Button loadFileButton;
        private Button clearButton;
        private Label countLabel;
        private Panel listPanel;
        private Label listHeaderLabel;
        private ListBox siteListBox;
        private Panel rightPanel;
        private Label statsHeaderLabel;
        private Label successLabel;
        private Label failedLabel;
        private Label totalLabel;
        private Label logHeaderLabel;
        private RichTextBox logTextBox;
        private Button clearLogButton;
        private ProgressBar progressBar;
        private Button button1;
    }
}