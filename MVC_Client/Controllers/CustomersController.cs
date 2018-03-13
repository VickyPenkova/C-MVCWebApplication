using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApiMessages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MVC_Client.ApiDTOs;

namespace MVC_Client.Controllers
{
    public class CustomersController : Controller
    {
        static HttpClient client = new HttpClient();

        public static List<CustomerDTO> customersInfoList;

        //GET /customers
        public IActionResult Index()
        {
            Task<List<CustomerDTO>> customersTask = GetCustomersInfoFromWebApiAsync();

            List<CustomerDTO> customerList = customersTask.Result;

            ViewBag.list = customerList;

            customersInfoList = new List<CustomerDTO>();

            foreach (var customer in customerList)
            {
                customersInfoList.Add(new CustomerDTO(customer.Id, customer.Name, customer.NumberProducts));
            }

            return View(customerList);
        }

        public List<CustomerDTO> GetUsers(string currentLookupName)
        {
            if(customersInfoList == null)
            {
                return new List<CustomerDTO>();      
            } else if (currentLookupName == null || currentLookupName.Equals(""))
            {
                return customersInfoList;
            }
            
            return customersInfoList.
                FindAll(current => current.Name.ToLower().
                StartsWith(currentLookupName.ToLower()));
        }

        public async Task<List<string>> GetCustomersFromWebApiAsync()
        {
            try
            {
                var resultList = new List<String>();
                var url = "http://localhost:54163/";
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                var getDataTask = client.GetAsync("http://localhost:54163/").
                ContinueWith(response =>
                {
                    var result = response.Result;

                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readResult = responseMessage.Content.ReadAsStringAsync().Result; 
                        var customerNames = JsonConvert.DeserializeObject<List<string>>(readResult);

                        resultList = customerNames;
                    }
                });
                //waint for async method to complete
                getDataTask.Wait();

                return await Task.Run(() => resultList.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<CustomerDTO>> GetCustomersInfoFromWebApiAsync()
        {
            try
            {
                var resultList = new List<CustomerDTO>();
                var url = "http://localhost:54163/customers/AllCustomersInfo";
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                var getDataTask = client.GetAsync(url).
                ContinueWith(response =>
                {
                    var result = response.Result;

                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readResult = responseMessage.Content.ReadAsStringAsync().Result;
                        var customerNames = JsonConvert.DeserializeObject<List<CustomerDTO>>(readResult);

                        resultList = customerNames;
                    }
                });
                //wait for async method to complete
                getDataTask.Wait();

                return await Task.Run(() => resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CustomerOrdersDTO ShowUserData(int userId, string name)
        {
            List<OrdersDTO> orderList = ShowUserDataFromWebApiAsync(userId).Result;
            return new CustomerOrdersDTO(userId, name, orderList);
        }

        //Http request to customer/{customerId}/orders
        private async Task<List<OrdersDTO>> ShowUserDataFromWebApiAsync(int id)
        {
            try
            {
                var resultList = new List<OrdersDTO>();
                var url = "http://localhost:54163/customer/" + id + "/orders";
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                var getDataTask = client.GetAsync(url).
                ContinueWith(response =>
                {
                    var result = response.Result;

                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readResult = responseMessage.Content.ReadAsStringAsync().Result;
                        var customerNames = JsonConvert.DeserializeObject<List<OrdersDTO>>(readResult);

                        resultList = customerNames;
                    }
                });
                //waint for async method to complete
                getDataTask.Wait();

                return await Task.Run(() => resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}