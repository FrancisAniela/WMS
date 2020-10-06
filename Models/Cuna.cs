using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CellariumAndroid.Models
{

    public class Cuna : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        private String _codigo;
        public int id { get; set; }

        public int EstatusCuna {get; set;}

		public string codigo { get; set; }

        public string BarcodeUnidadCarga { get; set; }

        public int capacidadMaxArticulo { get; set; }

		public string codTag { get; set; }

        public string CodigoCuna { get { return _codigo; } set { _codigo = value; OnPropertyChanged(); } }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



    }
}
