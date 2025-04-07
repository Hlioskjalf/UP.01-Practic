using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Zadanie8
{
    /// <summary>
    ///  Задается число N (нечетное). Рисуется квадрат NхN. Смоделировать процесс распространения инфекции, при условии, что исходной зараженной клеткой является центральная. 
    ///  В каждый интервал времени(задается пользователем) пораженная инфекцией клетка может с вероятностью 0,5 заразить любую из соседних(по горизонтали или вертикали) здоровых клеток
    ///  По прошествии шести единиц времени зараженная клетка становится невосприимчивой к инфекции
    ///  Возникший иммунитет действует в течение последующих четырех единиц времени, а затем клетка оказывается здоровой и может заразиться снова
    ///  В ходе моделирования описанного процесса выдавать, согласно указанного временного интервала, текущее состояние квадрата в каждом интервале времени отмечая различными цветами зараженные,
    ///  невосприимчивые к инфекции и здоровые клетки.
    ///  В ходе инфицирования, с указанной пользователем вероятностью, клетка, имеющая иммунитет, может опять заразиться, если рядом с ней будет зараженная клетка
    /// </summary>
    public partial class Zadanie8 : Form
    {
        private int _gridSize;
        private int _timeInterval;
        private double _reinfectionProbability;
        private CellState[,] _grid;
        private const int ImmuneDuration = 4;
        private const int InfectionDuration = 6;
        private const double InfectionProbability = 0.5;
        public Color HealthyColor = Color.White;
        public Color InfectedColor = Color.Red;
        public Color ImmuneColor = Color.Green;

        private SimulationFacade _simulationFacade;
        private GridRenderer _gridRenderer;

        public Zadanie8()
        {
            InitializeComponent();
            switchButton.Click += switchButton_Click;
            timer1.Tick += TimerTick;
            _simulationFacade = new SimulationFacade();
            _gridRenderer = new GridRenderer(gridPanel);
        }


        private void switchButton_Click(object sender, EventArgs e)
        {
            if (switchButton.Text == "Start")
            {
                try
                {
                    _gridSize = int.Parse(gridSizeTextBox.Text);
                    if (_gridSize % 2 == 0)
                    {
                        MessageBox.Show("The grid size must be odd!");
                        return;
                    }

                    _timeInterval = int.Parse(timeIntervalTextBox.Text);
                    _reinfectionProbability = double.Parse(reinfectionChanceTextBox.Text, CultureInfo.InvariantCulture);

                    _simulationFacade.InitializeGrid(_gridSize);
                    _grid = _simulationFacade.GetGrid();

                    StopInfection(false);

                    switchButton.BackColor = Color.Red;
                    switchButton.Text = "Stop";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Input error: {ex.Message}");
                }
            }
            else
            {
                StopInfection(true);

                switchButton.BackColor = Color.DarkGreen;
                switchButton.Text = "Start";
            }
        }


        private void TimerTick(object sender, EventArgs e)
        {
            _simulationFacade.SimulateInfection(_reinfectionProbability, InfectionDuration, ImmuneDuration, InfectionProbability);
            _grid = _simulationFacade.GetGrid();
            UpdateGridVisual();
        }

        private void UpdateGridVisual()
        {
            _gridRenderer.UpdateGridVisual(_grid, _gridSize, HealthyColor, InfectedColor, ImmuneColor);
        }

        private void TextBoxKeyPress_DigitsOnly(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TextBoxKeyPress_BinaryOnly(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '.')
                e.Handled = true;
        }

        private void gridSizeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxKeyPress_DigitsOnly(e);
        }

        private void timeIntervalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxKeyPress_DigitsOnly(e);
        }

        private void reinfectionProbabilityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxKeyPress_BinaryOnly(e);
        }

        private void StopInfection(bool val)
        {
            gridSizeTextBox.Enabled = val;
            timeIntervalTextBox.Enabled = val;
            reinfectionChanceTextBox.Enabled = val;
            switchButton.Enabled = val;
            switchButton.Enabled = !val;

            if (val)
                timer1.Stop();
            else
            {
                timer1.Interval = _timeInterval;
                timer1.Start();
            }
        }
    }

    public enum State
    {
        Healthy,
        Infected,
        Immune
    }
}