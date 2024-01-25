using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Services;

namespace RabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMQController : ControllerBase
    {

        private readonly RabbitMQService rabbitMQService;

        public RabbitMQController(RabbitMQService rabbitMQService)
        {
            this.rabbitMQService = rabbitMQService;
        }
    }
}
