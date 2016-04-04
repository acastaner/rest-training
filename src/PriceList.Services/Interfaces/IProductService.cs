using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceList.Lib;

namespace PriceList.Services.Interfaces
{
    public interface IProductService
    {
        // CRUD operations
        Product Create(Product product);
        Product Read(int id);
        Product Update(Product product);
        void Delete(int id);

        // Extra operations
        IList<Product> Search(string partNumber);
        Task PopulateData();

        // Dev operations
        IList<Product> GetAllProducts();
    }
}
