namespace Kata.UI.WinForms.WordWrap
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Services.WordWrap;

    public partial class WordWrapView : Form
    {
        private readonly WordWrapService service = new WordWrapService();


        public WordWrapView()
        {
            this.InitializeComponent();
        }


        private void WrapIt(string text, int limit) =>
            this.textBoxWrapped.Text = this.service.WordWrap(text, limit);


        private void OnWrapping_needed(object sender, EventArgs e)
        {
            this.WrapIt(this.textBoxOrig.Text, (int)this.numericUpDownWordWrapLimit.Value);
        }

        private void OnTextBoxOrig_KeyUp(object sender, KeyEventArgs e)
        {
            var codes = new List<Keys> { Keys.LineFeed, Keys.Space, Keys.Return };

            if (codes.Contains(e.KeyCode))
                this.WrapIt(this.textBoxOrig.Text, (int) this.numericUpDownWordWrapLimit.Value);
        }
    }
}
