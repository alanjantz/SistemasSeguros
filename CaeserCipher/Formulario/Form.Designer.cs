namespace Formulario
{
    partial class Form
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
            this.txtMensagemOriginal = new System.Windows.Forms.RichTextBox();
            this.btnCifrar = new System.Windows.Forms.Button();
            this.btnDecifrar = new System.Windows.Forms.Button();
            this.txtValor = new System.Windows.Forms.RichTextBox();
            this.lblMensagemOriginal = new System.Windows.Forms.Label();
            this.lblValor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMensagemOriginal
            // 
            this.txtMensagemOriginal.Location = new System.Drawing.Point(15, 25);
            this.txtMensagemOriginal.Name = "txtMensagemOriginal";
            this.txtMensagemOriginal.Size = new System.Drawing.Size(776, 132);
            this.txtMensagemOriginal.TabIndex = 0;
            this.txtMensagemOriginal.Text = "";
            // 
            // btnCifrar
            // 
            this.btnCifrar.Location = new System.Drawing.Point(15, 163);
            this.btnCifrar.Name = "btnCifrar";
            this.btnCifrar.Size = new System.Drawing.Size(75, 23);
            this.btnCifrar.TabIndex = 1;
            this.btnCifrar.Text = "Cifrar";
            this.btnCifrar.UseVisualStyleBackColor = true;
            this.btnCifrar.Click += new System.EventHandler(this.btnCifrar_Click);
            // 
            // btnDecifrar
            // 
            this.btnDecifrar.Location = new System.Drawing.Point(96, 163);
            this.btnDecifrar.Name = "btnDecifrar";
            this.btnDecifrar.Size = new System.Drawing.Size(75, 23);
            this.btnDecifrar.TabIndex = 2;
            this.btnDecifrar.Text = "Decifrar";
            this.btnDecifrar.UseVisualStyleBackColor = true;
            this.btnDecifrar.Click += new System.EventHandler(this.btnDecifrar_Click);
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(15, 216);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(776, 132);
            this.txtValor.TabIndex = 3;
            this.txtValor.Text = "";
            // 
            // lblMensagemOriginal
            // 
            this.lblMensagemOriginal.AutoSize = true;
            this.lblMensagemOriginal.Location = new System.Drawing.Point(12, 9);
            this.lblMensagemOriginal.Name = "lblMensagemOriginal";
            this.lblMensagemOriginal.Size = new System.Drawing.Size(59, 13);
            this.lblMensagemOriginal.TabIndex = 4;
            this.lblMensagemOriginal.Text = "Mensagem";
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(15, 200);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(31, 13);
            this.lblValor.TabIndex = 5;
            this.lblValor.Text = "Valor";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 358);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.lblMensagemOriginal);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.btnDecifrar);
            this.Controls.Add(this.btnCifrar);
            this.Controls.Add(this.txtMensagemOriginal);
            this.Name = "Form";
            this.Text = "Cifrar e Decifrar - códigos ultrasecretos do império romano";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtMensagemOriginal;
        private System.Windows.Forms.Button btnCifrar;
        private System.Windows.Forms.Button btnDecifrar;
        private System.Windows.Forms.RichTextBox txtValor;
        private System.Windows.Forms.Label lblMensagemOriginal;
        private System.Windows.Forms.Label lblValor;
    }
}

