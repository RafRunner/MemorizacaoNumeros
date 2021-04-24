﻿using MemorizacaoNumeros.src.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemorizacaoNumeros.src.view {
	public partial class TelaMensagem : Form {

		private readonly int height = Screen.PrimaryScreen.Bounds.Height;
		private readonly int width = Screen.PrimaryScreen.Bounds.Width;

		public TelaMensagem(string mensagem, bool mostrarBotao) {
			InitializeComponent();

			Location = new Point(0, 0);
			Size = new Size(width, height);

			var heightRatio = height / 1080.0;
			var widthRatio = width / 1920.0;

			ViewUtils.CorrigeTamanhoPosicaoFonte(lblMensagem, heightRatio, widthRatio);
			ViewUtils.CorrigeTamanhoPosicaoFonte(btnOk, heightRatio, widthRatio);

			lblMensagem.MaximumSize = new Size((int)(width * 0.8), 0);
			lblMensagem.AutoSize = true;
			lblMensagem.Text = mensagem;
			lblMensagem.Location = new Point((width - lblMensagem.Width) / 2, lblMensagem.Location.Y);

			if (mostrarBotao) {
				btnOk.Location = new Point {
					X = btnOk.Location.X,
					Y = lblMensagem.Location.Y + lblMensagem.Height
				};
			}
			else {
				btnOk.Visible = false;
			}

			ViewUtils.Justify(lblMensagem);
		}

		private void btnOk_Click(object sender, EventArgs e) {
			Close();
		}
	}
}
