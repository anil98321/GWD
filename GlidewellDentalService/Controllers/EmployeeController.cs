using GlidewellDentalService.Contexts;
using GlidewellDentalService.Model;
using GlidewellDentalService.Repository;
using GlidewellDentalService.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlidewellDentalService.Controllers
{
    [Route("glidewelldental/api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _path;
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly EmployeeDbContext _context;
        public EmployeeController(IWebHostEnvironment env)
        {
            _env = env;
            _path = Path.Combine(_env.ContentRootPath, @"App_Data\Employee.json");
        }

        // For Database
        //public EmployeeController(IWebHostEnvironment env, IEmployeeRepository employeeRepository, EmployeeDbContext context)
        //{
        //    _env = env;
        //    _path = Path.Combine(_env.ContentRootPath, @"App_Data\Employee.json");
        //    _employeeRepository = employeeRepository;
        //    _context = context;
        //}

        /// <summary>
        /// Get employyee list => GET: api/<EmployeeController>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<EmployeeModel> Get()
        {
            // construct the path to the notepad file 
            List<EmployeeModel> model = JsonFileUtils.PrettyRead<EmployeeModel>(_path);
            return model;
        }

        /// <summary>
        /// Get detail of the employee => GET api/<EmployeeController>/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public EmployeeModel Get(int id)
        {
            List<EmployeeModel> model = JsonFileUtils.PrettyRead<EmployeeModel>(_path);
            return model.Find(vf => vf.Id == id.ToString());
        }

        /// <summary>
        /// Add new employee => POST api/<EmployeeController>
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeModel value)
        {
            List<EmployeeModel> model = JsonFileUtils.PrettyRead<EmployeeModel>(_path);
            string id = string.Empty;
            id = GenerateEmployeeId(model);
            if (!string.IsNullOrEmpty(id)) 
            {
                value.Id = id;
                model.Add(value);
                JsonFileUtils.PrettyWrite(model, _path);
            }
            var result = new { id = id };
            Response.StatusCode = StatusCodes.Status201Created;
            return new JsonResult(result);
        }

        /// <summary>
        /// Update an employee => PUT api/<EmployeeController>/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EmployeeModel value)
        {
            List<EmployeeModel> model = JsonFileUtils.PrettyRead<EmployeeModel>(_path);
            if (model.Exists(vf => vf.Id == id.ToString()))
            {
                var mod = model.Find(vf => vf.Id == id.ToString());
                mod.Id = value.Id;
                mod.FirstName = value.FirstName;
                mod.LastName = value.LastName;
                mod.Email = value.Email;
                mod.MonthlySalary = value.MonthlySalary;
                JsonFileUtils.PrettyWrite(model, _path);
            }
            var result = new { id = id };
            Response.StatusCode = StatusCodes.Status200OK;
            return new JsonResult(result);
        }

        /// <summary>
        /// Delete an employee => DELETE api/<EmployeeController>/5
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            List<EmployeeModel> model = JsonFileUtils.PrettyRead<EmployeeModel>(_path);
            if (model.Exists(vf => vf.Id == id.ToString()))
            {
                model.Remove(model.Find(vf => vf.Id == id.ToString()));
                JsonFileUtils.PrettyWrite(model, _path);
            }
        }

        /// <summary>
        /// Generate Employee ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GenerateEmployeeId(List<EmployeeModel> model) 
        {           
            Random a = new Random();
            string id = a.Next(100, 100000).ToString();
            return id = !model.Exists(vf => vf.Id == id.ToString()) ? id : GenerateEmployeeId(model);
        }

    }
}
