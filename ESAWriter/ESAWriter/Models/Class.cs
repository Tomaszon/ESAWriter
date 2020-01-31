using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ESAWriter.Models
{
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
}
