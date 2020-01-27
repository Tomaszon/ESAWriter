namespace ESAWriter.Models.Document
{
	public class Page
	{
		public Margin Margin { get; set; }

		public Border Border { get; set; }

		public HeaderFooter Header { get; set; }

		public HeaderFooter Footer { get; set; }

		public PageNumber PageNumber { get; set; }

		public Background Background { get; set; }

		public int CharacterCount { get; set; }
	}
}
