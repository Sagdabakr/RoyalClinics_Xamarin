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
    public partial class UpdateLocations : ContentPage
    {
        UpdateLocations_ViewModel ViewModel;
        ObservableCollection<Location> NewLocations;
        public UpdateLocations()
        {
            InitializeComponent();
            ViewModel = new UpdateLocations_ViewModel();
            NewLocations = new ObservableCollection<Location>();
        }


        private async void UploadFileClicked(object sender, EventArgs e)
        {
            try
            {

                FileData filedata = await CrossFilePicker.Current.PickFile();
                if (filedata == null)
                    return;


                NewLocations = new ObservableCollection<Location>();

                XDocument doc = XDocument.Load(filedata.FilePath);

                foreach (XElement s in doc.Descendants("Location"))
                {
                    Location x = new Location()
                    {
                        LocationTreeId = int.Parse(s.Element("LocationTreeId").Value),
                        Name = s.Element("Name").Value,
                        NameEnglish = s.Element("NameEnglish").Value
                    };
                    NewLocations.Add(x);
                }

                
                if(NewLocations.Count != 0 )
                {
                    LocationsList.ItemsSource = NewLocations;
                    SavingOption.IsVisible = true;
                    LocationsCount.IsVisible = true;
                    ListCounter.Text = NewLocations.Count.ToString() + " New Locations";
                    ListFrame.IsVisible = true;
                    Header.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private async void BackButtonTapped(object sender, EventArgs e)
        {
            if (NewLocations.Count > 0)
            {
                var Save = await DisplayAlert("", "Do you want to save your new locations list ?", "Save", "Discard");
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
            if (NewLocations.Count > 0 )
            {
                object sender = new object();
                EventArgs e = new EventArgs();
                BackButtonTapped(sender, e);
            }
            else
                Navigation.PushAsync(new MainMenu());
            return true;
        }
        private async void  OnSaveClicked(object sender, EventArgs e)
        {
            if(NewLocations.Count == 0)
            {

            }
            else
            {
                await ViewModel.DeleteLocations();
                await ViewModel.SaveLocations(NewLocations);
                Application.Current.Properties["LocationsAvailable"] = "True";
                await DisplayAlert("", "New Locations Saved successfully", "Cancel");
                await Navigation.PushAsync(new MainMenu());
            }
          
        }
    }
}