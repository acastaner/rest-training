using Folke.Elm;

namespace PriceList.Lib
{
    public class Product: IFolkeTable
    {
        #region Properties
        public int Id { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string MinimumPrice { get; set; }
        public string Comment { get; set; }
        #endregion
        #region Constructors
        public Product()
        {
        }
        #endregion
        #region Methods
        #endregion
    }
}
