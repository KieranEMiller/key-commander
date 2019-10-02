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
        { }

        private KeyCdr.Data.KCUser _user;
        private KeyCdr.UserSession _session;

        public void Run()
        {
            _user = DoLogin();
            _session = new UserSession(_user, new WikipediaTextGeneratorWithLocalCache());

            RunGameLoop();
        }

        public void RunGameLoop()
        {
            string selection;
            while ((selection = ShowMenuAndPromptForNextAction()).Equals(EXIT_KEY, StringComparison.CurrentCultureIgnoreCase) == false)
            {
                ITextSample sample = MenuSelectionToTextSample(selection);
                ShowWaitMsgAndPauseForInput();
                Console.WriteLine(sample.GetText());

                _session.StartNewSequence(sample);
                string userInput = Console.ReadLine();
                var results = _session.Stop(userInput);

                ShowResults(results);
            }
        }

        private ITextSample MenuSelectionToTextSample(string selection)
        {
            ITextSample textToShow = null;
            if (selection.Equals("1")) textToShow = _session.GetWord();
            else if (selection.Equals("2")) textToShow = _session.GetSentence();
            else if (selection.Equals("3")) textToShow = _session.GetParagraph();

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

        private void ShowResults(IList<KeyCdr.Analytics.IAnalytic> results)
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
