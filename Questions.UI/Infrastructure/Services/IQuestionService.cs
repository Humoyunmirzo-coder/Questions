using Aplication.Services;
using Domain.Models;
using Domain.Models.Response;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class IQuestionService : IQuestionServices
    {
        private readonly IQuestionService _questionService  ;
        private readonly QuestionDbContext _questionDbContext ;

        public IQuestionService(IQuestionService questionService, QuestionDbContext questionDbContext)
        {
            _questionService = questionService;
            _questionDbContext = questionDbContext;
        }

        public Task<Response<DocxFile>> CreateDocxFile(DocxFile docxFile)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<Questions>>> GetAllQuestion(DocxFile docxFile)
        {
            throw new NotImplementedException();
        }

        public Task<Response<User>> GetALLUser(DocxFile docxFile)
        {
            throw new NotImplementedException();
        }
    }
}
