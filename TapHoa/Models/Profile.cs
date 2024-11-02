using System.ComponentModel.DataAnnotations;

public class Profile
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên không được để trống.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email không được để trống.")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Địa chỉ không được để trống.")]
    public string Address { get; set; }
}
