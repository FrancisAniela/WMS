using CellariumAndroid.Models;
using CellariumAndroid.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CellariumAndroid.Helpers;

namespace CellariumAndroid.Views.Recepcion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdenesDisponibles : ContentPage
    {
        public static OrdenEntradaViewModel viewmodel;
        public OrdenesDisponibles()
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
            ContenedorListView.IsVisible = !ContenedorListView.IsVisible;
            LblOrdenesRecepcion.IsVisible = !LblOrdenesRecepcion.IsVisible;
        }

        private async void ListViewOrdenesActivas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                OrdenEntrada orden = e.SelectedItem as OrdenEntrada;
                await viewmodel.ValidarOrdenEntrada(orden.IdOrdenEntradaCabecera);
                ((ListView)sender).SelectedItem = null;
            }
        }

        private async void Validar_Clicked(object sender, EventArgs e)
        {
            if (EntCodigoOrdenRecepcion.Text.IsNullOrEmpty())
                return;

            await viewmodel.ValidarOrdenEntrada(viewmodel.ExisteOrden(EntCodigoOrdenRecepcion.Text));
        }

    }
}