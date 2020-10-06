using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CellariumAndroid.Models
{
	public class Articulo : INotifyPropertyChanged
	{
		public int Id { get; set; }

		private string _articuloCodigo { get; set; }

		public string ArticuloCodigo { get => _articuloCodigo; set { _articuloCodigo = value; OnPropertyChanged(); } }

		private decimal _cantidad { get; set; }

		public string CodigoBarra { get; set; }

		public int IdUnidadCarga { get; set; }

		public int IdUbicacion { get; set; }

		public string CodigoUnidadCarga { get; set; }

		public decimal Cantidad { get => _cantidad; set { _cantidad = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


		public int IdArticuloEtiqueta { get; set; }

		public int IdArticulo { get; set; }

		public int IdEmpresa { get; set; }

		public int IdAlmacen { get; set; }

		public int IdCliente { get; set; }

	}



}