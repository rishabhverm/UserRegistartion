using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using user.bussiness.Abstartion;
using user.bussiness.DTO;
using user.bussiness.Entity;

namespace User.Client.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetAllusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                return Ok(new ResultDTO { Status = true, StatusCode = StatusCodes.Status200OK, Message = "All Users Retrieved successfully", data = users });
            }
            catch (Exception ex)
            {
                return Ok(new ResultDTO { Status = false, StatusCode = StatusCodes.Status500InternalServerError, Message = "Internal Server Error " + ex.Message, data = null });
            }
        }

        [HttpGet("Generate the ExcelFile for All User")]
        public async Task<FileResult> ExportinExecl()
        {

            var people = await _userRepository.GetAllUsers();
            var file = "user.xlsx";
            return GenerateExcel(file, people);
        }

        [HttpGet("download-user-excel/{userId}")]
        public async Task<IActionResult> DownloadUserExcel(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);

                if (user == null)
                {
                    return NotFound(new ResultDTO { Status = false, StatusCode = StatusCodes.Status404NotFound, Message = "User not found", data = null });
                }

                using (var stream = new MemoryStream())
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.Add("UserDetails");

                        // Set column headers
                        worksheet.Cells["A1"].Value = "Id";
                        worksheet.Cells["B1"].Value = "Name";
                        worksheet.Cells["C1"].Value = "Email";
                        worksheet.Cells["D1"].Value = "City";
                        worksheet.Cells["E1"].Value = "Region";

                        // Set user details in the worksheet
                        worksheet.Cells["A2"].Value = user.UserId;
                        worksheet.Cells["B2"].Value = user.UserName;
                        worksheet.Cells["C2"].Value = user.Email;
                        worksheet.Cells["D2"].Value = user.City;
                        worksheet.Cells["E2"].Value = user.Region;

                        package.Save();
                    }

                    stream.Position = 0;

                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"user_{userId}.xlsx");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDTO { Status = false, StatusCode = StatusCodes.Status500InternalServerError, Message = "Internal Server Error " + ex.Message, data = null });
            }
        }

      
        private FileResult GenerateExcel(string fileName,IEnumerable<Users>users)
        {
            DataTable dataTable = new DataTable("User");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id"),
                new DataColumn("Name"),
                new DataColumn("Email"),
                new DataColumn("City"),
                new DataColumn("Region")
            });
            foreach(var people in users)
            {
                dataTable.Rows.Add(people.UserId, people.UserName, people.Email, people.City, people.Region);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream()) 
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.speardsheetml.sheet",fileName);  
                }
            }
        }
    }
}
