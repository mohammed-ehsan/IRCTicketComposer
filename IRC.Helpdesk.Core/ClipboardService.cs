using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IRC.Helpdesk.Core
{
    public class ClipboardService : IClipBoard
    {
        public string GetText()
        {
            return Clipboard.GetText();
        }
    }
}
