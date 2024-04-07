using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.File
{
    public  class FilePath
    {
        public string Pathth { get; set; }

        public string FullPath { get; private set; }

        public FilePath(string path)
        {
            FullPath = path;
        }

        public string GetFileName()
        {
            return Path.GetFileName(FullPath);
        }

        public string GetDirectoryName()
        {
            return Path.GetDirectoryName(FullPath);
        }

        public long GetFileSize()
        {
            var fileInfo = new FileInfo(FullPath);
            return fileInfo.Length;
        }

    }
}
