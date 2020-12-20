using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace LoginForm.ViewModels
{

    public class ScannedInfo_ViewModel  : BaseViewModel
    {

        public ObservableCollection<Location> AllCountries
        {
            get { return _allLocations; }
            set { SetProperty(ref _allLocations, value); }
        }
        private ObservableCollection<Location> _allLocations;
        
       /* public ObservableCollection<Location> RemainCountries
        {
            get { return _RemainCountries; }
            set { SetProperty(ref _RemainCountries, value); }
        }
        private ObservableCollection<Location> _RemainCountries;*/
      
        public List<SheetInfo> AllScans { get; set; }
        public ScannedInfo_ViewModel()
        {
            AllCountries = new ObservableCollection<Location>();
            _allLocations = new ObservableCollection<Location>();
          //  RemainCountries = new ObservableCollection<Location>();
         ///   _RemainCountries = new ObservableCollection<Location>();
            AllScans = new List<SheetInfo>();            
        }              
        public async Task<Book> SaveNewScan(Book _book)
        {
            await App.Database.SaveNewBook(_book);
            return _book;
        }

        public async Task<ObservableCollection<Location>> GetAllCountriesSql()
        {
            var myList = await App.Database.GetLocationsAsync();
            ObservableCollection<Location>  rawData = new ObservableCollection<Location>(myList as List<Location>);            
            return rawData;          
        }
        public User GetCurrentUser(int ID)
        {
            var user = App.Database.GetUserAsync(ID).Result;
            return user;
        }
        public async Task<List<Book>>  GetAllScans(int UserID)
        {
            var AllRecords = await App.Database.GetAllBooks(UserID);            
            return AllRecords;
        }
        
  
    }
}
