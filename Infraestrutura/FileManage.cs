using System;
using System.IO;

namespace Infraestrutura
{
    public class FileManage
    {
        public static readonly string FolderDataIn = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "in");
        
        private readonly string _folderDataOut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "out");
        private readonly string _folderDataReaded = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "data", "readed");
    }
}
