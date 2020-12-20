using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using LoginForm.Models;
using SQLite;
using System.Reflection;
using System.Xml.Serialization;
using Xamarin.Forms;
using Java.Util;

namespace LoginForm.ViewModels
{
    public class LoginPageViewModel
    {
        public ObservableCollection<User> AllUsers { get; set; }
        public LoginPageViewModel()
        {
            AllUsers = new ObservableCollection<User>();
        }

        public User Login2(User _user)
        {              
            var allUsers =  App.Database.GetUsersAsync().Result;
            foreach (User u in allUsers)
            {
                if (u.UserName == _user.UserName && u.UserPassword == _user.UserPassword)
                {
                    return u;                   
                }                
            }
            return null;
        }              
      
        public async Task<User> Login(User _user)
        {
                        
            var SQLUsers = await App.Database.GetUsersAsync();

            if(SQLUsers.Count == 0)
            {
                User administrator = new User()
                {
                    UserName = "Manager",
                    UserPassword = "123456",
                };
                await App.Database.SaveUserAsync(administrator);
                administrator.ID = administrator.SqlliteID;
                await App.Database.SaveUserAsync(administrator);
                SQLUsers = await App.Database.GetUsersAsync();
            }

           

            foreach (User SqlUser in SQLUsers)
            {
                if(SqlUser.UserName == _user.UserName && SqlUser.UserPassword == _user.UserPassword)
                {
                    return SqlUser;
                }
            }
            return null;
        }

      /*  public async Task<List<User>> AllUsersXMLAsync()
        {
            var allUsers = new List<User>
            {               
                  new User{UserName = "admin" , UserPassword ="123456" , SqlliteID=1 , ID=1},               
            };
            System.Xml.Serialization.XmlSerializer writer =
             new System.Xml.Serialization.XmlSerializer(typeof(List<User>));
            System.IO.FileStream filexml = System.IO.File.Create(DependencyService.Get<IFileSystem>().GetExternalStorage() + "AllUsers.xml");
            writer.Serialize(filexml, allUsers);
            filexml.Close();         
            
            List<User> rawData = new List<User>();
            
            XDocument doc = XDocument.Load(DependencyService.Get<IFileSystem>().GetExternalStorage() + "AllUsers.xml");

            foreach (XElement s in doc.Descendants("User"))
                {                   
                    User x = new User()
                    {                       
                        UserName = s.Element("UserName").Value,
                        UserPassword = s.Element("UserPassword").Value,
                        ID = int.Parse(s.Element("ID").Value)
                    };
                     rawData.Add(x);
                     
                }
            foreach (User u in rawData)
            {
                await App.Database.SaveUserAsync(u);                
            }
            return rawData;
        }*/     

    }
}
