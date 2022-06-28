using BusinessLayer.Interface;
using CommanLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackendProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListBL toDoListBL;
        public ToDoListController(IToDoListBL addressBL)
        {
            this.toDoListBL = addressBL;
        }
       // [Authorize(Roles = Role.User)]
        [HttpPost("AddItem")]
        public IActionResult AddItem(ToDoListModel list)
        {
            try
            {
                var addData = this.toDoListBL.AddItem(list);
                if (addData.Equals("Task Added Successfully"))
                {
                    return this.Ok(new { Status = true, Response = addData });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Response = addData });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { status = false, Response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetList")]
        public IActionResult GetList()
        {
            try
            {
                var updatedBookDetail = this.toDoListBL.GetList();
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "List Fetched Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Priority Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPost("UpdateTask")]
        public IActionResult UpdateTask(UpdateModel update)
        {
            try
            {
                var user = this.toDoListBL.UpdateTask(update);
                if (user != null)
                {
                    return this.Ok(new { Success = true, message = "Task Details Updated Successfully", });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Task details Updated Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask(int ToDoListId)
        {
            try
            {
                if (this.toDoListBL.DeleteTask(ToDoListId))
                {
                    return this.Ok(new { Success = true, message = "Task Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid ToDoList Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

    }
}
