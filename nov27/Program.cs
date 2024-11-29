using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the File Management Utility");
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. List files in a directory");
            Console.WriteLine("2. Rename a file");
            Console.WriteLine("3. Move a file");
            Console.WriteLine("4. Delete a file");
            Console.WriteLine("5. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListFiles();
                    break;
                case "2":
                    RenameFile();
                    break;
                case "3":
                    MoveFile();
                    break;
                case "4":
                    DeleteFile();
                    break;
                case "5":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

  
    static void ListFiles()
    {
        Console.Write("Enter the directory path: ");
        string path = Console.ReadLine();

        if (Directory.Exists(path))
        {
            var files = Directory.GetFiles(path);
            Console.WriteLine("\nFiles in directory:");
            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }
        else
        {
            Console.WriteLine("Directory does not exist.");
        }
    }

  
    static void RenameFile()
    {
        Console.Write("Enter the directory path: ");
        string path = Console.ReadLine();

        if (Directory.Exists(path))
        {
            Console.Write("Enter the filename to rename: ");
            string oldName = Console.ReadLine();
            string oldFilePath = Path.Combine(path, oldName);

            if (File.Exists(oldFilePath))
            {
                Console.Write("Enter the new filename: ");
                string newName = Console.ReadLine();
                string newFilePath = Path.Combine(path, newName);

                try
                {
                    File.Move(oldFilePath, newFilePath);
                    Console.WriteLine($"File renamed to {newName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error renaming file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }
        else
        {
            Console.WriteLine("Directory does not exist.");
        }
    }


    static void MoveFile()
    {
        Console.Write("Enter the source directory path: ");
        string sourcePath = Console.ReadLine();

        if (Directory.Exists(sourcePath))
        {
            Console.Write("Enter the filename to move: ");
            string fileName = Console.ReadLine();
            string sourceFilePath = Path.Combine(sourcePath, fileName);

            if (File.Exists(sourceFilePath))
            {
                Console.Write("Enter the destination directory path: ");
                string destinationPath = Console.ReadLine();

                if (Directory.Exists(destinationPath))
                {
                    string destinationFilePath = Path.Combine(destinationPath, fileName);
                    try
                    {
                        File.Move(sourceFilePath, destinationFilePath);
                        Console.WriteLine($"File moved to {destinationPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error moving file: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Destination directory does not exist.");
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }
        else
        {
            Console.WriteLine("Source directory does not exist.");
        }
    }


    static void DeleteFile()
    {
        Console.Write("Enter the directory path: ");
        string path = Console.ReadLine();

        if (Directory.Exists(path))
        {
            Console.Write("Enter the filename to delete: ");
            string fileName = Console.ReadLine();
            string filePath = Path.Combine(path, fileName);

            if (File.Exists(filePath))
            {
                Console.Write($"Are you sure you want to delete {fileName}? (y/n): ");
                string confirmation = Console.ReadLine().ToLower();

                if (confirmation == "y")
                {
                    try
                    {
                        File.Delete(filePath);
                        Console.WriteLine("File deleted.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting file: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("File deletion canceled.");
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }
        else
        {
            Console.WriteLine("Directory does not exist.");
        }
       


    
        static void Main(string[] args)
        {
            // Set up Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()     // Log to console
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day) // Log to a file
                .CreateLogger();

            try
            {
                Log.Information("Application Starting...");
                // Your application logic here
                DoWork();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        static void DoWork()
        {
            Log.Information("Performing some work...");
          
            try
            {
                int result = 10 / int.Parse("0");  // This will throw an exception
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while doing work");
            }
        }
    }
}
