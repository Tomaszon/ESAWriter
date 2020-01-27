namespace ESAWriter.Models.Symbol
{
	public class Symbol
	{
		public string Code { get; set; } = "";

		public char ImportCharacter { get; set; } = '\0';

		public int Size { get; set; } = 1;

		public SymbolStyle Style { get; set; } = new SymbolStyle();

		public Capabilities Capabilities { get; set; } = new Capabilities();

		public Attributes Attributes { get; set; } = new Attributes();
	}
}