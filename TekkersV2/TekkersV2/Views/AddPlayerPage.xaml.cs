using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TekkersV2.Views
{
    public partial class AddPlayerPage : ContentPage
    {

        public AddPlayerPage()
        {
            InitializeComponent();
        }

        public AddPlayerPage(MainViewModel theViewModel)
        {
            InitializeComponent();
            BindingContext = theViewModel;
        }
    }
}
