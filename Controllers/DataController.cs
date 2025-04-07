using AspNetCoreLoggingAPI.Data;
using AspNetCoreLoggingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreLoggingAPI.Filters;

namespace AspNetCoreLoggingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(LoggingActionFilter))] // Logging filter applied to all actions in this controller
    public class DataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// POST /api/data/save
        /// Accepts a JSON array in the format:
        /// [
        ///    {"1": "value1"},
        ///    {"5": "value2"},
        ///    {"10": "value32"}
        /// ]
        /// Converts the data, sorts it by the Code field, clears the table, and saves new records.
        /// </summary>
        [HttpPost("save")]
        public async Task<IActionResult> SaveData([FromBody] Dictionary<string, string>[] inputData)
        {
            var records = new List<DataRecord>();

            // Convert input dictionary to DataRecord objects
            foreach (var dict in inputData)
            {
                foreach (var kvp in dict)
                {
                    if (int.TryParse(kvp.Key, out int code))
                    {
                        records.Add(new DataRecord
                        {
                            Code = code,
                            Value = kvp.Value
                        });
                    }
                    else
                    {
                        return BadRequest($"Invalid code format: {kvp.Key} is not an integer.");
                    }
                }
            }

            // Sort by Code field
            records = records.OrderBy(r => r.Code).ToList();

            // Clear the DataRecords table before saving
            _context.DataRecords.RemoveRange(_context.DataRecords);
            await _context.SaveChangesAsync();

            // Save new data
            await _context.DataRecords.AddRangeAsync(records);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Data saved successfully." });
        }

        /// <summary>
        /// GET /api/data/data?codeFilter=5
        /// Returns a list of records.
        /// If the "codeFilter" parameter is provided, it filters records by the Code field.
        /// </summary>
        [HttpGet("data")]
        public async Task<IActionResult> GetData([FromQuery] int? codeFilter)
        {
            IQueryable<DataRecord> query = _context.DataRecords;

            // Apply filtering if the parameter is specified
            if (codeFilter.HasValue)
            {
                query = query.Where(r => r.Code == codeFilter.Value);
            }

            var data = await query.OrderBy(r => r.Code).ToListAsync();
            return Ok(data);
        }
    }
}
