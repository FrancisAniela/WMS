using System;
using System.Collections.Generic;
using System.Text;

namespace CellariumAndroid.Models
{
	public class Tarea
	{
		public int Tipo { set; get; }

		public int IdTarea { set; get; }

		public int IdUnidadCarga { set; get; }

		//Esta propiedad se refiere al ID generado por una tabla externa, y que ayuda a identificar la proveniencia o el tipo de proceso al que pertenece la tarea.
		public int IdTareaExterna { set; get; }  

		public int UsuarioActual { set; get; }

		public int UsuarioAsignado { set; get; }

		public int Estatus { set; get; }

		public int IdSubModulo { set; get; }

		public double Progreso { set; get; }



	}
}
