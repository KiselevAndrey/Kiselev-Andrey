using System;
using System.Collections.Generic;
using System.Threading;

namespace labyrinth
{
    class Labyrinth
    {
        Cell[,] matrix;
        readonly Dictionary<string, int[]> start_finish_coordinate = new Dictionary<string, int[]>();
        readonly Dictionary<string, int[]> coordinate_changer = new Dictionary<string, int[]>();
        readonly int speed_visual_generator = 500;
        readonly bool print_help_lines = false;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Labyrinth(int width, int height)
        {
            Width = width;
            Height = height;
            FillingCoordinChanger();
            GenerateLabyrinth();
            //Print();
            Console.SetCursorPosition(0, Width);
        }

        void FillingCoordinChanger()
        {
            coordinate_changer["up"] = new int[2] { -1, 0 };
            coordinate_changer["down"] = new int[2] { 1, 0 };
            coordinate_changer["left"] = new int[2] { 0, -1 };
            coordinate_changer["right"] = new int[2] { 0, 1 };
        }

        void GenerateLabyrinth()
        {
            matrix = new Cell[Width, Height];
            string content;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    content = (i == 0 || j == 0 || i == Width - 1 || j == Height - 1) ? "border" : "wall";
                    matrix[i, j] = new Cell(i, j, content);
                }
            }
            GenerateStartFinish();
            Print();
            GeneratePath(start_finish_coordinate["start"]);
        }

        void GenerateStartFinish()
        {
            Random rand = new Random();
            int i = rand.Next(1, Width - 1);
            int j = rand.Next(1, Height - 1);
            if (i > j) j = 0;
            else i = 0;
            matrix[i, j].SetContent("start");
            start_finish_coordinate.Add("start", new int[2] { i, j });

            i = rand.Next(1, Width - 1);
            j = rand.Next(1, Height - 1);
            if (i > j) j = Height - 1;
            else i = Width - 1;
            matrix[i, j].SetContent("finish");
            start_finish_coordinate.Add("finish", new int[2] { i, j });
        }

        void GeneratePath(int[] cooirdinate)
        {
            string[] direction = new string[coordinate_changer.Keys.Count];
            coordinate_changer.Keys.CopyTo(direction, 0);

            Kiselev_Andrey.Array.Shaffle(ref direction);
            if(print_help_lines) Console.WriteLine($"{cooirdinate[0]}, {cooirdinate[1]}");
            for (int i = 0; i < direction.Length; i++)
            {
                if (!TryJump(cooirdinate, direction[i], 2, print_help_lines))
                {
                    continue;
                }
                if (!CheckPaths(cooirdinate, direction[i]))
                {
                    continue;
                }
                NewStep(cooirdinate, direction[i]);
                return;
            }
            StepBack(cooirdinate);
        }

        bool TryJump(int[] start_coordinate, string direction, int jump, bool print = true)
        {
            if (print) Console.WriteLine(direction);
            if (start_coordinate.Length != 2 || !coordinate_changer.ContainsKey(direction))
            {
                if (print) Console.WriteLine("Wrong coordinate.Length or direction");
                return false;
            }

            int x = start_coordinate[0] + coordinate_changer[direction][0] * jump;
            int y = start_coordinate[1] + coordinate_changer[direction][1] * jump;

            int[] temp = new int[2] { x, y };
            if (CheckAbroad(temp))
            {
                if (matrix[x, y].Content == "path")// || matrix[x, y].Content == "border")
                {
                    if (print) Console.WriteLine("Path is blocked");
                    return false;
                }
            }
            else return false;

            return true;
        }

        bool CheckPaths(int[] start_coordinate, string direction)
        {
            int[] temp = Step(start_coordinate, direction);
            if (matrix[temp[0], temp[1]].Content == "border" || matrix[temp[0], temp[1]].Content == "path")
            {
                return false;
            }
            foreach (var dir in coordinate_changer.Keys)
            {
                foreach (var dop_dir in coordinate_changer.Keys)
                {
                    if (ChekPlusDirection(dir, dop_dir, direction))
                    {
                        int[] plus_dir = PlusDirection(dir, dop_dir);
                        int[] t = PlusCoordinate(temp, plus_dir);
                        if (!CheckAbroad(t)) continue;
                        if (matrix[t[0], t[1]].Content == "path")
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        bool ChekPlusDirection(string dir1, string dir2, string forbidden_dir)
        {
            forbidden_dir = AntiDirection(forbidden_dir);
            if (dir1 == forbidden_dir || dir2 == forbidden_dir) return false;
            if ((dir1 == "up" && dir2 == "down") || (dir2 == "up" && dir1 == "down")) return false;
            if ((dir1 == "left" && dir2 == "right") || (dir2 == "left" && dir1 == "right")) return false;
            return true;
        }

        string AntiDirection(string direction)
        {
            switch (direction)
            {
                case "up":
                    return "down";

                case "down":
                    return "up";

                case "left":
                    return "right";

                case "right":
                    return "left";

                default:
                    return "";
            }
        }

        bool CheckAbroad(int[] coordinate)
        {
            if (coordinate.Length != 2) return false;
            if (coordinate[0] < 0 || coordinate[0] >= Width) return false;
            if (coordinate[1] < 0 || coordinate[1] >= Height) return false;
            return true;
        }

        int[] PlusDirection(string dir1, string dir2)
        {
            if (dir1 == dir2) return Step(new int[2], dir2);
            return Step(coordinate_changer[dir1], dir2);
        }

        int[] PlusCoordinate(int[] coord1, int[] coord2)
        {
            int[] temp = new int[2];
            temp[0] = coord1[0] + coord2[0];
            temp[1] = coord1[1] + coord2[1];

            return temp;
        }

        int[] Step(int[] start_coordinate, string direction, bool forward = true)
        {
            int[] result = new int[2];
            int step = forward ? 1 : -1;

            result[0] = start_coordinate[0] + coordinate_changer[direction][0] * step;
            result[1] = start_coordinate[1] + coordinate_changer[direction][1] * step;

            return result;
        }

        void NewStep(int[] start_coordinate, string direction, bool forward = true)
        {
            int[] t = Step(start_coordinate, direction, forward);
            if (forward)
            {
                matrix[t[0], t[1]].SetPrevCell(matrix[start_coordinate[0], start_coordinate[1]]);
                matrix[t[0], t[1]].WallFromPath();
            }
            UpdatePrintCoordinate(start_coordinate);
            GeneratePath(t);
        }

        void UpdatePrintCoordinate(int[] coordin)
        {
            if (speed_visual_generator < 1000)
            {
                Console.SetCursorPosition(coordin[1], coordin[0]);
                matrix[coordin[0], coordin[1]].Print();
                Thread.Sleep(1000 / speed_visual_generator);
            }
        }

        void StepBack(int[] start_coordinate)
        {
            Cell temp = matrix[start_coordinate[0], start_coordinate[1]];
            int[] prev_coord = temp.PrevCell.GetCoordinate();
            if (!temp.CompairCoord(prev_coord))
                GeneratePath(prev_coord);
        }

        public void Print()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    matrix[i, j].Print();
                }
                Console.WriteLine();
            }
        }
    }
}
