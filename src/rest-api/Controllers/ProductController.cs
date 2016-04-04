using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using PriceList.Lib;
using PriceList.Services.Interfaces;
using PriceList.Lib.Dto;
using PriceList.Lib.Mappings;

namespace rest_api.Controllers
{
    [Route("api/[controller]")]
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
        // Reads a Product
        [HttpGet("{id}")]
        public ProductDto Get(int id)
        {
            return productService.Read(id).ToProductDto();
        }

        // POST api/values
        // Creates a new Product
        [HttpPost]
        public IActionResult Post([FromBody]ProductDto productDto)
        {
            ProductDto newProduct = productService.Create(productDto.ToProduct()).ToProductDto();
            return Created("/api/product/" + newProduct.Id + "/", newProduct);
        }

        // PUT api/values/5
        // Updates a product
        [HttpPut]
        public IActionResult Put([FromBody]ProductDto productDto)
        {
            try
            {
                return Ok(productService.Update(productDto.ToProduct()).ToProductDto());
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // DELETE api/values/5
        // Deletes a product
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                productService.Delete(id);
                return Ok();
            }
            catch
            {
                return HttpNotFound();
            }

        }
    }
}
