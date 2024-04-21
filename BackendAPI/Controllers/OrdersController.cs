using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers;

[ApiController]
[Route("cheto-tam-orders")]
public class OrdersController : Controller
{
    public OrdersController()
    {

    }

    [HttpGet("")]
    public int[] GetData()
    {
        return [1, 2];
    }
}
