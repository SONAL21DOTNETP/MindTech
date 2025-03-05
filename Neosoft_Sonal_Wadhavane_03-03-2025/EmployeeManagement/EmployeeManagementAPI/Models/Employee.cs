using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [Phone(ErrorMessage = "Invalid Mobile Number.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Mobile number must be 10 digits and start with 6-9.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "PAN Number is required.")]
        [RegularExpression(@"[A-Z]{5}[0-9]{4}[A-Z]{1}", ErrorMessage = "Invalid PAN Number format.")]
        public string PANNumber { get; set; }

        [Required(ErrorMessage = "Passport Number is required.")]
        [RegularExpression(@"[A-Z0-9]{9}", ErrorMessage = "Invalid Passport Number format.")]
        public string PassportNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Employee), nameof(ValidatePastDate))]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Date of Joining is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Employee), nameof(ValidatePastDate))]
        public DateTime DateOfJoining { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Profile image is required.")]
        public string ProfileImage { get; set; }


        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("Male|Female", ErrorMessage = "Gender must be Male or Female.")]
        public string Gender { get; set; }

        public bool IsActive { get; set; } = true;

        
        public static ValidationResult ValidatePastDate(DateTime date, ValidationContext context)
        {
            return date < DateTime.Today ? ValidationResult.Success : new ValidationResult("Date must be in the past.");
        }
    }
}
