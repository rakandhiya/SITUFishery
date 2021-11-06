using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace SITUFishery.Messages
{
    public class ChangeActivePageMessage
    {
        public Screen ChildScreen { get; private set; }

        public ChangeActivePageMessage(Screen screen)
        {
            ChildScreen = screen;
        }
    }
}
