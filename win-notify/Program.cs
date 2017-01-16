using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Threading;

namespace win_notify
{
    class Program
    {

        static void Main(string[] args)
        {
            Dictionary<string, string> options = new Dictionary<string, string>();

            // All arguments is required
            options.Add("app", null);
            options.Add("timeout", null);
            options.Add("xml", null);

            // Parse
            for (int i = 0; i < args.Length; i++)
            {
                string argsName = args[i].Replace("--", ""); // Fix arg name
                if (options.ContainsKey(argsName))
                {
                    if (args.Length > i + 1)
                    {
                        options[argsName] = args[i + 1];
                    }
                }
            }

            bool argsIsValid = true; // If all arguments is valid
            foreach (string key in options.Keys)
            {
                if (options[key] == null)
                {
                    argsIsValid = false;
                    Console.WriteLine("Invalid argumet: --" + key);
                }
            }

            // Show notf
            if (argsIsValid) showNotification(options);
        }

        static void showNotification(Dictionary<string, string> options)
        {
            // Load xml
            XmlDocument toastXml = new XmlDocument();
            toastXml.LoadXml(options["xml"]);

            // Create notf and listeners
            ToastNotification toast = new ToastNotification(toastXml);

            toast.Activated     += Toast_Activated;
            toast.Dismissed     += Toast_Dismissed;
            toast.Failed        += Toast_Failed;

            // Show notf
            ToastNotificationManager.CreateToastNotifier(options["app"]).Show(toast);

            // Event expectation 
            if (options["timeout"] == "true")
                while (true) { Thread.Sleep(1000); }
        }

        private static void Toast_Activated(ToastNotification sender, object args)
        {
            Console.WriteLine("Activated");
            Environment.Exit(0);
        }

        private static void Toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
        {
            //from https://github.com/nels-o/toaster/
            String outputText = "";
            int exitCode = -1;
            switch (args.Reason)
            {
                case ToastDismissalReason.ApplicationHidden:
                    outputText = "Hidden";
                    exitCode = 1;
                    break;
                case ToastDismissalReason.UserCanceled:
                    outputText = "Dismissed";
                    exitCode = 2;
                    break;
                case ToastDismissalReason.TimedOut:
                    outputText = "Timeout";
                    exitCode = 3;
                    break;
            }
            Console.WriteLine(outputText);
            Environment.Exit(exitCode);
        }

        private static void Toast_Failed(ToastNotification sender, ToastFailedEventArgs args)
        {
            Console.WriteLine("Failed");
            Environment.Exit(-1);
        }

    }
}
