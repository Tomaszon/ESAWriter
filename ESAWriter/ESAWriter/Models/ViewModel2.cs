using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ESAWriter.Models
{
	public class ViewModel2<TModel> : ViewModelBase2<TModel> where TModel : ModelBase2
	{
		public ViewModelProperty2<string, int, TModel> A { get { return Get<string, int>("A"); } private set { Set("A", value); } }

		public ViewModel2(TModel model) : base(model)
		{
			A = CreateProperty<string, int>(p => $"Formatted: {p}");
		}
	}

	public class ViewModelBase2<TModel> : INotifyPropertyChanged where TModel : ModelBase2
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private readonly Dictionary<string, object> _vars = new Dictionary<string, object>();

		private TModel _model;
		public TModel Model
		{
			set
			{
				_model = value;
				foreach (var item in _vars)
				{
					((ViewModelProperty2<object, object, TModel>)item.Value).Model = value;
				}
			}
			get
			{
				return _model;
			}
		}

		public ViewModelBase2(TModel model)
		{
			Model = model;
		}

		protected ViewModelProperty2<TGet, TSet, TModel> CreateProperty<TGet, TSet>(Func<TSet, TGet> format)
		{
			return new ViewModelProperty2<TGet, TSet, TModel>(this, format);
		}

		protected ViewModelProperty2<TGet, TSet, TModel> Get<TGet, TSet>(string propertyName)
		{
			return _vars[propertyName] as ViewModelProperty2<TGet, TSet, TModel>;
		}

		protected void Set<TGet, TSet>(string propertyName, ViewModelProperty2<TGet, TSet, TModel> value)
		{
			value.Model = Model;
			value.PropertyInfo = Model.GetType().GetProperty(propertyName);

			_vars.Add(propertyName, value);
		}

		public void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}

	public class ViewModelProperty2<TGet, TSet, TModel> where TModel : ModelBase2
	{
		public TModel Model { get; set; }

		public ViewModelBase2<TModel> ViewModel { get; set; }

		public TGet Value { get { return Get(); } }

		public TSet Source { set { Set(value); } }

		public Func<TSet, TGet> Format { get; private set; }

		public PropertyInfo PropertyInfo { get; set; }

		public ViewModelProperty2(ViewModelBase2<TModel> viewModel, Func<TSet, TGet> format)
		{
			ViewModel = viewModel;
			Format = format;
		}

		private void Set(TSet value)
		{
			PropertyInfo.SetValue(Model, value);

			ViewModel.OnPropertyChanged(PropertyInfo.Name);
		}

		private TGet Get()
		{
			return Format.Invoke((TSet)PropertyInfo.GetValue(Model));
		}
	}

	public class Model2 : ModelBase2
	{
		public int A { get; set; }
	}

	public class ModelBase2
	{

	}
}
