using System;
using System.Collections.Generic;
using System.IO;

namespace Day_10a
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(@"day10a-input.txt");
            Dictionary<(int, int), int> quantities = new Dictionary<(int, int), int>();

            input = new[]
            {
                "......#.#.",
                "#..#.#....",
                "..#######.",
                ".#.#.###..",
                ".#..#.....",
                "..#....#.#",
                "#..#....#.",
                ".##.#..###",
                "##...#..#.",
                ".#....####"
            };

            int[][] field = new int[input.Length][];
            for (int i = 0; i < field.Length; i++)
                field[i] = new int[input[0].Length];

            for (int x = 0; x < input.Length; x++)
                for (int y = 0; y < input[x].Length; y++)
                    field[x][y] = input[x][y] == '#' ? 1 : 0;

            //Testing asteroid
            for (int x = 0; x < input.Length; x++)
                for (int y = 0; y < input[x].Length; y++)
                {
                    if (field[x][y] == 0) continue;

                    //Target asteroid
                    for (int tX = 0; tX < input.Length; tX++)
                        for (int tY = 0; tY < input[x].Length; tY++)
                        {
                            if (field[tX][tY] == 0) continue;
                            if (x == tX && y == tY) continue;

                            bool exists = true;
                            //Test every _other_ asteroid to see if they get in the way
                            for (int testX = 0; testX < input.Length; testX++)
                                for (int testY = 0; testY < input[x].Length; testY++)
                                {
                                    if ((testX == x && testY == y) || (testX == tX && testY == tY)) continue;

                                    if(ExistsOnLine((x,y),(tX, tY), (testX, testY))) exists = false;
                                }

                            if (exists)
                            {
                                if (!quantities.ContainsKey((x, y))) quantities.Add((x, y), 0);
                                quantities[(x, y)]++;
                            }
                        }
                }

            foreach (var quantity in quantities)
                Console.WriteLine($"X:{quantity.Key.Item1} Y:{quantity.Key.Item2} #:{quantity.Value}");
        }

        static void PrintField(int[][] field)
        {
            for (int x = 0; x < field.Length; x++)
            {
                for (int y = 0; y < field[0].Length; y++)
                {
                    Console.Write(field[x][y] == 1 ? "#" : ".");
                }
                Console.WriteLine();
            }
        }

        static Vector2 Lerp(Vector2 start, Vector2 end, float amount)
        {
            if (amount >= 1) return end;
            return new Vector2 { X = Lerp(start.X, end.X, amount), Y = Lerp(start.Y, end.Y, amount) };
        }

        static int Lerp(int start, int end, float amount) => (int)(start * (1 - amount) + end * amount);

        static bool ExistsOnLine((int, int) start, (int, int) end, (int, int) point)
        {
            int dxc = point.Item1 - start.Item1;
            int dyc = point.Item2 - start.Item2;

            int dxl = end.Item1 - start.Item1;
            int dyl = end.Item2 - start.Item2;

            return dxc * dyl - dyc * dxl == 0;
        }

        static float Cross((int, int) value1, (int, int) value2) =>
            Cross(new Vector2 {X = value1.Item1, Y = value1.Item2},
                new Vector2 {X = value2.Item1, Y = value2.Item2});

        static float Cross(Vector2 value1, Vector2 value2)
        {
            return value1.X * value2.Y
                   - value1.Y * value2.X;
        }

        private class Vector2
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
