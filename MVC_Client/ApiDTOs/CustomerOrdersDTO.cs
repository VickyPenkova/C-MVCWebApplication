using Newtonsoft.Json;
using System.Collections.Generic;

namespace MVC_Client.ApiDTOs
{
    public class CustomerOrdersDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("orderList")]
        public List<OrdersDTO> OrderList;

        public CustomerOrdersDTO(int id, string name, List<OrdersDTO> orderList)
        {
            this.Id = id;
            this.Name = name;
            this.OrderList = orderList;
        }
    }
}
