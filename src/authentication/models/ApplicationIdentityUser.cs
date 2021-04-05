using System;
using Microsoft.AspNetCore.Identity;

namespace BreakableLime.Authentication.models
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public string Class { get; set; }
        public string TokenId { get; set; } = Guid.NewGuid().ToString();
    }
}