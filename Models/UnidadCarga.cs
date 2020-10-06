using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CellariumAndroid.Models
{
    public class UnidadCarga
    {
        public int IdEmpresa { get; set; }

        public int IdUnidadCarga { get; set; }

        public int IdUbicacion { get; set; }

        public string CodigoBarra { get; set; }

    }
}