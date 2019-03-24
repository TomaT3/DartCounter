using DartCounter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DartCounter.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameView : ContentPage
	{
		public GameView()
		{
			InitializeComponent();
           // BindingContext = new ViewModel.GameViewModel(Navigation, gameModel);
        }
	}
}