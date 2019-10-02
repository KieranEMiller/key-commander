using System;
using System.Collections.Generic;
using System.Text;
using KeyCdr.Analytics;
using KeyCdr.TextSamples;

namespace KeyCdr.UI.ConsoleApp
{
    public class KeyCdrApp
    {
        private const string EXIT_KEY = "x";

        public KeyCdrApp()
        {
            _textSampleFetcher = new TextPrefetcher();
        }

        private TextPrefetcher _textSampleFetcher;
        private KeyCdr.Data.KCUser _user;

        public void Run()
        {
            _user = DoLogin();
            RunGameLoop();
        }

        public void RunGameLoop()
        {
            string selection;
            while ((selection = ShowMenuAndPromptForNextAction()).Equals(EXIT_KEY, StringComparison.CurrentCultureIgnoreCase) == false)
            {
                string textToShow = MenuSelectionToTextSample(selection);
                ShowWaitMsgAndPauseForInput();
                Console.WriteLine(textToShow);

                MeasuredKeySequence analysis = new MeasuredKeySequence(
                    textToShow, new List<AnalyticType> { AnalyticType.Speed, AnalyticType.Accuracy }
                );
                analysis.Start(_user);

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

        private KeyCdr.Data.KCUser DoLogin()
        {
            var userMgr = new KeyCdr.Users.UserManager();

            bool keepTrying = true;
            while(keepTrying)
            {
                Console.Clear();
                string loginName = PromptForLogin();
                if (string.IsNullOrWhiteSpace(loginName))
                {
                    _user = userMgr.CreateGuest();
                    Console.WriteLine("using a guest account ({0})", _user.LoginName);
                    return _user;
                }

                _user = userMgr.GetByLoginName(loginName);
                if(_user != null)
                {
                    Console.WriteLine("welcome back {0}", _user.LoginName);
                    return _user;
                }
            }

            return null;
        }

        private string PromptForLogin()
        {
            Console.WriteLine("Welcome to Key Commander");
            Console.WriteLine();
            Console.WriteLine("Enter your login name [blank for guest]: ");
            string loginName = Console.ReadLine();
            return loginName;
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

            ShowWaitMsgAndPauseForInput();
            Console.Clear();
        }

        private void ShowWaitMsgAndPauseForInput()
        {
            Console.WriteLine();
            Console.WriteLine("press enter when ready to continue");
            Console.ReadLine();
        }
    }
}
