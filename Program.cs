using System;
using System.IO;
namespace bruh
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");

            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // Create file
                Console.WriteLine("How many weeks of data is needed?");
                int weeks = int.Parse(Console.ReadLine());

                DateTime today = DateTime.Now;
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                DateTime dataStartDate = dataEndDate.AddDays(-(weeks * 7));
                Console.WriteLine(dataStartDate);

                Random rnd = new Random();
                StreamWriter sw = new StreamWriter("data.txt");

                while (dataStartDate < dataEndDate)
                {
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    sw.WriteLine($"{dataStartDate:M/d/yyyy},{string.Join("|", hours)}");
                    dataStartDate = dataStartDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                // TODO: parse data file
                StreamReader sr = new StreamReader("data.txt");
                while (!sr.EndOfStream){
                    string line = sr.ReadLine();
                    string date = line.Split(',')[0];
                    int month = int.Parse(date.Split('/')[0]);
                    int day = int.Parse(date.Split('/')[1]);
                    int year = int.Parse(date.Split('/')[2]);
                    string nights = line.Split(',')[1];
                    String[] singleNights = nights.Split('|');
                    
                    DateTime currentDay = new DateTime(year, month, day);

                    int total = 0;
                    for(int i = 0; i < 7; i++){
                        total += int.Parse(nights.Split('|')[i]);
                    }
                    double average = total / 7;
                    Console.WriteLine($"Week of {currentDay:MMM, dd, yyyy}");
                    Console.WriteLine("Mo Tu We Th Fr Sa Su Tot Avg");
                    Console.WriteLine("-- -- -- -- -- -- -- --- ---");
                    Console.WriteLine($"{singleNights[0],2}{singleNights[1],3}{singleNights[2],3}{singleNights[3],3}{singleNights[4],3}{singleNights[5],3}{singleNights[6],3}{total,4}{average,4}");
                }
            }
        }
    }
}