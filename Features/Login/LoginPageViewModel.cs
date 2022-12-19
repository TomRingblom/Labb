using Labb.Infrastructure.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Labb.Features.Login
{
    public class LoginPageViewModel
    {
        [Required]
        [NameTest(allowedName: "Tom", errorMessage: "This is the wrong name")]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
