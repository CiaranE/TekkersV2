using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.ViewModels;
using Xamarin.Forms;

namespace TekkersV2.Views
{
    public partial class FindPlayerPage : ContentPage
    {
        public FindPlayerPage(MainViewModel mvm)
        {
            InitializeComponent();

            BindingContext = mvm;
        }
    }
}
