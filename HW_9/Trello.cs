using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Kiselev_Andrey;

namespace HW_9
{
    [Serializable]
    class Trello
    {
        public string Name { get => "Trello"; }
        public List<Board> Boards { get; private set; }
        string pathJSON;

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
            temp.BoardChanged += SaveTrello;
            Boards.Add(temp);
            SaveTrello();
        }

        public void DelBoardConsole()
        {
            Boards.RemoveAt(StartMenu.Choiсe("Del Board", Boards));
            SaveTrello();
        }

        public void TravelToBoardConsole()
        {
            Console.Clear();
            Boards[StartMenu.Choiсe("Travel", Boards)].ManagerConsole();
        }

        void ChangeSelf(Trello trello)
        {
            Boards = trello.Boards;
        }

        public void SaveTrello()
        {
            Trello temp = new Trello();
            temp.ChangeSelf(this);
            string jsonString = JsonConvert.SerializeObject(temp, Formatting.Indented);
            File.WriteAllText(pathJSON, jsonString);
        }

        void LoadTrelloConsole()
        {
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory.ToString());
            di = di.Parent;
            di = di.Parent;
            di = di.Parent;
            string path = di.ToString() + "\\Saves";
                       
            // если нет папки saves
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var dir = new DirectoryInfo(path);
            
            List<FileInfo> files = new List<FileInfo>();
            foreach (var file in dir.GetFiles("*.json"))
            {
                files.Add(file);
            }

            sbyte choice = StartMenu.Choiсe("Name of saved file", files, "saved file");

            if (choice == -1)
            {
                pathJSON = path + "\\" + ConsoleRead.String("Input name of saved file: ") + ".json";
                using (File.Create(pathJSON)) { }
            }
            else
                pathJSON = files[choice].ToString();

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
                    board.BoardChanged += SaveTrello;
                }
            }
        }

        public void ManagerConsole()
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

            while (true)
            {
                byte choice = StartMenu.Choiсe(Name, nameChoice, nameHiddenChoice, Boards.Count != 0);

                if (choice == 0) break;

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

            StartMenu.Enter();
        }
    }
}
