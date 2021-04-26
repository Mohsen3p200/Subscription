using Application.Handlers.Subscriptions.Models.Dtos;
using Application.Handlers.Users.Queries;
using Application.Handlers.Users.Validations;
using AutoMapper;
using Domain.Entitties;
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

namespace Application.Handlers.Users.Handlers
{
    public class GetAllUserSubscriptionHandler
        : IRequestHandler<GetAllUserSubscriptionQuery, List<UserSubscriptionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllUserSubscriptionHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllUserSubscriptionHandler(IUnitOfWork unitOfWork,
           ILogger<GetAllUserSubscriptionHandler> logger,
           IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<UserSubscriptionDto>> Handle
            (GetAllUserSubscriptionQuery request, CancellationToken cancellationToken)
        {
            ApplyValidation(request);

            var userInDb = await _unitOfWork.User.GetByIdAsync(request.UserId);

            if (userInDb == null)
            {
            _logger.LogError($"User with the Id: ${request.UserId} does not exist.");

            throw new BadRequestException("UserNotExist", $"User with the Id: ${request.UserId} does not exist.");
            }

            var subscriptionsFroUser = await _unitOfWork.UserSubscription
                .GetUserSubscriptions(request.UserId);

            var subscriptionsForUserToReturn = _mapper.Map<List<UserSubscription>,List<UserSubscriptionDto>>
                                                (subscriptionsFroUser);

            return subscriptionsForUserToReturn;
        }

        private void ApplyValidation(GetAllUserSubscriptionQuery request)
        {
            _logger.LogInformation("Validation for getting all subscription for the user has been started.");

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            new GetAllUserSubscriptionQueryValidator().ValidateAndThrow(request);

            _logger.LogInformation("Validation for getting all subscription for the user has been done successfully.");
        }
    }
}
