using ESAWriter.ViewModels;
using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ESAWriter
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPageViewModel Model = new MainPageViewModel();
		private readonly Random r = new Random();

		public MainPage()
		{
			InitializeComponent();
		}

		private void ButtonTest_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			Model.Property2 = r.Next(10000);
		}
	}
}
