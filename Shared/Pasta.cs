using System;
using System.IO;

namespace Shared
{
    public static class Pasta
    {
        private static readonly string _userProfileFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public static string FolderDataIn
        {
            get { return Path.Combine(_userProfileFolder, "data", "in"); }
        }

        public static string FolderDataOut
        {
            get { return Path.Combine(_userProfileFolder, "data", "out"); }
        }

        public static string FolderDataReaded
        {
            get { return Path.Combine(_userProfileFolder, "data", "readed"); }
        }

        public static void GerenciarDiretorios()
        {
            if (!Directory.Exists(FolderDataIn)) Directory.CreateDirectory(FolderDataIn);
            if (!Directory.Exists(FolderDataOut)) Directory.CreateDirectory(FolderDataOut);
            if (!Directory.Exists(FolderDataReaded)) Directory.CreateDirectory(FolderDataReaded);
        }
    }
}