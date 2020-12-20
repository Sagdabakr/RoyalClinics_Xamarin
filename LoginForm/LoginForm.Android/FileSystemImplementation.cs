using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LoginForm;
using LoginForm.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystemImplementation))]
namespace LoginForm.Droid
{
    class FileSystemImplementation : IFileSystem
    {
        public string GetExternalStorage()
        {
            Context context = Android.App.Application.Context;
            var filePath = context.GetExternalFilesDir("");
            return filePath.Path;
        }
    }
}