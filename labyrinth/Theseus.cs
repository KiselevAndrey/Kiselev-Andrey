using System;
using System.Collections.Generic;
using System.Text;

namespace labyrinth
{
    class Theseus
    {
        readonly Labyrinth labyrinth;
        readonly Dictionary<string, Dictionary<string, int[]>> azimuth = new Dictionary<string, Dictionary<string, int[]>>();

        int[] pos;
        int[] pos_cursor;
        string direction;
        readonly string[] directions = { Info.Up, Info.Right, Info.Down, Info.Left };

        public int Step { get; private set; }

        public Theseus(Labyrinth labyrinth)
        {
            this.labyrinth = labyrinth;
            CreateAzimut();
            Step = 0;
            StartAdventure();
        }

        void CreateAzimut()
        {
            int[][] change_dir = new int[4][];
            change_dir[0] = new int[2] { -1, 0 };
            change_dir[1] = new int[2] { 0, 1 };
            change_dir[2] = new int[2] { 1, 0 };
            change_dir[3] = new int[2] { 0, -1 };

            for (int i = 0; i < directions.Length; i++)
            {
                azimuth[directions[i]] = new Dictionary<string, int[]>();
                for (int j = 0; j < directions.Length; j++)
                {
                    azimuth[directions[i]][directions[j]] = Kiselev_Andrey.Matrix.Geting(change_dir, i + j);
                }
            }

            direction = directions[3];
        }

        void StartAdventure()
        {
            RightHand(labyrinth.Height);
            LeftHand(labyrinth.Height + 1);
            Lee(labyrinth.Height + 2);
        }

        void RightHand(int column_cursor)
        {
            Step = 0;
            int[] p_c = { 0, column_cursor };
            pos_cursor = p_c;
            Console.SetCursorPosition(pos_cursor[0], pos_cursor[1]);

            pos = labyrinth.start_finish_coordinate[Info.Start];

            string text = "Right-hand traffic (step): ";
            Console.Write($"{text}{Step}");
            pos_cursor[0] = text.Length;

            while (!labyrinth.IsFinish(pos))
            {
                // Forward & Left not Finish
                if (!CheckFinish(Info.Up) && !CheckFinish(Info.Left))
                {
                    // Right != wall && != border && != abroad
                    if (DontWallBorderAbroad(pos, Info.Right))
                    {
                        Turn(Info.Right);
                        TryStep(Info.Up);
                    }
                    // Forward == Path or Way
                    else if (labyrinth.NameCell(pos, azimuth[direction][Info.Up]) == Info.Path
                        || labyrinth.NameCell(pos, azimuth[direction][Info.Up]) == Info.Way)
                    {
                        TryStep(Info.Up);
                    }
                    // Right == wall & Forward == wall
                    else
                    {
                        Turn(Info.Left);
                        TryStep(Info.Up);
                    }
                }
            }
            Console.ReadLine();
            labyrinth.Reset();
            Console.SetCursorPosition(0, pos_cursor[1] + 1);
        }

        void LeftHand(int column_cursor)
        {
            Step = 0;
            int[] p_c = { 0, column_cursor };
            pos_cursor = p_c;
            Console.SetCursorPosition(pos_cursor[0], pos_cursor[1]);

            pos = labyrinth.start_finish_coordinate[Info.Start];

            string text = "Left-hand traffic (step): ";
            Console.Write($"{text}{Step}");
            pos_cursor[0] = text.Length;

            while (!labyrinth.IsFinish(pos))
            {
                // Forward & Right not Finish
                if (!CheckFinish(Info.Up) && !CheckFinish(Info.Right))
                {
                    // Left != wall && != border && != abroad
                    if (DontWallBorderAbroad(pos, Info.Left))
                    {
                        Turn(Info.Left);
                        TryStep(Info.Up);
                    }
                    // Forward == Path or Way
                    else if (labyrinth.NameCell(pos, azimuth[direction][Info.Up]) == Info.Path
                        || labyrinth.NameCell(pos, azimuth[direction][Info.Up]) == Info.Way)
                    {
                        TryStep(Info.Up);
                    }
                    // Left == wall & Forward == wall
                    else
                    {
                        Turn(Info.Right);
                        TryStep(Info.Up);
                    }
                }
            }
            Console.ReadLine();
            labyrinth.Reset();
            Console.SetCursorPosition(0, pos_cursor[1] + 1);
        }

        void Turn(string dir)
        {
            int index, summ = 0;
            index = Array.IndexOf(directions, direction);
            if (index != -1)
            {
                if (dir == Info.Right) summ = 1;
                if (dir == Info.Left) summ = -1;

                direction = Kiselev_Andrey.Array.Geting(directions, index + summ);
            }
        }

        void TryStep(string dir)
        {
            if (DontWallBorderAbroad(pos, dir))
            {
                int[] temp = labyrinth.PlusCoordinate(pos, azimuth[direction][dir]);
                if (labyrinth.NameCell(pos) != Info.Start)
                {
                    labyrinth.ChangeContent(pos, Info.Way);
                }
                labyrinth.ChangeContent(temp, Info.Theseus);
                pos = temp;
                Step++;
                Console.SetCursorPosition(pos_cursor[0], pos_cursor[1]);
                Console.Write(Step);
            }
        }

        void TryStep(int[] coord)
        {
            if (DontWallBorderAbroad(coord))
            {
                int[] temp = coord;
                if (labyrinth.NameCell(pos) != Info.Start)
                {
                    labyrinth.ChangeContent(pos, Info.Way);
                }
                labyrinth.ChangeContent(temp, Info.Theseus);
                Step++;
                pos = temp;
                Console.SetCursorPosition(pos_cursor[0], pos_cursor[1]);
                Console.Write(Step);
            }
        }

        bool CheckFinish(string dir)
        {
            if (labyrinth.NameCell(pos, azimuth[direction][dir]) == Info.Finish)
            {
                TryStep(dir);
                return true;
            }
            return false;
        }

        bool DontWallBorderAbroad(int[] pos, string dir)
        {
            bool dont_wall = labyrinth.NameCell(pos, azimuth[direction][dir]) != Info.Wall;
            bool dont_border = labyrinth.NameCell(pos, azimuth[direction][dir]) != Info.Border;
            bool dont_abroad = labyrinth.NameCell(pos, azimuth[direction][dir]) != Info.Abroad;
            return dont_wall && dont_border && dont_abroad;
        }

        bool DontWallBorderAbroad(int[] pos)
        {
            bool dont_wall = labyrinth.NameCell(pos) != Info.Wall;
            bool dont_border = labyrinth.NameCell(pos) != Info.Border;
            bool dont_abroad = labyrinth.NameCell(pos) != Info.Abroad;
            return dont_wall && dont_border && dont_abroad;
        }


        void Lee(int column_cursor)
        {
            #region start info
            Step = 0;
            int[] p_c = { 0, column_cursor };
            pos_cursor = p_c;
            Console.SetCursorPosition(pos_cursor[0], pos_cursor[1]);

            pos = labyrinth.start_finish_coordinate[Info.Start];

            string text = "Lee traffic (step): ";
            Console.Write($"{text}{Step}");
            pos_cursor[0] = text.Length;
            #endregion

            // у стартовой ячейки LeePoint = 0
            int lp = 0;
            labyrinth.ChangeLeePoint(labyrinth.start_finish_coordinate[Info.Start], lp);

            Queue<int[]> find_set = new Queue<int[]>();

            find_set.Enqueue(labyrinth.start_finish_coordinate[Info.Start]);

            // запуск волны
            do
            {
                int[] temp = find_set.Dequeue();
                lp = labyrinth.GetLeePoint(temp);

                // пробегаемся по соседям
                foreach (var dir in directions)
                {
                    // если (сосед path или finish) и он не пронумерован
                    if ((labyrinth.CheckContent(temp, dir, Info.Path) || labyrinth.CheckContent(temp, dir, Info.Finish)) && !labyrinth.CheckLeePoint(temp, dir))
                    {
                        int[] t = labyrinth.Step(temp, dir);
                        find_set.Enqueue(t);
                        labyrinth.ChangeLeePoint(t, lp + 1);
                    }
                }

            } while (find_set.Count != 0 || !labyrinth.CheckLeePoint(labyrinth.start_finish_coordinate[Info.Finish]));

            // создание маршрута
            Stack<int[]> way_back = new Stack<int[]>();
            way_back.Push(labyrinth.start_finish_coordinate[Info.Finish]);

            while (lp != 0)
            {
                int[] temp = way_back.Peek();
                lp = labyrinth.GetLeePoint(temp);

                foreach (var dir in directions)
                {
                    if (labyrinth.GetLeePoint(temp, dir) < lp && labyrinth.GetLeePoint(temp, dir) != -1)
                    {
                        int[] t = labyrinth.Step(temp, dir);
                        way_back.Push(t);
                        break;
                    }
                }
            }

            // проход по маршруту
            way_back.Pop();
            while (way_back.TryPop(out int[] temp))
            {
                TryStep(temp);
            }

            Console.ReadLine();
            labyrinth.Reset();
            Console.SetCursorPosition(0, pos_cursor[1] + 1);

        }
    }
}