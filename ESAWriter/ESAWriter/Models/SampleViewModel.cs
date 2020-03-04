using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ESAWriter.Models
{
	public class SampleViewModel : ViewModelBase
	{
		public ViewModelProperty<string, int> A { get { return A.Get(); } set { Set(A, value); } }

		public SampleViewModel(ModelBase model) : base(model)
		{
			InitProperty(A, nameof(A), p => $"Formatted: {p}");
		}
	}

	public class ViewModelBase : INotifyPropertyChanged
	{
		private readonly ModelBase _model;

		public ViewModelBase(ModelBase model)
		{
			_model = model;
		}

		protected void InitProperty<TGet, TSet>(ViewModelProperty<TGet, TSet> property, string modelPropertyName, Func<TSet, TGet> format)
		{
			property = new ViewModelProperty<TGet, TSet>(_model, modelPropertyName, format);
		}

		protected void Set<TGet, TSet>(ViewModelProperty<TGet, TSet> property, TSet value, [CallerMemberName]string name = null)
		{
			property.Set(value);

			OnPropertyChanged(name);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName]string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}

	public class SampleModel : ModelBase
	{
		public int A { get; set; }
	}

	public class ModelBase
	{
		public T GetProperty<T>(string propertyName)
		{
			return (T)GetType().GetProperty(propertyName).GetValue(this);
		}

		public void SetProperty<T>(string propertyName, T value)
		{
			GetType().GetProperty(propertyName).SetValue(this, value);
		}
	}

	public class ViewModelProperty<TGet, TSet>
	{
		private TGet _getValue;

		private TSet _setValue;

		private readonly ModelBase _model;

		private readonly string _propertyName;

		private readonly Func<TSet, TGet> _format;

		public ViewModelProperty() { }

		public ViewModelProperty(ModelBase model, string propertyName, Func<TSet, TGet> format)
		{
			_model = model;
			_propertyName = propertyName;
			_format = format;
		}

		public static implicit operator ViewModelProperty<TGet, TSet>(TSet value)
		{
			return new ViewModelProperty<TGet, TSet>() { _setValue = value };
		}

		//for setter only
		public static implicit operator TSet(ViewModelProperty<TGet, TSet> property)
		{
			return property._setValue;
		}

		public static implicit operator TGet(ViewModelProperty<TGet, TSet> property)
		{
			return property.Get();
		}

		//for getter only
		public static implicit operator ViewModelProperty<TGet, TSet>(TGet value)
		{
			return new ViewModelProperty<TGet, TSet>() { _getValue = value };
		}

		public void Set(TSet value)
		{
			_model.SetProperty(_propertyName, value);
		}

		public TGet Get()
		{
			_getValue = _model.GetProperty<TGet>(_propertyName);
			return _getValue;
		}
	}
}
