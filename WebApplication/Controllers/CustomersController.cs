using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using WebApplication.Models;
using WebApplication.ApiDTOs;

namespace WebApplication.Controllers
{
    public class CustomersController : ApiController
    {
        private EntityDbModel db = new EntityDbModel();

        // GET: /customers
        public List<string> Getcustomers()
        {
            IQueryable<string> names = from b in db.customers
                                        select b.name;
            return names.ToList();
        }

        [Route("customers/AllCustomersInfo")]
        [HttpGet]
        public List<CustomerDTO> AllCustomersInfo()
        {

            IQueryable<int> numProducts = from b in db.customers
                                            select b.orders.Count;
           
            IQueryable<string> nameCustomers = from b in db.customers
                                                select b.name;

            IQueryable<int> idCustomers = from b in db.customers
                                            select b.id;

            List<int> userIdList = idCustomers.ToList();

            List<string> nameCustomersList = nameCustomers.ToList();

            var productsArr = numProducts.ToArray(); 

            List<CustomerDTO> customersInfoList = new List<CustomerDTO>();

            for(int i = 0; i < nameCustomersList.Count; i++)
            {
                customersInfoList.Add(
                    new CustomerDTO(userIdList[i], nameCustomersList[i], productsArr.ElementAt(i))
                    );
            }

            return customersInfoList;
        }
    }
}