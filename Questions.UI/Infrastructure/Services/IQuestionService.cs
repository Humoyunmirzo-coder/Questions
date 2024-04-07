using Aplication.Services;
using Domain.Models;
using Domain.Models.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Xceed.Words.NET;


namespace Infrastructure.Services
{
    public class IQuestionService : IQuestionServiceses
    {
      private readonly IQuestionServiceses _services;
        private readonly QuestionDbContext _questionDbContext;

        public IQuestionService(QuestionDbContext questionDbContext, IQuestionServiceses services)
        {

            _questionDbContext = questionDbContext;
            _services = services;
            
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

            return new Response<DocxFile>(true, $"{docxFile.Path}.docx fayli muvaffaqiyatli yaratildi", docxFile);
        }
        public async Task SaveFilePathToDatabase(string filePath)
        {
            DocxFile docxRecord = new DocxFile { Path = filePath };
            _questionDbContext.DocxFiles.Add(docxRecord);
            await _questionDbContext.SaveChangesAsync();
        }

        public async Task<Response<List<Questions>>> CreateQuestion(List<Questions> questions)
        {
            Random rng = new Random();
            var shuffledQuestions = questions.OrderBy(a => rng.Next()).ToList();

            foreach (var question in shuffledQuestions)
            {
                question.Options = question.Options.OrderBy(a => rng.Next()).ToList();
            }

            _questionDbContext.Questions.AddRange(shuffledQuestions);

            await _questionDbContext.SaveChangesAsync();


            return new Response<List<Questions>>(true, "Savollar muvaffaqiyatli yaratildi va saqlandi", shuffledQuestions);
        }

        public Task<Response<List<DocxFile>>> GetALLDocxFile()
        {
            var docxFiles = GetAllDocxFilesFromStorage();


            return Task.FromResult(new Response<List<DocxFile>>(true, "Barcha docx fayllar muvaffaqiyatli olingan", docxFiles));
        }



        public async Task<Response<List<Questions>>> GetAllQuestion()
        {
            try
            {


                var question = await _questionDbContext.Questions.ToListAsync();

                var options = await _questionDbContext.Options.ToListAsync();


                return await Task.FromResult(new Response<List<Questions>>(true, "Barcha savollar muvaffaqiyatli olingan", question));
            }
            catch (Exception ex)
            {

                return await Task.FromResult(new Response<List<Questions>>(false, ex.Message, null));
            }
        }



        private List<DocxFile> GetAllDocxFilesFromStorage()
        {
            var filesDirectory = @"D:\hp";
            var docxFiles = Directory.GetFiles(filesDirectory, "*.docx", SearchOption.AllDirectories)
                                     .Select(filePath => new DocxFile { Path = filePath })
                                     .ToList();
            return docxFiles;
        }

        public Task<Response<List<DocxFile>>> GetAllDocxFiles()
        {

            var docxFiles = GetAllDocxFilesFromStorage();


            return Task.FromResult(new Response<List<DocxFile>>(true, "Barcha docx fayllar muvaffaqiyatli olingan", docxFiles));
        }


    }
}
