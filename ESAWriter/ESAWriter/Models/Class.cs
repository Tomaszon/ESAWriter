using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ESAWriter.Models
{
	public class View : ViewBase
	{
		public string A { get { return Get<string, int>(p => string.Format("A Formatted: {0}", p)); } }

		public int B { get { return Get<int, int>(p => p * 2, "A"); } }

		public View(Class model) : base(model)
		{

		}
	}

	public class ViewBase : INotifyPropertyChanged
	{
		private readonly Dictionary<List<string>, PropertyInfo> _accessors = new Dictionary<List<string>, PropertyInfo>();

		public Class Model { get; set; }

		public ViewBase(Class model)
		{
			Model = model;

			Model.PropertyChanged += Model_PropertyChanged;

			Array.ForEach(model.GetType().GetProperties(), e => _accessors.Add(e.Name, e));
		}

		protected void Set(object value, [CallerMemberName]string name = null)
		{
			_accessors[name].SetValue(Model, value);

			OnPropertyChanged(name);
		}

		protected TOut Get<TOut, TStored>(Func<TStored, TOut> format = null, [CallerMemberName] string name = null)
		{
			return format is null ? (TOut)_accessors[name].GetValue(Model) : format.Invoke((TStored)_accessors[name].GetValue(Model));
		}

		private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChanged(e.PropertyName);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName]string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}

	public class Class : ClassBase
	{
		public int A { get { return Get<int>(); } set { Set(value); } }
	}

	public class ClassBase : INotifyPropertyChanged
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
		public List<Tuple<List<string>, PropertyInfo>> _dic { get; set; } = new List<Tuple<List<string>, PropertyInfo>>();

		public PropertyInfo this[string key]
		{
			get
			{
				return _dic.FirstOrDefault(t => t.Item1.Contains(key))?.Item2;
			}
		}

		public PropertyInfo this[PropertyInfo propertyInfo]
		{
			get
			{
				return _dic.FirstOrDefault(t => t.Item2 == propertyInfo)?.Item2;
			}
		}

		public void Add(string key, PropertyInfo propertyInfo)
		{
			if (this[key] == null)
			{

			}
		}
	}
}

