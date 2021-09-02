using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.AppServices
{
    public class ChangeNameService : IChangeNameService
    {

        string oldname;
        string newname;
        int foldercount = 0;
        int filecount = 0;
        public string ChangeName(string Path,string oldname, string newname)
        {
            //throw new NotImplementedException();
            this.oldname = oldname;
            this.newname = newname;
            var path = Path;
        
            return BatchRename(path);
        }
        public string BatchRename(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            if (dir.Exists)
            {
                RenameSubDirectories(dir);
                RenameFiles(dir);
                foreach (var subdir in  Directory.GetDirectories(dir.FullName,"*",SearchOption.AllDirectories))
                {
                    RenameFiles(new DirectoryInfo(subdir));
                }
                return $"操作成功,修改了{foldercount}个文件夹,修改了{filecount}个文件";
            }
            else
            {
                return $"操作失败,文件夹路径不存在";
            }
          
        }

        private void RenameSubDirectories(DirectoryInfo dir)
        {

            string[] subDirectories = Directory.GetDirectories(dir.FullName);
            foreach (string subDirectory in subDirectories)
            {
                 
                string subDirectoryName = Path.GetFileName(subDirectory);
                string newSubDirectory = subDirectory;
                if (subDirectoryName.Contains(oldname))
                {
                    string subNewDirectoryName = subDirectoryName.Replace(oldname, newname);
                    newSubDirectory = Path.Combine(dir.FullName, subNewDirectoryName);
                    Directory.Move(subDirectory, newSubDirectory);
                    foldercount++;
                }
                if(Directory.GetDirectories(newSubDirectory).Count()>0)
                {
                    RenameSubDirectories(new DirectoryInfo(newSubDirectory));
                }
            }   

        }

        private void RenameFiles(DirectoryInfo dir)
        {          
            foreach (FileInfo file in dir.GetFiles())
            {
                if (RenameFile(file))
                    filecount++;
            }
        }

        private bool RenameFile(FileInfo file)
        {
            if (file.Name.Contains(oldname))
            {
                var dir= Path.GetDirectoryName(file.FullName);
                var filenewname = file.Name.Replace(oldname, newname);
                file.MoveTo(Path.Combine(dir, filenewname));
                // 给文件改名
                return true;
            }
            else
                return false;
        }
    }
}
