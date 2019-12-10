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
            Dictionary<(int, int), List<(int, int)>> quantities = new Dictionary<(int, int), List<(int, int)>>();

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

            for (int x = 0; x < input.Length; x++)
                for (int y = 0; y < input[x].Length; y++)
                {
                    if (field[x][y] == 0) continue;

                    for (int tX = 0; tX < input.Length; tX++)
                        for (int tY = 0; tY < input[x].Length; tY++)
                        {
                            if (field[tX][tY] == 0) continue;
                            if (x == tX && y == tY) continue;

                            for (float i = 0; i <= 1.1f; i += 0.001f)
                            {
                                var pos = Lerp(new Vector2 { X = x, Y = y }, new Vector2 { X = tX, Y = tY }, i);
                                if (pos.X == x && pos.Y == y) continue;

                                if (field[pos.X][pos.Y] == 1 && !(pos.X == tX && pos.Y == tY)) break;

                                if (pos.X == tX && pos.Y == tY)
                                {
                                    if (!quantities.ContainsKey((x, y))) quantities.Add((x, y), new List<(int, int)>());
                                    if (!quantities[(x, y)].Contains((pos.X, pos.Y))) quantities[(x, y)].Add((pos.X, pos.Y));
                                }

                                //if (field[pos.X][pos.Y] == 1)
                                //{
                                //    if (!quantities.ContainsKey((x, y))) quantities.Add((x, y), new List<(int, int)>());
                                //    if (!quantities[(x, y)].Contains((pos.X, pos.Y))) quantities[(x, y)].Add((pos.X, pos.Y));
                                //    break;
                                //}
                            }
                        }
                }

            foreach (var quantity in quantities) 
                Console.WriteLine($"X:{quantity.Key.Item1} Y:{quantity.Key.Item2} #:{quantity.Value.Count}");

            //PrintField(field);
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


        private class Vector2
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
