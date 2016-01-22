﻿using BusinessLayer.DotService;
using BusinessLayer.TestGenerator;
using BusinessLayer.UserService;
using Data;
using MarkTheWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MarkTheWorld.Controllers.Api
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        private readonly UserService userService;

        public UserController()
        {
            userService = new UserService();
        }

        [Route("addUser")]
        [HttpPost]
        public IHttpActionResult PostUser(User User)
        {
            UserRegistrationModel userCopy = new UserRegistrationModel();
            if (User.UserName == null || User.PasswordHash == null)
                return Ok(userCopy);
            try
            {
                userCopy = userService.addUser(User);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(userCopy);
        }

        [Route("generate")]
        [HttpGet]
        public IHttpActionResult GenerateUsers()
        {
            
            try
            {
                GenerateObjects generate = new GenerateObjects();
                generate.GenerateXUsersWithYDots(5, 100);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(true);
        }

        [Route("getUser")]
        [HttpPost]
        public IHttpActionResult GetUsers(User user)
        {
            UserRegistrationModel userCopy = new UserRegistrationModel();
            try
            {
                userCopy = userService.getOne(user);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(userCopy);
        }

        [Route("topList")]
        [HttpGet]
        public IHttpActionResult GetTopUsers()
        {
            List<TopUser> users = new List<TopUser>();
            try
            {
                users = userService.getTopUsers();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(users);
        }

    }
}