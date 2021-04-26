using Application.Handlers.Users.Queries;
using FluentValidation;

namespace Application.Handlers.Users.Validations
{
    public class GetAllUserSubscriptionQueryValidator
        :AbstractValidator<GetAllUserSubscriptionQuery>
    {
        public GetAllUserSubscriptionQueryValidator()
        {
            RuleFor(x => x.UserId)
               .NotEmpty()
               .WithMessage("{PropertyName} is required.")
               .GreaterThan(-1)
               .WithMessage("{PropertyName} is required.");
        }
    }
}
