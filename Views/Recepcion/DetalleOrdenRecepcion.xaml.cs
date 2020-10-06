using CellariumAndroid.Models;
using CellariumAndroid.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CellariumAndroid.Views.Recepcion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleOrdenRecepcion : ContentPage
    {
        public static OrdenEntradaViewModel viewmodel;
        private bool edit = false;
        public DetalleOrdenRecepcion()
        {
            InitializeComponent();
            BindingContext = viewmodel;
        }

        internal void OnResume()
        {

        }

        protected override void OnAppearing()
        {

            base.OnAppearing();


        }


        private void BtnMostrarOrdenes_Clicked(object sender, EventArgs e)
        {
            edit = !edit;
            if (edit)
            {
                BtnMostrarOrdenes.Source = "recepcionEditWhite.png";
                BtnMostrarOrdenes.BackgroundColor = Xamarin.Forms.Color.Transparent;
            }
            else
            {
                FrameEdit.BorderColor = Xamarin.Forms.Color.Transparent;
                BtnMostrarOrdenes.Source = "recepcionEdit.png";
                BtnMostrarOrdenes.BackgroundColor = Xamarin.Forms.Color.White;
            }
        }

        private async void ListViewOrdenesActivas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                OrdenEntrada orden = e.SelectedItem as OrdenEntrada;

                viewmodel.codigoArticulo = orden.CodigoArticulo;
                viewmodel.OrdenEntrada = orden;
                CalidadRecepcion.viewmodel = viewmodel;
                await Shell.Current.GoToAsync("/CalidadRecepcion");
                ((ListView)sender).SelectedItem = null;
            }
        }

        private async void BtnRechazar_Clicked(object sender, EventArgs e)
        {
           await  this.Recibir(true);
        }

        private async void BtnAprobar_Clicked(object sender, EventArgs e)
        {
          await  this.Recibir(false);
        }

        private async Task Recibir(bool EsRechazada) 
        {

            string mensaje;

            mensaje = viewmodel.ValidarRecepcion();

            if (mensaje == null)
            {
                mensaje = "Orden recibida";
                mensaje = await viewmodel.RecibirRechazarOR(viewmodel.AllItems, EsRechazada);
            }

           ErrorWarningModal error = new ErrorWarningModal(mensaje);
           await Navigation.PushModalAsync(error);
            
        }
    }
}