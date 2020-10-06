using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CellariumAndroid.Models
{
    public class RolloDespacho : Rollos, INotifyPropertyChanged
    {
        private string _colorText { get; set; }
        private string _colorBack { get; set; }

        private string _nroPieza { get; set; }
        public int sts { get; set; } = 35;
    
        public String ColorText { get => _colorText; set { _colorText = value; OnPropertyChanged(); } }
        public String ColorBack { get => _colorBack; set { _colorBack = value; OnPropertyChanged(); } }

        public new String NroPieza { get => _nroPieza; set { _nroPieza = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}