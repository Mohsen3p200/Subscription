using Application.Handlers.Subscriptions.Queries;
using FluentValidation;

namespace Application.Handlers.Subscriptions.Validations
{
    class GetSubscriptionByIdQueryValidator:AbstractValidator<GetSubscriptionByIdQuery>
    {
        public GetSubscriptionByIdQueryValidator()
        {
            RuleFor(x => x.SubscriptionId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(-1)
                .WithMessage("{PropertyName} is required.");
        }
    }
}
