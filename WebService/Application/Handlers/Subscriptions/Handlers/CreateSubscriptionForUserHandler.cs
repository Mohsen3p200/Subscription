using Application.Handlers.Subscriptions.Commands;
using Application.Handlers.Subscriptions.Validations;
using Domain.Entitties;
using Domain.Exceptions;
using Domain.IRepositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Subscriptions.Handlers
{
    public class CreateSubscriptionForUserHandler : IRequestHandler<CreateSubscriptionForUserCommand, UserSubscription>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateSubscriptionForUserHandler> _logger;

        public CreateSubscriptionForUserHandler(IUnitOfWork unitOfWork,
            ILogger<CreateSubscriptionForUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<UserSubscription> Handle(CreateSubscriptionForUserCommand request, CancellationToken cancellationToken)
        {
            ApplyValidation(request);

            _logger.LogInformation($"The new subscription process has been started " +
                $"for the user with the Id: ${request.CreateSubscription.UserId}");

            var userInDb =await _unitOfWork.User
                .GetByIdAsync(request.CreateSubscription.UserId);

            if (userInDb == null)
            {
                _logger.LogError($"User with the Id: ${request.CreateSubscription.UserId} does not exist!");

                 throw new BadRequestException("UserNotExist", 
                    $"User with the Id of ${request.CreateSubscription.UserId}");
            }
               
            var subscriptionTypeInDb =await _unitOfWork.SubscriptionType
                .GetByIdAsync(request.CreateSubscription.SubscriptionTypeId);

            if (subscriptionTypeInDb == null)
            {
                _logger.LogError($"Subscription Type with the Id of ${request.CreateSubscription.SubscriptionTypeId} does not exist!");

                throw new BadRequestException("SubscriptionTypeNotExistNotExist", 
                    $"Subscription Type with the Id of ${request.CreateSubscription.SubscriptionTypeId} does not exist!");
            }
              

            var subscriptionPeriodInDb =await _unitOfWork.SubscriptionPeriod
                .GetByIdAsync(request.CreateSubscription.SubscriptionPeriodId);

            if (subscriptionPeriodInDb == null)
            {    
                _logger.LogError($"Subscription Period with the Id of ${request.CreateSubscription.SubscriptionPeriodId} does not exist!");

                 throw new BadRequestException("SubscriptionPeriodNotExistNotExist", 
                    $"Subscription Period with the Id of ${request.CreateSubscription.SubscriptionPeriodId} does not exist!");
            }

            var existingSubscription = await _unitOfWork.UserSubscription
                .CheckExistingSubscription(
                                           request.CreateSubscription.SubscriptionTypeId,
                                           request.CreateSubscription.SubscriptionPeriodId,
                                           request.CreateSubscription.UserId
                                           );

            if(existingSubscription!=null)
            {
                _logger.LogError($"User has alredy subscribe to this subscriptio.");

                throw new BadRequestException("SubscriptionExist",
                   $"Subscription exists for the user!");
            }

            var newSubscription = new UserSubscription()
            {
                SubscriptionPeriodId= subscriptionPeriodInDb.Id,
                SubscriptionTypeId =subscriptionTypeInDb.Id,
                UserId=userInDb.Id
            };

            _unitOfWork.UserSubscription.Add(newSubscription);

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"The new subscription with the Id :${newSubscription.Id} " +
                $"has been added to the user with the Id: ${userInDb.Id} successfully.");

            return newSubscription;
        }

        private void ApplyValidation(CreateSubscriptionForUserCommand request)
        {
            _logger.LogInformation("Validation for the new subscription has been started.");

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            new CreateSubscriptionForUserCommandValidator().ValidateAndThrow(request);

            _logger.LogInformation("Validation for the new subscription has been done successfully.");
        }
    }
}
