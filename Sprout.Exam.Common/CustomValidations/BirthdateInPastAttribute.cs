using System;
using System.ComponentModel.DataAnnotations;

namespace Sprout.Exam.Common.CustomValidations
{
    public class BirthdateInPastAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime birthdate = (DateTime)value;
            return birthdate <= DateTime.Now;
        }
    }
}
