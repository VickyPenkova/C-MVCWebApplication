using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication.ApiDTOs;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CustomerController : ApiController
    {
        private EntityDbModel db = new EntityDbModel();

        // GET: Customer/5
        [ResponseType(typeof(CustomerDTO))]
        public IHttpActionResult Getcustomer(int id)
        {
            CustomerDTO customer = db.customers.
                Include(cust => cust.orders).
                Select(cus =>
                new CustomerDTO()
                {
                    Id = cus.id,
                    Name = cus.name,
                    NumberProducts = cus.orders.Count
                }).SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [Route("customer/{customerId}/orders")]
        [HttpGet]
        public List<OrdersDTO> GetOrdersByCustomer(int customerId)
        {
            IQueryable<Models.order> rtn = from b in db.orders
                                           where b.customer_id == customerId
                                           select b;

            List<OrdersDTO> ordersList = new List<OrdersDTO>();

            rtn.ToList().ForEach(order => ordersList.Add(new OrdersDTO(order.name, order.sum, order.number_products,
                order.discontinued, order.products_in_stock)));

            return ordersList;
        }

        // PUT: Customer/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcustomer(int id, customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.id)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: Customer
        [ResponseType(typeof(customer))]
        public async Task<IHttpActionResult> Postcustomer(customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.customers.Add(customer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (customerExists(customer.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customer.id }, customer);
        }

        // DELETE: Customer/5
        [ResponseType(typeof(customer))]
        public async Task<IHttpActionResult> Deletecustomer(int id)
        {
            customer customer = await db.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.customers.Remove(customer);
            await db.SaveChangesAsync();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customerExists(int id)
        {
            return db.customers.Count(e => e.id == id) > 0;
        }
    }
}