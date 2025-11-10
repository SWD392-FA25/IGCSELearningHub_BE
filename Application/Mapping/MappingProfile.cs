using Application.DTOs.Accounts;
using Application.DTOs.Authentication;
using Application.DTOs.Devices;
using Application.DTOs.Quiz;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ConfigureAccounts();
            ConfigureQuizzes();
            ConfigureDevices();
        }

        private void ConfigureAccounts()
        {
            CreateMap<AccountRegistrationDTO, Account>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Active"))
                .ForMember(dest => dest.ExternalProvider, opt => opt.Condition(src => src.IsExternal));

            CreateMap<Account, AccountDTO>();
        }

        private void ConfigureQuizzes()
        {
            CreateMap<QuestionOption, QuizDetailOptionDTO>()
                .ForMember(dest => dest.OptionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.OptionText));

            CreateMap<QuizQuestion, QuizDetailQuestionDTO>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.OrderIndex, opt => opt.MapFrom(src => src.OrderIndex))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points))
                .ForMember(dest => dest.Stem, opt => opt.MapFrom(src => src.Question.Stem))
                .ForMember(dest => dest.Explanation, opt => opt.MapFrom(src => src.Question.Explanation))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Question.Difficulty))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Question.Type))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Question.QuestionOptions));

            CreateMap<Quiz, QuizSummaryDTO>()
                .ForMember(dest => dest.TotalQuestions, opt => opt.MapFrom(src => src.TotalQuestions ?? src.QuizQuestions.Count));

            CreateMap<Quiz, QuizDetailDTO>()
                .IncludeBase<Quiz, QuizSummaryDTO>()
                .ForMember(dest => dest.TotalQuestions, opt => opt.MapFrom(src => src.TotalQuestions ?? src.QuizQuestions.Count))
                .ForMember(dest => dest.RandomizeQuestions, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.RandomizeOptions, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.QuizQuestions.OrderBy(q => q.OrderIndex)));
        }

        private void ConfigureDevices()
        {
            CreateMap<Device, DeviceDTO>();
        }
    }
}
