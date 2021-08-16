using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using MVVM_App.Model;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using MVVM_App.Services;

namespace MVVM_App.ViewModel
{
    public class ListadoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool busy;

        public ObservableCollection<listado> PersonalList { get; set; }

        public Command RecoveryList { get; set; }

        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                RecoveryList.ChangeCanExecute();
            }
        }

        public async Task GetLista()
        {
            if (IsBusy) return;

            Exception error = null;
            try
            {
                IsBusy = true; //list request
                var service = DependencyService.Get<AzureService>();
                var items = await service.GetListado();

                PersonalList.Clear();
                foreach(var item in items)
                {
                    PersonalList.Add(item);
                }
            }
            catch(Exception ex)
            {
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ListadoViewModel()
        {
            RecoveryList = new Command(
                async() => await GetLista(), () => !IsBusy);
            PersonalList = new ObservableCollection<listado>();
        }

        public async Task UpdateL(listado who)
        {
            var service = DependencyService.Get<AzureService>();
            await service.UpdateList(who);
            await GetLista();
        }
    
    }
}
