using PranicAhmedbad.Models;
using System.ComponentModel.DataAnnotations;

namespace PranicAhmedbad.ViewModels
{
    public class AccountLoginViewModel
    {
        
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Login_Master? LoginMaster { get; set; }
    }
}
