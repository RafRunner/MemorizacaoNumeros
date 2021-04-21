using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemorizacaoNumeros.src.model {
	public class Experimento : ElementoDeBanco {

		public string InstrucaoInicial { get; set; }
		public int TempoTelaPretaIncial { get; set; }
		public int TempoTelaPretaITI { get; set; }
		public int TempoApresentacaoestimulo { get; set; }
		public int TamanhoBlocoTentativas { get; set; }

	}
}
