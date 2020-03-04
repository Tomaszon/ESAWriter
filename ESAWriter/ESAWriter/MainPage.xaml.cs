using ESAWriter.Models;
using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ESAWriter
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : ESABasePage<SampleModel, SampleViewModel>
	{
		//public MainPageViewModel Model = new MainPageViewModel();
		private readonly Random r = new Random();

		private readonly Polygon polygon = new Polygon();

		public MainPage() : base()
		{
			InitializeComponent();

			polygon.Points.Add(new Point(100, 100));
			polygon.Points.Add(new Point(200, 100));
			polygon.Points.Add(new Point(250, 200));
			polygon.Points.Add(new Point(100, 200));

			polygon.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
			polygon.StrokeThickness = 2;

			polygon.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

			canvas1.Children.Add(polygon);
		}

		private void ButtonTest_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			//Model.Property2 = r.Next(10000);

			Model.A = r.Next(10000);
		}
	}

	public abstract class ESABasePage<TModel, TViewModel> : Page where TViewModel : ViewModelBase where TModel: ModelBase
	{
		public ModelBase Model { get; }

		public ViewModelBase ViewModel { get; }

		public ESABasePage()
		{
			Model = new TModel();

			ViewModel = new TViewModel();
		}
	}
}
