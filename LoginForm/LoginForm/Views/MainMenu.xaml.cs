using LoginForm.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
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
    public partial class MainMenu : ContentPage
    {
        UpdateLocations_ViewModel ViewModel;
        public MainMenu()
        {
            InitializeComponent();
            ViewModel = new UpdateLocations_ViewModel();
        }     

        private async void ScanCodeClicked(object sender, EventArgs e)
        {
            var LocationsCount = await ViewModel.LocationsCount();
            if (LocationsCount == 0)
            {
                Application.Current.Properties["LocationsAvailable"] = "False";
                await DisplayAlert("", "Please Make Sure To Update Your Locations", "Cancel");
            }
            else
            {
                Application.Current.Properties["LocationsAvailable"] = "True";
                await Navigation.PushAsync(new ScanTransaction());
            }                                            
        }

        private async void AllTransactionsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllTransactions());
        }
        protected override bool OnBackButtonPressed()
        {            
            return true;
        }

        private async void OnLogOutClicked(object sender, EventArgs e)
        {
          

            foreach (Book info in ScanTransaction.RecentScans)
            {
                ScanTransaction.TempScans.Add(info);
            }

            ScanTransaction.LocationFrom = "";
            ScanTransaction.RecentScans.Clear();
            Application.Current.Properties["IsLoggedin"] = "False";
            Application.Current.Properties["UserID"] = "0";           
            await Navigation.PushAsync(new LoginPage());
        }

        private async void UpdatLocationsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateLocations());
        }

        private async void UpdateUsersClicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new UpdateUsers());
        }
    }
}