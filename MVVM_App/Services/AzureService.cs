using System;
using System.Collections.Generic;
using System.Text;
using MVVM_App.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using MVVM_App.Services;

[assembly: Dependency(typeof(AzureService))]

namespace MVVM_App.Services
{
    class AzureService
    {

        public MobileServiceClient MobileService { set; get; } = null;
        IMobileServiceSyncTable<listado> table;
        
        public async Task Initialize()
        {
            if (MobileService?.SyncContext?.IsInitialized ?? false)
                return;
            MobileService = new MobileServiceClient("http://eucmuxamarin.azurewebsites.net/");

            var path = "syncstore2.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);
            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<listado>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            table = MobileService.GetSyncTable<listado>();
        }

        public async Task<IEnumerable<listado>> GetListado()
        {
            await Initialize();
            await Synclistado();
            return await table.OrderBy(s => s.Name).ToEnumerableAsync();
        }
        
        public async Task Synclistado()
        {
            try
            {
                await MobileService.SyncContext.PushAsync();
                await table.PullAsync("listado", table.CreateQuery());
            }
            catch(Exception ex)
            {
                Debug.WriteLine("I can't synchronize the list. Working offline " + ex);
            }
        }

        public async Task UpdateList(listado who)
        {
            await Initialize();
            await table.UpdateAsync(who);
            await Synclistado();
        }

    }
}
