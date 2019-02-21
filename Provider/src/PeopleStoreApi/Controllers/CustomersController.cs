using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleStoreApi.Services;

namespace PeopleStoreApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var customer = _service.Get(id);

            return new ObjectResult(customer)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

    }
}
