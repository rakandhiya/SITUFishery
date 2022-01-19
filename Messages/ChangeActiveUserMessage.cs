using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITUFishery.Messages
{
    public class ChangeActiveUserMessage
    {
        public string ActiveUser { get; private set; }

        public ChangeActiveUserMessage(string activeUser)
        {
            ActiveUser = activeUser;
        }
    }
}
