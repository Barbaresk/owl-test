using GameLogic;

namespace TreasureApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random(); //не будет менять зерно
            var sps = new StandartProbabilityService(rand);
            while (true)
            {
                try
                {
                    Console.Write("Enter file path: ");
                    string? file = Console.ReadLine();
                    if (file != null)
                    {
                        IEnumerable<BoxInfo> data;
                        using (StreamReader sr = new StreamReader(file))
                        {
                            data = ReadDataFromFile(sr).ToList();
                        }

                        Run(sps, data);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        //методы чтения файла
        static IEnumerable<BoxInfo> ReadDataFromFile(StreamReader sr)
        {
            int i = 1; //счетик строк
            sr.ReadLine(); //пропуск заголовка
            var str = sr.ReadLine();
            while (str != null)
            {
                i++;
                var vals = str.Trim().Split("\t");
                if (vals.Length < 3 || vals.Length % 2 == 0) //если нет элементов, или количество параметров не равно (1 + 2*N) 
                {
                    throw new Exception($"incorrect params count in line {i}");
                }
                yield return new BoxInfo { 
                    Name = vals[0], 
                    Probabilities = GetProbabilitiesFromArray(vals.TakeLast(vals.Length - 1)).ToList() };
                str = sr.ReadLine();
            }
        }

        static IEnumerable<ProbabilityInfo> GetProbabilitiesFromArray(IEnumerable<string> vals)
        {
            for (int i = 0; i < vals.Count(); i += 2)
                yield return new ProbabilityInfo { Name = vals.ElementAt(i), Weight = Convert.ToUInt32(vals.ElementAt(i + 1)) };
        }

        static void Run(IProbabilityService ps, IEnumerable<BoxInfo> data)
        {
            var treasure = new Treasure(ps, data);
            while (true)
            {
                Console.Write("Existed boxes: ");
                foreach(var b in treasure.BoxNames)
                    Console.Write($"{b} ");
                Console.Write("\nEnter name of box: ");
                string? boxName = Console.ReadLine();
                if (boxName == null) continue;
                if (treasure.HasBox(boxName))
                    Console.WriteLine($"Opened item is {treasure.GetItemFromBox(boxName).Name}");
                else
                    Console.WriteLine($"Box {boxName} is not existed");
            }
        }
    }
}