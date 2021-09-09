using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Layer {
        public List<Row> ImageRows { get; set; } = new List<Row>();
    }

    public class Row
    {
        public List<int> Pixels { get; set; } = new List<int>();
    }

    public static class Day8
    {
        private static List<string> Input => InputHelper.GetInputListString(2019, 8).ToList();

        public static List<Layer> FillLayers(List<int> image)
        {
            List<Layer> layers = new List<Layer>();
            Row row = new Row();
            Layer layer = new Layer();

            for (int i = 0; i < image.Count; i++)
            {
                if (row.Pixels.Count == 25)
                {
                    layer.ImageRows.Add(row);
                    row = new Row();
                }

                if (layer.ImageRows.Count == 6)
                {
                    layers.Add(layer);
                    layer = new Layer();
                }

                row.Pixels.Add(image[i]);
            }

            layer.ImageRows.Add(row);
            layers.Add(layer);

            return layers;
        }

        public static void Part1(List<int> image)
        {
            List<Layer> layers = FillLayers(image);
            Layer fewestZeroesLayer = new Layer();
            int zeroCount = 25 * 6;

            foreach (Layer imageLayer in layers)
            {
                int count = 0;
                foreach (Row imageRow in imageLayer.ImageRows)
                {
                    count += imageRow.Pixels.Count(x => x == 0);
                }

                if (count < zeroCount)
                {
                    zeroCount = count;
                    fewestZeroesLayer = imageLayer;
                }
            }

            int ones = 0;
            int twos = 0;
            foreach (Row imageRow in fewestZeroesLayer.ImageRows)
            {
                ones += imageRow.Pixels.Count(x => x == 1);
                twos += imageRow.Pixels.Count(x => x == 2);
            }

            Console.WriteLine($"The number of 1 digits {ones} multiplied by the number of 2 digits {twos} in the row with the fewest 0 is {ones * twos}");

            return;
        }

        public static void Part2(List<int> image)
        {
            List<Layer> layers = FillLayers(image);
            Layer passwordImage = new Layer();
            passwordImage.ImageRows = layers[0].ImageRows;

            foreach (Layer layer in layers)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        if (passwordImage.ImageRows[i].Pixels[j] == 2)
                        {
                            passwordImage.ImageRows[i].Pixels[j] = layer.ImageRows[i].Pixels[j];
                        }
                    }
                }
            }

            foreach (Row row in passwordImage.ImageRows)
            {
                foreach (int pixel in row.Pixels)
                {
                    if (pixel == 0)
                    {
                        Console.Write(" ");
                    }
                    else if (pixel == 1)
                    {
                        Console.Write("#");
                    }
                    else if (pixel == 2)
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            return;
        }

        public static void Start()
        {
            List<int> image = new List<int>();
            for (int i = 0; i < Input[0].Length; i++)
            {
                image.Add(int.Parse(Input[0].Substring(i, 1)));
            }

            Part1(image);
            Part2(image);
        }
    }
}
