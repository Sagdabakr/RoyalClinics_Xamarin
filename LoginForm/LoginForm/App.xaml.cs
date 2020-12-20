using System;
using System.IO;
using Xamarin.Forms;
using LoginForm.Data;
using LoginForm.Views;
using LoginForm.Models;
using System.Collections.Generic;

namespace LoginForm
{
    public partial class App : Application
    {
        static ApplicationDataBase database;
        public static String DATABASE_NAME = "royalclinicsDB8.db3";
        
        public static ApplicationDataBase Database
        {
            get
            {
                if (database == null)
                {                    
                    database = new ApplicationDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));                    
                }
                return database;
            }
        }
       
        public  App()
        {
            InitializeComponent();            
             
            Application.Current.Properties["IsLoggedin"] = "False";
         //   Application.Current.Properties["LocationsAvailable"] = "False";
            var CurrentUser = Application.Current.Properties["IsLoggedin"].ToString();
            if (CurrentUser == "False")
            {
                Application.Current.Properties["UserID"] = "0";                
                
            }
          
/*
            List<Location> AllCountries = new List<Location>()
             {
                 new Location {LocationTreeId = 1 , Name = "المملكة العربية السعودية" , NameEnglish = "Saudi Arabia"},
                 new Location {LocationTreeId = 2 , Name = "المغرب" , NameEnglish = "Morocco"},
               //  new Location {LocationTreeId = 3 , Name = "جمهورية مصر العربية" , NameEnglish = "Egypt"},
                 new Location {LocationTreeId = 4 , Name = "قصر الدار البيضاء" , NameEnglish = "Casablanca palace"},
                 new Location {LocationTreeId = 5 , Name = "الرياض" , NameEnglish = "Riyadh"},
                 new Location {LocationTreeId = 6 , Name = "جدة" , NameEnglish = "Jeddah"},
               //  new Location {LocationTreeId = 7 , Name = "الدمام" , NameEnglish = "Dammam"},
                 new Location {LocationTreeId = 8 , Name = "مكة المكرمة" , NameEnglish = "Makkah"},
               //  new Location {LocationTreeId = 9 , Name = "عيادة الجراحة العامة" , NameEnglish = "General surgery clinic"},
               // new Location {LocationTreeId = 10 , Name = "قصر اليمامة" , NameEnglish = "Yamama Palace"},
                 new Location {LocationTreeId = 11 , Name = "قصر العوجاء" , NameEnglish = "Al-Awja Palace"},
               //  new Location {LocationTreeId = 12 , Name = "المبنى الاول" , NameEnglish = ""},
                // new Location {LocationTreeId = 13 , Name = "االدور الاول" , NameEnglish = ""},
               //  new Location {LocationTreeId = 14 , Name = "عيادة الدور الاول" , NameEnglish = ""},
               //  new Location {LocationTreeId = 15 , Name = "المبنى الثالث" , NameEnglish = "The third building"},
               //  new Location {LocationTreeId = 16 , Name = "عيادة الدور الارضي(اسنان)" , NameEnglish = "Ground floor clinic (dental)"},
               //  new Location {LocationTreeId = 17 , Name = " عيادة الدور الاول" , NameEnglish = "First floor clinic"},
              //   new Location {LocationTreeId = 18 , Name = "عيادة اسنان" , NameEnglish = ""},
                 new Location {LocationTreeId = 19 , Name = "عيادات الحرس الملكي بجدة" , NameEnglish = "Royal Guard Clinics"},
                 new Location {LocationTreeId = 20 , Name = "قصر منى" , NameEnglish = "Mina Palace"},
               //  new Location {LocationTreeId = 21 , Name = "القاهرة" , NameEnglish = ""},
                 new Location {LocationTreeId = 22 , Name = "قصر عرقة" , NameEnglish = "Irqah palace"},
                 new Location {LocationTreeId = 23 , Name = "الدور الثاني" , NameEnglish = "F2"},
                 new Location {LocationTreeId = 24 , Name = "الدور الاول" , NameEnglish = "F1"},
                 new Location {LocationTreeId = 25 , Name = "الديوان الملكي بالرياض" , NameEnglish = "Royal Court"},
                 new Location {LocationTreeId = 26 , Name = "عيادات الحرس الملكي بالرياض" , NameEnglish = "Royal Guard Clinics"},
                 new Location {LocationTreeId = 27 , Name = "الدور الاول" , NameEnglish = "F1"},
                 new Location {LocationTreeId = 28 , Name = "الدور الثاني" , NameEnglish = "F2"},
                 new Location {LocationTreeId = 29 , Name = "الدور الارضي" , NameEnglish = "Ground Floor"},
                 new Location {LocationTreeId = 30 , Name = "عيادة الدور الارضي - قصر العوجاء" , NameEnglish = "Ground floor clinic - Al Awja palace"},
                 new Location {LocationTreeId = 31 , Name = "عيادة رقم 1  - قصر العوجاء" , NameEnglish = "Clinic No. 1 - Al-Awja Palace"},
                 new Location {LocationTreeId = 32 , Name = "عيادة رقم 2 - قصر العوجاء" , NameEnglish = "Clinic No. 2 - Al-Awja Palace"},
                 new Location {LocationTreeId = 33 , Name = "عيادة رقم 2 - قصر عرقة" , NameEnglish = "Clinic No. 1 - Arqa Palace"},
                 new Location {LocationTreeId = 34 , Name = "عيادة رقم 1 - قصر عرقة" , NameEnglish = "Clinic No. 1 - Arqa Palace"},
                 new Location {LocationTreeId = 35 , Name = "عيادة الدور الارضي - الديوان الملكي بالرياض" , NameEnglish = "Ground floor clinic - Riyadh Royal Court"},
                 new Location {LocationTreeId = 36 , Name = "عيادة الدور الارضي - عيادات الحرس الملكي بالرياض" , NameEnglish = "Ground floor clinic - Royal Guard Clinics"},
                 new Location {LocationTreeId = 37 , Name = "قصر الصفا" , NameEnglish = "Safa Palace"},
                 new Location {LocationTreeId = 38 , Name = "الدور الاول" , NameEnglish = "F1"},
                 new Location {LocationTreeId = 39 , Name = "الدور الثاني" , NameEnglish = "F2"},
                 new Location {LocationTreeId = 40 , Name = "عيادة رقم 1 - قصر الصفا" , NameEnglish = "Clinic No. 1 - Safa Palace"},
                 new Location {LocationTreeId = 41 , Name = "عيادة رقم 2 - قصر الصفا" , NameEnglish = "Clinic No. 2 - Safa Palace"},
                 new Location {LocationTreeId = 42 , Name = "عيادة رقم 1 - قصر منى" , NameEnglish = "Clinic No. 1 - Mina Palace"},
                 new Location {LocationTreeId = 43 , Name = "نيوم" , NameEnglish = "Neom"},
                 new Location {LocationTreeId = 44 , Name = "قصر نيوم" , NameEnglish = "Neom Palace"},
                 new Location {LocationTreeId = 45 , Name = "عيادة رقم 1 - قصر نيوم" , NameEnglish = "Clinic No. 1 - Neom Palace"},
                 new Location {LocationTreeId = 46 , Name = "عيادة الدور الارضي - عيادات الحرس الملكي بجدة" , NameEnglish = "Clinic No. 1 - Jeddah Royal Guard Clinics"},
                 new Location {LocationTreeId = 47 , Name = "عيادة رقم 1 - قصر الدار البيضاء" , NameEnglish = "Clinic No. 1 - Casablanca Palace"},
                 new Location {LocationTreeId = 48 , Name = "الديوان الملكي بجدة" , NameEnglish = "Royal Court in Jeddah"},
                 new Location {LocationTreeId = 49 , Name = "عيادة الدور الارضي - الديوان الملكي بجدة" , NameEnglish = "Ground floor clinic - Royal Court, Jeddah"},
             };

            System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(List<Location>));
            System.IO.FileStream filexml = System.IO.File.Create(DependencyService.Get<IFileSystem>().GetExternalStorage() + "AllLocations.xml");
            writer.Serialize(filexml, AllCountries);
            filexml.Close();*/

            MainPage = new NavigationPage(new LoginPage());

        }

        protected override void OnStart()
        {
        
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
