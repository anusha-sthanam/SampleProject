using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class UpdateUserService : IUpdateUserService
    {
        public void Update(User user, string name, string email, UserTypes type, int age, decimal? annualSalary, IEnumerable<string> tags)
        {
            ValidateInputs(user, name, email, age, annualSalary, tags);
            user.SetEmail(email);
            user.SetName(name);
            user.SetType(type);
            user.SetAge(age);
            user.SetMonthlySalary(annualSalary.Value / 12);
            user.SetTags(tags);
        }

        private void ValidateInputs(User user, string name, string email, int age, decimal? annualSalary, IEnumerable<string> tags)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Name was not provided.");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "Email was not provided.");
            }
            if (age < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative.");
            }
            if (!annualSalary.HasValue)
            {
                throw new ArgumentNullException(nameof(annualSalary), "Annual Salary was not provided.");
            }
            if (annualSalary.HasValue && annualSalary.Value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(annualSalary), "Annual salary cannot be negative.");
            }
            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags), "Tags cannot be null.");
            }
        }
    }
}