using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ESAWriter.Models
{
	public class ModelFactory<TView, TModel>
	{
		public TView View { get; }

		public TModel Model { get; }

		public ModelFactory()
		{
			TView
		}
	}

	public class View : ViewBase
	{
		public string A { get { return Get<string, int>(p => string.Format("A Formatted: {0}", p)); } }

		public int B { get { return Get<int>(p => p * 2, nameof(Model.A)); } }

		public int C { get { return Get<int>(nameof(Model.A)); } set { Set(value); } }

		public int D { get { return Get<int>(nameof(Model.A)); } set { Set(value); } }

		public View(Model model) : base(model) { }
	}

	public abstract class ViewBase : INotifyPropertyChanged
	{
		private readonly ListKeyDictionary _accessors = new ListKeyDictionary();

		private readonly Dictionary<string, PropertyInfo> _modelProperties = new Dictionary<string, PropertyInfo>();

		private readonly Model _model;

		public ViewBase(Model model)
		{
			model.PropertyChanged += Model_PropertyChanged;

			_model = model;

			Array.ForEach(model.GetType().GetProperties(), p => _modelProperties.Add(p.Name, p));
		}

		protected void InitViewModelProperties()
		{
			Array.ForEach(GetType().GetProperties(), p => p.GetValue(this));
		}

		protected void Set(object value, [CallerMemberName]string accessorName = null)
		{
			PropertyInfo pi = _accessors[accessorName];

			pi.SetValue(_model, value);
		}

		protected TOut Get<TOut, TStored>(Func<TStored, TOut> transform, [CallerMemberName] string propertyName = null, [CallerMemberName] string accessorName = null)
		{
			PropertyInfo pi = _modelProperties[propertyName];
			_accessors.Add(accessorName, pi);

			return transform is null ? (TOut)_accessors[accessorName].GetValue(_model) : transform.Invoke((TStored)_accessors[accessorName].GetValue(_model));
		}

		protected T Get<T>(Func<T, T> transform, [CallerMemberName] string propertyName = null, [CallerMemberName] string accessorName = null)
		{
			return Get<T, T>(transform, propertyName, accessorName);
		}

		protected T Get<T>([CallerMemberName] string propertyName = null, [CallerMemberName] string accessorName = null)
		{
			return Get<T>(p => p, propertyName, accessorName);
		}

		private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnPropertiesChanged(_accessors.GetByPropertyName(e.PropertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName]string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		protected void OnPropertiesChanged(ListKeyValuePair keyValuePair)
		{
			keyValuePair.Key.ForEach(k => OnPropertyChanged(k));
		}
	}

	public class Model : ModelBase
	{
		public int A { get { return Get<int>(); } set { Set(value); } }
	}

	public class ModelBase : INotifyPropertyChanged
	{
		private readonly Dictionary<string, object> _vars = new Dictionary<string, object>();

		protected void Set(object value, [CallerMemberName]string name = null)
		{
			if (_vars.ContainsKey(name))
			{
				_vars.Remove(name);
			}

			_vars.Add(name, value);

			OnPropertyChanged(name);
		}

		protected T Get<T>([CallerMemberName] string name = null)
		{
			if (!_vars.ContainsKey(name))
			{
				_vars.Add(name, default(T));
			}

			return (T)_vars[name];
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName]string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}

	public class ListKeyDictionary
	{
		private readonly List<ListKeyValuePair> _dic = new List<ListKeyValuePair>();

		public PropertyInfo this[string key]
		{
			get
			{
				return Get(key)?.Value;
			}
		}

		public List<string> this[PropertyInfo propertyInfo]
		{
			get
			{
				return Get(propertyInfo)?.Key;
			}
		}

		public ListKeyValuePair Get(string key)
		{
			return _dic.FirstOrDefault(p => p.Key.Contains(key));
		}

		public ListKeyValuePair Get(PropertyInfo propertyInfo)
		{
			return _dic.FirstOrDefault(p => p.Value == propertyInfo);
		}

		public ListKeyValuePair GetByPropertyName(string propertyName)
		{
			return _dic.FirstOrDefault(p => p.Value.Name == propertyName);
		}

		public void Add(string key, PropertyInfo propertyInfo)
		{
			if (Get(key) is var k && k == null)
			{
				if (Get(propertyInfo) is var k2 && k2 != null)
				{
					k2.Key.Add(key);
				}
				else
				{
					_dic.Add(new ListKeyValuePair(key, propertyInfo));
				}
			}
			else if (k.Value != propertyInfo)
			{
				throw new ArgumentException("An element with the same key already exists in the collection with different value!");
			}
		}
	}

	public class ListKeyValuePair
	{
		public List<string> Key { get; set; } = new List<string>();

		public PropertyInfo Value { get; set; }

		public ListKeyValuePair(string key, PropertyInfo value)
		{
			Key.Add(key);
			Value = value;
		}
	}
}


