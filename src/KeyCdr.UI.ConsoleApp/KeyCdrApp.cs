using System;
using System.Collections.Generic;
using System.Text;
using KeyCdr.Analytics;
using KeyCdr.TextSamples;

namespace KeyCdr.UI.ConsoleApp
{
    public class KeyCdrApp
    {
        public KeyCdrApp()
        {
            _textSampleFetcher = new TextPrefetcher();
        }

        private TextPrefetcher _textSampleFetcher;

        public void RunGameLoop()
        {
            string selection;
            while ((selection = ShowMenuAndPromptForNextAction()).Equals("x", StringComparison.CurrentCultureIgnoreCase) == false)
            {
                string textToShow = MenuSelectionToTextSample(selection);
                Console.WriteLine("press enter when ready to begin.  good luck!");
                Console.ReadLine();
                Console.WriteLine(textToShow);

                MeasuredKeySequence analysis = new MeasuredKeySequence(
                    textToShow, new List<AnalyticType> { AnalyticType.Speed, AnalyticType.Accuracy }
                );
                analysis.Start();

                string userInput = Console.ReadLine();

                var results = analysis.Stop(userInput);
                ShowResults(analysis, results);
            }
        }

        private string MenuSelectionToTextSample(string selection)
        {
            string textToShow = string.Empty;
            if (selection.Equals("1")) textToShow = _textSampleFetcher.GetWord();
            else if (selection.Equals("2")) textToShow = _textSampleFetcher.GetSentence();
            else if (selection.Equals("3")) textToShow = _textSampleFetcher.GetParagraph();

            return textToShow;
        }

        private string ShowMenuAndPromptForNextAction()
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

        private void ShowResults(MeasuredKeySequence whatToMeasure, IList<KeyCdr.Analytics.IAnalytic> results)
        {
            Console.WriteLine();
            Console.WriteLine("your results:");
            foreach (var result in results)
                Console.WriteLine("\t{0}", result.GetResultSummary());

            Console.WriteLine();
            Console.WriteLine("press enter when ready to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
