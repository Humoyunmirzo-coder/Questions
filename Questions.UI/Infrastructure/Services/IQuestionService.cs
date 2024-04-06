using Aplication.Services;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Domain.Models;
using Domain.Models.Response;
using Infrastructure.Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Xceed.Words.NET;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Files;


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

            

          /*  using (QuestionDbContext context = new QuestionDbContext())
            {
                // ShuffledQuestions-ni ma'lumotlar bazasiga qo'shish
                context.Questions.AddRange(questions);
                // O'zgarishlarni saqlash
                await context.SaveChangesAsync();
            }*/



            return new Response<List<Questions>>(true, "Savollar muvaffaqiyatli yaratildi va saqlandi", shuffledQuestions);
        }

        public Task<Response<List<DocxFile>>> GetALLDocxFile()
        {
            // Barcha docx fayllarini olib, ro'yxatga joylash
            var docxFiles = GetAllDocxFilesFromStorage(); // Bu funksiya yaratilgan docx fayllarini olib keladi

            // Muvaffaqiyatli javobni qaytarish
            return Task.FromResult(new Response<List<DocxFile>>(true, "Barcha docx fayllar muvaffaqiyatli olingan", docxFiles));
        }
       


        public async Task<Response<List<Questions>>> GetAllQuestion()
        {
            try
            {

                // Bu yerda, ma'lumotlar manbai (masalan, ma'lumotlar bazasi) dan savollar ro'yxatini olishingiz kerak.
                var question = await _questionDbContext.Questions.ToListAsync();
                // Bu misolda, biz ma'lumotlar bazasidan savollarni qanday olish mumkinligini ko'rsatmayapmiz,
                var options = await _questionDbContext.Options.ToListAsync();

                // chunki bu sizning ma'lumotlar bazasi modeli va so'rovlariga bog'liq.

                // O'rniga, biz soxta (mock) ma'lumotlar bilan ishlayapmiz.
      
                // Agar savollar muvaffaqiyatli olingan bo'lsa, muvaffaqiyatli javob qaytariladi.
                return  await Task.FromResult(new Response<List<Questions>>(true, "Barcha savollar muvaffaqiyatli olingan", question));
            }
            catch (Exception ex)
            {
                // Agar xatolik yuz bersa, muvaffaqiyatsiz javob qaytariladi.
                return  await  Task.FromResult(new Response<List<Questions>>(false, ex.Message, null));
            }
        }


        // Bu metod ma'lum bir katalogdagi barcha .docx fayllarini ro'yxat sifatida qaytaradi
        private List<DocxFile> GetAllDocxFilesFromStorage()
        {
            var filesDirectory = @"C:\Users\humoy\OneDrive\Hujjatlar\Graphics\Filter Library"; // Masalan, @"C:\Documents\"
            var docxFiles = Directory.GetFiles(filesDirectory, "*.docx", SearchOption.AllDirectories)
                                     .Select(filePath => new DocxFile { Path = filePath })
                                     .ToList();
            return docxFiles;
        }

        public Task<Response<List<DocxFile>>> GetAllDocxFiles()
        {
            // Barcha docx fayllarini olib, ro'yxatga joylash
            var docxFiles = GetAllDocxFilesFromStorage();

            // Muvaffaqiyatli javobni qaytarish
            return Task.FromResult(new Response<List<DocxFile>>(true, "Barcha docx fayllar muvaffaqiyatli olingan", docxFiles));
        }

      
    }
}
