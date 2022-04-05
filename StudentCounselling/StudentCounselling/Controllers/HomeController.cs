using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentCounselling.Context;
using StudentCounselling.Dtos;
using StudentCounselling.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCounselling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private StudentCouncellingDbContext _dataBase;

        public HomeController(StudentCouncellingDbContext dataBase)
        {
            this._dataBase = dataBase;
        }


        [HttpGet, Route("GetRoleList/{searchText?}")]
        public IEnumerable<Role> GetRoleList(string searchText)
        {
            var role = this._dataBase.Role.Where(t => t.Description.ToLower().Contains(searchText.ToLower()) || searchText == null).ToList();
            return role;
        }
        [HttpGet, Route("GetDepartmentList/{searchText?}")]
        public IEnumerable<Department> GetDepartmentList(string searchText)
        {
            var department = this._dataBase.Department.Where(t => t.Description.ToLower().Contains(searchText.ToLower()) || t.Code.ToLower().Contains(searchText.ToLower()) 
            || searchText == null).ToList();
            return department;
        }
        [HttpGet, Route("GetAddressList/{searchText?}")]
        public IEnumerable<Address> GetAddressList(string searchText)
        {
            var address = this._dataBase.Address.Where(t => t.Description.ToLower().Contains(searchText.ToLower()) || t.Region.ToLower().Contains(searchText.ToLower())
            || t.City.ToLower().Contains(searchText.ToLower()) || t.SubCity.ToLower().Contains(searchText.ToLower())
            || t.Wereda.ToLower().Contains(searchText.ToLower()) || t.Kebele.ToLower().Contains(searchText.ToLower())
            || t.HouseNumber.ToLower().Contains(searchText.ToLower()) || searchText == null).Include(r=>r.User).ToList();
            return address;
        }
        [HttpGet, Route("GetUserList/{searchText?}")]
        public IEnumerable<User> GetUserList(string searchText)
        {
            var user = this._dataBase.User.Where(t => t.UserName.ToLower().Contains(searchText.ToLower()) || t.UserNumber.ToLower().Contains(searchText.ToLower())
            || t.FirstName.ToLower().Contains(searchText.ToLower()) || t.LastName.ToLower().Contains(searchText.ToLower()) || t.MiddleName.ToLower().Contains(searchText.ToLower())
            || t.PhoneNumber.ToLower().Contains(searchText.ToLower()) || t.EmailAddress.ToLower().Contains(searchText.ToLower())
            || searchText == null).Include(p => p.Role).Include(p => p.Department).ToList();

            return user;
        }
        [HttpGet, Route("GetUserById/{userId}")]
        public User GetUserById(string userId)
        {
            var user = this._dataBase.User.Where(t => t.UserId == userId).Include(p => p.Role).Include(p => p.Department).FirstOrDefault();
            if (user != null)
            {;
                return user;
            }
            return null;
        }
        [HttpPost, Route("CreateUser")]
        public string CreateUser(CreateUserModel user)
        {
            if (user.FirstName == null || user.LastName == null || user.UserName == null || user.UserNumber == null || user.Gender == null)
            {
                return BadRequest(error: "Please Fill The Requered Filds!").ToString();
            }
            User participant = new User();
            participant.FirstName = user.FirstName;
            participant.MiddleName = user.MiddleName;
            participant.LastName = user.LastName;
            participant.UserName = user.UserName;
            participant.Password = user.Password;
            participant.UserNumber = user.UserNumber;
            participant.PhoneNumber = user.PhoneNumber;
            participant.EmailAddress = user.EmailAddress;
            participant.Gender = user.Gender;
            if (user.DepartmentId != null)
            {
                var department = this._dataBase.Department.Where(t => t.DepartmentId == user.DepartmentId).FirstOrDefault();
                participant.Department = department;
            }
            else { participant.Department = null; }
            if (user.DepartmentId != null)
            {
                var role = this._dataBase.Role.Where(t => t.RoleId == user.RoleId).FirstOrDefault();
                participant.Role = role;
            }
            else { participant.Role = null; }
            this._dataBase.User.Add(participant);
            this._dataBase.SaveChanges();
            string id = participant.UserId.ToString();
            return id;
        }
        [HttpPut, Route("UpdateUser")]
        public User UpdateUser(UpdateUserModel user)
        {
            var participant = this._dataBase.User.Where(t => t.UserId == user.UserId).Include(p => p.Role).Include(p => p.Department).FirstOrDefault();
            if (participant != null)
            {
                participant.FirstName = user.FirstName;
                participant.MiddleName = user.MiddleName;
                participant.LastName = user.LastName;
                participant.UserName = user.UserName;
                participant.UserNumber = user.UserNumber;
                participant.Password = user.Password;
                participant.PhoneNumber = user.PhoneNumber;
                participant.EmailAddress = user.EmailAddress;

                if (user.DepartmentId != null)
                {
                    var department = this._dataBase.Department.Where(t => t.DepartmentId == user.DepartmentId).FirstOrDefault();
                    participant.Department = department;
                }
                else { participant.Department = null; }
                this._dataBase.User.Update(participant);
                this._dataBase.SaveChanges();
                return participant;
            }
            return null;
        }
        [HttpDelete,Route("RemoveUser")]
        public string RemoveUser(string userId)
        {
            var user = this._dataBase.User.Where(t=>t.UserId == userId).FirstOrDefault();
            if (user != null)
            { 
                this._dataBase.User.Remove(user);
                this._dataBase.SaveChanges();
                string id = user.UserId.ToString();
                return id;

            }
            return BadRequest(error: "There is no registered user by this id.").ToString();
        }
    }
}
