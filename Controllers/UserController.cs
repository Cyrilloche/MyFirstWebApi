using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApi.Context;
using MyFirstWebAPI.Models;

namespace MyFirstWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {


        private MyFirstWebApiDbContext _context;

        public UserController(MyFirstWebApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = await _context.Users.ToListAsync();
            return users;
        }

        // GET Method
        [HttpGet("{id}")]
        public async Task<User> GetUserById(int id)
        {
            User user = await _context.Users.FindAsync(id);
            return user;
        }

        [HttpGet("Moins_de_25_ans")]
        public async Task<List<User>> GetUserUnder25()
        {
            List<User> users = await _context.Users.Where(user => user.Age <25).ToListAsync();
            return users;
        }

        // POST Method
        [HttpPost]
        public async Task<User> AddUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // PUT Method
        [HttpPut("{id}")]
        public async Task<User> ModifyUser(int id, User currentUser)
        {
            User user = await GetUserById(id);
            if(user != null)
            {
                _context.Entry(currentUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return user;
        }

        // DELETE Method
        [HttpDelete]
        public async Task DeleteUser(int id)
        {
            User user = await GetUserById(id);
            if(user != null)
                _context.Remove(user);
        }
    }
}