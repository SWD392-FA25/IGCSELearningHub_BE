using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        IAssignmentRepository AssignmentRepository { get; }
        IAttemptAnswerRepository AttemptAnswerRepository { get; }
        ICoursePackageRepository CoursePackageRepository { get; }
        ICourseRepository CourseRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IQuestionOptionRepository QuestionOptionRepository { get; }
        IQuizRepository QuizRepository { get; }
        IQuizQuestionRepository QuizQuestionRepository { get; }
        IQuizAttemptRepository QuizAttemptRepository { get; }
        ILivestreamRegistrationRepository LivestreamRegistrationRepository  { get; }
        ILivestreamRepository LivestreamRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderRepository OrderRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IPaymentMethodRepository PaymentMethodRepository { get; }
        IProgressRepository ProgressRepository { get; }
        ISubmissionRepository SubmissionRepository { get; }
        

        public Task<int> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
