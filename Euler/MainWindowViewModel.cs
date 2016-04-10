using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Euler.MVVM;
using System.Collections.ObjectModel;
using Euler.Problems;
using System.Reflection;
using System.Diagnostics;

namespace Euler
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            IEnumerable<Type> problems = Assembly.GetExecutingAssembly().GetTypes()
                                                 .Where(t => t.IsClass &&
                                                             t.Namespace == "Euler.Problems" &&
                                                             t.IsAbstract == false &&
                                                             t.Name.Length > 7 &&
                                                             t.Name.Substring(0, 7) == "Problem");



            IList<IProblem> problemList = new List<IProblem>();

            foreach (Type type in problems)
            {
                problemList.Add(Activator.CreateInstance(type) as IProblem);
            }

            AvailableProblems = new ObservableCollection<IProblem>
                (problemList.OrderBy(o => o.ProblemNumber));
            
        }

        private ObservableCollection<IProblem> _AvailableProblems;

        public ObservableCollection<IProblem> AvailableProblems
        {
            get { return _AvailableProblems; }
            set
            {
                _AvailableProblems = value;
                RaisePropertyChanged("AvailableProblems");
            }
        }

        private IProblem _SelectedProblem;

        public IProblem SelectedProblem
        {
            get { return _SelectedProblem; }
            set
            {
                _SelectedProblem = value;
                RaisePropertyChanged("SelectedProblem");
                Answer = null;
                RunTimeText = null;
            }
        }

        private string _Answer;

        public string Answer
        {
            get { return _Answer; }
            set
            {
                _Answer = value;
                RaisePropertyChanged("Answer");
            }
        }

        private string _RunTimeText;

        public string RunTimeText
        {
            get { return _RunTimeText; }
            set
            {
                _RunTimeText = value;
                RaisePropertyChanged("RunTimeText");
            }
        }



        public RelayCommand CalculateCommand { get { return new RelayCommand(Calculate, CanCalculate); } }

        private bool CanCalculate(object obj)
        {
            return SelectedProblem != null;
        }

        private async void Calculate(object obj)
        {
            MarkBusy();

            Stopwatch stopwatch = Stopwatch.StartNew();

            Answer = await SelectedProblem.SolveAsync();

            stopwatch.Stop();

            RunTimeText = "Answer found in " + stopwatch.ElapsedMilliseconds.ToString("#,##0") + "ms";

            MarkFree();
        }
        
    }
}
