using Newtonsoft.Json;

namespace WebApplication.ApiDTOs
{
    public class CustomerDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("numberOfProducts")]
        public int NumberProducts { get; set; }

        public CustomerDTO()
        {
        }

        public CustomerDTO(int id, string name, int numberOfProducts)
        {
            this.Id = id;
            this.Name = name;
            this.NumberProducts = numberOfProducts;
        }
    }
}

