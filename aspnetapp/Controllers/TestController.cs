#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspnetapp.Models;


namespace aspnetapp.Controllers
{
    [Route("Test/")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public TestController(DataBaseContext context)
        {
            _context = context;
        }
        [HttpGet("GetCurrentDate")]
        public string GetCurrentDate()
        {
            return DateTime.Now.ToString();
        }

        [HttpPost("AddProduct")]
        public async Task<string> AddProduct(Product product)
        {
            var res = _context.Products.Add(product);
            var a = await _context.SaveChangesAsync();
            return "";
        }

        [HttpGet("GetProduct")]
        public async Task<List<Product>> GetProduct(string Name)
        {
            var res = _context.Products.Where(i=>i.Name == Name).ToList();
            await _context.SaveChangesAsync();
            return res;
        }

        [HttpGet("UpdateProduct")]
        public async Task<List<Product>> UpdateProduct(string Name)
        {
            _context.Products.Where(i => i.Name == Name).ToList().ForEach(i => { i.Price += 1; });
            await _context.SaveChangesAsync();
            return _context.Products.Where(i => i.Name == Name).ToList();
        }

        [HttpGet("CallSPTest")]
        public List<Product> CallSPTest(string Name)
        {
            var res = _context.GetEmployeeDetails(Name);
            return res;
        }
    }
}
