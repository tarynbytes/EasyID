
using System.CommandLine;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EasyID
{
    public class Driver
    {
        private string _rawInput;
        private int _length;
        private string _content;

        public Driver(string data)
        {
            _rawInput = data;
        }

        public string RawInput { get { return _rawInput; } }
        public int Length { get { return _length; } }
        public string Content { get { return _content; } }


        static async Task<int> Main(string[] args)
        {
            // Create root command
            var rootCommand = new RootCommand("A tool that identifies data types.");

            // Create subcommand arguments
            var dataArg = new Argument<string>(
                name: "data-string",
                description: "The data string to be identified.");

            var fileArg = new Argument<FileInfo?>(
                name: "file",
                description: "Specify FULL path of file to parse (i.e: C:\\Users\\user\\Documents\\parseMe.txt).");


            // Create argument options
            var lightModeOption = new Option<bool>(
                name: "--light-mode",
                description: "Background color of text displayed on the console: default is black, light mode is white.");

            var fgcolorOption = new Option<ConsoleColor>(
                name: "--fgcolor",
                description: "Foreground color of text displayed on the console.",
                getDefaultValue: () => ConsoleColor.White);

            lightModeOption.AddAlias("-lm");
            fgcolorOption.AddAlias("-fg");

            // Create subcommands
            var subCommandData = new Command("data", "Specifies the data string to be identified.");
            var subCommandFile = new Command("file", "Specifies the file to be parsed for data-strings to be identified.") { fgcolorOption, lightModeOption };

            rootCommand.Add(subCommandData);
            rootCommand.Add(subCommandFile);

            subCommandData.Add(dataArg);
            subCommandFile.Add(fileArg);

            // Create command handlers
            subCommandFile.SetHandler((fileArg, fgcolor, lightMode) =>
            {
                ParseFile(fileArg!, fgcolor, lightMode);
            },
            fileArg, fgcolorOption, lightModeOption);


            subCommandData.SetHandler((data) =>
            {
                ParseData(data);
            },
            dataArg);

            return await rootCommand.InvokeAsync(args);
        }

        internal static void ParseFile(
                    FileInfo file, ConsoleColor fgColor, bool lightMode)
        {
            //string originalDir = Directory.GetCurrentDirectory();
            string[]? dir = file.FullName.Split('\\').Reverse().Skip(1).Reverse().ToArray();
            string? dirPath = string.Join("\\", dir);
            Directory.SetCurrentDirectory(dirPath);
            try
            {
                Console.BackgroundColor = lightMode ? ConsoleColor.White : ConsoleColor.Black;
                Console.ForegroundColor = fgColor;
                var lines = File.ReadLines(file.Name).ToList();
                foreach (string line in lines)
                {
                    // File implementation of program (time permitted)
                    // Gather all words from file,
                    // For each word in the file that is potentially a data-string,
                    Console.WriteLine(line); // Output <word> in fun bolded different color
                    // Create a list of instantiated Driver objects for each word
                    // Call module-selection funciton on each Driver object in list
                };
            }
            catch
            {
                Console.WriteLine("File not found or inaccessible permissions.");
                System.Environment.Exit(1);
            }
            // Is change back to originalDir necessary?
        }

        internal static void ParseData(string data)
        {
            Driver d = new Driver(data);
            d._length = data.Length;
            if (Regex.IsMatch(data, "^[a-zA-Z0-9]*$")) { d._content = "alphanumeric"; }
            if (Regex.IsMatch(data, @"^\d+$")) { d._content = "numeric"; }
            if (Regex.IsMatch(data, @"^[a-zA-Z]+$")) { d._content = "alphabetic"; }

            Data.SetDriver(d);
            ModuleSelectTest(d);
        }

        public static void ModuleSelectTest(Driver d)
        {
            // 1. Call SSN init
            bool b = true;
            SocialSecurityNumber ssn = new SocialSecurityNumber(d);
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(ssn))
            {
                object value = descriptor.GetValue(ssn);
                if (value == null)
                {
                    Console.WriteLine("Not a Social Security Number!");
                    b = false;
                    break;
                }
            }
            // 2. Call SSN Process()
            if (b)
            {
                Console.WriteLine("Data matched on: <Social Security Number>!");
                ssn.Process();

            }
            // 3. Call SSN de-init
        }


        //To-Do: Intelligent module-selection function
        public static void ModuleSelect(Driver d)
        {
            System.Environment.Exit(1);
        }
    }
}
