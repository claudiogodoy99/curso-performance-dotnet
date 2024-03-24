using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Threadpool_Aspnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var threadPoolQueue = ThreadPool.PendingWorkItemCount;
            var threadNumber = ThreadPool.ThreadCount;


            var msg = $"Requisição processada pela thread: {threadId} \n ThreadPool numero de threads: {threadNumber} \n ThreadPool Tasks Pendendetes: {threadPoolQueue}";
            Console.WriteLine(msg);
            return Ok(msg);
        }
    }
}
