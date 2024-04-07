using Domain.Models;
using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;


namespace Aplication.Services
{
    public  interface IQuestionServiceses
    {
        Task<Response<List<Questions>>> CreateQuestion (List<Questions> questions) ;
        Task < Response< DocxFile>>  CreateDocxFile (  DocxFile docxFile ) ;
        Task<Response<List<Questions>>> GetAllQuestion () ;
        Task < Response< List< DocxFile>>>  GetALLDocxFile(  ) ;

        public interface IDocxService
        {
            Task<string> GenerateDocxFileAsync(Questions data);
        }


        public class DocxService 
        {
            public async Task<string> GenerateDocxFileAsync(DocxFile data)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedDoc.docx");  

                using (var doc = DocX.Create(filePath))
                {
                    foreach (var question in data.Questions)
                    {
                        var paragraph = doc.InsertParagraph(question.QuestionInfo);
                        paragraph.FontSize(12).SpacingAfter(5);

                        foreach (var option in question.Options)
                        {
                            var optionParagraph = doc.InsertParagraph(filePath);
                            optionParagraph.FontSize(12).SpacingAfter(2);
                        }
                    }
                    doc.Save();
                }

                return filePath;
            }

           
        }

    }
}
