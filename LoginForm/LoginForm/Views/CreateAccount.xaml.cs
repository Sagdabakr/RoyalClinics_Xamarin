using LoginForm.Models;
using LoginForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccount : ContentPage
    {
        CreateAccount_ViewModel CreateAccount_ViewModel;
        string isLoggedIn = Application.Current.Properties["IsLoggedin"].ToString();
        public CreateAccount()
        {
            InitializeComponent();
            CreateAccount_ViewModel = new CreateAccount_ViewModel();
            
            if(isLoggedIn == "True")
            {
                CancelButton.IsVisible = true;
                BackButton.IsVisible = true;
                LoginButton.IsVisible = false;
            }
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
                   
  
            if (string.IsNullOrEmpty(LoginName.Text) || string.IsNullOrEmpty(LoginPassword.Text) || LoginPassword.Text.Count() < 6)
            {
                WrongInfo.IsVisible = true;
            }

            else
            {
                User user = new User()
                {
                    UserName = LoginName.Text,
                    UserPassword = LoginPassword.Text
                };

                var userData = await CreateAccount_ViewModel.CreateAccount(user);
                if (userData == null)
                {
                    WrongInfo.IsVisible = true;
                }
                else
                {           
                    if (isLoggedIn == "True")
                    {
                      await  DisplayAlert("User Created","User "+ userData.UserName + " Created successfully", "Okay");
                      await Navigation.PushAsync(new MainMenu());
                    }
                    else
                    {
                        Application.Current.Properties["IsLoggedin"] = "True";
                        Application.Current.Properties["UserID"] = userData.ID.ToString();
                        await Navigation.PushAsync(new MainMenu());
                    }                    
                }
            }
                               
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainMenu());
        }

        private async void BackButtonTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainMenu());
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}