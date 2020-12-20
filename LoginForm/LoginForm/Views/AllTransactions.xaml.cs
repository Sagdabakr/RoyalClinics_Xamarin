using LoginForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllTransactions : ContentPage
    {
        AllTransactions_ViewModel ViewModel;
        public AllTransactions()
        {
            InitializeComponent();
            ViewModel = new AllTransactions_ViewModel();
            var UserID = int.Parse(Application.Current.Properties["UserID"].ToString());
            ViewModel.AllSheetInfo = ViewModel.GetAllTransactions(UserID);
            BindingContext = ViewModel;
        }

        private async void BackButtonTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainMenu());
        }
    }
}