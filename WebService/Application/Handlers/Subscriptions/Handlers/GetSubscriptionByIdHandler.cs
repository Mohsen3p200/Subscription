using Application.Handlers.Subscriptions.Models.Dtos;
using Application.Handlers.Subscriptions.Queries;
using Application.Handlers.Subscriptions.Validations;
using AutoMapper;
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
    public class GetSubscriptionByIdHandler : IRequestHandler<GetSubscriptionByIdQuery, UserSubscriptionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateSubscriptionForUserHandler> _logger;
        private readonly IMapper _mapper;

        public GetSubscriptionByIdHandler(IUnitOfWork unitOfWork,
            ILogger<CreateSubscriptionForUserHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserSubscriptionDto> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {
            ApplyValidation(request);

            var subscriptionInDb = await _unitOfWork.UserSubscription
                .GetSubscriptionWithDetails(request.SubscriptionId);

            if(subscriptionInDb==null)
            {
                _logger.LogWarning($"The subscription with the Id of ${request.SubscriptionId} does not exist");

                throw new NotFoundException();
            }

            var subscriptionToReturn=_mapper.Map<UserSubscriptionDto>(subscriptionInDb);

            return subscriptionToReturn;
        }

        private void ApplyValidation(GetSubscriptionByIdQuery request)
        {
            _logger.LogInformation("Validation for getting selected subscription has been started.");

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            new GetSubscriptionByIdQueryValidator().ValidateAndThrow(request);

            _logger.LogInformation("Validation for getting selected subscription has been done successfully.");
        }
    }
}
