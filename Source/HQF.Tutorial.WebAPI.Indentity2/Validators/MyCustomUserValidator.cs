using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HQF.Tutorial.WebAPI.Indentity2.Infrastructure;
using Microsoft.AspNet.Identity;

namespace HQF.Tutorial.WebAPI.Indentity2.Validators
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     if we want to enforce using only the following domains (“outlook.com”, “hotmail.com”, “gmail.com”, “yahoo.com”)
    ///     when the user self registers then we need to create a class and derive it from “UserValidator
    ///     <ApplicationUser>” class
    /// </remarks>
    public class MyCustomUserValidator : UserValidator<ApplicationUser>
    {
        private readonly List<string> _allowedEmailDomains = new List<string>
        {
            "outlook.com",
            "hotmail.com",
            "gmail.com",
            "yahoo.com",
            "163.com"
        };

        public MyCustomUserValidator(ApplicationUserManager appUserManager)
            : base(appUserManager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            var result = await base.ValidateAsync(user);

            var emailDomain = user.Email.Split('@')[1];

            if (!_allowedEmailDomains.Contains(emailDomain.ToLower()))
            {
                var errors = result.Errors.ToList();

                errors.Add(string.Format("Email domain '{0}' is not allowed", emailDomain));

                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}