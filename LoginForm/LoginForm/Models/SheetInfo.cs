using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginForm.Models
{
    public class SheetInfo
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }        
        public string LocationFrom { get; set; }
        public string LocationFrom_Code { get; set; }
        public string LocationTo { get; set; }
        public string LocationTo_Code { get; set; }
        public string BarCode { get; set; }
        public DateTime Date { get; set; }
        
        
    }
}
