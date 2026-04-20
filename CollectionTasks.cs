using System;
using System.Collections.Generic;
using System.IO;

namespace Lab7
{
    public static class CollectionTasks
    {
        public static void Task6_MoveFirstToEnd(List<int> list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }
            int first = list[0];
            for (int i = 0; i < list.Count - 1; i++)
            {
                list[i] = list[i + 1];
            }
            list[list.Count - 1] = first;
        }

        public static void Task7_RemoveElementsWithSameNeighbors
            (LinkedList<int> list)
        {
            if (list.Count < 2)
            {
                return;
            }
            LinkedListNode<int> current = list.First;
            LinkedListNode<int> firstNode = list.First;
            LinkedListNode<int> lastNode = list.Last;
            List<LinkedListNode<int>> toRemove 
                = new List<LinkedListNode<int>>();
            while (current != null)
            {
                LinkedListNode<int> prev = current.Previous;
                LinkedListNode<int> next = current.Next;
                if (prev == null)
                {
                    prev = lastNode;
                }
                if (next == null)
                {
                    next = firstNode;
                }
                if (prev.Value == next.Value)
                {
                    toRemove.Add(current);
                }
                current = current.Next;
            }
            for (int i = 0; i < toRemove.Count; i++)
            {
                list.Remove(toRemove[i]);
            }
        }

        public static void Task8_FurnitureAnalysis(
            string[] factories, string[][] purchases,
            out HashSet<string> allBoughtByEveryone,
            out HashSet<string> boughtBySome,
            out HashSet<string> boughtByNone)
        {
            allBoughtByEveryone = new HashSet<string>();
            boughtBySome = new HashSet<string>();
            boughtByNone = new HashSet<string>();
            Dictionary<string, HashSet<int>> boughtCount = 
                new Dictionary<string, HashSet<int>>();
            for (int i = 0; i < factories.Length; i++)
            {
                boughtCount[factories[i]] = new HashSet<int>();
            }
            for (int i = 0; i < purchases.Length; i++)
            {
                for (int j = 0; j < purchases[i].Length; j++)
                {
                    string factory = purchases[i][j];
                    if (boughtCount.ContainsKey(factory))
                    {
                        boughtCount[factory].Add(i);
                    }
                }
            }
            int totalBuyers = purchases.Length;
            foreach (string factory in factories)
            {
                int count = boughtCount[factory].Count;
                if (count == totalBuyers)
                {
                    allBoughtByEveryone.Add(factory);
                }
                else if (count > 0)
                {
                    boughtBySome.Add(factory);
                }
                else
                {
                    boughtByNone.Add(factory);
                }
            }
        }

        public static void Task9_PrintVoicelessConsonants(string filePath)
        {
            string text = File.ReadAllText(filePath);
            string[] words = text.Split(new char[] { ' ', '\n', '\r', '\t',
                '.', ',', '!', '?', ';', ':', '-' }, 
                StringSplitOptions.RemoveEmptyEntries);
            char[] voiceless = { 'к', 'п', 'с', 'т', 'ф', 'х', 'ц',
                'ч', 'ш', 'щ' };
            HashSet<char> result = new HashSet<char>();
            for (int i = 0; i < words.Length; i++)
            {
                if (i % 2 == 0)
                {
                    string word = words[i].ToLower();
                    for (int j = 0; j < voiceless.Length; j++)
                    {
                        if (word.IndexOf(voiceless[j]) >= 0)
                        {
                            result.Add(voiceless[j]);
                        }
                    }
                }
            }
            for (int i = 0; i < words.Length; i++)
            {
                if (i % 2 != 0)
                {
                    string word = words[i].ToLower();
                    for (int j = 0; j < voiceless.Length; j++)
                    {
                        if (word.IndexOf(voiceless[j]) >= 0)
                        {
                            result.Remove(voiceless[j]);
                        }
                    }
                }
            }
            List<char> sorted = new List<char>(result);
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                for (int j = i + 1; j < sorted.Count; j++)
                {
                    if (sorted[i] > sorted[j])
                    {
                        char temp = sorted[i];
                        sorted[i] = sorted[j];
                        sorted[j] = temp;
                    }
                }
            }
            Console.WriteLine("Глухие согласные в каждом нечётном и " +
                "ни в одном чётном:");
            for (int i = 0; i < sorted.Count; i++)
            {
                Console.Write(sorted[i] + " ");
            }
            Console.WriteLine();
        }

        public static void Task10_SourCreamAnalysis(string filePath, 
            out int count15, out int count20, out int count25)
        {
            count15 = 0;
            count20 = 0;
            count25 = 0;
            int minPrice15 = int.MaxValue;
            int minPrice20 = int.MaxValue;
            int minPrice25 = int.MaxValue;
            int shopsCount15 = 0;
            int shopsCount20 = 0;
            int shopsCount25 = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                int n = 0;
                if ((line = reader.ReadLine()) != null)
                {
                    if (!int.TryParse(line, out n))
                    {
                        n = 0;
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    string[] parts = line.Split(' ');
                    if (parts.Length != 4)
                    {
                        continue;
                    }
                    int fat;
                    if (!int.TryParse(parts[2], out fat))
                    {
                        continue;
                    }
                    if (fat != 15 && fat != 20 && fat != 25)
                    {
                        continue;
                    }
                    int price;
                    if (!int.TryParse(parts[3], out price))
                    {
                        continue;
                    }
                    if (price < 2000 || price > 5000)
                    {
                        continue;
                    }
                    if (fat == 15)
                    {
                        if (price < minPrice15)
                        {
                            minPrice15 = price;
                            shopsCount15 = 1;
                        }
                        else if (price == minPrice15)
                        {
                            shopsCount15++;
                        }
                    }
                    else if (fat == 20)
                    {
                        if (price < minPrice20)
                        {
                            minPrice20 = price;
                            shopsCount20 = 1;
                        }
                        else if (price == minPrice20)
                        {
                            shopsCount20++;
                        }
                    }
                    else if (fat == 25)
                    {
                        if (price < minPrice25)
                        {
                            minPrice25 = price;
                            shopsCount25 = 1;
                        }
                        else if (price == minPrice25)
                        {
                            shopsCount25++;
                        }
                    }
                }
            }
            count15 = (minPrice15 == int.MaxValue) ? 0 : shopsCount15;
            count20 = (minPrice20 == int.MaxValue) ? 0 : shopsCount20;
            count25 = (minPrice25 == int.MaxValue) ? 0 : shopsCount25;
        }
    }
}