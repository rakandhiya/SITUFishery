using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SITUFishery.Modules.AuthenticationModule.ViewModels;

namespace SITUFishery.Shell.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            IEventAggregator eventAggregator = new EventAggregator();
            ActivateItemAsync(new LoginViewModel(eventAggregator));
        }
    }
}
