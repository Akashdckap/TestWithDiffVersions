using System;
using System.IO;
using System.Text;

namespace Assess2.Logg
{
    public class LogFile
    {
        public void GetLog(string data)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents", "LogData.txt");

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                SetLog(fs, data);
            }
         }
        public void SetLog(FileStream fs,string values) {
            byte[] info = new UTF8Encoding(true).GetBytes(values + Environment.NewLine);
            fs.Write(info, 0, info.Length);
        }
    }
}
