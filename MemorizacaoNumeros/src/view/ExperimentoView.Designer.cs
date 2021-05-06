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
			this.lblCorreto = new System.Windows.Forms.Label();
			this.timerFade = new System.Windows.Forms.Timer(this.components);
			this.pnCorreto = new System.Windows.Forms.Panel();
			this.pnInput = new System.Windows.Forms.Panel();
			this.pnGrau = new System.Windows.Forms.Panel();
			this.lblGrau = new System.Windows.Forms.Label();
			this.pnNumero.SuspendLayout();
			this.pnCorreto.SuspendLayout();
			this.pnInput.SuspendLayout();
			this.pnGrau.SuspendLayout();
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
			// lblCorreto
			// 
			this.lblCorreto.AutoSize = true;
			this.lblCorreto.BackColor = System.Drawing.SystemColors.Desktop;
			this.lblCorreto.Font = new System.Drawing.Font("Microsoft YaHei", 48F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCorreto.ForeColor = System.Drawing.Color.Yellow;
			this.lblCorreto.Location = new System.Drawing.Point(50, 45);
			this.lblCorreto.Name = "lblCorreto";
			this.lblCorreto.Size = new System.Drawing.Size(288, 83);
			this.lblCorreto.TabIndex = 5;
			this.lblCorreto.Text = "Correto!";
			// 
			// timerFade
			// 
			this.timerFade.Tick += new System.EventHandler(this.timerFade_Tick);
			// 
			// pnCorreto
			// 
			this.pnCorreto.BackColor = System.Drawing.SystemColors.Desktop;
			this.pnCorreto.Controls.Add(this.pnGrau);
			this.pnCorreto.Controls.Add(this.lblCorreto);
			this.pnCorreto.Location = new System.Drawing.Point(1430, 46);
			this.pnCorreto.Name = "pnCorreto";
			this.pnCorreto.Size = new System.Drawing.Size(384, 179);
			this.pnCorreto.TabIndex = 2;
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
			// pnGrau
			// 
			this.pnGrau.BackColor = System.Drawing.SystemColors.Desktop;
			this.pnGrau.Controls.Add(this.lblGrau);
			this.pnGrau.Location = new System.Drawing.Point(0, 0);
			this.pnGrau.Name = "pnGrau";
			this.pnGrau.Size = new System.Drawing.Size(384, 179);
			this.pnGrau.TabIndex = 6;
			this.pnGrau.Visible = false;
			// 
			// lblGrau
			// 
			this.lblGrau.AutoSize = true;
			this.lblGrau.BackColor = System.Drawing.SystemColors.Desktop;
			this.lblGrau.Font = new System.Drawing.Font("Microsoft YaHei", 48F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGrau.ForeColor = System.Drawing.Color.Yellow;
			this.lblGrau.Location = new System.Drawing.Point(96, 45);
			this.lblGrau.Name = "lblGrau";
			this.lblGrau.Size = new System.Drawing.Size(181, 83);
			this.lblGrau.TabIndex = 5;
			this.lblGrau.Text = "Grau";
			// 
			// ExperimentoView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ClientSize = new System.Drawing.Size(1920, 1061);
			this.Controls.Add(this.pnInput);
			this.Controls.Add(this.pnCorreto);
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
			this.pnCorreto.ResumeLayout(false);
			this.pnCorreto.PerformLayout();
			this.pnInput.ResumeLayout(false);
			this.pnInput.PerformLayout();
			this.pnGrau.ResumeLayout(false);
			this.pnGrau.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnNumero;
		private System.Windows.Forms.Label lblNumero;
		private System.Windows.Forms.Button btnCerteza;
		private System.Windows.Forms.Button btnTalvez;
		private System.Windows.Forms.TextBox tbInput;
		private System.Windows.Forms.Label lblCorreto;
		private System.Windows.Forms.Timer timerFade;
		private System.Windows.Forms.Panel pnCorreto;
		private System.Windows.Forms.Panel pnInput;
		private System.Windows.Forms.Panel pnGrau;
		private System.Windows.Forms.Label lblGrau;
	}
}