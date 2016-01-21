using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.ExportData;

namespace Dothan.ExportCmd
{
    class Program
    {
        const string Tag_Tip = "cmd-> ";

        static void Main(string[] args)
        {
            Console.Title = "DAManager Command Line Client";
            CmdManager manager = new CmdManager();
            Console.Clear();

            if (args.Length > 0)
                manager.ExecuteExport(args);
            else
                OpenExportClient(manager);
        }

        private static void ShowWelcome()
        {
            Console.WriteLine("Welcome to the ExportCmdTools.");
            Console.WriteLine();
            Console.WriteLine("Copyright (c) 2015 qlzg. All rights reserved.");
            Console.WriteLine();
            Console.WriteLine("Type 'help;' for help. Type 'clear;' to clear the current input statement.");
            Console.WriteLine();
        }

        private static void OpenExportClient(CmdManager manager)
        {
            ShowWelcome();
            while (true)
            {
                Console.Write(Tag_Tip);
                string command = Console.ReadLine();
                if (command == null)
                {
                    // Ctrl+C退出应用
                    Console.Write("是否要退出(Y/N): ");
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                        continue;
                    }
                }

                command = command.TrimEnd(';');
                if (string.IsNullOrEmpty(command))
                    continue;
                if (command.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                    command.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                    command.Equals("bye", StringComparison.OrdinalIgnoreCase))
                    break;

                manager.ExecuteCommand(command.Split(' '));
            }
        }
    }
}
