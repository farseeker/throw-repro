using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErrorBugRepro
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }
        async void Run()
        {
            int runs = 0;
            do
            {
                runs++;
                Console.WriteLine(runs);
                try
                {
                    Task myTask = throwException();
                    myTask.Wait();
                }
                catch (AggregateException ag)
                {
                    foreach (Exception ex in ag.InnerExceptions)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Thread.Sleep(1000);
            } while (true);
        }

        async Task throwException()
        {
            throw new NotImplementedException();
        }

    }
}
