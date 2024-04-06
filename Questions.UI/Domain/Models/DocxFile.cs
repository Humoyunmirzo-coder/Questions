using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class DocxFile
    {
        [Key]
         public int Id { get; set; }
        public List< Questions> Questions { get; set; }
        public string Path { get; set; }

    }
}
