using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Kiselev_Andrey;

namespace HW_9
{
    [Serializable]
    class Trello
    {
        public string Name { get => "Trello"; }
        public List<Board> Boards { get; private set; }
        string pathJSON;
        string pathLog;

        public Trello()
        {
            Boards = new List<Board>();
        }

        public void AddBoard(Board board)
        {
            Boards.Add(board);
        }

        public void AddBoardConsole()
        {
            Board temp = new Board(ConsoleRead.String("Input name new board: "));
            temp.BoardChanged += SaveTrelloAsync;
            Boards.Add(temp);
            SaveTrelloAsync($"Added Board \"{temp.Name}\"");
        }

        public void DelBoardConsole()
        {
            int choice = StartMenu.Choiсe("Del Board", Boards);
            SaveTrelloAsync($"Board \"{Boards[choice].Name}\" is removed");
            Boards.RemoveAt(choice);
        }

        public void TravelToBoardConsole()
        {
            Console.Clear();
            int choice = StartMenu.Choiсe("Travel", Boards);
            SaveTrelloAsync($"Travel to board \"{Boards[choice].Name}\"");
            Boards[choice].ManagerConsole();
        }

        void ChangeSelf(Trello trello)
        {
            Boards = trello.Boards;
        }

        public async void SaveTrelloAsync(string text)
        {
            Trello temp = new Trello();
            temp.ChangeSelf(this);
            string jsonString = await Task.Run(() => JsonConvert.SerializeObject(temp, Formatting.Indented));
            await Task.Run(() => File.WriteAllText(pathJSON, jsonString));
            await Task.Run(() => File.AppendAllText(pathLog, $"{DateTime.Now} - {text}\n"));
        }

        string GetPathOrCreate(string folder)
        {
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory.ToString());
            di = di.Parent;
            di = di.Parent;
            di = di.Parent;
            string path = di.ToString() + $"\\{folder}";

            // если нет папки saves
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        string ChoiceOrCreateFilePath(string folder, string fileType, string purposeFile)
        {
            string path = GetPathOrCreate($"{folder}");
            var dir = new DirectoryInfo(path);

            List<FileInfo> files = new List<FileInfo>();
            foreach (var file in dir.GetFiles($"*.{fileType}"))
            {
                files.Add(file);
            }

            sbyte choice = StartMenu.Choiсe($"Name of {purposeFile} file", files, $"{purposeFile} file");

            string result;
            if (choice == -1)
            {
                result = path + "\\" + ConsoleRead.String($"Input name of {purposeFile} file: ") + $".{fileType}";
                using (File.Create(result)) { }
            }
            else
                result = files[choice].ToString();

            return result;
        }

        void LoadTrelloConsole()
        {
            pathJSON = ChoiceOrCreateFilePath("Saves", "json", "saved");
            pathLog = ChoiceOrCreateFilePath("Saves", "txt", "log");

            string jsonString = File.ReadAllText(pathJSON);

            if (string.IsNullOrWhiteSpace(jsonString))
            {
                AddBoardConsole();
            }
            else
            {
                ChangeSelf(JsonConvert.DeserializeObject<Trello>(jsonString));
                foreach (var board in Boards)
                {
                    board.BoardChanged += SaveTrelloAsync;
                }
            }
            SaveTrelloAsync($"Trello is load from json \"{pathJSON}\"");
        }

        public async void ManagerConsole()
        {
            SortedDictionary<byte, string> nameChoice = new SortedDictionary<byte, string>
            {
                [10] = "Load Trello"
            };
            SortedDictionary<byte, string> nameHiddenChoice = new SortedDictionary<byte, string>
            {
                [1] = "Add Board",
                [3] = "Print all Board",
                [2] = "Del Board",
                [4] = "Travel to Board",
            };

            bool flag = true;
            while (flag)
            {
                byte choice = StartMenu.Choiсe(Name, nameChoice, nameHiddenChoice, Boards.Count != 0);

                if (choice == 0)
                {
                    SaveTrelloAsync("Exit program");
                    await Task.Run(() => flag = false);
                }

                else if (choice == Dict.KeyByValue(nameChoice, "Load Trello")) LoadTrelloConsole();
                else if (choice == Dict.KeyByValue(nameChoice, "Add Board")) AddBoardConsole();
                else if (Boards.Count == 0)
                {
                    StartMenu.EnterClearConsole("Board count is null");
                    continue;
                }
                else if (choice == Dict.KeyByValue(nameChoice, "Print all Board")) Print();
                else if (choice == Dict.KeyByValue(nameChoice, "Del Board")) DelBoardConsole();
                else if (choice == Dict.KeyByValue(nameChoice, "Travel to Board")) TravelToBoardConsole();

                Console.Clear();
            }
        }

        public void Print()
        {
            Console.WriteLine($"\n\t{Name}\n");

            foreach (var board in Boards)
            {
                Console.WriteLine(board);
            }

            SaveTrelloAsync("Printad all Board name");
            StartMenu.Enter();
        }
    }
}
