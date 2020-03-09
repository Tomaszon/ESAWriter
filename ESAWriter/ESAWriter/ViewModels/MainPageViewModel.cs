using ESAWriter.Models;
using System.ComponentModel;

namespace ESAWriter.ViewModels
{
	public class ViewModel<TModel> : ViewModelBase<TModel> where TModel : class, INotifyPropertyChanged
	{
		public string A { get { return Get<string, int>(p => FormatStrings.Format(nameof(A), p, (B * 2).ToString() + "zz0")); } }

		public int B { get { return Get<int>(p => p * 2, nameof(Model.A)); } }

		public int C { get { return Get<int, int>(s => s, nameof(Model.A)); } set { Set(value); } }

		public string D { get { return Get<string>(nameof(Model.A)); } set { Set(value); } }

		public ViewModel(TModel model) : base(model) { }
	}

	public class Model : ModelBase
	{
		public int A { get { return Get<int>(); } set { Set(value); } }

		public int B { get { return Get<int>(); } set { Set(value); } }
	}
}
