//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Reflection;
//using System.Runtime.CompilerServices;

//namespace ESAWriter.Models
//{
//	public class SampleViewModel : ViewModelBase
//	{
//		public ViewModelProperty<string, int> A { get { return Get<string, int>(nameof(A)); } set { Set(nameof(A), value); } }

//		public SampleViewModel(ModelBase model) : base(model)
//		{
//			A = BindProperty<string, int>(nameof(A), p => $"Formatted: {p}");
//		}
//	}

//	public class ViewModelBase : INotifyPropertyChanged
//	{
//		private readonly ModelBase _model;

//		private readonly Dictionary<string, object> _vars = new Dictionary<string, object>();

//		public ViewModelBase(ModelBase model)
//		{
//			_model = model;
//		}

//		protected ViewModelProperty<TGet, TSet> BindProperty<TGet, TSet>(string modelPropertyName, Func<TSet, TGet> format)
//		{
//			return new ViewModelProperty<TGet, TSet>(_model, modelPropertyName, format);
//		}

//		protected void Set<TGet, TSet>(string propertyName, ViewModelProperty<TGet, TSet> value)
//		{
//			if (value.Tmp)
//			{
//				((ViewModelProperty<TGet, TSet>)_vars[propertyName]).Set(value);
//			}
//			else
//			{
//				_vars.Add(value.PropertyInfo.Name, value);
//			}

//			OnPropertyChanged(propertyName);
//		}

//		protected ViewModelProperty<TGet, TSet> Get<TGet, TSet>(string propertyName)
//		{
//			return ((ViewModelProperty<TGet, TSet>)_vars[propertyName]).Get();
//		}

//		public event PropertyChangedEventHandler PropertyChanged;

//		protected void OnPropertyChanged([CallerMemberName]string name = null)
//		{
//			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
//		}
//	}

//	public class SampleModel : ModelBase
//	{
//		public int A { get; set; }
//	}

//	public class ModelBase
//	{

//	}

//	public class ViewModelProperty<TGet, TSet>
//	{
//		public object TmpValue { get; private set; }

//		public bool Tmp { get; private set; }

//		private readonly ModelBase _model;

//		private readonly Func<TSet, TGet> _format;

//		public PropertyInfo PropertyInfo { get; set; }

//		public ViewModelProperty() { }

//		public ViewModelProperty(ModelBase model, string propertyName, Func<TSet, TGet> format)
//		{
//			_model = model;
//			PropertyInfo = model.GetType().GetProperty(propertyName);
//			_format = format;
//		}

//		public static implicit operator ViewModelProperty<TGet, TSet>(TSet value)
//		{
//			return new ViewModelProperty<TGet, TSet>() { TmpValue = value, Tmp = true };
//		}

//		//for setter only
//		public static implicit operator TSet(ViewModelProperty<TGet, TSet> property)
//		{
//			return (TSet)property.TmpValue;
//		}

//		public static implicit operator TGet(ViewModelProperty<TGet, TSet> property)
//		{
//			return (TGet)property.TmpValue;
//		}

//		//for getter only
//		public static implicit operator ViewModelProperty<TGet, TSet>(TGet value)
//		{
//			return new ViewModelProperty<TGet, TSet>() { TmpValue = value, Tmp = true };
//		}

//		public void Set(TSet value)
//		{
//			PropertyInfo.SetValue(_model, value);
//		}

//		public TGet Get()
//		{
//			return _format.Invoke((TSet)PropertyInfo.GetValue(_model));
//		}
//	}
//}
