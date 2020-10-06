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
    public class PedidoCunaAct :CunaAct
    {

        public static int idPedido { get; set; }

        public static String numeroDocumento { get; set; }

        public static int cantidadPedidosAsociadosCuna { get; set; }

        public static List<Cuna> cunas { get; set; }

        public static List<RolloAct> piezas { get; set; }

    }
}