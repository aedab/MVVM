using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MVVM_App.ViewModel;
using MVVM_App.Model;

namespace MVVM_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoPagina : ContentPage
    {
        ListadoViewModel vm;

        public ListadoPagina()
        {
            InitializeComponent();
            vm = new ListadoViewModel();
            BindingContext = vm;

            ListadoView.ItemSelected += Listdadoview_ItemSelected;  
        }

        private async void Listdadoview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var someone = e.SelectedItem as listado;
            if (someone == null) return;
            DetailPage dp = new DetailPage(someone,vm);
            await Navigation.PushModalAsync(dp);
            ListadoView.SelectedItem = null;
        }
    }
}