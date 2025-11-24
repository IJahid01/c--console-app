using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KeyAuth
{
    class Program
    {

       
        public static api KeyAuthApp = new api(
            name: "JAHID CHEATS", 
            ownerid: "JjpGjXJhfE",
            version: "1.0"
        );

        



        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern ushort GlobalFindAtom(string lpString);


        //static void JAHIDCHEATS(String massage)
        //{
        //    var guru = Deta
        //    String dateString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //}

        static void Main(string[] args)
        {

            securityChecks();

            Console.Title = "Loader";
            Console.WriteLine("\n\n Connecting..");
            KeyAuthApp.init();
            Console.WriteLine("\n Status: " + KeyAuthApp.response.message);
            Console.WriteLine("\n\n Welcome to the JAHID CHEATS Loader!");









            // Print the logo in a colorful way



            // Define the logo lines
            Console.WriteLine("\n\n");
            Console.WriteLine("\n\n");
            Console.WriteLine("\n\n");


            string[] lines = new string[]
       {
            "$$$$$$\\  $$\\   $$\\ $$$$$$$\\  $$\\   $$\\          $$$$$\\  $$$$$$\\  $$\\   $$\\ $$$$$$\\ $$$$$$$\\  ",
            "$$  __$$\\ $$ |  $$ |$$  __$$\\ $$ |  $$ |         \\__$$ |$$  __$$\\ $$ |  $$ |\\_$$  _|$$  __$$\\ ",
            "$$ /  \\__|$$ |  $$ |$$ |  $$ |$$ |  $$ |            $$ |$$ /  $$ |$$ |  $$ |  $$ |  $$ |  $$ |",
            "$$ |$$$$\\ $$ |  $$ |$$$$$$$  |$$ |  $$ |            $$ |$$$$$$$$ |$$$$$$$$ |  $$ |  $$ |  $$ |",
            "$$ |\\_$$ |$$ |  $$ |$$  __$$< $$ |  $$ |      $$\\   $$ |$$  __$$ |$$  __$$ |  $$ |  $$ |  $$ |",
            "$$ |  $$ |$$ |  $$ |$$ |  $$ |$$ |  $$ |      $$ |  $$ |$$ |  $$ |$$ |  $$ |  $$ |  $$ |  $$ |",
            "\\$$$$$$  |\\$$$$$$  |$$ |  $$ |\\$$$$$$  |      \\$$$$$$  |$$ |  $$ |$$ |  $$ |$$$$$$\\ $$$$$$$  |",
            " \\______/  \\______/ \\__|  \\__| \\______/        \\______/ \\__|  \\__|\\__|  \\__|\\______|\\_______/ "
       };

            // Define some colors
            ConsoleColor[] colors = new ConsoleColor[]
            {
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Yellow,
            ConsoleColor.Blue,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta,
            ConsoleColor.White,
            ConsoleColor.Gray
            };

            for (int i = 0; i < lines.Length; i++)
            {
                Console.ForegroundColor = colors[i % colors.Length];
                Console.WriteLine(lines[i]);
            }

            Console.ResetColor();


            Console.WriteLine("\n\n");







            // Check if the user is running the program with admin privileges

            autoUpdate();

            if (!KeyAuthApp.response.success)
            {
                Console.WriteLine("\n Status: " + KeyAuthApp.response.message);
                Thread.Sleep(1500);
                TerminateProcess(GetCurrentProcess(), 1);
            }

            Console.Write("\n [1] Login\n [2] Register\n\n Choose option: ");

            string username, password, key;

            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.Write("\n\n Enter username: ");
                    username = Console.ReadLine();
                    Console.Write("\n\n Enter password: ");
                    password = Console.ReadLine();
                    Console.Write("\n\n Enter 2fa code: (not using 2fa? Just press enter) ");
                    
                    KeyAuthApp.login(username, password);
                    break;
                case 2:
                    Console.Write("\n\n Enter username: ");
                    username = Console.ReadLine();
                    Console.Write("\n\n Enter password: ");
                    password = Console.ReadLine();
                    Console.Write("\n\n Enter license: ");
                    key = Console.ReadLine();
                    Console.Write("\n\n Enter email (just press enter if none): ");
                    
                    KeyAuthApp.register(username, password, key);
                    break;
                
                    
                   
                default:
                    Console.WriteLine("\n\n Invalid Selection");
                    Thread.Sleep(2500);
                    TerminateProcess(GetCurrentProcess(), 1);
                    break; 
            }

            if (!KeyAuthApp.response.success)
            {
                Console.WriteLine("\n Status: " + KeyAuthApp.response.message);
                Thread.Sleep(2500);
                TerminateProcess(GetCurrentProcess(), 1);
            }

            Console.WriteLine("\n Logged In!"); 

            if(string.IsNullOrEmpty(KeyAuthApp.response.message)) TerminateProcess(GetCurrentProcess(), 1);

            // user data
            Console.WriteLine("\n User data:");
            Console.WriteLine(" Username: " + KeyAuthApp.user_data.username);
            Console.WriteLine(" License: " + KeyAuthApp.user_data.subscriptions[0].key);
            Console.WriteLine(" IP address: " + KeyAuthApp.user_data.ip);
            Console.WriteLine(" Hardware-Id: " + KeyAuthApp.user_data.hwid);
            Console.WriteLine(" Created at: " + UnixTimeToDateTime(long.Parse(KeyAuthApp.user_data.createdate)));
            if (!string.IsNullOrEmpty(KeyAuthApp.user_data.lastlogin)) 
                Console.WriteLine(" Last login at: " + UnixTimeToDateTime(long.Parse(KeyAuthApp.user_data.lastlogin)));
            Console.WriteLine(" Your subscription(s):");
            for (var i = 0; i < KeyAuthApp.user_data.subscriptions.Count; i++)
            {
                Console.WriteLine(" Subscription name: " + KeyAuthApp.user_data.subscriptions[i].subscription + " - Expires at: " + UnixTimeToDateTime(long.Parse(KeyAuthApp.user_data.subscriptions[i].expiry)) + " - Time left in seconds: " + KeyAuthApp.user_data.subscriptions[i].timeleft);
            }

            Console.Write("\n [1] Enable 2FA\n [2] Disable 2FA\n Choose option: ");
            int tfaOptions = int.Parse(Console.ReadLine());
            switch (tfaOptions)
            {
                case 1:
                    KeyAuthApp.enable2fa();
                    break;
             
                default:
                    Console.WriteLine("\n\n Invalid Selection");
                    Thread.Sleep(2500);
                    TerminateProcess(GetCurrentProcess(), 1);
                    break; 
            }

            Console.WriteLine("\n Closing in five seconds...");
            Thread.Sleep(-1);
            Environment.Exit(0);
        }

        public static bool SubExist(string name)
        {
            if(KeyAuthApp.user_data.subscriptions.Exists(x => x.subscription == name))
                return true;
            return false;
        }
		
        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            }
            catch
            {
                dtDateTime = DateTime.MaxValue;
            }
            return dtDateTime;
        }

        static void checkAtom()
        {
            Thread atomCheckThread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(60000); 

                    ushort foundAtom = GlobalFindAtom(KeyAuthApp.ownerid);
                    if (foundAtom == 0)
                    {
                        TerminateProcess(GetCurrentProcess(), 1);
                    }
                }
            });

            atomCheckThread.IsBackground = true; 
            atomCheckThread.Start();
        }

        static void securityChecks()
        {
            
            var frames = new StackTrace().GetFrames();
            foreach (var frame in frames)
            {
                MethodBase method = frame.GetMethod();
                if (method != null && method.DeclaringType?.Assembly != Assembly.GetExecutingAssembly())
                {
                    TerminateProcess(GetCurrentProcess(), 1);
                }
            }

            
            var harmonyAssembly = AppDomain.CurrentDomain
            .GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == "0Harmony");

            if (harmonyAssembly != null)
            {
                TerminateProcess(GetCurrentProcess(), 1);
            }

            checkAtom();
        }

        static void autoUpdate()
        {
            if (KeyAuthApp.response.message == "invalidver")
            {
                if (!string.IsNullOrEmpty(KeyAuthApp.app_data.downloadLink))
                {
                    Console.WriteLine("\n Auto update avaliable!");
                    Console.WriteLine(" Choose how you'd like to auto update:");
                    Console.WriteLine(" [1] Open file in browser");
                    Console.WriteLine(" [2] Download file directly");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Process.Start(KeyAuthApp.app_data.downloadLink);
                            Environment.Exit(0);
                            break;
                        case 2:
                            Console.WriteLine(" Downloading file directly..");
                            Console.WriteLine(" New file will be opened shortly..");

                            WebClient webClient = new WebClient();
                            string destFile = Application.ExecutablePath;

                            string rand = random_string();

                            destFile = destFile.Replace(".exe", $"-{rand}.exe");
                            webClient.DownloadFile(KeyAuthApp.app_data.downloadLink, destFile);

                            Process.Start(destFile);
                            Process.Start(new ProcessStartInfo()
                            {
                                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                CreateNoWindow = true,
                                FileName = "cmd.exe"
                            });
                            Environment.Exit(0);

                            break;
                        default:
                            Console.WriteLine(" Invalid selection, terminating program..");
                            Thread.Sleep(1500);
                            Environment.Exit(0);
                            break;
                    }
                }
                Console.WriteLine("\n Status: Version of this program does not match the one online. Furthermore, the download link online isn't set. You will need to manually obtain the download link from the developer.");
                Thread.Sleep(2500);
                Environment.Exit(0);
            }
        }

        static string random_string()
        {
            string str = null;

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;
        }
    }
}
