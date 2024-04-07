using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public  class Questions
    {
        [Key]
        public int Id { get; set; }
        public string QuestionInfo { get; set; }
        public long OptionId { get; set; }
        public List<Options> Options { get; set; }

    }
}
