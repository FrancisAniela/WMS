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
	public class Almacenes: INotifyPropertyChanged
	{
		public int id { get; set; }
		public string almacen { get; set; }
		public int idEmpresa { get; set; }
		public int idSucursal { get; set; }
		public bool multiEmpresa { get; set; }
		public String Almacen { get => almacen ; set { almacen = value; OnPropertyChanged(); } }


		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


	}
}