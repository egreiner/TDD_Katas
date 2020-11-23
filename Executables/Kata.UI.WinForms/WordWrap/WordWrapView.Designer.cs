namespace Kata.UI.WinForms.WordWrap
{
    partial class WordWrapView
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.numericUpDownWordWrapLimit = new System.Windows.Forms.NumericUpDown();
            this.textBoxOrig = new System.Windows.Forms.TextBox();
            this.textBoxWrapped = new System.Windows.Forms.TextBox();
            this.buttonWrapp = new System.Windows.Forms.Button();
            this.labelBefore = new System.Windows.Forms.Label();
            this.labelAfter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWordWrapLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownWordWrapLimit
            // 
            this.numericUpDownWordWrapLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownWordWrapLimit.Location = new System.Drawing.Point(40, 668);
            this.numericUpDownWordWrapLimit.Name = "numericUpDownWordWrapLimit";
            this.numericUpDownWordWrapLimit.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownWordWrapLimit.TabIndex = 0;
            this.numericUpDownWordWrapLimit.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownWordWrapLimit.ValueChanged += new System.EventHandler(this.OnWrapping_needed);
            // 
            // textBoxOrig
            // 
            this.textBoxOrig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOrig.Location = new System.Drawing.Point(40, 48);
            this.textBoxOrig.Multiline = true;
            this.textBoxOrig.Name = "textBoxOrig";
            this.textBoxOrig.PlaceholderText = "text to be wrapped";
            this.textBoxOrig.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOrig.Size = new System.Drawing.Size(481, 218);
            this.textBoxOrig.TabIndex = 1;
            // 
            // textBoxWrapped
            // 
            this.textBoxWrapped.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWrapped.Location = new System.Drawing.Point(40, 299);
            this.textBoxWrapped.Multiline = true;
            this.textBoxWrapped.Name = "textBoxWrapped";
            this.textBoxWrapped.PlaceholderText = "wrapped text";
            this.textBoxWrapped.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxWrapped.Size = new System.Drawing.Size(481, 346);
            this.textBoxWrapped.TabIndex = 1;
            this.textBoxWrapped.WordWrap = false;
            // 
            // buttonWrapp
            // 
            this.buttonWrapp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonWrapp.Location = new System.Drawing.Point(427, 666);
            this.buttonWrapp.Name = "buttonWrapp";
            this.buttonWrapp.Size = new System.Drawing.Size(94, 29);
            this.buttonWrapp.TabIndex = 2;
            this.buttonWrapp.Text = "Wrap";
            this.buttonWrapp.UseVisualStyleBackColor = true;
            this.buttonWrapp.Click += new System.EventHandler(this.OnWrapping_needed);
            // 
            // labelBefore
            // 
            this.labelBefore.AutoSize = true;
            this.labelBefore.Location = new System.Drawing.Point(40, 25);
            this.labelBefore.Name = "labelBefore";
            this.labelBefore.Size = new System.Drawing.Size(53, 20);
            this.labelBefore.TabIndex = 3;
            this.labelBefore.Text = "Before";
            // 
            // labelAfter
            // 
            this.labelAfter.AutoSize = true;
            this.labelAfter.Location = new System.Drawing.Point(40, 276);
            this.labelAfter.Name = "labelAfter";
            this.labelAfter.Size = new System.Drawing.Size(42, 20);
            this.labelAfter.TabIndex = 3;
            this.labelAfter.Text = "After";
            // 
            // WordWrapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 722);
            this.Controls.Add(this.labelAfter);
            this.Controls.Add(this.labelBefore);
            this.Controls.Add(this.buttonWrapp);
            this.Controls.Add(this.textBoxWrapped);
            this.Controls.Add(this.textBoxOrig);
            this.Controls.Add(this.numericUpDownWordWrapLimit);
            this.Name = "WordWrapView";
            this.Text = "WordWrapView";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWordWrapLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownWordWrapLimit;
        private System.Windows.Forms.TextBox textBoxOrig;
        private System.Windows.Forms.TextBox textBoxWrapped;
        private System.Windows.Forms.Button buttonWrapp;
        private System.Windows.Forms.Label labelBefore;
        private System.Windows.Forms.Label labelAfter;
    }
}

