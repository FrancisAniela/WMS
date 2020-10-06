using Android.Net.Wifi.Aware;
using CellariumAndroid.Models;
using CellariumAndroid.Views;
using CellariumAndroid.Views.Recepcion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CellariumAndroid.ViewModels
{
    public class OrdenEntradaViewModel : BaseViewModel
    {
        public ObservableCollection<OrdenEntrada> _allItems;
        static List<OrdenEntrada> ordenesEntrada = null;
        private string _codigo;
        public bool _activatyIndicatorAvaible;
        public bool _opaque = false;
        public string query = "";
        public string codigoArticulo;
        public bool calidad = false;

        public OrdenEntrada OrdenEntrada { get; set; }
        public static OrdenEntrada OE { get; set; }
        public ObservableCollection<OrdenEntrada> AllItems { get => _allItems; set { _allItems = value; OnPropertyChanged(); } }

        public string Codigo { get => _codigo; set { _codigo = value; OnPropertyChanged(); } }

        public string CodigoArticulo { get => codigoArticulo; set { codigoArticulo = value; OnPropertyChanged(); } }

        public bool ActivatyIndicatorAvaible { get => _activatyIndicatorAvaible; set { _activatyIndicatorAvaible = value; OnPropertyChanged(); } }

        public bool IsOpaque { get => _opaque; set { _opaque = value; OnPropertyChanged(); } }

        public bool Calidad { get => calidad; set { calidad = value; OnPropertyChanged(); } }

        public string CantidadTotal { get => OrdenEntrada.CantidadRecibir.ToString(); set { OnPropertyChanged(); } }

        public string CantidadDefectuosa { get => OrdenEntrada.CantidadDefectuosa.ToString(); set { OnPropertyChanged(); } }


        public OrdenEntradaViewModel()
        {
            _allItems = new ObservableCollection<OrdenEntrada>();
            _opaque = false;
            _activatyIndicatorAvaible = false;
        }

        /// <summary>
        /// La consulta de orden de Entrada, devuelve el detalle y la cabecera de la orden, por lo que 
        /// debe filtrarse por codigo las ordenes a mostrar en el listView
        /// </summary>
        public void UpdateList()
        {
            _allItems.Clear();
            var ordenesCabecera = ordenesEntrada.Distinct().OrderBy(x => x.Codigo).ToList();
            for (int i = 0; i < ordenesEntrada.Count; i++)
            {
                if (_allItems.FirstOrDefault<OrdenEntrada>(x => x.Codigo == ordenesEntrada[i].Codigo) == null)
                    _allItems.Add(new OrdenEntrada()
                    {
                        Codigo = ordenesEntrada[i].Codigo,
                        IdOrdenEntradaCabecera = ordenesEntrada[i].IdOrdenEntradaCabecera,
                        CantidadRecibir = ordenesEntrada[i].CantidadRecibir,
                        CodigoArticulo = ordenesEntrada[i].CodigoArticulo,
                        CantidadOrdenada = ordenesEntrada[i].CantidadOrdenada,
                        CantidadRechazada = ordenesEntrada[i].CantidadRechazada,
                        IdArticulo = ordenesEntrada[i].IdArticulo,
                        CantidadInspeccionada = ordenesEntrada[i].CantidadInspeccionada,
                        IdColaTareas = ordenesEntrada[i].IdColaTareas,
                        Calidad = ordenesEntrada[i].Calidad,
                        CantidadDefectuosa = ordenesEntrada[i].CantidadDefectuosa,
                        CantidadRecibida = ordenesEntrada[i].CantidadRecibida,
                        Linea = ordenesEntrada[i].Linea,
                        IdOrdenEntradaDetalle = ordenesEntrada[i].IdOrdenEntradaDetalle,

                    });
            }
            //Ajusta el tamano del listView en funcion a la cantidad de filas que la componen
        }

        public async Task<string> CantidadARecibirPorLinea(double CantidadTotal, double CantidadDefectuosa)
        {
            var found = _allItems.FirstOrDefault(x => x.IdOrdenEntradaDetalle == OrdenEntrada.IdOrdenEntradaDetalle);
            int index = _allItems.IndexOf(found);


            string mensaje = "Linea de la orden no encontrada";
            if (found != null)
            {

                mensaje = ValidarCantidadRecibir(CantidadTotal, CantidadDefectuosa);
                if (mensaje == null)
                {

                    AllItems[index].CantidadRecibir = CantidadTotal;
                    AllItems[index].CantidadDefectuosa = CantidadDefectuosa;
                    OrdenEntradaViewModel.OE = AllItems[index];

                    ObservableCollection<OrdenEntrada> OE = new ObservableCollection<OrdenEntrada>();

                    foreach (OrdenEntrada oe in AllItems)
                    {
                        OE.Add(new OrdenEntrada()
                        {
                            CantidadRecibir = oe.CantidadRecibir,
                            CodigoArticulo = oe.CodigoArticulo,
                            CantidadOrdenada = oe.CantidadOrdenada,
                            CantidadRechazada = oe.CantidadRechazada,
                            IdArticulo = oe.IdArticulo,
                            CantidadInspeccionada = oe.CantidadInspeccionada,
                            IdOrdenEntradaCabecera = oe.IdOrdenEntradaCabecera,
                            IdColaTareas = oe.IdColaTareas,
                            Calidad = oe.Calidad,
                            CantidadDefectuosa = oe.CantidadDefectuosa,
                            CantidadRecibida = oe.CantidadRecibida,
                            Codigo = oe.Codigo,
                            Linea = oe.Linea,
                            IdOrdenEntradaDetalle = oe.IdOrdenEntradaDetalle
                        });

                    }

                    AllItems = OE;

                    await Shell.Current.Navigation.PopAsync();
                }

            }
            return mensaje;
        }

        public string ValidarRecepcion()
        {
            string mensaje;
            var found = _allItems.Select(x => x.CantidadRecibir > 0).ToList();

            if (found.Count == 0)
                mensaje = "Recepción vacia, debe recibir al menos un artículo de la lista";


            return null;

        }

        public string ValidarCantidadRecibir(double CantidadTotal, double CantidadDefectuosa)
        {
            if ((CantidadTotal + OrdenEntrada.CantidadRecibida) > OrdenEntrada.CantidadOrdenada)
                return "La cantidad a recibir es mayor a la cantidad ordena";
            if (CantidadTotal < CantidadDefectuosa)
                return "La cantidad defectuosa debe ser menor a la cantidad total";
            return null;

        }

        public int ExisteOrden(string Codigo)
        {
            var found = _allItems.FirstOrDefault(x => x.codigo == Codigo);
            int id = 0;
            if (found != null)
                id = found.IdOrdenEntradaCabecera;

            return id;
        }

        public async Task<bool> ValidarOrdenEntrada(int IdCabeceraOR)
        {
            var found = _allItems.FirstOrDefault(x => x.IdOrdenEntradaCabecera == IdCabeceraOR);
            bool existe = false;
            if (found != null)
            {
                _opaque = true;
                _activatyIndicatorAvaible = true;
                existe = true;
                UpdateListDetalle(IdCabeceraOR);
                DetalleOrdenRecepcion.viewmodel = this;
                _opaque = false;
                _activatyIndicatorAvaible = false;
                await ActualizarEstadoTarea(EstadoColaTarea.EnProceso, found.IdColaTareas);
                await Shell.Current.GoToAsync("//MenuPrincipal/DetalleOrdenRecepcion");
            }
            else
            {
                ErrorWarningModal error = new ErrorWarningModal("La orden no existe");
                await Navigation.PushModalAsync(error);
            }
            return existe;
        }

        private void UpdateListDetalle(int IdCabeceraOR)
        {
            _allItems.Clear();
            var ordenesCabecera = ordenesEntrada.Where(x => x.IdOrdenEntradaCabecera == IdCabeceraOR).OrderBy(x => x.Linea).ToList();
            _codigo = ordenesCabecera[0].Codigo;
            for (int i = 0; i < ordenesCabecera.Count; i++)
            {
                _allItems.Add(new OrdenEntrada()
                {
                    Codigo = ordenesEntrada[i].Codigo,
                    Linea = ordenesEntrada[i].Linea,
                    CodigoArticulo = ordenesEntrada[i].CodigoArticulo,
                    IdOrdenEntradaCabecera = ordenesEntrada[i].IdOrdenEntradaCabecera,
                    CantidadOrdenada = ordenesEntrada[i].CantidadOrdenada,
                    CantidadInspeccionada = ordenesEntrada[i].CantidadInspeccionada,
                    CantidadRecibida = ordenesEntrada[i].CantidadRecibida,
                    IdOrdenEntradaDetalle = ordenesEntrada[i].IdOrdenEntradaDetalle,
                    IdColaTareas = ordenesEntrada[i].IdColaTareas,
                    IdArticulo = ordenesEntrada[i].IdArticulo
                });
            }
            //Ajusta el tamano del listView en funcion a la cantidad de filas que la componen
        }


        //Devuelve la tarea asignada 
        public async Task<bool> SetOrdenesRecepcion(string Codigo, int idcabecera)
        {
            HttpClient client = new HttpClient();
            List<OrdenEntrada> ordenEntrada = null;
            OrdenEntrada ordenEntradaU = null;
            string query = "EXEC [Procesos].[usp_ConsultaOrdenesEntradaRecepcionTarea] " +
                            $" @idCabecera = {idcabecera}, @idAlmacen = {EmpresaSucursalAct.Almacen}, @Codigo = '{Codigo}'"
                            + $" ,  @idUsuario = {UserAct.Usuario_id}";

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

                if (jsonresult.Split(",{").Length == 0)
                {
                    return false;
                }
                if (jsonresult.Split(",{").Length > 1)
                {
                    ordenEntrada = JsonConvert.DeserializeObject<OrdenEntrada[]>(jsonresult).ToList();
                }
                else
                {
                    ordenEntradaU = JsonConvert.DeserializeObject<OrdenEntrada>(jsonresult);
                    ordenEntrada = new List<OrdenEntrada>();
                    ordenEntrada.Add(new OrdenEntrada()
                    {
                        Calidad = ordenEntradaU.Calidad,
                        CantidadOrdenada = ordenEntradaU.CantidadOrdenada,
                        codigo = ordenEntradaU.Codigo,
                        CantidadInspeccionada = ordenEntradaU.CantidadInspeccionada,
                        CantidadRechazada = ordenEntradaU.CantidadRechazada,
                        CantidadRecibida = ordenEntradaU.CantidadRecibida,
                        CodigoArticulo = ordenEntradaU.CodigoArticulo,
                        IdArticulo = ordenEntradaU.IdArticulo,
                        IdColaTareas = ordenEntradaU.IdColaTareas,
                        IdOrdenEntradaCabecera = ordenEntradaU.IdOrdenEntradaCabecera,
                        IdOrdenEntradaDetalle = ordenEntradaU.IdOrdenEntradaDetalle
                    });
                }
                ordenesEntrada = ordenEntrada;
                UpdateList();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public async Task<string> RecibirRechazarOR(ObservableCollection<OrdenEntrada> ordenEntrada, bool esRechazada = false)
        {
            string mensaje = "Error al recibir orden de recepción";

            foreach (OrdenEntrada lineaOrden in ordenEntrada)
            {
                if (esRechazada)
                {
                    lineaOrden.CantidadRechazada = lineaOrden.CantidadOrdenada;
                }
                lineaOrden.Calidad = esRechazada;
            }

            bool esRecibida = await this.RecibirOR(ordenEntrada);

            if (esRecibida)
                mensaje = null;

            return mensaje;
        }

        //Se envia la orden de entrada actualizada con los datos de la recepcion
        public async Task<bool> RecibirOR(ObservableCollection<OrdenEntrada> ordenEntrada)
        {
            string jsonModel = JsonConvert.SerializeObject(ordenEntrada);

            HttpClient client = new HttpClient();
            string query = "EXEC [Procesos].[usp_OrdenRecepcionRecibir] " +
                            $"@idAlmacen = {EmpresaSucursalAct.Almacen}"
                            + $" ,  @idUsuario = {UserAct.Usuario_id} , @JsonModel = '{jsonModel}'";

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

        public static async Task ActualizarEstadoTarea(int sts, int id)
        {
            HttpClient client = new HttpClient();
            string query = $"UPDATE [Procesos].[ColaTareas]" +
                                $"SET sts = {sts}  WHERE idColaTareas = {id}";
                             
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}