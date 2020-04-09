using System;
using System.Collections.Generic;
using System.Threading;

namespace labyrinth
{
    class Labyrinth
    {
        Cell[,] matrix;
        public readonly Dictionary<string, int[]> start_finish_coordinate = new Dictionary<string, int[]>();
        readonly Dictionary<string, int[]> coordinate_changer = new Dictionary<string, int[]>();
        public readonly int speed_visual_generator;
        readonly bool print_help_lines = false;

        public int Height { get; private set; }
        public int Width { get; private set; }

        public Labyrinth(int height, int width, int speed_visual_generator = 500)
        {
            Height = height;
            Width = width;
            this.speed_visual_generator = speed_visual_generator;
            FillingCoordinChanger();
            GenerateLabyrinth();
            if (speed_visual_generator < 1000)
                Console.SetCursorPosition(0, Height);
            else
            {
                Console.Clear();
                Print();
            }
        }

        void FillingCoordinChanger()
        {
            coordinate_changer[Info.Up] = new int[2] { -1, 0 };
            coordinate_changer[Info.Down] = new int[2] { 1, 0 };
            coordinate_changer[Info.Left] = new int[2] { 0, -1 };
            coordinate_changer[Info.Right] = new int[2] { 0, 1 };
        }

        void GenerateLabyrinth()
        {
            matrix = new Cell[Height, Width];
            string content;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    content = (i == 0 || j == 0 || i == Height - 1 || j == Width - 1) ? Info.Border : Info.Wall;
                    matrix[i, j] = new Cell(i, j, content);
                }
            }
            GenerateStartFinish();
            Print();
            GeneratePath(start_finish_coordinate[Info.Start]);
            EditFinishPosition();
        }

        void GenerateStartFinish()
        {
            Random rand = new Random();
            int i = rand.Next(1, Height - 1);
            int j = rand.Next(1, Width - 1);
            if (i > j) j = 0;
            else i = 0;
            matrix[i, j].SetContent(Info.Start);
            start_finish_coordinate.Add(Info.Start, new int[2] { i, j });

            i = rand.Next(1, Height - 1);
            j = rand.Next(1, Width - 1);
            if (i > j) j = Width - 1;
            else i = Height - 1;
            matrix[i, j].SetContent(Info.Finish);
            start_finish_coordinate.Add(Info.Finish, new int[2] { i, j });
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

        void EditFinishPosition()
        {
            Random rand = new Random();
            int[] temp;
            while (!FinishOurPath())
            {
                int[] fin = start_finish_coordinate[Info.Finish];
                int step = 1 - 2 * rand.Next(2);
                string direct = (fin[0] == Height - 1) ? Info.Left : Info.Up;

                if (TryJump(fin, direct, step))
                {
                    temp = Step(fin, direct, step > 0);
                    SwapContent(fin, temp);
                    start_finish_coordinate[Info.Finish] = temp;
                    UpdatePrintCoordinate(fin);
                    UpdatePrintCoordinate(temp);
                }
            }
        }

        void SwapContent(int[] a, int[] b)
        {
            string temp_content = matrix[a[0], a[1]].Content;
            matrix[a[0], a[1]].SetContent(matrix[b[0], b[1]].Content);
            matrix[b[0], b[1]].SetContent(temp_content);
        }

        bool FinishOurPath()
        {
            string[] direction = { Info.Up, Info.Left };
            foreach (var dir in direction)
            {
                int[] t = PlusCoordinate(start_finish_coordinate[Info.Finish], coordinate_changer[dir]);
                if (matrix[t[0], t[1]].CompairContent(Info.Path)) return true;
            }
            return false;
        }

        bool TryJump(int[] start_coordinate, string direction, int jump, bool print = false)
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
                if (matrix[x, y].Content == Info.Path)// || matrix[x, y].Content == "border")
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
            if (matrix[temp[0], temp[1]].Content == Info.Border || matrix[temp[0], temp[1]].Content == Info.Path)
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
                        if (matrix[t[0], t[1]].Content == Info.Path)
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
            if ((dir1 == Info.Up && dir2 == Info.Down) || (dir2 == Info.Up && dir1 == Info.Down)) return false;
            if ((dir1 == Info.Left && dir2 == Info.Right) || (dir2 == Info.Left && dir1 == Info.Right)) return false;
            return true;
        }

        string AntiDirection(string direction)
        {
            if (direction == Info.Up) return Info.Down;
            else if (direction == Info.Down) return Info.Up;
            else if (direction == Info.Left) return Info.Right;
            else return Info.Left;
        }

        public bool CheckAbroad(int[] coordinate)
        {
            if (coordinate.Length != 2) return false;
            if (coordinate[0] < 0 || coordinate[0] >= Height) return false;
            if (coordinate[1] < 0 || coordinate[1] >= Width) return false;
            return true;
        }

        int[] PlusDirection(string dir1, string dir2)
        {
            if (dir1 == dir2) return Step(new int[2], dir2);
            return Step(coordinate_changer[dir1], dir2);
        }

        public int[] PlusCoordinate(int[] coord1, int[] coord2)
        {
            int[] temp = new int[2];
            temp[0] = coord1[0] + coord2[0];
            temp[1] = coord1[1] + coord2[1];

            return temp;
        }

        public int[] Step(int[] start_coordinate, string direction, bool forward = true)
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
            UpdatePrintCoordinate(t);
            GeneratePath(t);
        }

        public void UpdatePrintCoordinate(int[] coordin)
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

        public string NameCell(int[] coord, int[] plus_coord)
        {
            int[] t = PlusCoordinate(coord, plus_coord);
            return CheckAbroad(t) ? NameCell(t) : Info.Abroad;
        }

        public string NameCell(int[] coord)
        {
            if (CheckAbroad(coord)) return matrix[coord[0], coord[1]].Content;
            return "none";
        }

        public void Print()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    matrix[i, j].Print();
                }
                Console.WriteLine();
            }
        }

        public void ChangeContent(int[] coordinate, string content)
        {
            if (CheckAbroad(coordinate))
            {
                matrix[coordinate[0], coordinate[1]].SetContent(content);
                UpdatePrintCoordinate(coordinate);
            }
        }

        public bool CheckContent(int[] coordinate, string dir, string content)
        {
            int[] temp = PlusCoordinate(coordinate, coordinate_changer[dir]);
            if (!CheckAbroad(temp)) return false;
            return NameCell(temp) == content;
        }

        public bool IsFinish(int[] coord)
        {
            return coord[0] == start_finish_coordinate[Info.Finish][0]
                && coord[1] == start_finish_coordinate[Info.Finish][1];
        }

        public void Reset()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int[] temp = new int[2] { i, j };
                    if (NameCell(temp) == Info.Way)
                        ChangeContent(temp, Info.Path);
                    else if (NameCell(temp) == Info.Theseus)
                        ChangeContent(temp, Info.Finish);
                }
            }
        }

        public int GetLeePoint(int[] coord)
        {
            return matrix[coord[0], coord[1]].LeePoint;
        }

        public int GetLeePoint(int[] coord, string dir, int err_value = int.MaxValue)
        {
            int[] temp = PlusCoordinate(coord, coordinate_changer[dir]);
            if (!CheckAbroad(temp)) return err_value;
            return matrix[temp[0], temp[1]].LeePoint;
        }

        public void ChangeLeePoint(int[] coord, int lee_point)
        {
            matrix[coord[0], coord[1]].LeePoint = lee_point;
        }

        public bool CheckLeePoint(int[] coord, int lee_point)
        {
            if (!CheckAbroad(coord)) return false;
            return matrix[coord[0], coord[1]].LeePoint == lee_point;
        }

        public bool CheckLeePoint(int[] coord)
        {
            if (!CheckAbroad(coord)) return false;
            return matrix[coord[0], coord[1]].LeePoint > -1;
        }

        public bool CheckLeePoint(int[] coord, string dir)
        {
            int[] temp = PlusCoordinate(coord, coordinate_changer[dir]);
            if (!CheckAbroad(temp)) return false;
            return matrix[temp[0], temp[1]].LeePoint > -1;
        }
    }
}
