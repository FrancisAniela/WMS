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
    public class PedidoCuna
    {

        public int idCuna { get; set; }
        public String codigoBarraUnidadCarga { get; set; }

        public int idPedido { get; set; }

        public String numeroDocumento { get; set; }

        
        public int cantidadPedidosAsociadosCuna{ get; set; }

    }
}