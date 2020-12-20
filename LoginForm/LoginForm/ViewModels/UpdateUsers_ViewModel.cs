using System;
using System.Collections.Generic;
using System.Text;
using LoginForm.Models;
using LoginForm.Data;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace LoginForm.ViewModels
{
    public class UpdateUsers_ViewModel
    {
        public async Task SaveNewUsers(ObservableCollection<User> NewUsers)
        {
            foreach(User _user in NewUsers)
            {                
               var x= await App.Database.SaveUserAsync(_user);
                _user.ID = _user.SqlliteID;
                var y = await App.Database.SaveUserAsync(_user);
            }
        }
        
       
    }
}
