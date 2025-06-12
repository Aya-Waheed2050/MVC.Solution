using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class ForgetPasswordViewModel
    {

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; } = null!;

    }
}
