using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginForm.Models
{
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int SqlliteID { get; set; }
        public int ID { get; set; }
        public Guid Oid { get; set; }
        public string  UserName { get; set; }
        public string  UserPassword { get; set; }
    }
}
