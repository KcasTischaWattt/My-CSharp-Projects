
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Peer4
{
    partial class NotePad
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotePad));
            this.tabControlAllTabs = new System.Windows.Forms.TabControl();
            this.tabControlMenuTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNewFileButton = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenFileButton = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveFileButton = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAsButton = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbBoldTextButton = new System.Windows.Forms.ToolStripButton();
            this.tsbCursiveTextButton = new System.Windows.Forms.ToolStripButton();
            this.tsbUndelineTextButton = new System.Windows.Forms.ToolStripButton();
            this.tsbCrossedTextButton = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.timesNewRomanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambriaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.garamondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dejaVuSabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tahomaSansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownAutosaveInterval = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.italicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.underlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.strikeoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMenuTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAllTabs
            // 
            this.tabControlAllTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlAllTabs.Location = new System.Drawing.Point(12, 55);
            this.tabControlAllTabs.Name = "tabControlAllTabs";
            this.tabControlAllTabs.SelectedIndex = 0;
            this.tabControlAllTabs.Size = new System.Drawing.Size(862, 427);
            this.tabControlAllTabs.TabIndex = 1;
            // 
            // tabControlMenuTabs
            // 
            this.tabControlMenuTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMenuTabs.Controls.Add(this.tabPage1);
            this.tabControlMenuTabs.Controls.Add(this.tabPage2);
            this.tabControlMenuTabs.Controls.Add(this.tabPage3);
            this.tabControlMenuTabs.Controls.Add(this.tabPage4);
            this.tabControlMenuTabs.Location = new System.Drawing.Point(13, 1);
            this.tabControlMenuTabs.Name = "tabControlMenuTabs";
            this.tabControlMenuTabs.SelectedIndex = 0;
            this.tabControlMenuTabs.Size = new System.Drawing.Size(861, 56);
            this.tabControlMenuTabs.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(853, 28);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewFileButton,
            this.tsbOpenFileButton,
            this.tsbSaveFileButton,
            this.tsbSaveAsButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(847, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNewFileButton
            // 
            this.tsbNewFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbNewFileButton.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewFileButton.Image")));
            this.tsbNewFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewFileButton.Name = "tsbNewFileButton";
            this.tsbNewFileButton.Size = new System.Drawing.Size(35, 22);
            this.tsbNewFileButton.Text = "New";
            this.tsbNewFileButton.Click += new System.EventHandler(this.tsbNewFileButton_Click);
            // 
            // tsbOpenFileButton
            // 
            this.tsbOpenFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOpenFileButton.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenFileButton.Image")));
            this.tsbOpenFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenFileButton.Name = "tsbOpenFileButton";
            this.tsbOpenFileButton.Size = new System.Drawing.Size(49, 22);
            this.tsbOpenFileButton.Text = "Open...";
            this.tsbOpenFileButton.Click += new System.EventHandler(this.tsbOpenFileButton_Click);
            // 
            // tsbSaveFileButton
            // 
            this.tsbSaveFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSaveFileButton.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveFileButton.Image")));
            this.tsbSaveFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveFileButton.Name = "tsbSaveFileButton";
            this.tsbSaveFileButton.Size = new System.Drawing.Size(35, 22);
            this.tsbSaveFileButton.Text = "Save";
            this.tsbSaveFileButton.Click += new System.EventHandler(this.tsbSaveFileButton_Click);
            // 
            // tsbSaveAsButton
            // 
            this.tsbSaveAsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSaveAsButton.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAsButton.Image")));
            this.tsbSaveAsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAsButton.Name = "tsbSaveAsButton";
            this.tsbSaveAsButton.Size = new System.Drawing.Size(60, 22);
            this.tsbSaveAsButton.Text = "Save As...";
            this.tsbSaveAsButton.Click += new System.EventHandler(this.tsbSaveAsButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(853, 28);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Edit";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsbBoldTextButton,
            this.tsbCursiveTextButton,
            this.tsbUndelineTextButton,
            this.tsbCrossedTextButton});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(847, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel1.Text = "Style:";
            // 
            // tsbBoldTextButton
            // 
            this.tsbBoldTextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBoldTextButton.Image = ((System.Drawing.Image)(resources.GetObject("tsbBoldTextButton.Image")));
            this.tsbBoldTextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBoldTextButton.Name = "tsbBoldTextButton";
            this.tsbBoldTextButton.Size = new System.Drawing.Size(23, 22);
            this.tsbBoldTextButton.Text = "toolStripButton1";
            this.tsbBoldTextButton.Click += new System.EventHandler(this.tsbBoldTextButton_Click);
            // 
            // tsbCursiveTextButton
            // 
            this.tsbCursiveTextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCursiveTextButton.Image = ((System.Drawing.Image)(resources.GetObject("tsbCursiveTextButton.Image")));
            this.tsbCursiveTextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCursiveTextButton.Name = "tsbCursiveTextButton";
            this.tsbCursiveTextButton.Size = new System.Drawing.Size(23, 22);
            this.tsbCursiveTextButton.Text = "toolStripButton2";
            this.tsbCursiveTextButton.Click += new System.EventHandler(this.tsbCursiveTextButton_Click);
            // 
            // tsbUndelineTextButton
            // 
            this.tsbUndelineTextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndelineTextButton.Image = ((System.Drawing.Image)(resources.GetObject("tsbUndelineTextButton.Image")));
            this.tsbUndelineTextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndelineTextButton.Name = "tsbUndelineTextButton";
            this.tsbUndelineTextButton.Size = new System.Drawing.Size(23, 22);
            this.tsbUndelineTextButton.Text = "toolStripButton3";
            this.tsbUndelineTextButton.Click += new System.EventHandler(this.tsbUndelineTextButton_Click);
            // 
            // tsbCrossedTextButton
            // 
            this.tsbCrossedTextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCrossedTextButton.Image = ((System.Drawing.Image)(resources.GetObject("tsbCrossedTextButton.Image")));
            this.tsbCrossedTextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCrossedTextButton.Name = "tsbCrossedTextButton";
            this.tsbCrossedTextButton.Size = new System.Drawing.Size(23, 22);
            this.tsbCrossedTextButton.Text = "toolStripButton4";
            this.tsbCrossedTextButton.Click += new System.EventHandler(this.tsbCrossedTextButton_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.toolStrip3);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(853, 28);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Format";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(847, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timesNewRomanToolStripMenuItem,
            this.calibriToolStripMenuItem,
            this.cambriaToolStripMenuItem,
            this.garamondToolStripMenuItem,
            this.arialToolStripMenuItem,
            this.dejaVuSabsToolStripMenuItem,
            this.tahomaSansToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(44, 22);
            this.toolStripDropDownButton1.Text = "Font";
            // 
            // timesNewRomanToolStripMenuItem
            // 
            this.timesNewRomanToolStripMenuItem.Name = "timesNewRomanToolStripMenuItem";
            this.timesNewRomanToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.timesNewRomanToolStripMenuItem.Text = "Times New Roman";
            this.timesNewRomanToolStripMenuItem.Click += new System.EventHandler(this.timesNewRomanToolStripMenuItem_Click);
            // 
            // calibriToolStripMenuItem
            // 
            this.calibriToolStripMenuItem.Name = "calibriToolStripMenuItem";
            this.calibriToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.calibriToolStripMenuItem.Text = "Calibri";
            this.calibriToolStripMenuItem.Click += new System.EventHandler(this.calibriToolStripMenuItem_Click);
            // 
            // cambriaToolStripMenuItem
            // 
            this.cambriaToolStripMenuItem.Name = "cambriaToolStripMenuItem";
            this.cambriaToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.cambriaToolStripMenuItem.Text = "Cambria";
            this.cambriaToolStripMenuItem.Click += new System.EventHandler(this.cambriaToolStripMenuItem_Click);
            // 
            // garamondToolStripMenuItem
            // 
            this.garamondToolStripMenuItem.Name = "garamondToolStripMenuItem";
            this.garamondToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.garamondToolStripMenuItem.Text = "Garamond";
            this.garamondToolStripMenuItem.Click += new System.EventHandler(this.garamondToolStripMenuItem_Click);
            // 
            // arialToolStripMenuItem
            // 
            this.arialToolStripMenuItem.Name = "arialToolStripMenuItem";
            this.arialToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.arialToolStripMenuItem.Text = "Arial";
            this.arialToolStripMenuItem.Click += new System.EventHandler(this.arialToolStripMenuItem_Click);
            // 
            // dejaVuSabsToolStripMenuItem
            // 
            this.dejaVuSabsToolStripMenuItem.Name = "dejaVuSabsToolStripMenuItem";
            this.dejaVuSabsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.dejaVuSabsToolStripMenuItem.Text = "DejaVu Sabs";
            this.dejaVuSabsToolStripMenuItem.Click += new System.EventHandler(this.dejaVuSabsToolStripMenuItem_Click);
            // 
            // tahomaSansToolStripMenuItem
            // 
            this.tahomaSansToolStripMenuItem.Name = "tahomaSansToolStripMenuItem";
            this.tahomaSansToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.tahomaSansToolStripMenuItem.Text = "Tahoma";
            this.tahomaSansToolStripMenuItem.Click += new System.EventHandler(this.tahomaToolStripMenuItem_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.toolStrip4);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(853, 28);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Options";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownAutosaveInterval,
            this.toolStripButton1});
            this.toolStrip4.Location = new System.Drawing.Point(3, 3);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(847, 25);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripDropDownAutosaveInterval
            // 
            this.toolStripDropDownAutosaveInterval.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownAutosaveInterval.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownAutosaveInterval.Image")));
            this.toolStripDropDownAutosaveInterval.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownAutosaveInterval.Name = "toolStripDropDownAutosaveInterval";
            this.toolStripDropDownAutosaveInterval.Size = new System.Drawing.Size(111, 22);
            this.toolStripDropDownAutosaveInterval.Text = "Autosave Interval";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(129, 22);
            this.toolStripButton1.Text = "Change Color Scheme";
            this.toolStripButton1.Click += new System.EventHandler(this.buttonChangeScheme_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.formatToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 114);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boldToolStripMenuItem,
            this.italicToolStripMenuItem,
            this.underlineToolStripMenuItem,
            this.strikeoutToolStripMenuItem});
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            this.formatToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.formatToolStripMenuItem.Text = "Format";
            // 
            // boldToolStripMenuItem
            // 
            this.boldToolStripMenuItem.Name = "boldToolStripMenuItem";
            this.boldToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.boldToolStripMenuItem.Text = "Bold";
            this.boldToolStripMenuItem.Click += new System.EventHandler(this.boldToolStripMenuItem_Click);
            // 
            // italicToolStripMenuItem
            // 
            this.italicToolStripMenuItem.Name = "italicToolStripMenuItem";
            this.italicToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.italicToolStripMenuItem.Text = "Italic";
            this.italicToolStripMenuItem.Click += new System.EventHandler(this.italicToolStripMenuItem_Click);
            // 
            // underlineToolStripMenuItem
            // 
            this.underlineToolStripMenuItem.Name = "underlineToolStripMenuItem";
            this.underlineToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.underlineToolStripMenuItem.Text = "Underline";
            this.underlineToolStripMenuItem.Click += new System.EventHandler(this.underlineToolStripMenuItem_Click);
            // 
            // strikeoutToolStripMenuItem
            // 
            this.strikeoutToolStripMenuItem.Name = "strikeoutToolStripMenuItem";
            this.strikeoutToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.strikeoutToolStripMenuItem.Text = "Strikeout";
            this.strikeoutToolStripMenuItem.Click += new System.EventHandler(this.strikeoutToolStripMenuItem_Click);
            // 
            // NotePad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 494);
            this.Controls.Add(this.tabControlMenuTabs);
            this.Controls.Add(this.tabControlAllTabs);
            this.KeyPreview = true;
            this.Name = "NotePad";
            this.Text = "NotePad--";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.tabControlMenuTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        



        #endregion
        public System.Windows.Forms.TabControl tabControlAllTabs;
        private System.Windows.Forms.TabControl tabControlMenuTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNewFileButton;
        private System.Windows.Forms.ToolStripButton tsbOpenFileButton;
        private System.Windows.Forms.ToolStripButton tsbSaveFileButton;
        private System.Windows.Forms.ToolStripButton tsbSaveAsButton;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton tsbBoldTextButton;
        private System.Windows.Forms.ToolStripButton tsbCursiveTextButton;
        private System.Windows.Forms.ToolStripButton tsbUndelineTextButton;
        private System.Windows.Forms.ToolStripButton tsbCrossedTextButton;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem timesNewRomanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calibriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambriaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem garamondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dejaVuSabsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tahomaSansToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem formatToolStripMenuItem;
        private ToolStripMenuItem boldToolStripMenuItem;
        private ToolStripMenuItem italicToolStripMenuItem;
        private ToolStripMenuItem underlineToolStripMenuItem;
        private ToolStripMenuItem strikeoutToolStripMenuItem;
        private ToolStrip toolStrip4;
        private ToolStripDropDownButton toolStripDropDownAutosaveInterval;
        private ToolStripButton toolStripButton1;
    }
}

