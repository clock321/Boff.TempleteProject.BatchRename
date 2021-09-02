using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.AppServices
{
    public interface IChangeNameService
    {
         string ChangeName(string Path, string oldname,string newname);
    }
}
