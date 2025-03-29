using Domain.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class UpdateQuestionRequestValidator : AbstractValidator<UpdateQuestionRequest>
    {
        public UpdateQuestionRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content cannot be empty");

            RuleFor(x => x.id)
                .NotEmpty().WithMessage("id cannot be empty");

            RuleForEach(x => x.Options).SetValidator(new UpdateOptionRequestValidator());
        }
    }
}
