using Microsoft.AspNetCore.Identity;

namespace MandadosExpress.Models
{
    public class User : IdentityUser
    {
        /// <summary>
        /// First Name of user
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; set; }
    }
}
