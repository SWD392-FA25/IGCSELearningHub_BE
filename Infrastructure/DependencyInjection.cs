using Application;
using Application.Authentication.Interfaces;
using Application.IRepository;
using Application.Notifications;
using Application.Utils;
using Application.Utils.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Email;
using Infrastructure.Notifications;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<EmailOptions>(config.GetSection("Email"));
            services.AddTransient<IEmailSender, MailKitEmailSender>();
            services.AddSingleton(provider =>
            {
                if (FirebaseApp.DefaultInstance != null)
                {
                    return FirebaseApp.DefaultInstance;
                }

                var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger("FirebaseInit");
                var inlineCredentials = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIALS");
                GoogleCredential credential;

                if (!string.IsNullOrWhiteSpace(inlineCredentials))
                {
                    credential = GoogleCredential.FromJson(inlineCredentials);
                    logger.LogInformation("🔥 Firebase credentials loaded from environment variable.");
                }
                else
                {
                    var env = provider.GetRequiredService<IHostEnvironment>();
                    var firebaseSection = config.GetSection("Firebase");
                    var credentialsPath = firebaseSection.GetValue<string>("CredentialsPath")
                                           ?? "Infrastructure/Firebase/igcse-learning-hub-firebase-adminsdk.json";
                    if (!Path.IsPathRooted(credentialsPath))
                    {
                        credentialsPath = Path.Combine(env.ContentRootPath, credentialsPath);
                    }
                    credential = GoogleCredential.FromFile(credentialsPath);
                    logger.LogInformation("🔥 Firebase credentials loaded from local file: {Path}", credentialsPath);
                }

                var options = new AppOptions
                {
                    Credential = credential
                };
                return FirebaseApp.Create(options);
            });
            services.AddSingleton(provider =>
            {
                var app = provider.GetRequiredService<FirebaseApp>();
                return FirebaseAuth.GetAuth(app);
            });
            services.AddSingleton(provider =>
            {
                var app = provider.GetRequiredService<FirebaseApp>();
                return FirebaseMessaging.GetMessaging(app);
            });
            services.AddSingleton<IPushNotificationService, FirebasePushNotificationService>();
            services.AddSingleton<IPaymentRealtimeNotifier, NoOpPaymentRealtimeNotifier>();
            services.AddSingleton<IExternalAuthProvider, FirebaseExternalAuthProvider>();
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
            services.AddScoped<IUnitRepository, UnitRepository>();
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
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            #endregion

            #region quartz config

            #endregion
            return services;
        }
    }
}
