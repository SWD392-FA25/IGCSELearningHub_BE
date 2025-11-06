using Application;
using Application.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppDbContext _context;
        public readonly IAccountRepository _accountRepository;

        public readonly IAssignmentRepository _assignmentRepository;
        public readonly IAttemptAnswerRepository _attemptAnswerRepository;
        public readonly ICoursePackageRepository _coursePackageRepository;
        public readonly ICourseRepository _courseRepository;
        public readonly IEnrollmentRepository _enrollmentRepository;
        public readonly IQuestionRepository _questionRepository;
        public readonly IQuestionOptionRepository _questionOptionRepository;
        public readonly IQuizRepository _quizRepository;
        public readonly IQuizQuestionRepository _quizQuestionRepository;
        public readonly IQuizAttemptRepository _quizAttemptRepository;
        public readonly ICurriculumRepository _curriculumRepository;
        public readonly ILessonRepository _lessonRepository;
        public readonly ILessonCompletionRepository _lessonCompletionRepository;
        public readonly ILivestreamRegistrationRepository _livestreamRegistrationRepository;
        public readonly ILivestreamRepository _livestreamRepository;
        public readonly IOrderDetailRepository _orderDetailRepository;
        public readonly IOrderRepository _orderRepository;
        public readonly IPaymentRepository _paymentRepository;
        public readonly IPaymentMethodRepository _paymentMethodRepository;
        public readonly IProgressRepository _progressRepository;
        public readonly ISubmissionRepository _submissionRepository;
        public readonly IRefreshTokenRepository _refreshTokenRepository;

        public IAccountRepository AccountRepository => _accountRepository;
        public IAssignmentRepository AssignmentRepository => _assignmentRepository;
        public IAttemptAnswerRepository AttemptAnswerRepository => _attemptAnswerRepository;
        public ICoursePackageRepository CoursePackageRepository => _coursePackageRepository;
        public ICourseRepository CourseRepository => _courseRepository;
        public IEnrollmentRepository EnrollmentRepository => _enrollmentRepository;
        public IQuestionRepository QuestionRepository => _questionRepository;
        public IQuestionOptionRepository QuestionOptionRepository => _questionOptionRepository;
        public IQuizRepository QuizRepository => _quizRepository;   
        public IQuizQuestionRepository QuizQuestionRepository => _quizQuestionRepository;
        public IQuizAttemptRepository QuizAttemptRepository => _quizAttemptRepository;
        public ICurriculumRepository CurriculumRepository => _curriculumRepository;
        public ILessonRepository LessonRepository => _lessonRepository;
        public ILessonCompletionRepository LessonCompletionRepository => _lessonCompletionRepository;
        public ILivestreamRegistrationRepository LivestreamRegistrationRepository => _livestreamRegistrationRepository;
        public ILivestreamRepository LivestreamRepository => _livestreamRepository;
        public IOrderDetailRepository OrderDetailRepository => _orderDetailRepository;
        public IOrderRepository OrderRepository => _orderRepository;
        public IPaymentRepository PaymentRepository => _paymentRepository;
        public IPaymentMethodRepository PaymentMethodRepository => _paymentMethodRepository;
        public IProgressRepository ProgressRepository => _progressRepository;
        public ISubmissionRepository SubmissionRepository => _submissionRepository;
        public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository;

        public UnitOfWork(AppDbContext context,
            IAccountRepository accountRepository,
            IAssignmentRepository assignmentRepository,
            IAttemptAnswerRepository attemptAnswerRepository,
            ICoursePackageRepository coursePackageRepository,
            ICourseRepository courseRepository,
            IEnrollmentRepository enrollmentRepository,
            IQuestionRepository questionRepository,
            IQuestionOptionRepository questionOptionRepository,
            IQuizRepository quizRepository,
            IQuizQuestionRepository quizQuestionRepository,
            IQuizAttemptRepository quizAttemptRepository,
            ICurriculumRepository curriculumRepository,
            ILessonRepository lessonRepository,
            ILessonCompletionRepository lessonCompletionRepository,
            ILivestreamRegistrationRepository livestreamRegistrationRepository,
            ILivestreamRepository livestreamRepository,
            IOrderDetailRepository orderDetailRepository,
            IOrderRepository orderRepository,
            IPaymentRepository paymentRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IProgressRepository progressRepository,
            ISubmissionRepository submissionRepository,
            IRefreshTokenRepository refreshTokenRepository
            )
        {
            _context = context;
            _accountRepository = accountRepository;
            _assignmentRepository = assignmentRepository;
            _attemptAnswerRepository = attemptAnswerRepository;
            _coursePackageRepository = coursePackageRepository;
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _questionRepository = questionRepository;
            _questionOptionRepository = questionOptionRepository;
            _quizRepository = quizRepository;
            _quizQuestionRepository = quizQuestionRepository;
            _quizAttemptRepository = quizAttemptRepository;
            _curriculumRepository = curriculumRepository;
            _lessonRepository = lessonRepository;
            _lessonCompletionRepository = lessonCompletionRepository;
            _livestreamRegistrationRepository = livestreamRegistrationRepository;
            _livestreamRepository = livestreamRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _paymentRepository = paymentRepository;
            _progressRepository = progressRepository;
            _submissionRepository = submissionRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
