using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer
{
    //[Table("User", Schema = "UM")] //is for create table with another name and different schema (dbo).
    //[Index(nameof(Email), IsUnique = true)] // this is ti create index and make it unique.
    public class User
    {
        //[Key] // is for make primary key
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // is for indexing automat or manual.
        public int UserId { get; set; }
        //[Column("Username", TypeName = "nvarchar")] // is for change name in database and define data type.
        //[MaxLength(100)] // is for make limit length.
        public string Name { get; set; }
        public string Family { get; set; }
        //[Required] // make not null column in database.
        //[MaxLength(200)]
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        //[NotMapped]
        public string FullName => Name + Family;

        public virtual List<UserProduct> UserProducts { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
