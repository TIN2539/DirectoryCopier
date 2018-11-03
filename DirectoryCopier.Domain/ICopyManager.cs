using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCopier.Domain
{
    public interface ICopyManager
    {
        int CountOfAllFiles(string path);
        void CopyFileAsync(string sourcePath, string targetPath);
        IEnumerable<string> GetAllFiles(string path);
    }
}
