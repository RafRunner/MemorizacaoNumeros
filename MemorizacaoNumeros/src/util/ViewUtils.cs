using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MemorizacaoNumeros.src.util {
	public class ViewUtils {
		public static long GetIdSelecionadoInListView(ListView listView) {
			return Convert.ToInt64(listView.SelectedItems[0].SubItems[1].Text);
		}

		public static string SelecionaArquivoComFiltro(FileDialog fileDialog, string filter = null) {
			string retorno = string.Empty;
			if (filter != null) {
				fileDialog.Filter = filter;
			}
			DialogResult result = fileDialog.ShowDialog();
			if (result == DialogResult.OK) {
				retorno = fileDialog.FileName;
			}
			fileDialog.Filter = string.Empty;
			return retorno;
		}

		public static void CorrigeTamanhoPosicaoFonte(Control controle, double heightRatio, double widthRatio) {
			controle.Height = Convert.ToInt32(controle.Height * heightRatio);
			controle.Width = Convert.ToInt32(controle.Width * widthRatio);
			controle.Location = new Point {
				X = Convert.ToInt32(controle.Location.X * widthRatio),
				Y = Convert.ToInt32(controle.Location.Y * heightRatio)
			};
            controle.Font = new Font(controle.Font.Name, Convert.ToInt32(controle.Font.Size * heightRatio));
        }

        public static List<Control> GetAllFilhos(Control root) {
            var filhos = new List<Control>();

            StepGetAllFilhos(root, filhos);

            return filhos;
        }

        private static void StepGetAllFilhos(Control root, List<Control> filhos) {
            foreach (Control filho in root.Controls) {
                filhos.Add(filho);
                StepGetAllFilhos(filho, filhos);
			}
        }

        public static void CorrigeEscalaTodosOsFilhos(Control root, double heightRatio, double widthRatio) {
            foreach (Control filho in GetAllFilhos(root)) {
                CorrigeTamanhoPosicaoFonte(filho, heightRatio, widthRatio);
            }
        }

        public static void Justify(Label label) {
            var blocks = label.Text.Split(new[] { "\r\n" }, StringSplitOptions.None).Select(l => l.Trim()).ToArray();

            var result = new List<string>();

            foreach (var block in blocks) {
                var text = BreakTextEven(block, label);
                var lines = text.Split(new[] { "\r\n" }, StringSplitOptions.None).Select(l => l.Trim()).ToArray();

                for (int i = 0; i < lines.Length - 1; i++) {
                    result.Add(StretchToWidth(lines[i], label));
                }
                result.Add(lines.Last());
            }

            label.Text = string.Join("\r\n", result);
        }

        private static string BreakTextEven(string text, Label label) {
            var widthGoal = label.Width;
            var currentLine = "";
            var brokenText = "";
            var words = text.Split(new[] { " " }, StringSplitOptions.None);

            foreach (var word in words) {
                if (TextRenderer.MeasureText(currentLine + word, label.Font).Width > widthGoal) {
                    brokenText += "\r\n";
                    currentLine = "";
                    continue;
                }

                currentLine += word + " ";
                brokenText += word + " ";
            }

            return brokenText;
		}

        private static string StretchToWidth(string text, Label label) {
            if (text.Length < 2)
                return text;

            // A hair space is the smallest possible non-visible character we can insert
            const char hairspace = '\u200A';

            // If we measure just the width of the space we might get too much because of added paddings so we have to do it a bit differently
            double basewidth = TextRenderer.MeasureText(text, label.Font).Width;
            double doublewidth = TextRenderer.MeasureText(text + text, label.Font).Width;
            double doublewidthplusspace = TextRenderer.MeasureText(text + hairspace + text, label.Font).Width;
            double spacewidth = doublewidthplusspace - doublewidth;

            //The space we have to fill up with spaces is whatever is left
            double leftoverspace = label.Width - basewidth;

            //Calculate the amount of spaces we need to insert
            int approximateInserts = Math.Max(0, (int)Math.Floor(leftoverspace / spacewidth));

            //Insert spaces
            return InsertFillerChar(hairspace, text, approximateInserts);
        }

        private static string InsertFillerChar(char filler, string text, int inserts) {
            string result = text;
            int inserted = 0;

            while (inserted < inserts) {
                for (int i = 0; inserted < inserts && i < result.Length; i++) {
                    var c = result[i];

                    if (c != filler && char.IsWhiteSpace(c)) {
                        result = result.Substring(0, i) + filler + result.Substring(i);
                        inserted++;
                        i++;
                    }
                }
            }

            return result;
        }
    }
}
