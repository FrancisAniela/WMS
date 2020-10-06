using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CellariumAndroid.Models
{
    public class Inventario
    {

        //USP: [Procesos].[usp_Select_Estante_Inventario],
        //Params: @IdCabInventario => Id(int), Codigo(string), CodBarra(string), Articulo(string), articulo_id(int), Almacen(string), Zona(string)
        //        Seccion(string), Pasillo(string), EstrcAlmnje(string), Ubicacion(string), Barra(string), CantidadContada(decimal), Lineas(int), Registros(int)

        public int Id { get; set; }

        public string Codigo { get; set; }  //Codigo de artículo (Visible)

        public string CodBarra { get; set; }//Codificación u alias del código del articulo, transformado a codigo de barra (Este codigo no es visible, usos de validacion). 

        public string Articulo { get; set; }

        public int articulo_id { get; set; }

        public string Almacen { get; set; }

        public string Zona { get; set; }

        public string Seccion { get; set; }

        public string Pasillo { get; set; }

        public string EstrcAlmnje { get; set; }

        public string Nivel { get; set; }

        public string Ubicacion { get; set; }

        public string CodUbi { get; set; } //Codigo de ubiacion (Visible)
        
        public string Barra { get; set; } //Codificación u alias del codigo de la ubicación, transformado a codigo de barra (Este codigo no es visible, usos de validacion). 

        public int CantidadContada { get; set; }

        public bool ControlCantidad { get; set; }

        public int Lineas { get; set; }

        public int Registros { get; set; }

        public int ArtEtq_id { get; set; }


    }

}
