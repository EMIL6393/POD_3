using AutoMapper;
using POD_3.DAL.Entity;
using POD_3.DAL.Entity.SubscriptionManagementMod;
using POD_3.DAL.Models;

namespace POD_3.MappingProfile
{
    public class DefaultProfile:Profile
    {
        public DefaultProfile()
        {
            CreateMap<SubscriptionPlan, SubscriptionPlanModel>();
            CreateMap<UserSubscription, SubscriptionDetailModel>();
            CreateMap<User, LoginResponseModel>();
            CreateMap<SubscriptionRequestModel, UserSubscription>()
                .ForMember(m => m.SubscriptionStartDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddDays(1)))
                .ForMember(m => m.SubscriptionEndDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddMonths(src.planDuration)));
            CreateMap<RenewRequestModel, UserSubscription>()
                .ForMember(m => m.SubscriptionStartDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddDays(1)))
                .ForMember(m => m.SubscriptionEndDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddMonths(src.planDuration)));
            CreateMap<SubscriptionCancelationRequestModel, SubscriptionCancellation>()
                .ForMember(m => m.CancellationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
