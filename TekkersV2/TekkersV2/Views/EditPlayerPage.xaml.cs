using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.ViewModels;
using Xamarin.Forms;

namespace TekkersV2.Views
{
    public partial class EditPlayerPage : ContentPage
    {
        public EditPlayerPage()
        {
            InitializeComponent();
        }

        public EditPlayerPage(MainViewModel theViewModel)
        {
            InitializeComponent();

            BindingContext = theViewModel;
        }

        private void SaveButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
