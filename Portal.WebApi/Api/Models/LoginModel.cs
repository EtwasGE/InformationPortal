using System.ComponentModel.DataAnnotations;

namespace Portal.WebApi.Api.Models
{
    public class LoginModel
    {
        public string TenancyName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}