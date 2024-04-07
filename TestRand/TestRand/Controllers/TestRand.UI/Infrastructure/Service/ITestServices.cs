using Aplication.Service;
using Domain.Model;
using Domain.Response;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace Infrastructure.Service
{

    public class ITestServices : ITestService
    {
        private readonly ITestService _service;
        private readonly TestDbContext _context;

        public ITestServices(ITestService service , TestDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<Response<DocxFile>> CreateDocxFile(DocxFile docxFile)
        {
            string filePath = Path.Combine("SizningYo'lingiz", $"{docxFile.Path}.docx");

            using (var doc = DocX.Create(filePath))
            {
                foreach (var question in docxFile.Questions)
                {
                    doc.InsertParagraph(question.QuestionInfo).Bold();

                    foreach (var answer in question.Options)
                    {
                        doc.InsertParagraph(answer.Option);
                    }
                    doc.InsertParagraph();
                }
                doc.Save();
            }

            return null;   // await new Response<DocxFile>(true, $"{docxFile.Path}.docx fayli muvaffaqiyatli yaratildi", docxFile);
        }

        public Task<Response<List<Questions>>> CreateQuestion(List<Questions> questions)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<DocxFile>>> GetALLDocxFile()
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<Questions>>> GetAllQuestion()
        {
            throw new NotImplementedException();
        }
    }
}
