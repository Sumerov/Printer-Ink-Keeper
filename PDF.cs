using System;
using System.Diagnostics;
using System.Linq;

namespace InkKeeper
{
    public class PDF
    {
        public static Boolean PrintPDFs(string pdfFileName)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Verb = "print";

                //Define location of adobe reader/command line
                //switches to launch adobe in "print" mode
                proc.StartInfo.FileName =
                  @"C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe";
                proc.StartInfo.Arguments = String.Format(@"/p /h {0}", pdfFileName);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (proc.HasExited == false)
                {
                    proc.WaitForExit(10000);
                }

                proc.EnableRaisingEvents = true;

                proc.Close();
                KillAdobe("AcroRd32");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //For whatever reason, sometimes adobe likes to be a stage 5 clinger.
        //So here we kill it with fire.
        private static bool KillAdobe(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses().Where(
                         clsProcess => clsProcess.ProcessName.StartsWith(name)))
            {
                clsProcess.Kill();
                return true;
            }
            return false;
        }
    }//END Class
}
