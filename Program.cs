using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace InkKeeper
{
    class inkKeeper {
        private string configPath;
        private DateTime lastDate;
        private DateTime currentDate;
        private int interval;

        public inkKeeper() {
            this.configPath = "config.txt";
            this.currentDate = DateTime.Now;
            try
            {
                string[] lines = File.ReadAllLines(this.configPath);
                string str = "";
                foreach(string line in lines)
                {
                    Console.WriteLine(line);
                    string[] subs = line.Split('=');
                    string variable = subs[0];
                    string value = subs[1];
                    switch (variable)
                    {
                        case "interval":
                            this.interval = int.Parse(value);
                            break;
                        case "lastDate":
                            this.lastDate = DateTime.Parse(value);
                            break;
                    }
                }
            } catch (Exception e)
            {
                string type = e.GetType().ToString();
                Console.WriteLine(type);
                this.lastDate = DateTime.Now;
                if(type == "System.IO.FileNotFoundException") Console.WriteLine("File was not found, creating...");
                if (type == "System.FormatException") Console.WriteLine("File has incorrect format, creating new one...");
                this.interval = 5;
                this.writeData();
            }
        }

        public void writeData() 
        {
            string str = "lastDate= " + this.currentDate.ToString() + "\ninterval= " + this.interval.ToString();
            File.WriteAllText(configPath, str); 
        }
        public string returnPath() { return this.configPath; }
        public int returnInterval() { return this.interval; }
        public DateTime returnCurrentDate() { return this.currentDate; }
        public DateTime returnLastDate() { return this.lastDate; }
        public TimeSpan compareDates() { return this.currentDate.Subtract(this.lastDate); }

        public bool compareThreshold(int threshold)
        {
            if (this.compareDates() > TimeSpan.FromDays(threshold)) return true;
            return false;
        }
        public void debugAddDays(double days) { this.currentDate = this.currentDate.AddDays(days); }
        public bool checkInkDecay(int days)
        {
            bool shouldIprint = this.compareThreshold(days);
            if(shouldIprint)
            {
                writeData();
                string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath) + "\\PDF";
                string[] files = Directory.GetFiles(@strWorkPath);
                foreach (string file in files.Where(
                            file => file.ToUpper().Contains(".PDF")))
                {
                    Console.WriteLine("priting:" + file);
                    PDF.PrintPDFs(file);
                }
                return true;
            }
            return false;
        }

    }
    
    class Program {
        static void Main(string[] args)
        {
            inkKeeper keeper = new inkKeeper();
            int days = keeper.returnInterval();
            Console.WriteLine(" \n_____________________________________________\n");
            Console.WriteLine("Ink keeper script! Current interval: " + days.ToString()) ;
            Console.WriteLine("_____________________________________________\n");
            if (!keeper.checkInkDecay(days)) {
                Console.WriteLine("Interval not reached yet");
                Console.WriteLine("Next print: " + keeper.returnLastDate().AddDays(days).ToString());
            }
            Console.WriteLine("_____________________________________________ \n");
            Console.WriteLine("Good bye!");

            /* Debug section */
            /*
            bool debug = true;
            string reads;
            while (debug == true)
            {
                try
                {
                    Console.WriteLine("Number OR Y|YES|0");
                    reads = Console.ReadLine();
                    if (reads == "Y" || reads == "YES" || reads == "0") debug = false;
                    else keeper.debugAddDays(double.Parse(reads));
                } catch (Exception e)
                {
                    Console.WriteLine("Incorrect number, skipping...");
                }

                Console.WriteLine("Current date:" + keeper.returnCurrentDate().ToString());
                Console.WriteLine("Last date:" + keeper.returnLastDate().ToString());
                Console.WriteLine("differences:" + keeper.compareDates());
                Console.WriteLine("Threshold?:" + keeper.compareThreshold(4));
            }
            */
            Thread.Sleep(5000);
        }
    }
}
