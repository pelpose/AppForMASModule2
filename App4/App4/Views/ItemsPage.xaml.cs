using System.Collections.Generic;
using Xamarin.Forms;
using App4.Models;

namespace App4.Views
{
	public partial class ItemsPage : ContentPage
    {

        public ItemsPage()
        {
            InitializeComponent();

        }

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            List<Product> product = await AzureManager.AzureManagerInstance.GetEmotion();

            test.ItemsSource = product;
        }



    }
}