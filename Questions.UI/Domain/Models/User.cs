using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class User
    {

        public int Id { get; set; }
         public int DocxId { get; set; }
         public DocxFile DocxFile  { get; set; }
    }
}
