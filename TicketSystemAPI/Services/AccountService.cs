﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TicketSystemAPI.Entities.Dto;
using TicketSystemAPI.Data;
using TicketSystemAPI.Entities;
using TicketSystemAPI.Exceptions;

namespace TicketSystemAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(UserRegisterDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly DataContext _dataContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(DataContext dataContext, IPasswordHasher<User> passwordHasher)
        {
            _dataContext = dataContext;
            _passwordHasher = passwordHasher;
        }
        public void RegisterUser(UserRegisterDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                RoleId = (int)RoleTypes.User,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
            
            };
            
            if(_dataContext.Users.Where(m => m.Email == newUser.Email).Any())
            { 
                throw new BadRequestException("Email already exists"); 
            }

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            _dataContext.Users.Add(newUser);
            _dataContext.SaveChanges();
        }
    }
}
