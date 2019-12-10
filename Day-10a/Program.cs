using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_10a
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(@"day10a-input.txt");

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

            //input = new[]
            //{
            //    "#.#...#.#.",
            //    ".###....#.",
            //    ".#....#...",
            //    "##.#.#.#.#",
            //    "....#.#.#.",
            //    ".##..###.#",
            //    "..#...##..",
            //    "..##....##",
            //    "......#...",
            //    ".####.###."
            //};

            //input = new[]
            //{
            //    ".#..#..###",
            //    "####.###.#",
            //    "....###.#.",
            //    "..###.##.#",
            //    "##.##.#.#.",
            //    "....###..#",
            //    "..#.#..#.#",
            //    "#..#.#.###",
            //    ".##...##.#",
            //    ".....#.#.."
            //};


            List<(int x, int y, int q)> asteroids = new List<(int x, int y, int q)>();

            for (int y = 0; y < input.Length; y++)
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#') asteroids.Add((x, y, 0));
                }

            for (int i = 0; i < asteroids.Count; i++)
            {
                for (int t = 0; t < asteroids.Count; t++)
                {
                    bool canSee = true;

                    for (int o = 0; o < asteroids.Count; o++)
                    {
                        if (asteroids[o] == asteroids[i] || asteroids[o] == asteroids[t]) continue;

                        if (IsCollinear((asteroids[i].x, asteroids[i].y),
                            (asteroids[t].x, asteroids[t].y),
                            (asteroids[o].x, asteroids[o].y)))
                            canSee = false;
                    }

                    if (canSee)
                    {
                        var newValue = asteroids[i];
                        newValue.q++;
                        asteroids[i] = newValue;
                    }
                }
            }

            var bestLocation = asteroids.OrderByDescending(x => x.q).First();
            Console.WriteLine($"Best location is x:{bestLocation.x} y:{bestLocation.y} value:{bestLocation.q}");
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

        static bool IsCollinear((int x, int y) p1, (int x, int y) p2, (int x, int y) p3)
        {
            int a = p1.x * (p2.y - p3.y) +
                    p2.x * (p3.y - p1.y) +
                    p3.x * (p1.y - p2.y);

            return a == 0;
        }
    }
}
