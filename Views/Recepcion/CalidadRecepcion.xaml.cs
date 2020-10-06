using CellariumAndroid.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CellariumAndroid.Helpers;

namespace CellariumAndroid.Views.Recepcion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalidadRecepcion : ContentPage
    {
        public static OrdenEntradaViewModel viewmodel;

        public CalidadRecepcion()
        {
            InitializeComponent();
            BindingContext = viewmodel;
            this.Load();
        }

        private void Load() 
        {
            EntCantidadDefectuosa.Text = viewmodel.CantidadDefectuosa;
            EntCantidadRecibir.Text = viewmodel.CantidadTotal;
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

        }


        private  async void Validar_Clicked(object sender, EventArgs e)
        {
            if (EntCantidadRecibir.Text.IsNullOrEmpty())
                return;

            double CantidadRecibir = Convert.ToDouble(EntCantidadRecibir.Text);
            double CantidadDefectuosa = 0;

            if(!EntCantidadDefectuosa.Text.IsNullOrEmpty())
                CantidadDefectuosa = Convert.ToDouble(EntCantidadDefectuosa.Text);

            string mensaje = await viewmodel.CantidadARecibirPorLinea(CantidadRecibir, CantidadDefectuosa);

            if(mensaje !=null) 
            {
                ErrorWarningModal error  = new ErrorWarningModal(mensaje);
                await Navigation.PushModalAsync(error);
            }

        }

        private void EntOrdenRecepcion_Completed(object sender, EventArgs e)
        {

        }

        private void EntCantidadDefectuosa_Completed(object sender, EventArgs e)
        {

        }

        private void EntCantidadRecibir_Completed(object sender, EventArgs e)
        {

        }
    }
}