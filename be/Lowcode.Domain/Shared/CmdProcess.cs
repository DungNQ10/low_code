using System;
using System.Collections.Generic;
using System.Text;

namespace Lowcode.Domain.Shared
{
    public static class CmdProcess
    {
        public static bool Process(string commandText)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/user:Administrator /C {commandText}" ;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
