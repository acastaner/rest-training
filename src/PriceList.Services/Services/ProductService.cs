using Folke.Elm;
using Folke.Elm.Fluent;
using PriceList.Lib;
using PriceList.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileHelpers;
using PriceList.Lib.Mappings;

namespace PriceList.Services.Services
{
    public class ProductService: IProductService
    {
        #region Properties
        private readonly IFolkeConnection session;
        #endregion
        #region Constructors
        public ProductService(IFolkeConnection session)
        {
            this.session = session;
        }
        #endregion
        #region Methods
        // CRUD operations
        public Product Create(Product product)
        {
            using(var t = session.BeginTransaction())
            {
                session.Save(product);
                t.Commit();
                return product;
            }
        }
        public Product Read(int id)
        {
            return session.Get<Product>(id);
        }
        /// <summary>
        /// Update a Product. Returns null if the provided product cannot be found in the database.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Product Update(Product product)
        {
            using(var t = session.BeginTransaction())
            {
                // Get the "old" product information from db
                // and update them with the new info
                Product oldProduct = session.Get<Product>(product.Id);

                if (oldProduct == null)
                    return oldProduct;

                oldProduct.Comment = product.Comment;
                oldProduct.Description = product.Description;
                oldProduct.Name = product.Name;
                oldProduct.PartNumber = product.PartNumber;
                oldProduct.Price = product.Price;

                session.Update(oldProduct);
                t.Commit();
                return oldProduct;
            }
        }
        public void Delete(int id)
        {
            Product product = session.Get<Product>(id);

            if (product == null)
                return;

            session.Delete<Product>(product);
            return;
        }

        #region Extra operations
        public IList<Product> Search(string partNumber)
        {
            return session.SelectAllFrom<Product>()
                                            .Where(x => x.PartNumber.Like("%"+ partNumber + "%"))
                                            .ToList();

        }
        #endregion
        #region Dev operations
        public IList<Product> GetAllProducts()
        {
            return session.SelectAllFrom<Product>()
                                        .OrderBy(x => x.Id).Asc()
                                        .ToList();
        }
        public async Task PopulateData()
        {
            var engine = new FileHelperEngine<CsvProduct>();
            var csvProducts = engine.ReadFile("pricelist.csv");
            foreach(CsvProduct csvProduct in csvProducts)
            {
                Product product = csvProduct.ToProduct();
                Create(product);
            }
        }
        #endregion
        #endregion
    }
}
