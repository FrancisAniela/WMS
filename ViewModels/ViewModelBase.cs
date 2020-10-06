using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Xamarin.Forms;


namespace CellariumAndroid.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static INavigation navigation;
        public static HttpClient Client = new HttpClient();
        public static BoxView Opaque = new BoxView();
        public static ActivityIndicator defaultActivityIndicator = new ActivityIndicator();

        public BaseViewModel()
        {
        }


        public static INavigation Navigation { get => navigation; set => navigation = value; }


        public virtual void HHTriggerEvent(bool pressed)
        {
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
