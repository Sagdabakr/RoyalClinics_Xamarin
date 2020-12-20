using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LoginForm.Models
{     
    
    public class Location
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int LocationTreeId { get; set; }

       
        public string Name { get; set; }

       
        public string NameEnglish { get; set; }	
    }
}
