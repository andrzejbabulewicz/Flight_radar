using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project.sources
{
    public class ShortcutStringEventArgs : EventArgs
    {
        public List<string> StringList { get; set; }

        public ShortcutStringEventArgs(List<string> stringList)
        {
            StringList = stringList;
        }
    }
}
