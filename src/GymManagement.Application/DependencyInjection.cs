using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using GymManagement.Application.Features.Auth.Commands.Login;
using GymManagement.Application.Features.Members.Commands.CreateMember;
using GymManagement.Application.Features.Members.Commands.UpdateMember;
using GymManagement.Application.Features.Members.Commands.DeactivateMember;
using GymManagement.Application.Features.Members.Queries.GetMember;
using GymManagement.Application.Features.Members.Queries.SearchMembers;
using GymManagement.Application.Features.Measurements.Commands.CreateMeasurement;
using GymManagement.Application.Features.Measurements.Queries.GetMeasurements;
using GymManagement.Application.Features.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Features.Subscriptions.Queries.GetMemberSubscriptions;
using GymManagement.Application.Features.Subscriptions.Queries.GetExpiringSoon;
using GymManagement.Application.Features.Plans.Commands.CreatePlan;
using GymManagement.Application.Features.Plans.Commands.UpdatePlan;
using GymManagement.Application.Features.Plans.Queries.GetPlans;
using GymManagement.Application.Features.Offers.Commands.CreateOffer;
using GymManagement.Application.Features.Offers.Commands.DeactivateOffer;
using GymManagement.Application.Features.Offers.Queries.GetOffers;
using GymManagement.Application.Features.Staff.Commands.CreateStaff;
using GymManagement.Application.Features.Staff.Queries.GetStaff;
using GymManagement.Application.Features.Roles.Queries.GetRoles;
using GymManagement.Application.Mappings;

namespace GymManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MemberMappingProfile).Assembly));
        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();

        // Auth
        services.AddScoped<LoginCommandHandler>();

        // Members
        services.AddScoped<CreateMemberCommandHandler>();
        services.AddScoped<UpdateMemberCommandHandler>();
        services.AddScoped<DeactivateMemberCommandHandler>();
        services.AddScoped<GetMemberQueryHandler>();
        services.AddScoped<SearchMembersQueryHandler>();

        // Measurements
        services.AddScoped<CreateMeasurementCommandHandler>();
        services.AddScoped<GetMeasurementsQueryHandler>();

        // Subscriptions
        services.AddScoped<CreateSubscriptionCommandHandler>();
        services.AddScoped<GetMemberSubscriptionsQueryHandler>();
        services.AddScoped<GetExpiringSoonQueryHandler>();

        // Plans
        services.AddScoped<CreatePlanCommandHandler>();
        services.AddScoped<UpdatePlanCommandHandler>();
        services.AddScoped<GetPlansQueryHandler>();

        // Offers
        services.AddScoped<CreateOfferCommandHandler>();
        services.AddScoped<DeactivateOfferCommandHandler>();
        services.AddScoped<GetOffersQueryHandler>();

        // Staff
        services.AddScoped<CreateStaffCommandHandler>();
        services.AddScoped<GetStaffQueryHandler>();

        // Roles
        services.AddScoped<GetRolesQueryHandler>();

        return services;
    }
}
