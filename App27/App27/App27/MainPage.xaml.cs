using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App27
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            DependencyService.Get<IDataStore<Item>>().AddItemAsync(new Item() {Id = 4, Text = "test", Description = "four-test" });

            Task<Item> i = DependencyService.Get<IDataStore<Item>>().GetItemAsync(1);
            Item myItem = i.Result;
            Console.WriteLine("-----"+myItem.Text +myItem.Description);

            Task<Item> fourthItem = DependencyService.Get<IDataStore<Item>>().GetItemAsync(4);
            Item myItem4 = fourthItem.Result;
            Console.WriteLine("-----"+myItem4.Text + myItem4.Description);
        }

    }

    public interface IDataStore<Item>
    {
        Task<bool> AddItemAsync(Item item);
        Task<bool> DeleteItem(string id);

        Task<Item> GetItemAsync(int id);
    }

    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}
