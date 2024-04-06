using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class Options
    {
        [ForeignKey("id")]
        public int Id { get; set; }
        public string Option { get; set; }
        public string TrueOption{ get; set; }
    }
}
