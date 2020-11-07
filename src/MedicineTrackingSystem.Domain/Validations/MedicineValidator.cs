using FluentValidation;
using FluentValidation.Results;
using MedicineTrackingSystem.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineTrackingSystem.Domain.Validations
{
    public class MedicineValidator : AbstractValidator<MedicineEditDto>
    {
        public MedicineValidator()
        {
            RuleFor(medicine => medicine.Name).NotEmpty().WithMessage("Medicine Name is required");
        }
    }
}
