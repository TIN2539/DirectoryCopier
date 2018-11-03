using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirectoryCopier.Domain
{
    public class CopyManager : ICopyManager
    {
        public void CopyFileAsync(string sourcePath, string targetPath)
        {
            for (int i = 0; i < 3; i++)
            {
                ThreadPool.QueueUserWorkItem(state => CopyFile(sourcePath, targetPath));
            }
        }

        private void CopyFile(string sourcePath, string targetPath)
        {
            throw new NotImplementedException();
        }

        public int CountOfAllFiles(string path)
        {
            return Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories).Count();
        }

        public IEnumerable<string> GetAllFiles(string path)
        {
            return Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
        }
    }
}
