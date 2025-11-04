using Application;
using Application.Authentication;
using Application.Authentication.Interfaces;
using Application.Payments.Interfaces;
using Application.Payments.Services;
using Application.IRepository;
using Application.Services;
using Application.Services.Interfaces;
using Application.Utils;
using Application.Utils.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("IGCSELearningHub_DB")));

            #region repo config
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IAttemptAnswerRepository, AttemptAnswerRepository>();
            services.AddScoped<ICoursePackageRepository, CoursePackageRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionOptionRepository, QuestionOptionRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuizQuestionRepository, QuizQuestionRepository>();
            services.AddScoped<IQuizAttemptRepository, QuizAttemptRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonCompletionRepository, LessonCompletionRepository>();
            services.AddScoped<ILivestreamRegistrationRepository, LivestreamRegistrationRepository>();
            services.AddScoped<ILivestreamRepository, LivestreamRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IProgressRepository, ProgressRepository>();
            services.AddScoped<ISubmissionRepository, SubmissionRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            #endregion

            #region service config
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
            services.AddScoped<IStudentSubmissionService, StudentSubmissionService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderQueryService, OrderQueryService>();
            services.AddScoped<IProgressService, ProgressService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<ILivestreamPublicService, LivestreamPublicService>();
            services.AddScoped<ICoursePackagePublicService, CoursePackagePublicService>();
            services.AddScoped<ILessonPublicService, LessonPublicService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            #endregion

            #region quartz config

            #endregion
            return services;
        }
    }
}
