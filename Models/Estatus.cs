using System;
using System.Collections.Generic;
using System.Text;

namespace CellariumAndroid.Models
{
	public class Estatus
	{
		/*SUGERENCIA PARA LA BASE DE DATOS: Compactar los estatus por grupo establecer una precedencia logica/numerica para cada sub conjunto de estatus*/


		//Recordar cambiar los campos donde se esté utilizando esta propiedad como comparacion lógica por su propiedad de estatus correspondiente
		public int EstatusActual { get; set; }

		public static int EstatusActualProceso{ get; set;}

		public static int Inactivo { get; } = 0;

		public static int Activo { get; } = 1;

		public static int Liberado { get; } = 2;

		public static int InactivoPorInventario { get; } = 3; //Pienso que bloqueado podría quedar mejor???

		public static int SinIniciar { get; } = 4;

		public static int Procesando { get; } = 5; //AKA en Progreso

		public static int Finalizado { get; } = 6;

		public static int SegundoConteo { get; } = 7;

		public static int Ajustes { get; } = 8;

		public static int Eliminado { get; } = 9;

		public static int Cola { get; } = 10;

		public static int ParcialmenteCompleto { get; } = 11;

		public static int PredespachoCompleto { get; } = 12;

		public static int DespachoHabilitado { get; } = 13;

		public static int ConfirmacionCompletada { get; } = 14;

		public static int ConfirmacionParcial { get; } = 15;

		public static int Cancelado { get; } = 16;

		public static int RecepcionParcial { get; } = 17;

		public static int RecepcionCompletada { get; } = 18;

		public static int Cerrado { get; } = 19;

		public static int SinInspeccion { get; } = 20;

		public static int InspeccionRechazada { get; } = 21;

		public static int Inspeccion { get; } = 22;

		public static int PorSincronizarERP { get; } = 23;

		public static int Produccion { get; } = 24;

		public static int Transito { get; } = 25;

		public static int Almacenado { get; } = 26; //Creo que se puede confundir con otros clases  o modelos, o campos. 
													//Cambio de despcripcion tal vez? Tenia Almacen. Cambie a Almacenado
		public static int Error { get; } = 27;

		public static int Ubicado { get; } = 28;

		public static int Planificado { get; } = 29;

		public static int EsperaRespuesta { get; } = 30;
		//El 30 y el 31 también los veo muy oscuros. Especialmente el 30 a nivel de BD.
		public static int LaboratorioDeCalidad { get; } = 31;

		public static int ProcesandoPicking { get; } = 32;

		public static int Predespacho { get; } = 33;        //El 12 tambien es bastante oscuro para este estatus

		//Se comieron el estatus 34

		public static int NoName { get; } = 35; //Creo que coincide con 13 y 2. Principalmente con 13?? Revisar porfa. Por eso no le puse nombre.

		public static int SinEspecificacion { get; } = 36;


		/* Me parece que de 36 en adelante son muy especificos para el proyecto actual y no se si de igual forma, se usarian en la aplicacion movil????    */


	}
}

