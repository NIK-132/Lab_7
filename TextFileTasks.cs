using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Lab7
{
    public static class TextFileTasks
    {
        private static Random _random = new Random();

        public static void FillTextFileSingleInt(string path, int count)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < count; i++)
                {
                    int number = _random.Next(-100, 101);
                    writer.WriteLine(number);
                }
            }
        }

        public static void FillTextFileMultipleInts(string path,
            int lines, int numbersPerLine)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < lines; i++)
                {
                    string line = "";
                    for (int j = 0; j < numbersPerLine; j++)
                    {
                        int number = _random.Next(-100, 101);
                        line = line + number;
                        if (j < numbersPerLine - 1)
                        {
                            line = line + " ";
                        }
                    }
                    writer.WriteLine(line);
                }
            }
        }

        public static void FillTextFileWithText(string path, string[] lines)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    writer.WriteLine(lines[i]);
                }
            }
        }

        public static void FillBinaryFileWithInts(string path, int count)
        {
            using (BinaryWriter writer = 
                new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    int number = _random.Next(-100, 101);
                    writer.Write(number);
                }
            }
        }

        public static void FillBinaryFileWithToys(string path, int count)
        {
            List<Toy> toys = new List<Toy>();
            string[] names = { "Кукла", "Машинка", "Мяч",
                "Конструктор", "Пазл", "Робот" };
            int[] agesMin = { 0, 1, 2, 3, 4, 5 };
            for (int i = 0; i < count; i++)
            {
                string name = names[_random.Next(names.Length)];
                int cost = _random.Next(100, 1001);
                int ageMin = agesMin[_random.Next(agesMin.Length)];
                int ageMax = ageMin + _random.Next(1, 6);
                Toy toy = new Toy(name, cost, ageMin, ageMax);
                toys.Add(toy);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, toys);
            }
        }

        public static int Task1_CountMaxOccurrences(string filePath)
        {
            int max = int.MinValue;
            int count = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    if (int.TryParse(line, out int number))
                    {
                        if (number > max)
                        {
                            max = number;
                            count = 1;
                        }
                        else if (number == max)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        public static int Task2_CountEvenNumbers(string filePath)
        {
            int evenCount = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    string[] parts = line.Split(' ');
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (int.TryParse(parts[i], out int number))
                        {
                            if (number % 2 == 0)
                            {
                                evenCount++;
                            }
                        }
                    }
                }
            }
            return evenCount;
        }

        public static void Task3_CopyLinesContaining(string sourcePath,
            string destPath, string substring)
        {
            using (StreamReader reader = new StreamReader(sourcePath))
            using (StreamWriter writer = new StreamWriter(destPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(substring))
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        public static int Task4_MaxMinDifference(string binaryFilePath)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            using (BinaryReader reader = 
                new BinaryReader(File.Open(binaryFilePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();
                    if (number < min)
                    {
                        min = number;
                    }
                    if (number > max)
                    {
                        max = number;
                    }
                }
            }
            return max - min;
        }

        public static void Task5_MostExpensiveToys(string binaryFilePath,
            int k, string outputPath)
        {
            List<Toy> toys = null;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream stream = 
                new FileStream(binaryFilePath, FileMode.Open))
            {
                toys = (List<Toy>)serializer.Deserialize(stream);
            }
            int maxCost = 0;
            for (int i = 0; i < toys.Count; i++)
            {
                if (toys[i].Cost > maxCost)
                {
                    maxCost = toys[i].Cost;
                }
            }
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                for (int i = 0; i < toys.Count; i++)
                {
                    if (maxCost - toys[i].Cost <= k)
                    {
                        writer.WriteLine(toys[i].Name);
                    }
                }
            }
        }
    }

    public struct Toy
    {
        private string _name;
        private int _cost;
        private int _ageMin;
        private int _ageMax;

        public Toy(string name, int cost, int ageMin, int ageMax)
        {
            _name = name;
            _cost = cost;
            _ageMin = ageMin;
            _ageMax = ageMax;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Cost
        {
            get { return _cost; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Цена не может" +
                        " быть отрицательной");
                }
                _cost = value;
            }
        }

        public int AgeMin
        {
            get { return _ageMin; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Минимальный возраст не " +
                        "может быть отрицательным");
                }
                _ageMin = value;
            }
        }

        public int AgeMax
        {
            get { return _ageMax; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Максимальный возраст не" +
                        " может быть отрицательным");
                }
                if (value < _ageMin)
                {
                    throw new ArgumentException("Максимальный возраст не" +
                        " может быть меньше минимального");
                }
                _ageMax = value;
            }
        }
    }
}