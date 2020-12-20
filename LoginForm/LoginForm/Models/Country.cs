using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginForm.Models
{
    public class Country
    {
        [PrimaryKey, AutoIncrement]
        public int CountryID{ get; set; }
        public string CountryCode{ get; set; }
        public string CountryName { get; set; }        
    }
}
