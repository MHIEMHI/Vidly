    using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using  System.Data.Entity;
using System.Web.Http;
    using AutoMapper;
    using Vidly.Dtos;
    using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET : /Api/Customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery =   _context.Customers.Include(c => c.MembershipType);

            if (!string.IsNullOrWhiteSpace(query))
                customersQuery= customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer,CustomerDto>  );

            return Ok(customerDtos);
        }

        //GET : /Api/Customers/1
        public IHttpActionResult GetCustomer(int Id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                NotFound();
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri +"/"+ customer.Id), customerDto);
        }

        //PUT /api/customer/1
        [HttpPut]
        public void updateCustomer(int Id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == Id);
            if(customerInDB == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDB);
            /*
            customerInDB.Name = customerDto.Name;
            customerInDB.BirthDate = customerDto.BirthDate;
            customerInDB.IsSubscribedToNewsLetter = customerDto.IsSubscribedToNewsLetter;
            customerInDB.MembershipTypeId = customerDto.MembershipTypeId;
            */

            _context.SaveChanges();
        }
        //Delete /api/customer/1
        [HttpDelete]
        public void deleteCustomer(int Id)
        { 
            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == Id);
            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customerInDB);
            _context.SaveChanges();
        }
    }
}
