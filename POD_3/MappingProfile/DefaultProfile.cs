using AutoMapper;
using POD_3.DAL.Entity;
using POD_3.DAL.Entity.AccountManagementMod;
using POD_3.DAL.Entity.SubscriptionManagementMod;
using POD_3.DAL.Entity.SupportModule;
using POD_3.DAL.Models;

namespace POD_3.MappingProfile
{
    public class DefaultProfile:Profile
    {
        public DefaultProfile()
        {
            CreateMap<SubscriptionPlan, SubscriptionPlanModel>();
            CreateMap<UserSubscription, SubscriptionDetailModel>();
            CreateMap<SocialAccountType, AccountTypesModel>();
            CreateMap<AccountTypesModel, SocialAccountType>();
            CreateMap<AccountRequestModel, UserSocialAccount>();

            CreateMap<User, LoginResponseModel>();
            CreateMap<SubscriptionRequestModel, UserSubscription>()
                .ForMember(m => m.SubscriptionStartDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddDays(1)))
                .ForMember(m => m.SubscriptionEndDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddMonths(src.planDuration)));
            CreateMap<RenewRequestModel, UserSubscription>()
                .ForMember(m => m.SubscriptionStartDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddDays(1)))
                .ForMember(m => m.SubscriptionEndDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddMonths(src.planDuration)));
            CreateMap<SubscriptionCancelationRequestModel, SubscriptionCancellation>()
                .ForMember(m => m.CancellationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<SupportTicket, CreateTicketModel>()
               .ForMember(m => m.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<CreateTicketModel, SupportTicket>();
            CreateMap<SupportTicket, TicketsListModel>();
            CreateMap<TicketSolution, CloseDetailsModel>()
                 .ForMember(m => m.ResolvedOn, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<CloseDetailsModel, TicketSolution>();
            CreateMap<TicketDetailsModel, SupportTicket>();
            CreateMap<SupportTicket, TicketDetailsModel>();

        }
    }
}
