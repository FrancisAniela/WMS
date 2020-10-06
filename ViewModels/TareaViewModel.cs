using CellariumAndroid.Helpers;
using CellariumAndroid.Models;
using CellariumAndroid.Views;
using CellariumAndroid.Views.Recepcion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CellariumAndroid.ViewModels
{
	class TareaViewModel : BaseViewModel
	{

		public static DespachoViewModel despachoCunas;
		public static EsperaPortalViewModel portal;
		public static DespachoArticuloViewModel articuloDespacho;
		public static OrdenEntradaViewModel ordenEntrada;

		public TareaViewModel()
		{

		}

		public static async Task ConsultarColaDeTareas(BoxView Opaque, ActivityIndicator defaultActivityIndicator, int Tipo = 0, [CallerMemberName] string MethodName = null)
		{
			HttpClient client = new HttpClient();
			Tarea TareaActual;//= new Tarea();

			string query = ("EXEC [Procesos].[usp_ConsultarColaDeTareas] " +
				"  @Usuario = " + UserAct.Usuario_id +
				", @Tipo = " + Tipo );

			var postData = new List<KeyValuePair<string, string>>();
			client.Timeout = TimeSpan.FromSeconds(10);
			postData.Add(new KeyValuePair<string, string>("server", Server.ServerDB));
			postData.Add(new KeyValuePair<string, string>("database", Server.Database));
			postData.Add(new KeyValuePair<string, string>("usernamesql", Server.UsernameDB));
			postData.Add(new KeyValuePair<string, string>("passwordsql", Server.PasswordDB));
			postData.Add(new KeyValuePair<string, string>("query", query));
			var content = new FormUrlEncodedContent(postData);

			try
			{
				Opaque.IsVisible = true;
				defaultActivityIndicator.IsRunning = true;

				var response = await client.PostAsync("http://" + Server.IpAddress + "/Cellarium/getdata.php", content);
				var jsonresult = response.Content.ReadAsStringAsync().Result;

				try
				{
					//Siempre devolverá una sola tarea a la vez para todos los procesos, 
					//A Excepción del proceso de Almacenaje

					if (jsonresult.Split(",{").Length > 1)
					{
						var TaskList = JsonConvert.DeserializeObject<List<Tarea>>(jsonresult);
						TareaActual = TaskList.FirstOrDefault();
					}

					else
					{
						TareaActual = JsonConvert.DeserializeObject<Tarea>(jsonresult);
					}

					if ( TareaActual == null )
					{
						defaultActivityIndicator.IsRunning = false;
						Opaque.IsVisible = false;
						ErrorWarningModal error = new ErrorWarningModal("No hay tareas en la cola en estos momentos");
						await Navigation.PushModalAsync(error);
					}

					else if ( TareaActual != null )
					{
						switch (TareaActual.Tipo)
						{
							case 0:
								//Inventario
									defaultActivityIndicator.IsRunning = false;
								Opaque.IsVisible = false;
								TomaInventarioViewModel.TareaTomaInventario = TareaActual;
								TomaInventarioViewModel.ObtenerUbicaciones(EmpresaSucursalAct.Almacen);
								break;

							case 1:
								//Picking
									defaultActivityIndicator.IsRunning = false;
								Opaque.IsVisible = false;
								string proceso = "picking";
								await Shell.Current.GoToAsync($"//MenuPrincipal/IdentificarUnidadCarga?proceso={proceso}&objetojson={jsonresult}");
								break;

							case 2:

								break;

							case 3:
								PaseProduccion produccion = new PaseProduccion();
								await Navigation.PushModalAsync(produccion);
								await Shell.Current.GoToAsync("/PaseProduccion");
								break;

							case 4:

								break;

							case 5:
								//Almacenaje
									defaultActivityIndicator.IsRunning = false;
								Opaque.IsVisible = false;

								if ( MethodName.Equals("GetTareaAlmacenaje") )
								{
									if (jsonresult.Split(",{").Length == 1)
									{
										AlmacenajeViewModel.TareasAlmacenaje = new List<Tarea>();
										AlmacenajeViewModel.TareasAlmacenaje.Add(TareaActual);
									}

									else
									{
										AlmacenajeViewModel.TareasAlmacenaje = JsonConvert.DeserializeObject<List<Tarea>>(jsonresult);
									}
								}

								break;
						}
					}
				}


				catch (Exception ex)
				{
					var s = ex.Message;
					defaultActivityIndicator.IsRunning = false;
					Opaque.IsVisible = false;
					ErrorWarningModal error = new ErrorWarningModal("Ocurrió un error al consultar la cola de tareas");
					await Navigation.PushModalAsync(error);
				}

			}

			catch (HttpRequestException ex)
			{
				var s = ex.Message;
				defaultActivityIndicator.IsRunning = false;
				Opaque.IsVisible = false;
				ErrorWarningModal error = new ErrorWarningModal("Ocurrió un error al tratar conectarse con el servidor");
				await Navigation.PushModalAsync(error);
			}

			content.Dispose();
			client.Dispose();
		}


		public static async Task UpdateColaDeTareas(BoxView Opaque, ActivityIndicator defaultActivityIndicator, Tarea TareaActual)
		{
			HttpClient client = new HttpClient();
			string query = ( "EXECUTE [Procesos].[usp_UpdateColaDeTareas] " +
				"  @Tipo = " + TareaActual.Tipo +
				", @IdTarea = " + TareaActual.IdTarea +
				", @IdTareaExterna = " + TareaActual.IdTareaExterna +
				", @UsuarioActual = " + TareaActual.UsuarioActual +
				", @UsuarioAsignado = " + TareaActual.UsuarioAsignado +
				", @Estatus = " + TareaActual.Estatus +
				", @Progreso = " + Manejador.ManejadorProgreso(TareaActual.Progreso) );

			var postData = new List<KeyValuePair<string, string>>();
			client.Timeout = TimeSpan.FromSeconds(10);
			postData.Add(new KeyValuePair<string, string>("server", Server.ServerDB));
			postData.Add(new KeyValuePair<string, string>("database", Server.Database));
			postData.Add(new KeyValuePair<string, string>("usernamesql", Server.UsernameDB));
			postData.Add(new KeyValuePair<string, string>("passwordsql", Server.PasswordDB));
			postData.Add(new KeyValuePair<string, string>("query", query));
			var content = new FormUrlEncodedContent(postData);

			try
			{
				Opaque.IsVisible = true;
				defaultActivityIndicator.IsRunning = true;

				var response = await client.PostAsync("http://" + Server.IpAddress + "/Cellarium/getdata.php", content);
				var jsonresult = response.Content.ReadAsStringAsync().Result;


				try
				{
					if (jsonresult.Split(",{").Length > 1)
					{
						var TaskList = JsonConvert.DeserializeObject<List<Tarea>>(jsonresult);
						TareaActual = TaskList.FirstOrDefault();
					}

					else
					{
						TareaActual = JsonConvert.DeserializeObject<Tarea>(jsonresult);
					}


					if ( (jsonresult.Length == 0) || (TareaActual == null) )
					{
						defaultActivityIndicator.IsRunning = false;
						Opaque.IsVisible = false;
						ErrorWarningModal error = new ErrorWarningModal("Ocurrió un error al consultar la cola de tareas");
						await Navigation.PushModalAsync(error);

					}

					else if ( (jsonresult.Length != 0) || (TareaActual != null) )
					{
						switch (TareaActual.Tipo)
						{
							case 0:
								//Inventario
								TomaInventarioViewModel.TareaTomaInventario = TareaActual;
								break;
							case 1:
								//Picking
								PickingViewModel.TareaPicking = TareaActual;
								break;
							case 2:

								break;
							case 3:

								break;
							case 4:

								break;
							case 5:
								//Almacenaje
									if (jsonresult.Split(",{").Length == 1)
									{
										AlmacenajeViewModel.TareasAlmacenaje = new List<Tarea>();
										AlmacenajeViewModel.TareasAlmacenaje.Add(TareaActual);
									}

									else
									{
										AlmacenajeViewModel.TareasAlmacenaje = JsonConvert.DeserializeObject<List<Tarea>>(jsonresult);
									}

								break;
						}


					}



				}


				catch (Exception ex)
				{


				}


			}



			catch (HttpRequestException ex)
			{


			}

			Opaque.IsVisible = false;
			defaultActivityIndicator.IsRunning = false;

			content.Dispose();
			client.Dispose();
		}


		/// <summary>
		// Redirecciona a la vista correspondiente segun el submodulo de despacho en curso 
		/// </summary>
		/// <returns></returns>	
		public static async Task VerificarSubModulo(SubModulo subModulo)
		{
			ErrorWarningModal error;

			if (SubModulo.IdentificacionCuna == subModulo.idSubModulo)
			{
				despachoCunas = new DespachoViewModel();
				IdentificarCunaDespacho.viewmodel = despachoCunas;
				await LanzarIdentificacionCuna();
			}

			if (SubModulo.PasePorPortal == subModulo.idSubModulo)
				await LanzarPortal();

			if (SubModulo.DespachoCunaPedidos == subModulo.idSubModulo)
				await LanzarDespachoCunaPedidos();



			if (SubModulo.Auditoria == subModulo.idSubModulo)
			{

			}
			if (SubModulo.FinalizarPedido == subModulo.idSubModulo)
			{
				DespachoViewModel.Navigation = Navigation;
				await Shell.Current.GoToAsync("//MenuPrincipal/FinalizarDespacho");
			}

			if (SubModulo.SinTareaDespacho == subModulo.idSubModulo)
			{
				error = new ErrorWarningModal("No hay tareas en la cola en estos momentos");
				await Navigation.PushModalAsync(error);
			}
		}

		public static async Task LanzarDespachoCunaPedidos()
		{
			DespachoArticuloViewModel.Navigation = Navigation;
			if (PedidoCunaAct.cantidadPedidosAsociados > 1)
			{
				articuloDespacho = new DespachoArticuloViewModel();
				ArticuloCunaDespacho.viewmodel = articuloDespacho;
				await articuloDespacho.SetPiezasAsociadasCuna();
				await Shell.Current.GoToAsync("//MenuPrincipal/ArticuloDespacho");
			}
			else
				await Shell.Current.GoToAsync("//MenuPrincipal/DespachoArticulo");

		}

		public static async Task LanzarPortal()
		{
			portal = new EsperaPortalViewModel(PedidoCunaAct.id);
			EsperaPortal.viewModel = portal;
			await Shell.Current.GoToAsync("//MenuPrincipal/Portal");
		}

		public static async Task LanzarIdentificacionCuna()
		{
			bool lanzarVista = await despachoCunas.pedidoUnidadCarga();

			if (lanzarVista)
				await Shell.Current.GoToAsync("//MenuPrincipal/Despacho_Cuna");
		}

		/// <summary>
		//Consulta el progreso actual de una tarea de despacho
		/// </summary>
		/// <returns></returns>
		public static async Task SetSubModulo(BoxView Opaque, ActivityIndicator defaultActivityIndicator)
		{
			HttpClient client = new HttpClient();
			SubModulo subModulo = new SubModulo { idSubModulo = 6 };
			string query = String.Format("EXEC [Procesos].[usp_ConsultaSubModuloDespacho] " + "@idAlmacen={0} , @Usuario_id = {1}" +
				", @tipo = {2} "
				, EmpresaSucursalAct.Almacen, UserAct.Usuario_id, 2);
			var postData = new List<KeyValuePair<string, string>>();
			client.Timeout = TimeSpan.FromSeconds(4);
			postData.Add(new KeyValuePair<string, string>("server", Server.ServerDB));
			postData.Add(new KeyValuePair<string, string>("database", Server.Database));
			postData.Add(new KeyValuePair<string, string>("usernamesql", Server.UsernameDB));
			postData.Add(new KeyValuePair<string, string>("passwordsql", Server.PasswordDB));
			postData.Add(new KeyValuePair<string, string>("query", query));
			var content = new FormUrlEncodedContent(postData);
			try
			{
				Opaque.IsVisible = true;
				defaultActivityIndicator.IsRunning = true;
				var response = await client.PostAsync("http://" + Server.IpAddress + "/Cellarium/getdata.php", content);
				var jsonresult = response.Content.ReadAsStringAsync().Result;

				if (jsonresult.ToString() != "")
				{
					subModulo = JsonConvert.DeserializeObject<SubModulo>(jsonresult);
					PedidoCunaAct.id = subModulo.idUnidadCarga;
					PedidoCunaAct.idPedido = subModulo.IdCabeceraTarea;
					PedidoCunaAct.numeroDocumento = subModulo.numeroDocumento;
					PedidoCunaAct.codigoBarra = subModulo.codigoBarraUnidadCarga;
					PedidoCunaAct.cantidadPedidosAsociados = subModulo.pedidoAsociado;
				}
				Opaque.IsVisible = false;
				defaultActivityIndicator.IsRunning = false;
				await VerificarSubModulo(subModulo);
			}
			catch (HttpRequestException exc)
			{
				defaultActivityIndicator.IsRunning = false;
				Opaque.IsVisible = false;
				Console.WriteLine(exc.Message);
				ErrorWarningModal error = new ErrorWarningModal("Error en la conexión a la base de datos, revise la configuración de credenciales del servidor");
				await Navigation.PushModalAsync(error);
			}
			catch (Exception e)
			{
				defaultActivityIndicator.IsRunning = false;
				Opaque.IsVisible = false;
				ErrorWarningModal error = new ErrorWarningModal("Error en el proceso actual");
				await Navigation.PushModalAsync(error);
			}
		}


		public static async Task SetSubModuloRecepecion(BoxView Opaque, ActivityIndicator defaultActivityIndicator)
		{
			HttpClient client = new HttpClient();
			SubModulo subModulo = new SubModulo { idSubModulo = 6 };
			string query = "EXEC [Procesos].[usp_ConsultaSubModuloOrdenEntradaRecepcion] "+
							$"@idAlmacen={EmpresaSucursalAct.Almacen} , @Usuario_id = {UserAct.Usuario_id}" +
							$", @tipo = {6} ";

			var postData = new List<KeyValuePair<string, string>>();
			client.Timeout = TimeSpan.FromSeconds(4);
			postData.Add(new KeyValuePair<string, string>("server", Server.ServerDB));
			postData.Add(new KeyValuePair<string, string>("database", Server.Database));
			postData.Add(new KeyValuePair<string, string>("usernamesql", Server.UsernameDB));
			postData.Add(new KeyValuePair<string, string>("passwordsql", Server.PasswordDB));
			postData.Add(new KeyValuePair<string, string>("query", query));
			var content = new FormUrlEncodedContent(postData);
			try
			{
				Opaque.IsVisible = true;
				defaultActivityIndicator.IsRunning = true;
				var response = await client.PostAsync("http://" + Server.IpAddress + "/Cellarium/getdata.php", content);
				var jsonresult = response.Content.ReadAsStringAsync().Result;
				if (jsonresult.ToString() == "")
				{
					subModulo = new SubModulo()
					{
						IdCabeceraTarea = 0,
						numeroDocumento = "", 
						idSubModulo = 1
					};
				}
				else if (jsonresult.Split(",{").Length > 1)
				{
					subModulo = JsonConvert.DeserializeObject<SubModulo>(jsonresult);
					
				}
				await RecepcionSubModulo(subModulo);
				Opaque.IsVisible = false;
				defaultActivityIndicator.IsRunning = false;
			}
			catch (HttpRequestException exc)
			{
				defaultActivityIndicator.IsRunning = false;
				Opaque.IsVisible = false;
				Console.WriteLine(exc.Message);
				ErrorWarningModal error = new ErrorWarningModal("Error en la conexión a la base de datos, revise la configuración de credenciales del servidor");
				await Navigation.PushModalAsync(error);
			}
			catch (Exception e)
			{
				defaultActivityIndicator.IsRunning = false;
				Opaque.IsVisible = false;
				ErrorWarningModal error = new ErrorWarningModal("Error en el proceso actual");
				await Navigation.PushModalAsync(error);
			}
		}


		public static async Task RecepcionSubModulo(SubModulo subModulo) 
		{
			ordenEntrada = new OrdenEntradaViewModel();
			bool existenOrdenes = await ordenEntrada.SetOrdenesRecepcion(subModulo.numeroDocumento, subModulo.IdCabeceraTarea);

			if (existenOrdenes)
			{
				OrdenesDisponibles.viewmodel = ordenEntrada;

				switch(subModulo.idSubModulo) 
				{					
					case 1:
						OrdenesDisponibles.viewmodel = ordenEntrada;
						await Shell.Current.GoToAsync("/OC");
						break;
					case 2:
						DetalleOrdenRecepcion.viewmodel = ordenEntrada;
						await Shell.Current.GoToAsync("/DetalleOrdenRecepcion");
						break;
				}
			}
			else
			{
				ErrorWarningModal error = new ErrorWarningModal("No existen tareas disponibles");
				await Navigation.PushModalAsync(error);
			}

		}

		public async Task<bool> InsertColaTareaDetalle(Tarea tarea) 
		{
				HttpClient client = new HttpClient();
				string query = "INSERT INTO [Procesos].[ColaTareas_Detalle]"
								+"([idColaTareas],[progreso],[submodulo],[idUnidadCarga])"
								+"VALUES" +
								$"({tarea.IdTarea},{tarea.Progreso},{tarea.IdSubModulo},{tarea.IdUnidadCarga})";
				var postData = new List<KeyValuePair<string, string>>();
				client.Timeout = TimeSpan.FromSeconds(4);
				postData.Add(new KeyValuePair<string, string>("server", Server.ServerDB));
				postData.Add(new KeyValuePair<string, string>("database", Server.Database));
				postData.Add(new KeyValuePair<string, string>("usernamesql", Server.UsernameDB));
				postData.Add(new KeyValuePair<string, string>("passwordsql", Server.PasswordDB));
				postData.Add(new KeyValuePair<string, string>("query", query));
				var content = new FormUrlEncodedContent(postData);
			try
			{
				var response = await client.PostAsync("http://" + Server.IpAddress + "/Cellarium/getdata.php", content);
				var jsonresult = response.Content.ReadAsStringAsync().Result;
				var jsonObject = JsonConvert.SerializeObject(jsonresult);

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}

		}

		public async Task<bool> UpdateColaTarea(int modulo, int idTarea, int progreso, int idSubmodulo, int idUnidadCarga)
		{
			HttpClient client = new HttpClient();
			string query = "UPDATE [Procesos].[ColaTareas_Detalle]" +
								"SET" +
									$",[progreso] = {progreso}"+
									$",[submodulo] = {idSubmodulo}"+
									$",[idUnidadCarga] = {idUnidadCarga}"+
									$"WHERE idColaTareas = {idTarea}";
									
			var postData = new List<KeyValuePair<string, string>>();
			client.Timeout = TimeSpan.FromSeconds(4);
			postData.Add(new KeyValuePair<string, string>("server", Server.ServerDB));
			postData.Add(new KeyValuePair<string, string>("database", Server.Database));
			postData.Add(new KeyValuePair<string, string>("usernamesql", Server.UsernameDB));
			postData.Add(new KeyValuePair<string, string>("passwordsql", Server.PasswordDB));
			postData.Add(new KeyValuePair<string, string>("query", query));
			var content = new FormUrlEncodedContent(postData);
			try
			{
				var response = await client.PostAsync("http://" + Server.IpAddress + "/Cellarium/getdata.php", content);
				var jsonresult = response.Content.ReadAsStringAsync().Result;
				var jsonObject = JsonConvert.SerializeObject(jsonresult);

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

	}
}
