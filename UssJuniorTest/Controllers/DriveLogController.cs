using Microsoft.AspNetCore.Mvc;
using UssJuniorTest.Core.Services;
using UssJuniorTest.Core.Utilities;

namespace UssJuniorTest.Controllers;

[Route("api/driveLog")]
public class DriveLogController : Controller
{
    private DriveLogService _service;

    public DriveLogController(DriveLogService service)
    {
        _service = service;
    }

    // TODO
    // public ??? GetDriveLogsAggregation(??? args)
    // {
    // return ???    
    // }

    [HttpGet("logs")]
    public IActionResult GetDriveLogsAggregation([FromQuery] QueryParameters args)
    {
        if (args == null) { return BadRequest(); }
        try
        {
            var aggregation = _service.GetDriveLogs(args);
                return Ok(aggregation);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }
}