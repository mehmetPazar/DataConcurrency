using DataConcurrency.Data;
using DataConcurrency.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataConcurrency.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly SchoolDbContext _dbContext;

    public StudentController(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Student request)
    {
        await _dbContext.Students.AddAsync(request);
        await _dbContext.SaveChangesAsync();
        return StatusCode(200, true);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await _dbContext.Students.ToListAsync();
        return StatusCode(200, this._dbContext.Students.ToList());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Student student)
    {
        try
        {
            Student studentDb = await _dbContext.Students.FindAsync(id);
            studentDb.Name = student.Name;
            studentDb.Surname = student.Surname;

            return StatusCode(200,await _dbContext.SaveChangesAsync());
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var entry = ex.Entries.Single();
            var clientValues = (Student)entry.Entity;
            var databaseEntry = entry.GetDatabaseValues();

            if (databaseEntry == null)
            {
                Console.WriteLine("Bu ürün silinmiş.");
            }
            else
            {
                var databaseValues = (Student)databaseEntry.ToObject();

                Console.WriteLine($"Client Value: {clientValues.Name}{clientValues.Surname}");
                Console.WriteLine($"Database Value: {databaseValues.Name}{databaseValues.Surname}");
            }
            
            return BadRequest();
        }
    }
}