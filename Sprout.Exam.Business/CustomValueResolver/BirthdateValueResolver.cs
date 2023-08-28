using AutoMapper;

using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess.Models;

using System;

namespace Sprout.Exam.Business.CustomValueResolver
{
    /// <summary>
    /// This will format the Date when mapping objects from Model to DTO 
    /// </summary>
    public class BirthdateValueResolver : IValueResolver<Employee, EmployeeDto, string>
    {
        public string Resolve(Employee source, EmployeeDto destination, string destMember, ResolutionContext context)
        {
            if (DateTime.TryParse(source.Birthdate.ToString(), out DateTime parsedDate))
            {
                return parsedDate.ToString("yyyy-MM-dd");
            }

            return source.Birthdate.ToString(); // Return the original value if parsing fails
        }
    }
}

