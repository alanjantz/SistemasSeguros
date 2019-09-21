using CaeserCipher;
using System;

namespace Formulario
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void btnCifrar_Click(object sender, EventArgs e)
        {
            txtValor.Text = Cesar.Cifrar(txtMensagemOriginal.Text);
        }

        private void btnDecifrar_Click(object sender, EventArgs e)
        {
            txtValor.Text = Cesar.Decifrar(txtMensagemOriginal.Text);
        }
    }
}
