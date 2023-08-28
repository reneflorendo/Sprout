using System;
using System.ComponentModel.DataAnnotations;

using Sprout.Exam.Common.CustomValidations;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public abstract class BaseSaveEmployeeDto
    {
        private const int FullNameMinLength = 5;
        private const int FullNameMaxLength = 100;

      //  [Required(ErrorMessage = "Full Name is required.")]
      //  [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength, ErrorMessage = "Full Name must be between 5 and 100 characters.")]
        public string FullName { get; set; }

      //  [Required(ErrorMessage = "TIN is required.")]
      //  [StringLength(10, MinimumLength = 10, ErrorMessage = "TIN must be exactly 10 characters.")]
        public string Tin { get; set; }

        //[Required(ErrorMessage = "Birthdate is required.")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[BirthdateInPast(ErrorMessage = "Birthdate cannot be in the future.")]
        //[MinAge(18, ErrorMessage = "Employee must be at least 18 years old.")]
        public DateTime Birthdate { get; set; }
        public int TypeId { get; set; }
    }
}
