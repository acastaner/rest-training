using Microsoft.AspNet.Mvc;
using PriceList.Lib;
using PriceList.Services.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace rest_api.Controllers
{
    [Route("api/[controller]")]
    public class DevController: Controller
    {
        #region Properties
        private IProductService productService;
        #endregion
        #region Constructors
        public DevController(IProductService productService)
        {
            this.productService = productService;
        }
        #endregion
        #region Methods
        [HttpGet]
        public FileContentResult AllProductsCsv()
        {
            IList<Product> products = productService.GetAllProducts();
            var sb = new StringBuilder();

            sb.Append("Id,PartNumber\n");

            foreach(var product in products)
            {
                sb.AppendFormat("{0},{1}\n", product.Id, product.PartNumber);
            }
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv");
        }
        #endregion
    }
}
