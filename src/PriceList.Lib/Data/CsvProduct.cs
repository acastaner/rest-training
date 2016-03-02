using FileHelpers;

namespace PriceList.Lib
{
    [DelimitedRecord(",")]
    public class CsvProduct
    {
        public string PartNumber;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Name;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Price;
        public string Uom;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Description;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string MinimumPrice;
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Comment;
    }

    public class CsvProductDevExport
    {
        public int Id { get; set; }
        public string PartNumber { get; set; }

    }
}
