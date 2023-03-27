int opc;
do
{
    opc = App.Option();

    if (opc == 1)
    {
        Console.WriteLine("Organize Folder");
        App.Organize();
    }

} while (opc != 0);



public class App
{

    public static int Option()
    {
        Console.WriteLine("Select Option");
        Console.WriteLine("1 - Organize Folder  ");
        Console.WriteLine("0 - Exit");
        Console.Write("Option: ");
        return PatternOption(Console.ReadLine());
    }
    private static int PatternOption(string? opc) => opc switch
    {
        "1" => 1,
        "0" => 0,
        _ => -1
    };
    public static void Organize()
    {
        Console.Write("Enter path directory should you like organized: ");
        var path = Console.ReadLine();

        if (string.IsNullOrEmpty(path))
        {
            Console.WriteLine("Path is null");
        }
        else
        {
            var directory_exists = Directory.Exists(path);

            if (directory_exists)
            {
                var files_in_directory = Directory.GetFiles(path);

                foreach (string file in files_in_directory)
                {
                    var file_info = new FileInfo(file);

                    var folder_name = file_info.Extension.Replace(".", "");

                    var folder_path = path + "/" + folder_name;



                    if (Directory.Exists(folder_path))
                    {
                        if (File.Exists(folder_path + "/" + file_info.Name))
                        {
                            File.Delete(folder_path + "/" + file_info.Name);
                        }
                        File.Move(file, folder_path + "/" + file_info.Name);
                    }
                    else
                    {
                        Directory.CreateDirectory(path + "/" + folder_name);

                        Directory.Move(file, path + "/" + folder_name + "/" + file_info.Name);

                    }
                }

                var directories = Directory.GetDirectories(path);
                foreach (var directory in directories)
                {
                    var file_info = new FileInfo(directory);
                    Console.WriteLine(file_info.FullName);
                }
                Console.WriteLine("Directory organized successfully");
                Console.WriteLine("Press Enter To Continue");
            }
            else
            {
                Console.WriteLine("Directory does not exist");
                Console.WriteLine("Press Enter To Continue");
            }
        }

        Console.ReadKey();
    }


}


