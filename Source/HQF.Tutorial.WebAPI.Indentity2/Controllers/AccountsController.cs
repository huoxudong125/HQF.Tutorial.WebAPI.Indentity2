using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HQF.Tutorial.WebAPI.Indentity2.Infrastructure;
using HQF.Tutorial.WebAPI.Indentity2.Models;
using Microsoft.AspNet.Identity;

namespace HQF.Tutorial.WebAPI.Indentity2.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        [Authorize]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(AppUserManager.Users.ToList().Select(u => TheModelFactory.Create(u)));
        }

        [Authorize]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();
        }

        [Authorize]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();
        }


        /// <summary>
        /// </summary>
        /// <remarks>
        ///     Post Body eg.
        ///     {
        ///     "Email":"works4you@163.com",
        ///     "UserName":"test",
        ///     "Password":"test@123",
        ///     "ConfirmPassword":"test@123",
        ///     "FirstName":"QF",
        ///     "LastName":"Huo",
        ///     }
        /// </remarks>
        /// <param name="createUserModel"></param>
        /// <returns></returns>
        [Route("create")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date
            };

            var addUserResult = await AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            //Rest of code is removed for brevity

            var code = await AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new {userId = user.Id, code}));

            await
                AppUserManager.SendEmailAsync(user.Id, "Confirm your account",
                    "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");


            var locationHeader = new Uri(Url.Link("GetUserById", new {id = user.Id}));

            return Created(locationHeader, TheModelFactory.Create(user));
        }

        /// <summary>
        ///     邮箱确认
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            var result = await AppUserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            return GetErrorResult(result);
        }

        /// <summary>
        ///     修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("ChangePassword")]
        [Authorize]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await
                    AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [Authorize]
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)

            var appUser = await AppUserManager.FindByIdAsync(id);

            if (appUser != null)
            {
                var result = await AppUserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
            }

            return NotFound();
        }
    }
}