﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.PayAsYouGo || 
                customer.MembershipTypeId == MembershipType.UnKnown)
            {
                return ValidationResult.Success;
            }
            if(customer.BirthDate ==null)
                return  new ValidationResult("BirthDate is required");
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at leaset 18 years old to go on a mmember ship");

        }
    }
}