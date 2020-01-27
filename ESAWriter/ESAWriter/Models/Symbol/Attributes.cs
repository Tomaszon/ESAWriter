using System.Drawing;

namespace ESAWriter.Models.Symbol
{
	public class Attributes
	{
		public SymbolColor SymbolColor { get; set; } = new SymbolColor();

		public bool Long { get; set; }

		public bool Underline
		{
			get { return SymbolColor.UnderlineColor.ToArgb() != Color.Transparent.ToArgb(); }

			set { SymbolColor.UnderlineColor = Color.Black; }
		}

		public bool Outline
		{
			get { return SymbolColor.OutlineColor.ToArgb() != Color.Transparent.ToArgb(); }

			set { SymbolColor.OutlineColor = Color.Black; }
		}

		public bool Shadow
		{
			get { return SymbolColor.ShadowColor.ToArgb() != Color.Transparent.ToArgb(); }

			set { SymbolColor.ShadowColor = Color.Black; }
		}

		public bool Animate { get; set; }
	}
}