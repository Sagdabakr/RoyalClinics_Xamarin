using LoginForm.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LoginForm.ViewModels;

namespace LoginForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateUsers : ContentPage
    {
        ObservableCollection<User> NewUsers;
        UpdateUsers_ViewModel ViewModel;        
        public UpdateUsers()
        {
            InitializeComponent();
            NewUsers = new ObservableCollection<User>();
            ViewModel = new UpdateUsers_ViewModel();
        }

        private async void UploadFileClicked(object sender, EventArgs e)
        {
            try
            {                
                FileData filedata = await CrossFilePicker.Current.PickFile();
                if (filedata == null)
                    return;


                NewUsers = new ObservableCollection<User>();

                XDocument doc = XDocument.Load(filedata.FilePath);

                foreach (XElement s in doc.Descendants("Location"))
                {
                    User x = new User()
                    {
                        Oid = Guid.Parse(s.Element("Oid").Value),
                        UserName = s.Element("UserName").Value,
                        UserPassword = "123456",                       
                    };
                    NewUsers.Add(x);
                }


                if (NewUsers.Count != 0)
                {
                    UsersList.ItemsSource = NewUsers;
                    SavingOption.IsVisible =true;
                    ListFrame.IsVisible = true;
                    Header.IsVisible = false;
                    UsersCount.IsVisible = true;
                    ListCounter.Text = NewUsers.Count.ToString() + " New Users";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if(NewUsers.Count == 0)
            {

            }
            else
            {
                await ViewModel.SaveNewUsers(NewUsers);
                await DisplayAlert("", "New users list saved successfully", "Cancel");
                await Navigation.PushAsync(new MainMenu());
            }          
        }

        private async void BackButtonTapped(object sender, EventArgs e)
        {
            if(NewUsers.Count > 0)
            {
                var Save = await DisplayAlert("", "Do you want to save your new users list ?", "Save", "Discard");
                if (Save)
                {
                    OnSaveClicked(sender, e);
                }
                else if (!Save)
                    await Navigation.PushAsync(new MainMenu());
            }
            else             
                await Navigation.PushAsync(new MainMenu());
        }
        protected override bool OnBackButtonPressed()
        {
            
            if(NewUsers.Count > 0)
            {
                object sender = new object();
                EventArgs e = new EventArgs();
                BackButtonTapped(sender, e);
            }
            else
            Navigation.PushAsync(new MainMenu());
            return true;
        }
    }
}