using System;
using System.Collections.Generic;
using System.IO;

namespace Lab7
{
    public static class CollectionTasks
    {
        public static void Task6_MoveFirstToEnd<T>(List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }
            T first = list[0];
            for (int i = 0; i < list.Count - 1; i++)
            {
                list[i] = list[i + 1];
            }
            list[list.Count - 1] = first;
        }

        public static void Task7_RemoveElementsWithSameNeighbors<T>
            (LinkedList<T> list)
        {
            if (list.Count < 2)
            {
                return;
            }
            LinkedListNode<T> current = list.First;
            LinkedListNode<T> firstNode = list.First;
            LinkedListNode<T> lastNode = list.Last;
            List<LinkedListNode<T>> toRemove = new List<LinkedListNode<T>>();
            while (current != null)
            {
                LinkedListNode<T> prev = current.Previous;
                LinkedListNode<T> next = current.Next;
                if (prev == null)
                {
                    prev = lastNode;
                }
                if (next == null)
                {
                    next = firstNode;
                }
                if (EqualityComparer<T>.
                    Default.Equals(prev.Value, next.Value))
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
            HashSet<string> allFactories = new HashSet<string>();
            for (int i = 0; i < factories.Length; i++)
            {
                allFactories.Add(factories[i]);
            }

            List<HashSet<string>> buyerSets = new List<HashSet<string>>();
            for (int i = 0; i < purchases.Length; i++)
            {
                HashSet<string> set = new HashSet<string>();
                for (int j = 0; j < purchases[i].Length; j++)
                {
                    set.Add(purchases[i][j]);
                }
                buyerSets.Add(set);
            }

            HashSet<string> intersection = new HashSet<string>(allFactories);
            for (int i = 0; i < buyerSets.Count; i++)
            {
                intersection.IntersectWith(buyerSets[i]);
            }
            allBoughtByEveryone = intersection;

            HashSet<string> union = new HashSet<string>();
            for (int i = 0; i < buyerSets.Count; i++)
            {
                union.UnionWith(buyerSets[i]);
            }

            boughtByNone = new HashSet<string>(allFactories);
            boughtByNone.ExceptWith(union);

            boughtBySome = new HashSet<string>(union);
            boughtBySome.ExceptWith(allBoughtByEveryone);
        }

        public static void Task9_PrintVoicelessConsonants(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден: " + filePath);
            }
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
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден: " + filePath);
            }
            Dictionary<int, (int minPrice, int count)> data =
                new Dictionary<int, (int, int)>();
            data[15] = (int.MaxValue, 0);
            data[20] = (int.MaxValue, 0);
            data[25] = (int.MaxValue, 0);

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
                    int currentMin = data[fat].minPrice;
                    int currentCount = data[fat].count;
                    if (price < currentMin)
                    {
                        data[fat] = (price, 1);
                    }
                    else if (price == currentMin)
                    {
                        data[fat] = (currentMin, currentCount + 1);
                    }
                }
            }
            count15 = (data[15].minPrice == int.MaxValue) ? 0 : data[15].count;
            count20 = (data[20].minPrice == int.MaxValue) ? 0 : data[20].count;
            count25 = (data[25].minPrice == int.MaxValue) ? 0 : data[25].count;
        }
    }
}