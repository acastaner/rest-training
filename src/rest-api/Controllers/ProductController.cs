using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using PriceList.Lib;
using PriceList.Services.Interfaces;
using PriceList.Lib.Dto;
using PriceList.Lib.Mappings;

namespace rest_api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductController : Controller
    {
        #region Properties
        private IProductService productService;
        #endregion
        #region Constructors
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        #endregion
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Produces(typeof(ProductDto))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(ProductDto))]
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return productService.Read(id);
        }

        // POST api/values
        [Produces(typeof(ProductDto))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(ProductDto))]
        [HttpPost]
        public ProductDto Post([FromBody]ProductDto productDto)
        {
            return productService.Create(productDto.ToProduct()).ToProductDto();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
