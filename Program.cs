using System;
using System.Collections.Generic;

namespace Lab7
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("=== Задания 1–5 (текстовые и бинарные файлы) ===\n");

            string intSinglePath = "ints_single.txt";
            TextFileTasks.FillTextFileSingleInt(intSinglePath, 20);
            int maxCount = TextFileTasks.Task1_CountMaxOccurrences(intSinglePath);
            Console.WriteLine("Задание 1. Количество вхождений максимального элемента: " + maxCount);

            string intMultiPath = "ints_multi.txt";
            TextFileTasks.FillTextFileMultipleInts(intMultiPath, 10, 5);
            int evenCount = TextFileTasks.Task2_CountEvenNumbers(intMultiPath);
            Console.WriteLine("Задание 2. Количество чётных элементов: " + evenCount);

            string textSourcePath = "text_source.txt";
            string textDestPath = "text_dest.txt";
            string[] lines = {
                "Сегодня старшеклассники выполняли ЕГЭ по информатике и ИКТ",
                "Эта строка не содержит нужную комбинацию",
                "Формат данных должен быть правильным",
                "Информатика и программирование"
            };
            TextFileTasks.FillTextFileWithText(textSourcePath, lines);
            TextFileTasks.Task3_CopyLinesContaining(textSourcePath, textDestPath, "форма");
            Console.WriteLine("Задание 3. Скопированы строки, содержащие 'форма' в файл " + textDestPath);

            string binaryIntsPath = "data.bin";
            TextFileTasks.FillBinaryFileWithInts(binaryIntsPath, 15);
            int diff = TextFileTasks.Task4_MaxMinDifference(binaryIntsPath);
            Console.WriteLine("\nСодержимое бинарного файла " + binaryIntsPath + ":");
            using (BinaryReader reader = new BinaryReader(File.Open(binaryIntsPath, FileMode.Open)))
            {
                int index = 1;
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int num = reader.ReadInt32();
                    Console.Write(num + " ");
                    if (index % 10 == 0) Console.WriteLine();
                    index++;
                }
                Console.WriteLine();
            }
            Console.WriteLine("Задание 4. Разность max и min: " + diff);

            string toysPath = "toys.xml";
            TextFileTasks.FillBinaryFileWithToys(toysPath, 10);
            string outputToysPath = "expensive_toys.txt";
            TextFileTasks.Task5_MostExpensiveToys(toysPath, 50, outputToysPath);
            Console.WriteLine("Задание 5. Названия самых дорогих игрушек (в пределах 50 руб.) записаны в " + outputToysPath);

            Console.WriteLine("\n=== Задания 6–10 (коллекции) ===\n");

            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            CollectionTasks.Task6_MoveFirstToEnd(list);
            Console.Write("Задание 6. Список после переноса первого в конец: ");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.WriteLine();

            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddLast(1);
            linkedList.AddLast(2);
            linkedList.AddLast(3);
            linkedList.AddLast(2);
            linkedList.AddLast(1);
            CollectionTasks.Task7_RemoveElementsWithSameNeighbors(linkedList);
            Console.Write("Задание 7. LinkedList после удаления: ");
            foreach (int val in linkedList)
            {
                Console.Write(val + " ");
            }
            Console.WriteLine();

            string[] factories = { "ФабрикаА", "ФабрикаБ", "ФабрикаВ", "ФабрикаД" };
            string[][] purchases = new string[][]
            {
                new string[] { "ФабрикаА", "ФабрикаБ" },
                new string[] { "ФабрикаА", "ФабрикаВ" },
                new string[] { "ФабрикаА", "ФабрикаБ", "ФабрикаВ" }
            };
            CollectionTasks.Task8_FurnitureAnalysis(factories, purchases,
                out HashSet<string> all,
                out HashSet<string> some,
                out HashSet<string> none);
            Console.Write("Задание 8. Куплены всеми: ");
            foreach (string f in all) Console.Write(f + " ");
            Console.Write("\nКуплены некоторыми: ");
            foreach (string f in some) Console.Write(f + " ");
            Console.Write("\nНе куплены никем: ");
            foreach (string f in none) Console.Write(f + " ");
            Console.WriteLine();

            string textForConsonants = "consonants.txt";
            string[] consonantLines = {
                "кот песок ток шум",
                "сок полк шарф",
                "кит песня чаша"
            };
            File.WriteAllLines(textForConsonants, consonantLines);
            Console.WriteLine("\nЗадание 9. Результат:");
            CollectionTasks.Task9_PrintVoicelessConsonants(textForConsonants);

            string creamDataPath = "cream.txt";
            string[] creamLines = {
                "5",
                "Перекресток Короленко 15 3200",
                "Ашан Ленина 15 3100",
                "Магнит Мира 20 3500",
                "Перекресток Садовая 20 3400",
                "Лента Невский 25 4000"
            };
            File.WriteAllLines(creamDataPath, creamLines);
            CollectionTasks.Task10_SourCreamAnalysis(creamDataPath, out int c15, out int c20, out int c25);
            Console.WriteLine($"\nЗадание 10. Количество магазинов с минимальной ценой: 15% - {c15}, 20% - {c20}, 25% - {c25}");

            Console.ReadKey();
        }
    }
}