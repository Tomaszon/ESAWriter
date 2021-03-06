﻿using ESAWriter.ViewModels;
using ModelContainer;
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
	public sealed partial class MainESAPage : Page
	{
		//public MainPageViewModel Model = new MainPageViewModel();
		private readonly Random r = new Random();

		private readonly Polygon polygon = new Polygon();

		public ModelContainer<ViewModel, Model> ModelContainer = new ModelContainer<ViewModel, Model>();

		public MainESAPage()
		{
			InitializeComponent();

			polygon.Points.Add(new Point(100, 100));
			polygon.Points.Add(new Point(200, 100));
			polygon.Points.Add(new Point(250, 200));
			polygon.Points.Add(new Point(100, 200));

			polygon.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
			polygon.StrokeThickness = 2;

			polygon.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));


			ModelContainer.ViewModel.FormatStrings.Add(nameof(ModelContainer.ViewModel.A), "Formatted: {0} {1} {0}");
			//canvas1.Children.Add(polygon);
		}

		public void ButtonTest_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			//Model.Property2 = r.Next(10000);

			//ViewModel.View.ModifyFormat("A", "F");

			ModelContainer.Model.A = r.Next(10000);

			//ViewModel.View.D = 30;
		}
	}
}
