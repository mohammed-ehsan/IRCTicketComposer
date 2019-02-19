using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Helpdesk.Core
{
    public class FileInfo : IFileInfo
    {
        #region Private Properties

        private System.IO.FileInfo info;

        #endregion

        #region Public Properties

        public bool Exists { get; }
        public long Length { get; }
        public string PhysicalPath { get; }
        public string Name { get; }
        public DateTimeOffset LastModified { get; }
        public bool IsDirectory { get; }

        #endregion  

        #region Constructors

        public FileInfo(string filePath)
        {
            this.PhysicalPath = filePath;
            if (!File.Exists(filePath))
                throw new FileNotFoundException();
            this.Exists = true;
            info = new System.IO.FileInfo(filePath);
            this.Name = info.Name;
            this.IsDirectory = false;
            this.LastModified = info.LastWriteTime;
        }

        #endregion

        public Stream CreateReadStream()
        {
            return info.OpenRead();
        }
    }
}
