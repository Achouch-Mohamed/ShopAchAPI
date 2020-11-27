using ShopAch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopAch.Controllers
{    
    public class UsersController : ApiController
    {
        DbShoppingEntities db = new DbShoppingEntities();

        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {       
            return Ok(db.Users.ToList());
        }

        [HttpPost]
        public IHttpActionResult PostUsers(Users user)
        {
            db.Users.Add(user);
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return Ok("Users Add Succesfully");
            }
            else
            {
                return BadRequest("Les informations incorrect");
            }
        }
        [HttpPut]
        public IHttpActionResult PutUsers(Users user,int id)
        {
            if (id <= 0)
            {
                BadRequest("Id Incorrect");
            }

            var Users = db.Users.Where(m => m.Id_User == id).FirstOrDefault();
         
            if (Users!=null)
            {
                Users.Id_User = db.Users.Count() + 1;
                Users.FirstName_User = user.FirstName_User;
                Users.LastName_User = user.LastName_User;
                Users.Adresse_User = user.Adresse_User;
                Users.Adresse_User = user.Adresse_User;
                Users.Password_User = user.Password_User;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok("Users Update Succesfully");
            }
            else
            {
                return BadRequest("Update Failed");
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteUsers(int id)
        {
            if (id <= 0)

                return BadRequest(" Id Incorrect");

            var user = db.Users.Where(m => m.Id_User == id).FirstOrDefault();

            db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok(db.Users.ToList());
         
        }
      
        [Route("Login")]
        [HttpPost]
        public IHttpActionResult Login(string E_mail, string Password)
        {
            Users uti = new Users();
            var log = db.Users.Where(m => m.Email_User.Equals(E_mail) && m.Password_User.Equals(Password)).FirstOrDefault();

            if (log == null)
            {
                return BadRequest("Login Failed");
            }
            else
            {

                return Ok("Login Successfully");
            }
        }
    }
}
