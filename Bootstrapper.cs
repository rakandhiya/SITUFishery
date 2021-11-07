using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using SITUFishery.Shell.ViewModels;
using SITUFishery.Helpers;

namespace SITUFishery
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
            _ = ConventionManager.AddElementConvention<PasswordBox>(
                PasswordBoxHelper.BoundPasswordProperty,
                "Password",
                "PasswordChanged");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            _ = DisplayRootViewFor<ShellViewModel>();
        }
    }
}
