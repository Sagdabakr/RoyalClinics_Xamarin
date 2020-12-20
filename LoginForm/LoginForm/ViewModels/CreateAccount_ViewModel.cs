using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoginForm.ViewModels
{
    public class CreateAccount_ViewModel
    {      
        public async Task<User>  CreateAccount(User user)
        {
            var allUsers = await App.Database.GetUsersAsync();
            foreach (User u in allUsers)
            {
                if (u.UserName == user.UserName)
                {
                    return null;
                }
            }
            var f = await App.Database.SaveUserAsync(user) ;
            return f;           
        }
    }
}
