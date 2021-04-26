
using Application.Handlers.Subscriptions.Models;
using FluentValidation;

namespace Application.Handlers.Subscriptions.Validations
{
    public class CreateSubscriptionForUserModelValidator:AbstractValidator<CreateSubscriptionForUserModel>
    {
        public CreateSubscriptionForUserModelValidator()
        {
            RuleFor(x => x.SubscriptionPeriodId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(-1)
                .WithMessage("{PropertyName} is required.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(-1)
                .WithMessage("{PropertyName} is required.");

            RuleFor(x => x.SubscriptionTypeId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(-1)
                .WithMessage("{PropertyName} is required.");
        }
    }
}
