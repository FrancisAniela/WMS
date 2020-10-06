using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CellariumAndroid.Services;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CellariumAndroid.ViewModels
{
    public class PrinterViewModel
    {



        public PrinterServices PrinterServicesProvider 
        {
            get;
            set;
        }



        public PrinterViewModel()
        {
            PrinterServicesProvider = new PrinterServices();
        }



        public void Init()
        {
            

            Console.WriteLine( PrinterServicesProvider.ToString() );



            var impresora = PrinterServicesProvider.CurrentZebraPrinter;
            impresora.CommunicationManager.Open();
            impresora.GraphicsManager.GetImage(Resource.Drawable.pallet.ToString());

            

            impresora.Calibrate();


        }







    }




}