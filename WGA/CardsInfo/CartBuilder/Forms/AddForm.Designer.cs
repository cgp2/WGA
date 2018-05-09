namespace CartBuilder
{
    partial class AddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddForm));
            this.textCardInfo = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.shieldBox = new System.Windows.Forms.TextBox();
            this.hpBox = new System.Windows.Forms.TextBox();
            this.attackBox = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SkillsCheckBox = new System.Windows.Forms.CheckedListBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.ImagePathBox = new System.Windows.Forms.TextBox();
            this.ImagePathButton = new System.Windows.Forms.Button();
            this.ClassCardBox = new System.Windows.Forms.ComboBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.BCSettingsBox = new System.Windows.Forms.TextBox();
            this.DRSettingsBox = new System.Windows.Forms.TextBox();
            this.ASettingsBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textCardInfo
            // 
            this.textCardInfo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textCardInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textCardInfo.Location = new System.Drawing.Point(12, 12);
            this.textCardInfo.Name = "textCardInfo";
            this.textCardInfo.ReadOnly = true;
            this.textCardInfo.ShortcutsEnabled = false;
            this.textCardInfo.Size = new System.Drawing.Size(225, 32);
            this.textCardInfo.TabIndex = 0;
            this.textCardInfo.TabStop = false;
            this.textCardInfo.Text = "Имя:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(12, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(225, 32);
            this.textBox1.TabIndex = 21;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Атака:";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox5.Location = new System.Drawing.Point(12, 126);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.ShortcutsEnabled = false;
            this.textBox5.Size = new System.Drawing.Size(225, 32);
            this.textBox5.TabIndex = 0;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "Щиты:";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox6.Location = new System.Drawing.Point(12, 88);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.ShortcutsEnabled = false;
            this.textBox6.Size = new System.Drawing.Size(225, 32);
            this.textBox6.TabIndex = 0;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "Здоровье:";
            // 
            // nameBox
            // 
            this.nameBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.nameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameBox.Location = new System.Drawing.Point(243, 12);
            this.nameBox.MaxLength = 10;
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(756, 32);
            this.nameBox.TabIndex = 1;
            // 
            // shieldBox
            // 
            this.shieldBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.shieldBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.shieldBox.Location = new System.Drawing.Point(243, 126);
            this.shieldBox.MaxLength = 3;
            this.shieldBox.Name = "shieldBox";
            this.shieldBox.Size = new System.Drawing.Size(756, 32);
            this.shieldBox.TabIndex = 4;
            this.shieldBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.shieldBox_KeyPress);
            // 
            // hpBox
            // 
            this.hpBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.hpBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hpBox.Location = new System.Drawing.Point(243, 88);
            this.hpBox.MaxLength = 3;
            this.hpBox.Name = "hpBox";
            this.hpBox.Size = new System.Drawing.Size(756, 32);
            this.hpBox.TabIndex = 3;
            this.hpBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hpBox_KeyPress);
            // 
            // attackBox
            // 
            this.attackBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.attackBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.attackBox.Location = new System.Drawing.Point(243, 50);
            this.attackBox.MaxLength = 3;
            this.attackBox.Name = "attackBox";
            this.attackBox.Size = new System.Drawing.Size(756, 32);
            this.attackBox.TabIndex = 2;
            this.attackBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.attackBox_KeyPress);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox7.Location = new System.Drawing.Point(12, 164);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.ShortcutsEnabled = false;
            this.textBox7.Size = new System.Drawing.Size(225, 32);
            this.textBox7.TabIndex = 0;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "Описание карты:";
            // 
            // descriptionBox
            // 
            this.descriptionBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.descriptionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.descriptionBox.Location = new System.Drawing.Point(243, 164);
            this.descriptionBox.MaxLength = 70;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(756, 32);
            this.descriptionBox.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.okButton.Location = new System.Drawing.Point(799, 795);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(148, 44);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // SkillsCheckBox
            // 
            this.SkillsCheckBox.CheckOnClick = true;
            this.SkillsCheckBox.FormattingEnabled = true;
            this.SkillsCheckBox.Location = new System.Drawing.Point(243, 278);
            this.SkillsCheckBox.Name = "SkillsCheckBox";
            this.SkillsCheckBox.ScrollAlwaysVisible = true;
            this.SkillsCheckBox.Size = new System.Drawing.Size(756, 382);
            this.SkillsCheckBox.TabIndex = 9;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox8.Location = new System.Drawing.Point(12, 278);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.ShortcutsEnabled = false;
            this.textBox8.Size = new System.Drawing.Size(225, 32);
            this.textBox8.TabIndex = 0;
            this.textBox8.TabStop = false;
            this.textBox8.Text = "Свойства карты:";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(12, 202);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ShortcutsEnabled = false;
            this.textBox2.Size = new System.Drawing.Size(225, 32);
            this.textBox2.TabIndex = 0;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "Класс карты:";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox3.Location = new System.Drawing.Point(12, 240);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.ShortcutsEnabled = false;
            this.textBox3.Size = new System.Drawing.Size(225, 32);
            this.textBox3.TabIndex = 0;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "Путь к изображению:";
            // 
            // ImagePathBox
            // 
            this.ImagePathBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ImagePathBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ImagePathBox.Location = new System.Drawing.Point(243, 240);
            this.ImagePathBox.Name = "ImagePathBox";
            this.ImagePathBox.Size = new System.Drawing.Size(672, 32);
            this.ImagePathBox.TabIndex = 7;
            // 
            // ImagePathButton
            // 
            this.ImagePathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ImagePathButton.Location = new System.Drawing.Point(921, 240);
            this.ImagePathButton.Name = "ImagePathButton";
            this.ImagePathButton.Size = new System.Drawing.Size(78, 32);
            this.ImagePathButton.TabIndex = 8;
            this.ImagePathButton.Text = "***";
            this.ImagePathButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ImagePathButton.UseVisualStyleBackColor = true;
            this.ImagePathButton.Click += new System.EventHandler(this.ImagePathButton_Click);
            // 
            // ClassCardBox
            // 
            this.ClassCardBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClassCardBox.FormattingEnabled = true;
            this.ClassCardBox.Items.AddRange(new object[] {
            "People",
            "Insect",
            "Mechanism"});
            this.ClassCardBox.Location = new System.Drawing.Point(243, 202);
            this.ClassCardBox.Name = "ClassCardBox";
            this.ClassCardBox.Size = new System.Drawing.Size(756, 34);
            this.ClassCardBox.TabIndex = 6;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox4.Location = new System.Drawing.Point(12, 665);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.ShortcutsEnabled = false;
            this.textBox4.Size = new System.Drawing.Size(225, 32);
            this.textBox4.TabIndex = 22;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "BattleCry value:";
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox9.Location = new System.Drawing.Point(12, 703);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.ShortcutsEnabled = false;
            this.textBox9.Size = new System.Drawing.Size(225, 32);
            this.textBox9.TabIndex = 23;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "DeathRattle value:";
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox10.Location = new System.Drawing.Point(12, 741);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.ShortcutsEnabled = false;
            this.textBox10.Size = new System.Drawing.Size(225, 32);
            this.textBox10.TabIndex = 24;
            this.textBox10.TabStop = false;
            this.textBox10.Text = "Aura value:";
            // 
            // BCSettingsBox
            // 
            this.BCSettingsBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BCSettingsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BCSettingsBox.Location = new System.Drawing.Point(243, 665);
            this.BCSettingsBox.MaxLength = 3;
            this.BCSettingsBox.Name = "BCSettingsBox";
            this.BCSettingsBox.Size = new System.Drawing.Size(753, 32);
            this.BCSettingsBox.TabIndex = 10;
            this.BCSettingsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BCSettingsBox_KeyPress);
            // 
            // DRSettingsBox
            // 
            this.DRSettingsBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DRSettingsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DRSettingsBox.Location = new System.Drawing.Point(243, 703);
            this.DRSettingsBox.MaxLength = 3;
            this.DRSettingsBox.Name = "DRSettingsBox";
            this.DRSettingsBox.Size = new System.Drawing.Size(753, 32);
            this.DRSettingsBox.TabIndex = 12;
            this.DRSettingsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DRSettingsBox_KeyPress);
            // 
            // ASettingsBox
            // 
            this.ASettingsBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ASettingsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ASettingsBox.Location = new System.Drawing.Point(243, 741);
            this.ASettingsBox.MaxLength = 3;
            this.ASettingsBox.Name = "ASettingsBox";
            this.ASettingsBox.Size = new System.Drawing.Size(753, 32);
            this.ASettingsBox.TabIndex = 14;
            this.ASettingsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ASettingsBox_KeyPress);
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1008, 851);
            this.Controls.Add(this.ASettingsBox);
            this.Controls.Add(this.DRSettingsBox);
            this.Controls.Add(this.BCSettingsBox);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.ClassCardBox);
            this.Controls.Add(this.ImagePathButton);
            this.Controls.Add(this.ImagePathBox);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.SkillsCheckBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.attackBox);
            this.Controls.Add(this.hpBox);
            this.Controls.Add(this.shieldBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textCardInfo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddForm";
            this.Text = "AddForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textCardInfo;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox shieldBox;
        private System.Windows.Forms.TextBox hpBox;
        private System.Windows.Forms.TextBox attackBox;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.CheckedListBox SkillsCheckBox;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox ImagePathBox;
        private System.Windows.Forms.Button ImagePathButton;
        private System.Windows.Forms.ComboBox ClassCardBox;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox BCSettingsBox;
        private System.Windows.Forms.TextBox DRSettingsBox;
        private System.Windows.Forms.TextBox ASettingsBox;
    }
}