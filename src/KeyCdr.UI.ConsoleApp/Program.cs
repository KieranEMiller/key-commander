using System;
using System.Collections.Generic;
using KeyCdr.TextSamples;

namespace KeyCdr.UI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RunGameLoop();
        }

        private static void RunGameLoop()
        {
            string selection;
            while((selection = ShowMenuAndPromptForNextAction()).Equals("x", StringComparison.CurrentCultureIgnoreCase) == false)
            {
                string textToShow = MenuSelectionToTextSample(selection);
                Console.WriteLine("press enter when ready to begin.  good luck!");
                Console.ReadLine();
                Console.WriteLine(textToShow);

                MeasuredKeySequence analysis = new MeasuredKeySequence(textToShow);
                analysis.Start();

                string userInput = Console.ReadLine();

                var results = analysis.Stop(userInput);
                ShowResults(analysis, results);
            }
        }

        private static string MenuSelectionToTextSample(string selection)
        {
            ITextSampleGenerator generator = new KeyCdr.TextSamples.HardCodedTextGenerator();

            string textToShow = string.Empty;
            if (selection.Equals("1")) textToShow = generator.GetWord();
            else if (selection.Equals("2")) textToShow = generator.GetSentence();
            else if (selection.Equals("3")) textToShow = generator.GetParagraph();

            return textToShow;
        }

        private static string ShowMenuAndPromptForNextAction()
        {
            Console.WriteLine("Welcome to Key Commander");
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Practice Words");
            Console.WriteLine("2. Practice Sentences");
            Console.WriteLine("3. Practice Paragraphs");
            Console.WriteLine("X to quit");
            return Console.ReadLine();
        }

        private static void ShowResults(MeasuredKeySequence whatToMeasure, IList<KeyCdr.Analytics.IAnalytic> results)
        {
            Console.WriteLine();
            Console.WriteLine("your results:");
            Console.WriteLine("\tkey sequence");
            foreach (var result in results)
                Console.WriteLine("\t{0}", result.GetResultSummary());

            Console.WriteLine();
            Console.WriteLine("press enter when ready to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
