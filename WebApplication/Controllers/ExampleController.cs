using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace WebApplication.Controllers;

[ApiController]
[Route("")]
public class ExampleController : ControllerBase
{
    [HttpGet]
    [OutputCache(Duration = 0, NoStore = true)]
    public IEnumerable<string> Memory()
    {
        List<string> list = new();

        for (var i = 0; i < 1_000_000; i++)
        {
            list.Add(i.ToString());
        }

        return list;
    }

    [HttpGet("collect", Name = "Garbage collect")]
    public void Collect()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
