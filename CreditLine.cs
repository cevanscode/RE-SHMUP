using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE_SHMUP
{
    public class CreditLine
    {
        public string Text;
        public bool IsTitle;

        public CreditLine(string text, bool isTitle)
        {
            Text = text;
            IsTitle = isTitle;
        }
    }
}