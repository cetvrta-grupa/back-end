﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
	//[Authorize(Policy = "touristPolicy")]
	[Route("api/user")]
	public class UserController : BaseApiController
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public ActionResult<PagedResult<UserDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
		{
			var result = _userService.GetPaged(page, pageSize);
			return CreateResponse(result);
		}

		[HttpPost]
		public ActionResult<UserDto> Create([FromBody] UserDto user)
		{
			var result = _userService.Create(user);
			return CreateResponse(result);
		}

		[HttpPut("{id:int}")]
		public ActionResult<UserDto> Update([FromBody] UserDto user)
		{
			var result = _userService.Update(user);
			return CreateResponse(result);
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id)
		{
			var result = _userService.Delete(id);
			return CreateResponse(result);
		}

        [HttpPut("update-password/{username}/{password}")]
        public ActionResult<UserDto> UpdatePassword(string username, string password)
        {
            var result = _userService.UpdatePassword(username, password);
            return CreateResponse(result);
        }

        [HttpGet("get-user/{username}")]
        public ActionResult<UserDto> GetUser(string username)
        {
            var result = _userService.GetUserByUsername(username);
            return CreateResponse(result);
        }

        [HttpGet("get-user-by-id/{id:int}")]
        public ActionResult<UserDto> GetUser(int id)
        {
			var result = _userService.GetUserById(id);
            return CreateResponse(result);
        }
    }
}
