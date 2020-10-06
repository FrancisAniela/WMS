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
    public class SubModulo
    {
        public int idSubModulo { get; set; }

        public int idUnidadCarga { get; set; }

        public int IdCabeceraTarea { get; set; }

        public int idColaTareas { get; set; }

        public string numeroDocumento { get; set; }

        public int pedidoAsociado { get; set; }

        public string codigoBarraUnidadCarga { get; set; }

        //SubModulos de despacho
        public static int IdentificacionCuna { get; } = 1;

        public static int PasePorPortal { get; } = 2;

        public static int DespachoCunaPedidos { get; } = 3;

        public static int ListoParaDespachar { get; } = 4;

        public static int Auditoria { get; } = 5;

        public static int SinTareaDespacho { get; } = 6;

        public static int FinalizarPedido { set; get; } = 7;


        //SubModulos Recepcion
        public static int ValidacionRecepcion { get; } = 1;

        public static int ControlCalidad { get; } = 2;

        public static int Etiquetado { set; get; } = 3;


    }
}