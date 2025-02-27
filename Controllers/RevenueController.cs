using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/revenue")]
[ApiController]
public class RevenueController : ControllerBase
{
    private static readonly List<RevenueData> MockData = new()
    {
        new RevenueData { Date = new DateTime(2021, 06, 01), Income = 200, Outcome = 50 },
        new RevenueData { Date = new DateTime(2021, 06, 02), Income = 300, Outcome = 100 },
        new RevenueData { Date = new DateTime(2021, 06, 03), Income = 250, Outcome = 70 },
        new RevenueData { Date = new DateTime(2021, 06, 04), Income = 400, Outcome = 150 },
        new RevenueData { Date = new DateTime(2021, 06, 05), Income = 500, Outcome = 200 },
    };

    [HttpGet]
    public ActionResult<IEnumerable<RevenueData>> GetRevenue([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        var filteredData = MockData
            .Where(d => d.Date >= from && d.Date <= to)
            .Select(d => new RevenueData
            {
                Date = d.Date,
                Income = d.Income,
                Outcome = d.Outcome,
                Revenue = d.Income - d.Outcome
            })
            .ToList();

        return Ok(filteredData);
    }
}

public class RevenueData
{
    public DateTime Date { get; set; }
    public decimal Income { get; set; }
    public decimal Outcome { get; set; }
    public decimal Revenue { get; set; }
}
