namespace MemorizacaoNumeros.src.view {
	partial class ExperimentoView {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.pnNumero = new System.Windows.Forms.Panel();
			this.lblNumero = new System.Windows.Forms.Label();
			this.btnCerteza = new System.Windows.Forms.Button();
			this.btnTalvez = new System.Windows.Forms.Button();
			this.tbInput = new System.Windows.Forms.TextBox();
			this.lblMensagem = new System.Windows.Forms.Label();
			this.timerFade = new System.Windows.Forms.Timer(this.components);
			this.pnMensagem = new System.Windows.Forms.Panel();
			this.pnInput = new System.Windows.Forms.Panel();
			this.pnNumero.SuspendLayout();
			this.pnMensagem.SuspendLayout();
			this.pnInput.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnNumero
			// 
			this.pnNumero.BackColor = System.Drawing.SystemColors.Desktop;
			this.pnNumero.Controls.Add(this.lblNumero);
			this.pnNumero.Location = new System.Drawing.Point(768, 275);
			this.pnNumero.Name = "pnNumero";
			this.pnNumero.Size = new System.Drawing.Size(384, 179);
			this.pnNumero.TabIndex = 1;
			// 
			// lblNumero
			// 
			this.lblNumero.AutoSize = true;
			this.lblNumero.Font = new System.Drawing.Font("Microsoft YaHei", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNumero.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.lblNumero.Location = new System.Drawing.Point(83, 43);
			this.lblNumero.Name = "lblNumero";
			this.lblNumero.Size = new System.Drawing.Size(225, 83);
			this.lblNumero.TabIndex = 0;
			this.lblNumero.Text = "12345";
			// 
			// btnCerteza
			// 
			this.btnCerteza.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.btnCerteza.Font = new System.Drawing.Font("Microsoft YaHei", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCerteza.Location = new System.Drawing.Point(290, 537);
			this.btnCerteza.Name = "btnCerteza";
			this.btnCerteza.Size = new System.Drawing.Size(341, 120);
			this.btnCerteza.TabIndex = 2;
			this.btnCerteza.Text = "Certeza";
			this.btnCerteza.UseVisualStyleBackColor = false;
			this.btnCerteza.Click += new System.EventHandler(this.btnCerteza_Click);
			// 
			// btnTalvez
			// 
			this.btnTalvez.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.btnTalvez.Font = new System.Drawing.Font("Microsoft YaHei", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTalvez.Location = new System.Drawing.Point(1290, 537);
			this.btnTalvez.Name = "btnTalvez";
			this.btnTalvez.Size = new System.Drawing.Size(340, 120);
			this.btnTalvez.TabIndex = 3;
			this.btnTalvez.Text = "Talvez";
			this.btnTalvez.UseVisualStyleBackColor = false;
			this.btnTalvez.Click += new System.EventHandler(this.btnTalvez_Click);
			// 
			// tbInput
			// 
			this.tbInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbInput.Font = new System.Drawing.Font("Microsoft YaHei", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbInput.Location = new System.Drawing.Point(0, 40);
			this.tbInput.Name = "tbInput";
			this.tbInput.Size = new System.Drawing.Size(384, 85);
			this.tbInput.TabIndex = 4;
			this.tbInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.tbInput.WordWrap = false;
			this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
			this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
			// 
			// lblMensagem
			// 
			this.lblMensagem.AutoSize = true;
			this.lblMensagem.BackColor = System.Drawing.SystemColors.Desktop;
			this.lblMensagem.Font = new System.Drawing.Font("Microsoft YaHei", 48F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMensagem.ForeColor = System.Drawing.Color.Yellow;
			this.lblMensagem.Location = new System.Drawing.Point(50, 45);
			this.lblMensagem.Name = "lblMensagem";
			this.lblMensagem.Size = new System.Drawing.Size(288, 83);
			this.lblMensagem.TabIndex = 5;
			this.lblMensagem.Text = "Correto!";
			// 
			// timerFade
			// 
			this.timerFade.Tick += new System.EventHandler(this.timerFade_Tick);
			// 
			// pnMensagem
			// 
			this.pnMensagem.BackColor = System.Drawing.SystemColors.Desktop;
			this.pnMensagem.Controls.Add(this.lblMensagem);
			this.pnMensagem.Location = new System.Drawing.Point(1430, 46);
			this.pnMensagem.Name = "pnMensagem";
			this.pnMensagem.Size = new System.Drawing.Size(384, 179);
			this.pnMensagem.TabIndex = 2;
			this.pnMensagem.Visible = false;
			// 
			// pnInput
			// 
			this.pnInput.BackColor = System.Drawing.SystemColors.Window;
			this.pnInput.Controls.Add(this.tbInput);
			this.pnInput.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.pnInput.Location = new System.Drawing.Point(768, 750);
			this.pnInput.Name = "pnInput";
			this.pnInput.Size = new System.Drawing.Size(384, 179);
			this.pnInput.TabIndex = 5;
			// 
			// ExperimentoView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ClientSize = new System.Drawing.Size(1920, 1061);
			this.Controls.Add(this.pnInput);
			this.Controls.Add(this.pnMensagem);
			this.Controls.Add(this.btnTalvez);
			this.Controls.Add(this.btnCerteza);
			this.Controls.Add(this.pnNumero);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ExperimentoView";
			this.Text = "ExperimentoView";
			this.pnNumero.ResumeLayout(false);
			this.pnNumero.PerformLayout();
			this.pnMensagem.ResumeLayout(false);
			this.pnMensagem.PerformLayout();
			this.pnInput.ResumeLayout(false);
			this.pnInput.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnNumero;
		private System.Windows.Forms.Label lblNumero;
		private System.Windows.Forms.Button btnCerteza;
		private System.Windows.Forms.Button btnTalvez;
		private System.Windows.Forms.TextBox tbInput;
		private System.Windows.Forms.Label lblMensagem;
		private System.Windows.Forms.Timer timerFade;
		private System.Windows.Forms.Panel pnMensagem;
		private System.Windows.Forms.Panel pnInput;
	}
}