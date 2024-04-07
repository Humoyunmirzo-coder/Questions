using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public  class User
    {
        public int Id { get; set; }
        public int DocxId { get; set; }
        public DocxFile DocxFile { get; set; } = new DocxFile();
    }
}
