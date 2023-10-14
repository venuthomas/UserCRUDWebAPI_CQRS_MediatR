using FluentValidation;
using UserCRUDWebAPI_CQRS_MediatR.Features.Commands;

namespace UserCRUDWebAPI_CQRS_MediatR.Validations
{
    public class SaveUserDetailsValidation : AbstractValidator<SaveUserDetailsCommand>
    {
        public SaveUserDetailsValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("The First Name field is required!")
                .MaximumLength(20).WithMessage("The First Name field's max length is 20")
                .MinimumLength(3).WithMessage("The First Name field's min length is 3");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("The Last Name field is required!")
                .MaximumLength(20).WithMessage("The Last Name field's max length is 20")
                .MinimumLength(3).WithMessage("The Last Name field's min length is 3");
        }
    }
}
