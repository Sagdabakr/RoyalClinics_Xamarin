using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginForm.Models
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int AssetId { get; set; }
        public int SNOfAssetId { get; set; }
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set; }
        public string DateOfAction { get; set; }
        public DateTime FilterDate { get; set; }
        public int IsDone { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string LocationFrom { get; set; }       
        public string LocationTo { get; set; }      
        public string BarCode { get; set; }                          
        public string TimeOfAction { get; set; }                          
        public string CheckDetails { get; set; }                                                   
        public int ListIndex { get; set; }                                                   
    }
}
