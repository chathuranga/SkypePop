﻿namespace SkypePop
{
    partial class SkypePopDialog
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
            this.components = new System.ComponentModel.Container();
            this.txtReceived = new System.Windows.Forms.RichTextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.cmbActiveChats = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnHide = new System.Windows.Forms.PictureBox();
            this.context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHide)).BeginInit();
            this.context.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtReceived
            // 
            this.txtReceived.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(236)))), ((int)(((byte)(243)))));
            this.txtReceived.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReceived.Location = new System.Drawing.Point(8, 35);
            this.txtReceived.Name = "txtReceived";
            this.txtReceived.Size = new System.Drawing.Size(335, 138);
            this.txtReceived.TabIndex = 0;
            this.txtReceived.Text = "";
            // 
            // txtSend
            // 
            this.txtSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(241)))), ((int)(((byte)(220)))));
            this.txtSend.Location = new System.Drawing.Point(9, 177);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(335, 20);
            this.txtSend.TabIndex = 1;
            this.txtSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSend_KeyDown);
            this.txtSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSend_KeyPress);
            // 
            // cmbActiveChats
            // 
            this.cmbActiveChats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(103)))), ((int)(((byte)(48)))));
            this.cmbActiveChats.DropDownHeight = 100;
            this.cmbActiveChats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActiveChats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActiveChats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbActiveChats.ForeColor = System.Drawing.Color.White;
            this.cmbActiveChats.FormattingEnabled = true;
            this.cmbActiveChats.IntegralHeight = false;
            this.cmbActiveChats.Location = new System.Drawing.Point(48, 4);
            this.cmbActiveChats.Name = "cmbActiveChats";
            this.cmbActiveChats.Size = new System.Drawing.Size(275, 21);
            this.cmbActiveChats.TabIndex = 2;
            this.cmbActiveChats.SelectedIndexChanged += new System.EventHandler(this.cmbActiveChats_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnHide);
            this.panel1.Controls.Add(this.cmbActiveChats);
            this.panel1.Controls.Add(this.txtSend);
            this.panel1.Controls.Add(this.txtReceived);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 210);
            this.panel1.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::SkypePop.Properties.Resources.exit;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(328, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 16);
            this.btnClose.TabIndex = 4;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHide
            // 
            this.btnHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnHide.ContextMenuStrip = this.context;
            this.btnHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHide.Location = new System.Drawing.Point(3, 3);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(26, 21);
            this.btnHide.TabIndex = 3;
            this.btnHide.TabStop = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // context
            // 
            this.context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSettings,
            this.miExit});
            this.context.Name = "context";
            this.context.Size = new System.Drawing.Size(153, 70);
            // 
            // miSettings
            // 
            this.miSettings.Name = "miSettings";
            this.miSettings.Size = new System.Drawing.Size(152, 22);
            this.miSettings.Text = "Settings";
            this.miSettings.Click += new System.EventHandler(this.miSettings_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(152, 22);
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // SkypePopDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = global::SkypePop.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(355, 210);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(355, 210);
            this.MinimumSize = new System.Drawing.Size(355, 210);
            this.Name = "SkypePopDialog";
            this.Text = "SkypePopDialog";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHide)).EndInit();
            this.context.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtReceived;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.ComboBox cmbActiveChats;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnHide;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.ContextMenuStrip context;
        private System.Windows.Forms.ToolStripMenuItem miSettings;
        private System.Windows.Forms.ToolStripMenuItem miExit;

    }
}