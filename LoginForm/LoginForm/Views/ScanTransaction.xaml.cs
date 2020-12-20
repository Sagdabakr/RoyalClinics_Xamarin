using LoginForm.Models;
using LoginForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginForm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanTransaction : ContentPage
    {
        ScannedInfo_ViewModel ViewModel;
        static User CurrentUser;
        public static ObservableCollection<Book> RecentScans = new ObservableCollection<Book>();       
        public static ObservableCollection<Book> TempScans = new ObservableCollection<Book>();
        public static string LocationFrom = "";
        static string LocationFrom_Code = "";
        static string LocationTo = "";
        static string LocationTo_Code = "";
        static ObservableCollection<Location> RemainCountries;
        public ScanTransaction()
        {
            InitializeComponent();
            CurrentUser = new User();
            ViewModel = new ScannedInfo_ViewModel();
            CurrentUser = ViewModel.GetCurrentUser(int.Parse(Application.Current.Properties["UserID"].ToString()));                   
            NewScansList.ItemsSource = RecentScans;
            BindingContext = ViewModel;
        }  
        protected async override void OnAppearing()
        {
            ViewModel.AllCountries = await ViewModel.GetAllCountriesSql();
            RemainCountries = await ViewModel.GetAllCountriesSql();
             
            if (RecentScans.Count() == 0)
            {
                SaveButton.IsEnabled = false;
                CancelButton.IsEnabled = false;
                if (!string.IsNullOrEmpty(LocationFrom))
                {   
                    var temp_locationTo = LocationTo;
                    LocationFrom_Picker.SelectedItem = ViewModel.AllCountries.Where(x => x.Name == LocationFrom).FirstOrDefault();
                    LocationTo_Picker.IsEnabled = true;
                    LocationTo_Picker.SelectedItem = RemainCountries.Where(x => x.Name == temp_locationTo).FirstOrDefault();
                }
            }
            else
            {
                var location = RecentScans.Last();
                LocationFrom_Picker.SelectedItem = ViewModel.AllCountries.Where(x => x.LocationTreeId == location.FromLocationId).FirstOrDefault();
                LocationTo_Picker.IsEnabled = true;
                LocationTo_Picker.SelectedItem = RemainCountries.Where(x => x.LocationTreeId == location.ToLocationId).FirstOrDefault();
                TotalRecentScans.Text = RecentScans.Count.ToString() ;
                if (RecentScans.Count == 1)
                    TotalRecentScans.Text += " Scan";
                else
                    TotalRecentScans.Text += " Scans";
                TotalRecentScans.IsVisible = true;
            }
        } 
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            string XmlFileName = "/Assets.xml";    
            foreach (Book book in RecentScans)
            {
                await  ViewModel.SaveNewScan(book);
                book.CheckDetails = book.id.ToString() + "/" + book.DateOfAction + "/" + book.TimeOfAction;
                await ViewModel.SaveNewScan(book);
            }
            
            var AllScans = await ViewModel.GetAllScans(CurrentUser.ID);
            var xml = new XElement("books");
            foreach(Book book in AllScans)
            {
                xml.Add(new XElement("book",
                    new XElement("id", book.id),
                    new XElement("AssetId", book.AssetId),
                    new XElement("SNOfAssetId", book.SNOfAssetId),
                    new XElement("FromLocationId", book.FromLocationId),
                    new XElement("ToLocationId", book.ToLocationId),
                    new XElement("DateOfAction", book.DateOfAction),
                    new XElement("IsDone", book.IsDone),
                    new XElement("UserID", book.UserID),
                    new XElement("UserName", book.UserName),
                    new XElement("LocationFrom", book.LocationFrom),
                    new XElement("LocationTo", book.LocationTo),
                    new XElement("BarCode", book.BarCode),
                    new XElement("TimeOfAction", book.TimeOfAction),
                    new XElement("CheckDetails", book.CheckDetails)
                    ));
            };
   
            System.Xml.Serialization.XmlSerializer writer =
             new System.Xml.Serialization.XmlSerializer(typeof(XElement));                     

            System.IO.FileStream filexml = System.IO.File.Create(DependencyService.Get<IFileSystem>().GetExternalStorage() + XmlFileName );          
            writer.Serialize(filexml, xml);
          
            AllScans.Clear();
            RecentScans.Clear();
            filexml.Close();
            await DisplayAlert("", "Transactions content Saved", "Okay" );
            LocationFrom = "";
            await Navigation.PushAsync(new MainMenu());
        }
        private async void LocationFromChanged(object sender, EventArgs e)
        {
            var x = ((sender as Picker).SelectedItem) as Location;
            LocationFrom = x.Name;
            LocationFrom_Code = x.LocationTreeId.ToString();
            LocationTo = "";
            LocationTo_Code = "";
            if(RemainCountries.Count < ViewModel.AllCountries.Count )
            {
                RemainCountries = await ViewModel.GetAllCountriesSql();
            }
            var removed = RemainCountries.Where(c => c.LocationTreeId == x.LocationTreeId).FirstOrDefault();
            RemainCountries.Remove(removed);
            LocationTo_Picker.ItemsSource = RemainCountries;
            LocationTo_Picker.SelectedIndex = -1;
            LocationTo_Picker.IsEnabled = true;
            NewBarCode.IsEnabled = false;
           
        }
        private void LocationToChanged(object sender, EventArgs e)
        {
            var x = ((sender as Picker).SelectedItem) as Location;
            if (x != null && RemainCountries!=null)
            {
                LocationTo = x.Name;
                LocationTo_Code = x.LocationTreeId.ToString();
                NewBarCode.IsEnabled = true;
            }
        }
        private async void BackButtonTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainMenu());
        }
        private async void OnCancelClicked(object sender, EventArgs e)
        {

            var Save = await DisplayAlert("Warning", "Do You Want To Save Your Recent Transactions ?", "Save", "Discard");
            if (Save)
            {
                OnSaveClicked(sender, e);
            }
            else if(!Save)
                RecentScans.Clear();

            await Navigation.PushAsync(new ScanTransaction());

        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new MainMenu());
            return true;
        }                  
        private async void ScanNewRecord(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LocationFrom) || string.IsNullOrEmpty(LocationTo) || string.IsNullOrEmpty(NewBarCode.Text) )
                await DisplayAlert("Before scanning", "Make sure to choose a valid location from & location to  ", "cancel");
           

            else
            {
                var x = NewBarCode.Text.Split('/');
               
                
                if (x.Count()==4  )
                {                    
                    bool isIntString1 = x[0].All(char.IsDigit);
                    bool isIntString2 = x[1].All(char.IsDigit);
                    if( isIntString1 && isIntString2 )
                    {
                        Book NewBook = new Book()
                        {
                            UserID      = CurrentUser.ID,
                            UserName    = CurrentUser.UserName,
                            LocationFrom = LocationFrom,
                            LocationTo   = LocationTo,
                            BarCode      = NewBarCode.Text,
                            FromLocationId = int.Parse(LocationFrom_Code),
                            ToLocationId   = int.Parse(LocationTo_Code),
                            DateOfAction   = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                            TimeOfAction= DateTime.Now.ToString("HH:mm:ss"),
                            AssetId = int.Parse(x[0]),
                            SNOfAssetId = int.Parse(x[1]),
                            IsDone = 0,
                            FilterDate = DateTime.Now,
                            ListIndex = RecentScans.Count  +  1
                        };
                        RecentScans.Add(NewBook);                        
                        TotalRecentScans.IsVisible = true;
                        TotalRecentScans.Text = RecentScans.Count.ToString();
                        if (RecentScans.Count == 1)
                            TotalRecentScans.Text += " Scan";
                        else
                            TotalRecentScans.Text += " Scans";
                        SaveButton.IsEnabled = true;
                        CancelButton.IsEnabled = true;
                    }
                    else
                        await DisplayAlert("", "Enter a valid barcode format", "Cancel");
                }
                else
                {
                    await DisplayAlert("", "Enter a valid barcode format", "Cancel");                    
                }
                NewBarCode.Text = string.Empty;
                NewBarCode.Placeholder = "ScanHere";

            }
        }

    }
}