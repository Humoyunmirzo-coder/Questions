using Aplication.Services;
using Domain.Models;
using Domain.Models.Response;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Xceed.Words.NET;

namespace Questions.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionServiceses _questionServices;
    

        public QuestionController(IQuestionServiceses questionServices)
        {
            _questionServices = questionServices;
           
        }

        [HttpPost]
        [Route("create-docx")]
        public IActionResult CreateDocxFile([FromBody] DocxFile data)
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
                        var optionParagraph = doc.InsertParagraph(option.Option);
                        optionParagraph.FontSize(12).SpacingAfter(2);
                    }
                }
                doc.Save();
            }
            return Ok(new { message = "Document successfully created", filePath });
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromQuery] Domain.Models.Questions questions)
        {
            var questionsList = new List<Domain.Models.Questions> { questions };
            var saveResponse = await _questionServices.CreateQuestion(questionsList);

            if (!saveResponse.Success)
            {
                return BadRequest(saveResponse.Message);
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedDocs", $"{Guid.NewGuid()}.docx");

            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            using (var doc = DocX.Create(filePath))
            {
                var questionParagraph = doc.InsertParagraph(questions.QuestionInfo);
                questionParagraph.FontSize(12).SpacingAfter(5);

                foreach (var option in questions.Options)
                {
                    var optionParagraph = doc.InsertParagraph(option.Option);
                    optionParagraph.FontSize(12).SpacingAfter(2);
                }
                doc.Save();
            }
            return Ok(new { message = "Document successfully created", filePath });
        }

        [HttpPost("CreateQuestion")]
        public async Task<IActionResult> GetAllQuestion([FromBody] List<Domain.Models.Questions> questions)
        {
            if (questions == null || !questions.Any())
            {
                return BadRequest("Questions list is empty.");
            }
            Random rng = new Random();
            var shuffledQuestions = questions.OrderBy(a => rng.Next()).ToList();

            foreach (var question in shuffledQuestions)
            {
                question.Options = question.Options.OrderBy(a => rng.Next()).ToList();
            }

            var response = await _questionServices.CreateQuestion(shuffledQuestions);
  
            return Ok(new Response<List<Domain.Models.Questions>>(true, "Savollar muvaffaqiyatli yaratildi va saqlandi", shuffledQuestions));
        }







    }

}


