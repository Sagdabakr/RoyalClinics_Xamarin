using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.ViewModels
{
    public class UpdateLocations_ViewModel
    {
        public async Task SaveLocations(ObservableCollection<Location> NewLocations)
        {
            foreach(Location location in NewLocations)
            {
                await App.Database.SaveLocationAsync(location);
            }
        }
        
        public async Task DeleteLocations()
        {            
           await App.Database.DeleteAllLocations();            
        }
        
        public async Task<int> LocationsCount()
        {            
           var c = await App.Database.GetLocationsAsync();
            return c.Count;
        }
    }
}
