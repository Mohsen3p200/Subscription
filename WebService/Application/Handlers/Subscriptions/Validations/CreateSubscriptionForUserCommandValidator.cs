using Application.Handlers.Subscriptions.Commands;
using FluentValidation;

namespace Application.Handlers.Subscriptions.Validations
{
    public class CreateSubscriptionForUserCommandValidator: AbstractValidator<CreateSubscriptionForUserCommand>
    {
        public CreateSubscriptionForUserCommandValidator()
        {
            RuleFor(r => r.CreateSubscription)
                .SetValidator(new CreateSubscriptionForUserModelValidator());
        }
    }
}
