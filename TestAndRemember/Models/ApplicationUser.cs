using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAndRemember.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public Guid UserId { get; set; }

        // TODO: Upgrade to latest https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL
        // and get auto-incrementing of UserId working
        public long UserId { get; set; }
    }
}
