namespace Kata.UI.WinForms.WordWrap
{
    using System;
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
    }
}
