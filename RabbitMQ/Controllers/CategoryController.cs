using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Contexts;
using RabbitMQ.Entities;
using RabbitMQ.Models;
using RabbitMQ.Services;

namespace RabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly RabbitMQContext _context;
        private readonly RabbitMQService _service;
        public CategoryController(RabbitMQContext context, RabbitMQService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _context.Categories;
            _service.SendLog(new Log { LogType = LogTypes.Success, Message = "Returned Records Successfully." });
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Post(CategoryCreateModel ccm)
        {
            Category category = new Category
            {
                Name = ccm.Name,
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            _service.SendLog(new Log { LogType = LogTypes.Success, Message = $"{category.Name} Added Successfully." });
            return Ok(category);
        }
    }
}
