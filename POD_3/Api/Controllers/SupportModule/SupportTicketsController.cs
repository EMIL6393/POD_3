using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POD_3.BLL.Repositories.Repository;
using POD_3.BLL.Services.Interfaces;
using POD_3.Context;
using POD_3.DAL.Entity.SupportModule;
using POD_3.DAL.Models;
using System.Net;

namespace POD_3.Api.Controllers.SupportModule
{
    public class SupportTicketsController:BaseController
    {

        private readonly DefaultContext context;
        private readonly IMapper mapper;
        private readonly ICreateSupportTicketService _createService;
        private readonly ICloseTicketService _closeService;
        private readonly IFetchAllTicketsByUserNameService _fetchAllTicketsByUserNameService;
        private readonly IFetchOpenTicketsService? _fetchOpenTicketsService;
        private readonly IFetchTicketByIdService? _fetchTicketByIdService;
        private readonly ISubscriptionPlanSLARepository _subscriptionPlanSLARepository;
        private readonly IRepositoryWrapper repository;


        public SupportTicketsController(DefaultContext context, IMapper mapper, ICreateSupportTicketService createService, ICloseTicketService closeService, IFetchAllTicketsByUserNameService fetchAllTicketsByUserNameService, IFetchOpenTicketsService fetchOpenTicketsService, IFetchTicketByIdService fetchTicketByIdService, IRepositoryWrapper wrapper, ISubscriptionPlanSLARepository subscriptionPlanSLARepository)
        {
            this.context = context;
            this.mapper = mapper;
            _createService = createService;
            _closeService = closeService;
            _fetchAllTicketsByUserNameService = fetchAllTicketsByUserNameService;
            _fetchOpenTicketsService = fetchOpenTicketsService;
            _fetchTicketByIdService = fetchTicketByIdService;
            repository = wrapper;
            _subscriptionPlanSLARepository = subscriptionPlanSLARepository;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateSupportTicket(CreateTicketModel newTicket)
        {
            try
            {

                var userSubscription = await repository.UserSubscriptionRepository.GetByUsernameAsync(newTicket.RaisedByUserName);
                var planname = await repository.SubscriptionPlanRepository.GetByIdAsync(userSubscription.PlanId);
                if (planname.Name == "basic" && newTicket.TicketType == "PostManagement")
                {
                    var apiErrors = new List<ApiError>
                        {
                           new ApiError { ErrorCode = 1002, ErrorMessage = "Basic plan users cannot create support tickets for PostManagement queries" }
                         };
                    return GenerateErrorResponse(apiErrors, HttpStatusCode.BadRequest, "Failed to create support ticket");
                }
                if (planname.Name == "basic")
                {
                    var ticketCount = await _fetchAllTicketsByUserNameService.GetTicketCountForUserAsync(newTicket.RaisedByUserName, DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
                    if (ticketCount >= 1)
                    {
                        var apiErrors = new List<ApiError>
                       {
                           new ApiError { ErrorCode = 1003, ErrorMessage = "Basic plan users are allowed only 1 support ticket per week" }
                       };
                        return GenerateErrorResponse(apiErrors, HttpStatusCode.BadRequest, "Failed to create support ticket");
                    }
                }
                var slaDays = _subscriptionPlanSLARepository.GetSLADays(planname.Name);
                var expectedResolutionDate = DateTime.UtcNow.AddDays(slaDays);
                newTicket.ExpectedResolutionOn = expectedResolutionDate;
                // var createdTicket = await _createService.CreateTicketAsync(newTicket); without model and mapper

                var supportTicket = mapper.Map<SupportTicket>(newTicket);
                //var supportTicket = mapper.Map<CreateTicketModel, SupportTicket>(newTicket);
                var createdTicket = await _createService.CreateTicketAsync(supportTicket);

                return GenerateSuccessResponse($"Support ticket created with ID: {createdTicket.TicketId}", "Ticket created successfully");
            }

            catch (Exception ex)
            {
                var apiErrors = new List<ApiError>
                {
                    new ApiError { ErrorCode = 1001, ErrorMessage = ex.Message }
                };
                return GenerateErrorResponse(apiErrors, HttpStatusCode.BadRequest, "Failed to create support ticket");
            }
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetSupportTicketsById(int ticketId)
        {
            try
            {
                var ticket = await _fetchTicketByIdService.GetByIdAsync(ticketId);
                if (ticket == null)
                {
                    return GenerateErrorResponse(new List<ApiError> { new ApiError { ErrorCode = 404, ErrorMessage = "Ticket not found" } }, HttpStatusCode.NotFound);
                }
                var ticketModel = mapper.Map<TicketDetailsModel>(ticket);
                return GenerateSuccessResponse(ticketModel, $"Support ticket found with ID: {ticket.TicketId}");
            }
            catch (Exception ex)
            {
                return GenerateErrorResponse(new List<ApiError> { new ApiError { ErrorCode = 500, ErrorMessage = ex.Message } }, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("open")]

        public async Task<IActionResult> FetchOpenTickets()
        {
            try
            {
                var openTickets = await _fetchOpenTicketsService.GetOpenTicketsAsync();
                if (openTickets == null || openTickets.Count == 0)
                {
                    return GenerateErrorResponse(new List<ApiError> { new ApiError { ErrorCode = 404, ErrorMessage = "No open tickets found" } }, HttpStatusCode.NotFound);
                }
                var ticketListModels = mapper.Map<List<TicketsListModel>>(openTickets);

                return GenerateSuccessResponse(ticketListModels, "Open tickets fetched successfully");
                // return GenerateSuccessResponse(openTickets, "Open tickets fetched successfully");
            }
            catch (Exception ex)
            {
                return GenerateErrorResponse(new List<ApiError> { new ApiError { ErrorCode = 500, ErrorMessage = ex.Message } }, HttpStatusCode.InternalServerError);
            }
        }

        // [HttpGet("{userName}")]
        [HttpGet("user/{userName}")]
        public async Task<IActionResult> GetAllTicketsByUserName(string userName)
        {
            try
            {
                var tickets = await _fetchAllTicketsByUserNameService.GetTicketsByUsernameAsync(userName);
                if (tickets == null || !tickets.Any())
                {
                    return GenerateErrorResponse(new List<ApiError> { new ApiError { ErrorCode = 404, ErrorMessage = "No tickets found for the specified username" } }, HttpStatusCode.NotFound);
                }
                var ticketListModels = mapper.Map<List<TicketsListModel>>(tickets);

                return GenerateSuccessResponse(ticketListModels, "Tickets fetched successfully");
                // return GenerateSuccessResponse(tickets, "Tickets fetched successfully");
            }
            catch (Exception ex)
            {
                return GenerateErrorResponse(new List<ApiError> { new ApiError { ErrorCode = 500, ErrorMessage = ex.Message } }, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{ticketId}/resolve")]
        public async Task<IActionResult> ResolveAndCloseTickets(int ticketId, string resolvedByUserName, DateTime resolvedOn, string resolutionDetails)
        {
            try
            {
                var ticketSolution = mapper.Map<TicketSolution>(new CloseDetailsModel
                {
                    ResolvedByUserName = resolvedByUserName,
                    ResolvedOn = resolvedOn,
                    ResolutionDetails = resolutionDetails
                });

                var ticket = await _closeService.CloseTicketAsync(ticketId, ticketSolution.ResolvedByUserName, ticketSolution.ResolvedOn, ticketSolution.ResolutionDetails);
                return GenerateSuccessResponse(ticket, "Ticket closed successfully");
                
            }
            catch (Exception ex)
            {
                return GenerateErrorResponse(new List<ApiError> { new ApiError { ErrorCode = 400, ErrorMessage = ex.Message } }, HttpStatusCode.BadRequest);
            }
        }


    }
}
