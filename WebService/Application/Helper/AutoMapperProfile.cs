using Application.Handlers.Subscriptions.Models.Dtos;
using AutoMapper;
using Domain.Entitties;

namespace Application.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserSubscription, UserSubscriptionDto>()
                .ForMember(d => d.SubscriptionPeriod, s => s.MapFrom(m => m.SubscriptionPeriod.Name))
                .ForMember(d => d.SubscriptionType, s => s.MapFrom(m => m.SubscriptionType.Name))
                .ForMember(d => d.NameOfUser, s => s.MapFrom(m => $"{m.User.Firstname} {m.User.Lastname}"))
                .ForMember(d => d.UserEmail, s => s.MapFrom(m => m.User.Email));


        }
    }
}
