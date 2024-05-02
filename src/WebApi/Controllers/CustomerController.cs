using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase { }