using Domain.Models;
using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public  interface IQuestionServices
    {
        Task < Response< List< Questions>>>  GetAllQuestion ( DocxFile docxFile ) ;
        Task < Response< DocxFile>>  CreateDocxFile (  DocxFile docxFile ) ;
        Task < Response< User>>  GetALLUser(  DocxFile docxFile ) ;

    }
}
