using MemorizacaoNumeros.src.service;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoUmRealizado : EntidadeDeBanco {

		public long IdExperimentoUm { get; set; }
		private ExperimentoUm experimentoUm;
		public ExperimentoUm ExperimentoUm {
			get {
				if (experimentoUm == null) experimentoUm = ExperimentoUmService.GetById(IdExperimentoUm);
				return experimentoUm;
			}
			set {
				IdExperimentoUm = GetId(value);
				experimentoUm = value;
			}
		}

		public string DataHoraInicio { get; set; }

		public DateTime DateTimeInicio {
			get => Convert.ToDateTime(DataHoraInicio, new CultureInfo("pt-BR"));
			set => DataHoraInicio = value.ToString(ExperimentoRealizado.FORMATO_DATE_TIME);
		}

		private List<Evento> eventos = new List<Evento>();

		public void RegistrarEvento(Evento evento) {
			evento.Horario = Convert.ToInt64((DateTime.Now - DateTimeInicio).TotalSeconds);
			eventos.Add(evento);
		}

		public List<Evento> GetListaEventos() {
			return eventos;
		}

		public void SetListaEventos(List<Evento> eventos) {
			this.eventos = eventos;
		}

		// Inicio da parte do comportamento do experimento

		private readonly GeradorNumeros geradorNumeros = new GeradorNumeros();
		private readonly Random random = new Random();


		// 0 - Pré treino, 1 - Linha de Base, 2 - Fase Experimental
		public int faseAtual = 0;

		private int tamanhoAtualSequencia = 0;
		private int tentativaBlocoAtual = 0;

		private int corretasConsecutivasPreTreino = 0;

		public int tamanhoMaximoLinhaDeBase;
		private int talvezUltimoBlocoLinhaDeBase = 0;

		private int digitosASeremVariados = 1;
		private int quantidadeEstimulosFracos = 0;
		private int talvezEstimulosFracos = 0;
		private int blocosExecutados = 0;

		// Variáveis para o resumo do experimento

		private int acertosPreTreino = 0;
		private int errosPreTreino = 0;

		// Separados por tamanho da sequência
		private List<int> acertosTalvezLinhaDeBase = new List<int>() { 0 };
		private List<int> errosTalvezLinhaDeBase = new List<int>() { 0 };
		private List<int> acertosCertezaLinhaDeBase = new List<int>() { 0 };
		private List<int> errosCertezaLinhaDeBase = new List<int>() { 0 };

		private int acertosTalvezFracoExperimental = 0;
		private int errosTalvezFracoExperimental = 0;
		private int acertosCertezaFracoExperimental = 0;
		private int errosCertezaFracoExperimental = 0;
		private int acertosTalvezForteExperimental = 0;
		private int errosTalvezForteExperimental = 0;
		private int acertosCertezaForteExperimental = 0;
		private int errosCertezaForteExperimental = 0;

		private string NomeFaseAtual {
			get {
				if (faseAtual == 0) {
					return "Pré Treino";
				}
				if (faseAtual == 1) {
					return "Linha de Base";
				}
				if (faseAtual == 2) {
					return "Fase Experimental";
				}
				return "Fim do Experimento";
			}
		}

		// Retorna true se terminamos uma fase/fomos para uma nova
		public bool RegistrarResposta(bool acertou, bool certeza, string sequenciaModelo, string sequenciaDigitada) {
			// Supostamente nunca deve acontecer
			if (faseAtual > 2) {
				return true;
			}

			var origem = NomeFaseAtual;
			var origemResumo = faseAtual.ToString();
			var comparacaoSequencias = $"Sequência modelo: {sequenciaModelo}, Sequência digitada: {sequenciaDigitada}.";
			var acerto = acertou ? "acertou" : "errou";
			var cert = certeza ? "certeza" : "talvez";

			// Pré treino
			if (faseAtual == 0) {
				tamanhoAtualSequencia = ExperimentoUm.TamanhoSequenciaInicial;

				if (acertou) {
					corretasConsecutivasPreTreino++;
					acertosPreTreino++;

					RegistrarEvento(new Evento(origem, $"Participante acertou. Acertos consecutivos: {corretasConsecutivasPreTreino}. {comparacaoSequencias}"));

					// O participante acertou quantidade necessária de vezes consecutivas, vamos para a próxima fase
					if (corretasConsecutivasPreTreino == ExperimentoUm.CriterioAcertoPreTreino) {
						corretasConsecutivasPreTreino = 0;
						faseAtual++;

						RegistrarEvento(new Evento(origem, $"Acertos consecutivos necessários para finalizar Pré treino alcançados."));
						RegistrarEvento(new Evento(origemResumo, $"Acertos;Erros: {acertosPreTreino};{errosPreTreino}"));

						return true;
					}
				}
				else {
					errosPreTreino++;
					corretasConsecutivasPreTreino = 0;

					RegistrarEvento(new Evento(origem, $"Participante errou. {comparacaoSequencias}"));
				}
			}
			// Linha de Base
			else if (faseAtual == 1) {
				tentativaBlocoAtual++;

				var index = tamanhoAtualSequencia - experimentoUm.TamanhoSequenciaInicial;

				if (acertou) {
					if (certeza) {
						acertosCertezaLinhaDeBase[index]++;
					}
					else {
						acertosTalvezLinhaDeBase[index]++;
					}
				}
				else {
					if (certeza) {
						errosCertezaLinhaDeBase[index]++;
					}
					else {
						errosTalvezLinhaDeBase[index]++;
					}
				}

				if (!certeza) {
					talvezUltimoBlocoLinhaDeBase++;
				}

				RegistrarEvento(new Evento(origem, $"Participante {acerto}, selecionou {cert}. {comparacaoSequencias}"));

				// Acabamos de terminar um bloco
				if (tentativaBlocoAtual == ExperimentoUm.TamanhoBlocoTentativas) {
					tentativaBlocoAtual = 0;

					RegistrarEvento(new Evento(origem, $"Fim do bloco de tentativas. Tamanho sequência atual: {tamanhoAtualSequencia}"));

					// O participante escolheu talvez acima do trashold
					if (talvezUltimoBlocoLinhaDeBase >= ExperimentoUm.CalculaCriterioTalvezLinhaDeBase()) {
						RegistrarEvento(new Evento(origem,
							$"Quantidade de talvez acima do critério ({talvezUltimoBlocoLinhaDeBase}/{ExperimentoUm.TamanhoBlocoTentativas}). Passando para próxima fase"));

						tamanhoMaximoLinhaDeBase = tamanhoAtualSequencia;
						talvezUltimoBlocoLinhaDeBase = 0;
						faseAtual++;

						for (int i = 0; i <= index; i++) {
							RegistrarEvento(new Evento(origemResumo,
								$"Tamanho {experimentoUm.TamanhoSequenciaInicial + i}: Acertos certeza;Erros certeza: {acertosCertezaLinhaDeBase[i]};{errosCertezaLinhaDeBase[i]}"));
							RegistrarEvento(new Evento(origemResumo,
								$"Tamanho {experimentoUm.TamanhoSequenciaInicial + i}: Acertos talvez;Erros talvez: {acertosTalvezLinhaDeBase[i]};{errosTalvezLinhaDeBase[i]}"));
						}

						return true;
					}
					// O tamanho da sequência aumenta
					else {
						RegistrarEvento(new Evento(origem,
							$"Quantidade de talvez abaixo do critério ({talvezUltimoBlocoLinhaDeBase}/{ExperimentoUm.TamanhoBlocoTentativas}). Aumentando tamanho da sequência"));

						tamanhoAtualSequencia++;
						talvezUltimoBlocoLinhaDeBase = 0;

						acertosCertezaLinhaDeBase.Add(0);
						acertosTalvezLinhaDeBase.Add(0);
						errosCertezaLinhaDeBase.Add(0);
						errosTalvezLinhaDeBase.Add(0);
					}
				}
			}

			// Fase Experimental
			else if (faseAtual == 2) {
				tentativaBlocoAtual++;

				if (tamanhoAtualSequencia > tamanhoMaximoLinhaDeBase) {
					if (acertou) {
						if (certeza) {
							acertosCertezaFracoExperimental++;
						}
						else {
							acertosTalvezFracoExperimental++;
						}
					}
					else {
						if (certeza) {
							errosCertezaFracoExperimental++;
						}
						else {
							errosTalvezFracoExperimental++;
						}
					}
				}
				else {
					if (acertou) {
						if (certeza) {
							acertosCertezaForteExperimental++;
						}
						else {
							acertosTalvezForteExperimental++;
						}
					}
					else {
						if (certeza) {
							errosCertezaForteExperimental++;
						}
						else {
							errosTalvezForteExperimental++;
						}
					}
				}

				if (tamanhoAtualSequencia > tamanhoMaximoLinhaDeBase) {
					quantidadeEstimulosFracos++;
					if (!certeza) {
						talvezEstimulosFracos++;
					}
				}

				var forca = tamanhoAtualSequencia > tamanhoMaximoLinhaDeBase ? "Fraco" : "Forte";

				RegistrarEvento(new Evento(origem,$"Estímulo {forca}. Participante {acerto}, selecionou {cert}. {comparacaoSequencias}"));

				// Acabamos de terminar um bloco
				if (tentativaBlocoAtual == ExperimentoUm.TamanhoBlocoTentativas) {
					blocosExecutados++;
					tentativaBlocoAtual = 0;

					RegistrarEvento(new Evento(origem, $"Fim do bloco {blocosExecutados}"));

					// Terminamos o número de blocos previstos
					if (blocosExecutados == ExperimentoUm.NumeroBlocosFaseExperimental) {
						blocosExecutados = 0;

						if (talvezEstimulosFracos < ExperimentoUm.CalculaCriterioReforcoFaseExperimental(quantidadeEstimulosFracos)) {
							RegistrarEvento(new Evento(origem,
								$"Talvez selecionados em estímulos fracos insuficientes ({talvezEstimulosFracos}/{quantidadeEstimulosFracos}). Aumentando a variação de dígitos"));

							quantidadeEstimulosFracos = 0;
							talvezEstimulosFracos = 0;
							digitosASeremVariados++;
						}
						else {
							RegistrarEvento(new Evento(origem,
								$"Talvez selecionados em estímulos fracos suficientes ({talvezEstimulosFracos}/{quantidadeEstimulosFracos}). Fim do Experimento Um"));

							quantidadeEstimulosFracos = 0;
							talvezEstimulosFracos = 0;
							faseAtual++;

							RegistrarEvento(new Evento(origemResumo, $"Acertos certeza fraco;Erros certeza fraco: {acertosCertezaFracoExperimental};{errosCertezaFracoExperimental}"));
							RegistrarEvento(new Evento(origemResumo, $"Acertos talvez fraco;Erros talvez fraco: {acertosTalvezFracoExperimental};{errosTalvezFracoExperimental}"));
							RegistrarEvento(new Evento(origemResumo, $"Acertos certeza forte;Erros certeza forte: {acertosCertezaForteExperimental};{errosCertezaForteExperimental}"));
							RegistrarEvento(new Evento(origemResumo, $"Acertos talvez forte;Erros talvez forte: {acertosTalvezForteExperimental};{errosTalvezForteExperimental}"));

							return true;
						}
					}
				}
			}

			return false;
		}

		// Função que vai gerando os números para o experimento. Se retornar null, o experimento acabou (talvez mudar isso)
		public string GeraNumero() {
			// Acabou o experimento
			if (faseAtual > 2) {
				return null;
			}

			// Inicializando o tamanho da sequência
			if (faseAtual == 0 && tamanhoAtualSequencia == 0) {
				tamanhoAtualSequencia = ExperimentoUm.TamanhoSequenciaInicial;
			}

			// Fase Experimental
			if (faseAtual == 2) {
				tamanhoAtualSequencia = tamanhoMaximoLinhaDeBase + digitosASeremVariados * (random.Next(2) == 1 ? -1 : 1);
				if (tamanhoAtualSequencia < 1) {
					tamanhoAtualSequencia = 1;
				}
			}

			// Experimento acabou
			return geradorNumeros.GerarNumero(tamanhoAtualSequencia);
		}
	}
}
