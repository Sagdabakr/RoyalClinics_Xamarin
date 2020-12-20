using System;
using LoginForm.ViewModels;
using LoginForm.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginPageViewModel LoginPageViewModel;
       
        public LoginPage()
        {
            InitializeComponent();
            LoginPageViewModel = new LoginPageViewModel();          
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {

            User user = new User()
            {
                UserName = LoginName.Text,
                UserPassword = LoginPassword.Text
            };

            var userData =await  LoginPageViewModel.Login(user);
            if (userData == null)
            {
                WrongInfo.IsVisible = true;
                LoginName.Text = "";
                LoginPassword.Text = "";
            }
            else
            {

                foreach (Book info in ScanTransaction.TempScans)
                {
                    if (info.UserID == userData.ID)
                    {
                        ScanTransaction.RecentScans.Add(info);
                    }
                }
                foreach (Book info in ScanTransaction.RecentScans)
                {      
                       ScanTransaction.TempScans.Remove(info);
                }               
                Application.Current.Properties["IsLoggedin"] = "True";
                Application.Current.Properties["UserID"] = userData.ID.ToString();               
                await Navigation.PushAsync(new MainMenu());                
            }
          
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccount());
        }
    }
}