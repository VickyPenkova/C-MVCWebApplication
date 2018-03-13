using Newtonsoft.Json;

namespace WebApplication.ApiDTOs
{
    public class OrdersDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sum")]
        public double Sum { get; set; }

        [JsonProperty("numberOfProducts")]
        public int NumberProducts { get; set; }

        [JsonProperty("discontinued")]
        public bool Discontinued { get; set; }

        [JsonProperty("productsInStock")]
        public int ProductsInStock { get; set; }

        public OrdersDTO(string name, double sum, int numberProducts, bool discontinued, int productsInStock)
        {
            this.Name = name;
            this.Sum = sum;
            this.NumberProducts = numberProducts;
            this.Discontinued = discontinued;
            this.ProductsInStock = productsInStock;
        }
    }
}