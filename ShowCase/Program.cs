using System;
using System.Diagnostics;
using Spursa;

namespace ShowCase
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      Stopwatch stopWatch = new();
      stopWatch.Start();
      
      GeneratorConfig gc = SpursaGenUtilities.LoadFromYaml();

      stopWatch.Stop();
      // Get the elapsed time as a TimeSpan value.
      TimeSpan ts = stopWatch.Elapsed;

      // Format and display the TimeSpan value.
      string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
          ts.Hours, ts.Minutes, ts.Seconds,
          ts.Milliseconds / 10);

      foreach (Controller c in gc.Controllers)
      {
        Console.WriteLine("= " + c.Name + " - " + c.BasePath);
        foreach (Route r in c.Routes)
        {
          Console.WriteLine(" * " + r.Path);
          Console.WriteLine("     Get: " + r.Get?.Method);
          Console.WriteLine("     Put: " + r.Put?.Method);
          Console.WriteLine("    Post: " + r.Post?.Method);
          Console.WriteLine("  Delete: " + r.Delete?.Method);
        }
      }

      Console.WriteLine("RunTime " + elapsedTime);
    }
  }
}
