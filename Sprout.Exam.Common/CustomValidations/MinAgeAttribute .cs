using System;
using System.ComponentModel.DataAnnotations;

namespace Sprout.Exam.Common.CustomValidations
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private readonly int _minAge;

        public MinAgeAttribute(int minAge)
        {
            _minAge = minAge;
        }

        public override bool IsValid(object value)
        {
            DateTime birthdate = (DateTime)value;
            int age = DateTime.Today.Year - birthdate.Year;
            if (birthdate > DateTime.Today.AddYears(-age)) age--;
            return age >= _minAge;
        }
    }
}
