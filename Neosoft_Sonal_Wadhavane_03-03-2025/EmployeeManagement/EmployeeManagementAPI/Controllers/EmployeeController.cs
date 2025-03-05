using Microsoft.AspNetCore.Mvc;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            return employee;
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // PUT: api/Employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            //if (id != employee.Id) return BadRequest();
            //_context.Entry(employee).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            //return NoContent();
            if (id != employee.Id) return BadRequest();

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null) return NotFound();

            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //[HttpPost("upload/{employeeId}")]
        //public async Task<IActionResult> UploadProfilePicture(int employeeId, IFormFile file)
        //{
        //    //if (file == null || file.Length == 0)
        //    //    return BadRequest("Invalid file.");

        //    //var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Employee/");

        //    //if (!Directory.Exists(uploadsFolder))
        //    //{
        //    //    Directory.CreateDirectory(uploadsFolder);
        //    //}

        //    //var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //    //var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //    //using (var stream = new FileStream(filePath, FileMode.Create))
        //    //{
        //    //    await file.CopyToAsync(stream);
        //    //}

        //    //string relativePath = "/Uploads/Employee/" + uniqueFileName; // Store this in DB

        //    //return Ok(new { FilePath = relativePath });
        //    if (file == null || file.Length == 0)
        //        return BadRequest("Invalid file.");

        //    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Employee/");
        //    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

        //    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    var employee = await _context.Employees.FindAsync(employeeId);
        //    if (employee == null) return NotFound();

        //    employee.ProfileImage = "/Uploads/Employee/" + uniqueFileName;
        //    await _context.SaveChangesAsync();

        //    return Ok(new { FilePath = employee.ProfileImage });
        //}
        [HttpPost("upload/{employeeId}")]
        public async Task<IActionResult> UploadProfilePicture(int employeeId, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Employee/");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null) return NotFound();

            employee.ProfileImage = "/Uploads/Employee/" + uniqueFileName;
            await _context.SaveChangesAsync();

            return Ok(new { FilePath = employee.ProfileImage });
        }


    }
}
