using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CellariumAndroid.ViewModels;
using Newtonsoft.Json;

namespace CellariumAndroid.Models
{

    public class ItemsPicking: Inventario
    {

        /*Hereda de la Clase (Model): Inventario, con el fin de obtener los Setters y Getters referentes info básica de un artículo.*/

        public int IdPedido { get; set; }

        public int IdArticuloEtiqueta { get; set; }

        public int IdUnidadCarga { get; set; }

        public int IdUbicacion { get; set; }

        public int IdPasillo { get; set; }

        public int PosicionEstructura { get; set; }

        public string NumeroPieza { get; set; }

        public int EstatusPieza { get; set; }

        public int EstatusUnidadCarga { get; set; }

        public double Cantidad { get; set; }

        public string NumeroEtiqueta { get; set; }


        public static explicit operator ItemsPicking(PickingViewModel obj)
        {

            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            /*Forma ideal*/
            //return JsonConvert.DeserializeObject<ItemsPicking>(JsonConvert.SerializeObject(obj));

            ItemsPicking output = new ItemsPicking()
            {
                Codigo = obj.Codigo,
                CodUbi = obj.CodigoUbicacion,
                Articulo = obj.Descripcion,
                CantidadContada = obj.CantidadContada,
                ControlCantidad = obj.ControlEAN,     //RECORDAR ELIMINAR LOS CAMPOS ESPECIFICOS DE JEANTEX
                Pasillo = obj.Pasillo,
                EstrcAlmnje = obj.Estructura,
                Ubicacion = obj.Ubicacion,
                NumeroPieza = obj.NumeroPieza,
                EstatusPieza = obj.EstatusPieza,
                NumeroEtiqueta = obj.NumeroEtiqueta,
                IdPedido = obj.IdPedidoPicking,
                IdArticuloEtiqueta = obj.IdArticuloEtiqueta,
                IdUnidadCarga = obj.IdUnidadCarga,
                EstatusUnidadCarga = obj.EstatusUnidadCarga,
                IdUbicacion = obj.IdUbicacion,
                Cantidad = obj.Cantidad,
                CodBarra = obj.CodigoBarraItem,
                Barra = obj.CodigoBarraUbicacion
            };

            return output;

            throw new NotImplementedException();
        }
    }

}