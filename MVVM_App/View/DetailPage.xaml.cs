using MVVM_App.Model;
using MVVM_App.ViewModel;
using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVM_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        listado per;
        ListadoViewModel vm;

        public DetailPage(listado who, ListadoViewModel listVM)
        {
            InitializeComponent();
            per = who;
            vm = listVM;
            BindingContext = per;
        }

        private void ButtonSpeak_Clicked(object sender, EventArgs e)
        {
            CrossTextToSpeech.Current.Speak(this.per.Description);
        }

        private void ButtonWeb_Clicked(object sender, EventArgs e)
        {
            if (per.Website.StartsWith("http"))
                Device.OpenUri(new Uri(per.Website));
        }

        private async void ButtonRet_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            per.Title = EditTitle.Text;
            await vm.UpdateL(per);
        }
    }
}