using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickSplit.Application.Users.Commands.AddFriendCommand;
using QuickSplit.Application.Users.Commands.CreateUser;
using QuickSplit.Application.Users.Commands.DeleteUser;
using QuickSplit.Application.Users.Commands.UpdateUser;
using QuickSplit.Application.Users.Models;
using QuickSplit.Application.Users.Queries.GetFriends;
using QuickSplit.Application.Users.Queries.GetUserById;
using QuickSplit.Application.Users.Queries.GetUsers;

namespace QuickSplit.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [Authorize]
        [HttpGet(Name = "GetUser")]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get([FromQuery] string find, [FromQuery] int? excludeFriendsOfId)
        {
            IEnumerable<UserModel> users = await Mediator.Send(new GetUsersQuery()
            {
                SearchNameQuery = find,
                NotFriendWithQuery = excludeFriendsOfId
            });
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            UserModel user = await Mediator.Send(new GetUserByIdQuery
            {
                Id = id
            });
            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand user)
        {
            UserModel created = await Mediator.Send(user);
            return CreatedAtRoute("GetUser", created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Put(int id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            UserModel updated = await Mediator.Send(command);
            return Ok(updated);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteUserCommand
            {
                Id = id
            });

            return Ok();
        }

        [Authorize]
        [HttpGet("{id}/friends")]
        public async Task<IActionResult> GetFriends(int id)
        {
            IEnumerable<UserModel> friends = await Mediator.Send(new GetFriendsQuery() { UserId = id });
            return Ok(friends);
        }

        [Authorize]
        [HttpPost("{id}/friends")]
        public async Task<IActionResult> AddFriend(int id, [FromBody] AddFriendCommand command)
        {
            command.CurrentUserId = id;
            await Mediator.Send(command);
            return Ok();
        }
    }
}