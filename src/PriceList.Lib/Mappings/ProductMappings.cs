using PriceList.Lib.Dto;

namespace PriceList.Lib.Mappings
{
    public static class ProductMappings
    {
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                PartNumber = product.PartNumber,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }
        public static Product ToProduct(this ProductDto productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                PartNumber = productDto.PartNumber,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            };
        }

        public static CsvProductDevExport ToDevExport(this Product product)
        {
            return new CsvProductDevExport
            {
                Id = product.Id,
                PartNumber = product.PartNumber
            };
        }

        public static Product ToProduct(this CsvProduct csvProduct)
        {
            return new Product
            {
                PartNumber = csvProduct.PartNumber,
                Name = csvProduct.Name,
                Price = csvProduct.Price,
                Description = csvProduct.Description,
                MinimumPrice = csvProduct.MinimumPrice,
                Comment = csvProduct.Comment
            };
        }
    }
}
