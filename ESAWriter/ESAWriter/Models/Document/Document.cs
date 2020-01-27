namespace ESAWriter.Models.Document
{
	public class Document
	{
		public string Author { get; set; }

		public string CreationDate { get; set; }

		public string Path { get; set; }

		public string Title { get; set; }

		public string LastSaveDate { get; set; }

		public string Comment { get; set; }

		public Margin DefaultMargin { get; set; }

		public Border DefaultBorder { get; set; }

		public HeaderFooter DefaultHeader { get; set; }

		public HeaderFooter DefaultFooter { get; set; }

		public PageNumber DefaultPageNumber { get; set; }

		public Background DefaultBackground { get; set; }

		public long Size { get; set; }

		public int CharacterCount { get; set; }

		public bool Modified { get; set; }

		public int PageCount { get; set; }

		public PageSize PageSize { get; set; }
	}
}
