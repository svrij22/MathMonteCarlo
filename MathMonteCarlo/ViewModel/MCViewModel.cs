using MathMonteCarlo.MonteCarlo;
using MathMonteCarlo.MonteCarlo.Model;
using MathMonteCarlo.Numbers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMonteCarlo.ViewModel
{
    public class MCViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Observable collection
        /// </summary>
        public ObservableCollection<string> Titles { get; set; } = new();
        public Dictionary<string, string> Outputs { get; set; } = new();
        public string SelectedOutput { get; set; } = String.Empty;

        public int _SelectedIndex = 0;
        public int SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                if (value >= 0)
                    SelectedOutput = Outputs[Titles[value]];
                OnPropertyChanged(nameof(SelectedOutput));
                _SelectedIndex = value;
            }
        }

        /// <summary>
        /// Singleton pattern
        /// </summary>
        public static MCViewModel Instance { get; set; }
        public MCViewModel()
        {
            Instance = this;

            Log("Log test 1", "log 123");
            Log("Log test 1", "log 123");
            Log("Log test 1", "log 123");

            SelectedOutput = "Hoi. Selecteer beneden in het lijstje het resultaat dat je wilt bekijken.";
        }

        public void Run()
        {
            //Test number generator
            GeneratorTester.TestCongruentual();
            GeneratorTester.TestMidSquare();
            GeneratorTester.TestBuiltIn();
            GeneratorTester.TestLagFib();

            //Test weights
            WeighedRandom.Test();


            //Run opdracht 1
            for (int i = 1; i <= 3; i++)
            {
                Simulator sim = new Simulator(i.ToString() + " - Opdr.1");
                sim.Run<PouleWithOdds>();
                sim.CalculateStatistics();
                sim.PrintStatistics();
            }
            for (int i = 1; i <= 3; i++)
            {
                Simulator sim = new Simulator(i.ToString() + " - Opdr.2");
                sim.Run<PouleWithPoisson>();
                sim.CalculateStatistics();
                sim.PrintStatistics();
            }

            //Poisson test
            PoissonMath.Test();
        }

        public static void Log(string title, string content)
        {

            //Add to list
            if (!Instance.Titles.Contains(title))
                Instance.Titles.Add(title);

            //Add to dict
            if (!Instance.Outputs.ContainsKey(title))
                Instance.Outputs.Add(title, "");

            Instance.OnPropertyChanged(nameof(SelectedOutput));
            Instance.Outputs[title] += content + "\n";
        }

        /// <summary>
        /// property changed
        /// </summary>

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
