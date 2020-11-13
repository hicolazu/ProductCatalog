using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
    [Route("v1/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly StoreDataContext _context;

        public CategoryController(StoreDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public IEnumerable<Category> Get()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        [HttpGet("{id}")]
        public Category Get(int id)
        {
            // Find() ainda não suporta AsNoTracking
            return _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpGet("{id}/products")]
        [ResponseCache(Duration = 30)]
        public IEnumerable<Product> GetProducts(int id)
        {
            return _context.Products.AsNoTracking().Where(x => x.CategoryId == id).ToList();
        }

        [HttpPost]
        public Category Post([FromBody]Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }

        [HttpPut]
        public Category Put([FromBody]Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            _context.SaveChanges();

            return category;
        }

        [HttpDelete]
        public Category Delete([FromBody]Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return category;
        }
    }
}