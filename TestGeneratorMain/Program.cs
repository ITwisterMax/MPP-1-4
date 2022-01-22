using System;
using TestGeneratorMain.Block;

namespace TestGeneratorMain
{
    /// <summary>
    ///     Entry point
    /// </summary>
    class Program
    {
        /// <summary>
        ///     Main method
        /// </summary>
        /// 
        /// <param name="args">Arguments</param>
        static void Main(string[] args)
        {
            // Get files and src / dest path
            var src = @"..\..\Tests\Source";
            var files = new string[] { "T1.cs", "T3.cs"};
            var dest = @"..\..\Tests\Result"; 
            
            // Generate tests
            new Pipeline().Generate(src, files, dest, 2);
            Console.ReadLine();
        }
    }
}
