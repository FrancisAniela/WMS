using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CellariumAndroid.Models
{

	public class Rollos
	{

		public string nEtiqueta { get; set; }
		public int id { get; set; }
		public string codArt { get; set; }
		public string NroPieza { get; set; }
		public decimal Metros { get; set; }
		public string Calidad { get; set; }
		public string BarCodUnidadCarga { get; set; }
		public int idUnidadCarga { get; set; }
		public int idUbicacion { get; set; }
		public string _ColorText { get; set; }
		public static bool TextCuna { get; set; }

		public String ColorText { get => _ColorText; set { _ColorText = value; OnPropertyChanged(); } }

		public string Etiqueta { get { return nEtiqueta; } set { nEtiqueta = value; OnPropertyChanged(); } }

		public decimal Longitud { get { return Metros; } set { Metros = value; OnPropertyChanged(); } }

		public string CodCalidad { get { return Calidad; } set { Calidad = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


		public override bool Equals(object obj)
		{
			var item = obj as Rollos;

			if (item == null)
			{
				return false;
			}

			return nEtiqueta.Equals(item.nEtiqueta);
		}


	}
}
