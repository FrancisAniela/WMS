using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CellariumAndroid.Models
{
    public class OrdenEntrada : INotifyPropertyChanged
    {
        public string codigo { get; set; }
        public string Codigo { get => codigo; set { codigo = value; OnPropertyChanged(); } }
        public int IdArticulo { get; set; }
        public int Linea { get; set; }
        public double CantidadOrdenada { get; set; }
        public double CantidadInspeccionada { get; set; }
        public double CantidadRecibida { get; set; }
        public double CantidadRechazada { get; set; }
        public double CantidadDefectuosa { get; set; } = 0;
        public int IdColaTareas { get; set; }
        public int IdOrdenEntradaDetalle { get; set; }
        public string codigoArticulo { get; set; }
        public string CodigoArticulo { get; set; }
        public bool Calidad { get; set; }
        public int IdOrdenEntradaCabecera { get; set; }
        public double CantidadRecibir { get; set ;  }                                              
        private string _color { get => Linea % 2 == 0 ? "White" : "White"; }
        public string Color { get => _color; set { OnPropertyChanged(); } }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}