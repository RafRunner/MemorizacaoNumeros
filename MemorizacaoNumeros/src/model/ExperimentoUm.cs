using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoUm : Experimento {

		public int CriterioAcertoPreTreino { get; set; }
		public int CriterioTalvezLinhaDeBase { get; set; }
		public int CriterioReforcoFaseExperiemntal { get; set; }
	}
}
