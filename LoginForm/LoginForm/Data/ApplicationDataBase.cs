using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoginForm.Models;
using SQLite;

namespace LoginForm.Data
{
    public class ApplicationDataBase
    {
        readonly SQLiteAsyncConnection _database;

        /*Tables Creation*/
        public ApplicationDataBase(string dbPath)
        {            
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();                         
            _database.CreateTableAsync<Book>().Wait();          
            _database.CreateTableAsync<Location>().Wait();          
        }
        ////////////////////////////////////////////////////////////////////////////////////
        public async Task<List<User>> GetUsersAsync()
        {
            return await _database.Table<User>().ToListAsync();
        }
        
        public async Task<List<Location>> GetLocationsAsync()
        {
            return await _database.Table<Location>().ToListAsync();
           
        } 
        ////////////////////////////////////////////////////////////////////////////////////

        public Task<User> GetUserAsync(int id)
        {
            return _database.Table<User>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        public Task<Location> GetLocationAsync(int id)
        {
            return _database.Table<Location>()
                            .Where(i => i.LocationTreeId == id)
                            .FirstOrDefaultAsync();
        }              
        public async Task<List<Book>> GetAllBooks(int Userid)
        {
             var x = await _database.Table<Book>()
                            .Where(i => i.UserID == Userid).ToListAsync();
            return x;
        }               
        public Task<List<Book>> GetBooksAsync_Desc(int Userid)
        {            
            var s = _database.QueryAsync<Book>("SELECT * FROM Book WHERE UserID = ? ORDER BY FilterDate  DESC", Userid.ToString());
            return s;          
        }   

        ////////////////////////////////////////////////////////////////////////////////////
        public async Task<User> SaveUserAsync(User user)
        {
            var allUsers = await App.Database.GetUsersAsync();
            foreach (User u in allUsers)
            {
                if(u.SqlliteID == user.SqlliteID)
                {
                     await _database.UpdateAsync(user);
                    return user;
                }
            }
            await _database.InsertAsync(user);
            return await _database.Table<User>()
                        .Where(i => i.UserName == user.UserName)
                            .FirstOrDefaultAsync();
        }

        
        public async Task<int> SaveNewBook(Book _book)
        {
            if (_book.id != 0)
            {
                return await _database.UpdateAsync(_book);
            }
            else
            {
                var n =await _database.InsertAsync(_book);
                return n;                
            }
        }
        
        public async Task<int> SaveLocationAsync(Location _location)
        {
            if (_location.id != 0)
            {
                return await _database.UpdateAsync(_location);
            }
            else
            {
                var n =await _database.InsertAsync(_location);
                return n;                
            }
        }

        public async Task<int> DeleteUserAsync(User note)
        {
            return await _database.DeleteAsync(note);
        }
        
        public async Task<int> DeleteAllLocations()
        {
            return await _database.ExecuteAsync("delete from " + "Location");
        }
    }
}
