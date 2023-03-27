int opc;
do
{
    opc = App.Option(); // Lê a opção selecionada pelo usuário e armazena em opc.

    if (opc == 1) // Verifica se a opção selecionada é 1 (Organizar Diretório).
    {
        Console.WriteLine("Organize Folder"); // Exibe uma mensagem no console.
        App.Organize(); // Chama a função Organize() para organizar o diretório.
    }

} while (opc != 0); // Repete o loop até que a opção selecionada seja 0 (Sair).


public class App
{
    // Exibe o menu de opções e lê a entrada do usuário.
    public static int Option()
    {
        Console.WriteLine("Select Option");
        Console.WriteLine("1 - Organize Folder  ");
        Console.WriteLine("0 - Exit");
        Console.Write("Option: ");
        return PatternOption(Console.ReadLine());
    }

    // Converte a entrada do usuário em um valor inteiro correspondente à opção selecionada.
    private static int PatternOption(string? opc) => opc switch
    {
        "1" => 1,
        "0" => 0,
        _ => -1
    };

    // Organiza o diretório com base nas extensões dos arquivos.
    public static void Organize()
    {
        Console.Write("Enter path directory should you like organized: ");
        var path = Console.ReadLine(); // Lê o caminho do diretório fornecido pelo usuário e armazena em path.


        // valida se o caminho é nulo ou vazio.
        if (string.IsNullOrEmpty(path))
        {
            Console.WriteLine("Path is null");
        }
        else
        {
            var directory_exists = Directory.Exists(path);

            // Verifica se o diretório existe.
            if (directory_exists)
            {
                var files_in_directory = Directory.GetFiles(path); // Armazena todos os arquivos do diretório.

                // Percorre todos os arquivos do diretório.
                foreach (string file in files_in_directory)
                {
                    // Cria um objeto FileInfo com base no arquivo atual.
                    var file_info = new FileInfo(file);

                    // Armazena o nome da pasta que será criada com base na extensão do arquivo.
                    var folder_name = file_info.Extension.Replace(".", "");

                    // Armazena o caminho da pasta que será criada.
                    var folder_path = path + "/" + folder_name;


                    // Verifica se a pasta já existe.
                    if (Directory.Exists(folder_path))
                    {            
                        if (File.Exists(folder_path + "/" + file_info.Name))
                        {
                            // Deleta o arquivo caso já exista um arquivo com o mesmo nome.
                            File.Delete(folder_path + "/" + file_info.Name);
                        }

                        // Move o arquivo para a pasta.
                        File.Move(file, folder_path + "/" + file_info.Name);
                    }
                    else
                    {
                        // Cria a pasta caso ela não exista.
                        Directory.CreateDirectory(path + "/" + folder_name);
                        
                        // Move o arquivo para a pasta.
                        Directory.Move(file, path + "/" + folder_name + "/" + file_info.Name);

                    }
                }

                var directories = Directory.GetDirectories(path);
                    // imprime o nome dos diretórios criados
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


