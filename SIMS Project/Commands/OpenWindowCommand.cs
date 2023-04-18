using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Project.Commands
{
    public class OpenWindowCommand : CommandBase
    {
        public Type _windowType;
        public object[] _args;

        public OpenWindowCommand(Type type, params object?[]? args)
        {
            _windowType = type;
            _args = args;
        }

        public override void Execute(object? parameter)
        {
            Window newWindow = (Window) Activator.CreateInstance(_windowType, _args);
            newWindow.ShowDialog();
        }
    }
}
