using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Project.Commands
{
    public class CloseWindowCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            Window currentWindow = (Window) parameter;
            currentWindow.Close();
        }
    }
}
