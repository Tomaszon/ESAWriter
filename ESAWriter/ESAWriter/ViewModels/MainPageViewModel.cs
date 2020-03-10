using ESAWriter.Models;
using System.ComponentModel;

namespace ESAWriter.ViewModels
{
	public class ViewModel : ViewModelBase
	{
		public string A { get { return Get<int, string>(p => FormatStrings.Format(nameof(A), p, (B * 2).ToString() + "zz0")); } }

		public int B { get { return Get<int>(nameof(Model.A), p => p * 2); } }

		public int C { get { return Get<int, int>(s => s, nameof(Model.A)); } set { Set(value); } }

		public string D { get { return Get<int, string>(p => p.ToString() + " Ft", nameof(Model.A)); } set { Set(value, p => int.Parse(p.Replace(" Ft", ""))); } }

		public ViewModel(ModelBase model) : base(model) { }
	}

	public class Model : ModelBase
	{
		public int A { get { return Get<int>(5); } set { Set(value); } }

		public int B { get { return Get<int>(); } set { Set(value); } }
	}
}
