using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEc.Data.Customers
{
    [Table("Customers", Schema = "Sales")]
    public class Customer
    {
        [Key]
        public int custId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public string LoginName { get; set; }
    }

    public class Login
    {
        public string Usernme { get; set; }
        public string Password { get; set; }
    }
}
