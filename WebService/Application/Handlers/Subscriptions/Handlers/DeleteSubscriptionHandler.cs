using Application.Handlers.Subscriptions.Commands;
using Application.Handlers.Subscriptions.Validations;
using AutoMapper;
using Domain.Exceptions;
using Domain.IRepositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Subscriptions.Handlers
{
    public class DeleteSubscriptionHandler : IRequestHandler<DeleteSubscriptionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateSubscriptionForUserHandler> _logger;

        public DeleteSubscriptionHandler(IUnitOfWork unitOfWork,
           ILogger<CreateSubscriptionForUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            ApplyValidation(request);

            var subscriptionInDb = await _unitOfWork.UserSubscription.GetByIdAsync(request.SubscriptionId);

            if (subscriptionInDb == null)
            {
                _logger.LogWarning($"The subscription with the Id of ${request.SubscriptionId} does not exist.");

                throw new NotFoundException();
            }

            _unitOfWork.UserSubscription.Remove(subscriptionInDb);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }

        private void ApplyValidation(DeleteSubscriptionCommand request)
        {
            _logger.LogInformation("Validation to delete selected subscription has been started.");

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            new DeleteSubscriptionCommandValidator().ValidateAndThrow(request);

            _logger.LogInformation("Validation to delete selected subscription has been done successfully.");
        }
    }
}
