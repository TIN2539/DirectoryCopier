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
        public void CopyFileAsync(ICollection<string> sourcePath, string targetPath)
        {
            for (int i = 0; i < 3; i++)
            {
                ThreadPool.QueueUserWorkItem(state => CopyFile(sourcePath, targetPath));
            }
        }

        private void CopyFile(ICollection<string> sourcePath, string targetPath)
        {
            var file = sourcePath.First();
            sourcePath.Remove(file);
            FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read);

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
