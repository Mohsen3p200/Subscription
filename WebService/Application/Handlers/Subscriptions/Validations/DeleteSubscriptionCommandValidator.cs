using Application.Handlers.Subscriptions.Commands;
using Application.Handlers.Subscriptions.Queries;
using FluentValidation;

namespace Application.Handlers.Subscriptions.Validations
{
    class DeleteSubscriptionCommandValidator: AbstractValidator<DeleteSubscriptionCommand>
    {
        public DeleteSubscriptionCommandValidator()
        {
            RuleFor(x => x.SubscriptionId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(-1)
                .WithMessage("{PropertyName} is required.");
        }
    }
}
