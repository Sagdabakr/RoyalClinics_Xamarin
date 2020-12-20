using System;
using System.Collections.Generic;
using System.Text;
using LoginForm.Models;
namespace LoginForm.ViewModels
{
    public class AllTransactions_ViewModel
    {
        public List<Book> AllSheetInfo { get; set; }
        public AllTransactions_ViewModel()
        {
            AllSheetInfo = new List<Book>();
        }
        public User GetCurrentUser(int ID)
        {
            var user = App.Database.GetUserAsync(ID).Result;
            return user;
        }
        public List<Book> GetAllTransactions(int ID)
        {
            var allTrans = App.Database.GetBooksAsync_Desc(ID).Result;              
            return allTrans;
        }
    }
}
