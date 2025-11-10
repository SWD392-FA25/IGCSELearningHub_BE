using Application.Authentication;
using Application.Authentication.Interfaces;
using Application.Mapping;
using Application.Payments.Interfaces;
using Application.Payments.Services;
using Application.Services;
using Application.Services.Interfaces;
using Application.Utils;
using Application.Utils.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAccessTokenFactory, AccessTokenFactory>();
            services.AddScoped<IRefreshTokenManager, RefreshTokenManager>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IQuizTakingService, QuizTakingService>();
            services.AddScoped<ICourseCatalogService, CourseCatalogService>();
            services.AddScoped<ICourseAdminService, CourseAdminService>();
            services.AddScoped<IAssignmentAdminService, AssignmentAdminService>();
            services.AddScoped<ILivestreamAdminService, LivestreamAdminService>();
            services.AddScoped<ICoursePackageAdminService, CoursePackageAdminService>();
            services.AddScoped<IEnrollmentAdminService, EnrollmentAdminService>();
            services.AddScoped<ILessonAdminService, LessonAdminService>();
            services.AddScoped<IEnrollmentStudentService, EnrollmentStudentService>();
            services.AddScoped<IAssignmentStudentService, AssignmentStudentService>();
            services.AddScoped<ILessonStudentService, LessonStudentService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IStudentSubmissionService, StudentSubmissionService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderQueryService, OrderQueryService>();
            services.AddScoped<IProgressService, ProgressService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<ILivestreamPublicService, LivestreamPublicService>();
            services.AddScoped<ICoursePackagePublicService, CoursePackagePublicService>();
            services.AddScoped<ILessonPublicService, LessonPublicService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<IDeviceService, DeviceService>();

            return services;
        }
    }
}
