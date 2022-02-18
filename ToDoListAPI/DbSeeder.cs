using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI;
using ToDoListAPI.Data;
using ToDoListAPI.Entities;

namespace ToDoListApi
{
    public class DbSeeder
    {
        private readonly DataContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public DbSeeder(DataContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (_dbContext.Database.IsRelational())
                {
                    var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                    if (pendingMigrations != null && pendingMigrations.Any())
                    {
                        _dbContext.Database.Migrate();
                    }
                }

                var adminUser = _dbContext.Users.Where(x => x.Email == "admin");

                if (adminUser == null)
                {
                    var admin = new User
                    {
                        DateOfBirth = DateTime.Now,
                        Email = "admin",
                        FirstName = "admin",
                        LastName = "admin",
                        Nationality = "admin",
                        RoleId = 3
                    };
                    string hashedPassword = _passwordHasher.HashPassword(admin, "admin123");

                    admin.PasswordHash = hashedPassword;
                    _dbContext.Users.Add(admin);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Statuses.Any())
                {
                    var statuses = GetStatuses();
                    _dbContext.Statuses.AddRange(statuses);
                    _dbContext.SaveChanges();
                }


            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };

            return roles;
        }

        private IEnumerable<Status> GetStatuses()
        {
            var statuses = new List<Status>()
            {
                new Status()
                {
                    StatusOption = "In progress"
                },
                new Status()
                {
                    StatusOption = "Done"
                },
                new Status()
                {
                    StatusOption = "Just started"
                },
            };

            return statuses;
        }


    }
}
