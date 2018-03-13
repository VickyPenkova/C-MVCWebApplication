using Newtonsoft.Json;
namespace ApiMessages
{
    // This class is going to serve as a message qhen talking to the Api later
    public class CustomerDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("numberOfProducts")]
        public int NumberProducts { get; set; }

        public CustomerDTO(int id, string name, int numProducts)
        {
            this.Id = id; 
            this.Name = name;
            this.NumberProducts = numProducts;
        }
    }
}