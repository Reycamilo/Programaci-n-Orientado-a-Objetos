

using Microsoft.AspNetCore.Identity;

namespace PersonasApp.Entities
{
    public class RoleEntity : IdentityRole
    {
        public int MyProperty { get; set; }
    }
}