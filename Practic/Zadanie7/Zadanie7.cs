using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Zadanie7
{
    public partial class Zadanie7 : Form
    {
        private const int BoardSize = 7;

        private GameBoardFacade _gameBoardFacade;
        private Point _selectedPiece;
        private PictureBox _selectedPictureBox;

        public Zadanie7()
        {
            InitializeComponent();
            CreateBoard();
            InitializeGame();

            foreach (Control control in tableLayoutPanel1.Controls)
                if (control is PictureBox cellPictureBox) 
                    cellPictureBox.Click += Cell_Click;

            HelpButtonClicked += ShapedForm1_HelpButtonClicked;

            KeyPreview = true;
            KeyDown += Task7_KeyDown;
        }

        private void Task7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && _selectedPiece != Point.Empty)
            {
                ClearSelection();
                UpdateUi();
            }
        }

        private void ClearSelection()
        {
            ClearHighlighting();
            _selectedPiece = Point.Empty;
            _selectedPictureBox = null; 
        }

        private void CreateBoard()
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            tableLayoutPanel1.ColumnCount = BoardSize;
            tableLayoutPanel1.RowCount = BoardSize;

            for (int i = 0; i < BoardSize; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / BoardSize));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / BoardSize));
            }

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    PictureBox cellPictureBox = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(0),
                        Padding = new Padding(0),
                        BackColor = Color.White
                    };

                    // Making corner gray cells inaccessible
                    if ((row < 2 && (col < 2 || col > 4)) || (row > 4 && (col < 2 || col > 4)))
                    {
                        cellPictureBox.BackColor = Color.Gray;
                        cellPictureBox.Enabled = false;
                    }

                    cellPictureBox.Click += Cell_Click;
                    tableLayoutPanel1.Controls.Add(cellPictureBox, col, row);
                }
            }
        }

        private void InitializeGame()
        {
            _gameBoardFacade = new GameBoardFacade(BoardSize);
            _selectedPiece = Point.Empty;
            _selectedPictureBox = null;
            UpdateUi();
        }

        private void ShapedForm1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Goal: chickens must occupy the top 3x3 square, and the foxes must eat as many chickens as the chickens need to win." +
                "\nChickens moves first. Foxes move after chickens and must eat them if there'r possibility. " +
                "\n\nDeselect - spacebar", "Foxes and chickens", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                e.Cancel = true;
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            if (!(sender is PictureBox clickedCell)) return;

            Point cellLocation = GetCellLocation(clickedCell);

            if (_gameBoardFacade.CurrentPlayer == PlayerType.Chicken)
                HandleChickenMove(cellLocation, clickedCell);
            else
                HandleFoxMove(cellLocation, clickedCell);
        }

        private Point GetCellLocation(PictureBox cell)
        {
            TableLayoutPanelCellPosition position = tableLayoutPanel1.GetPositionFromControl(cell);
            return new Point(position.Column, position.Row);
        }

        private void HandleChickenMove(Point cellLocation, PictureBox clickedCell)
        {
            if (_selectedPiece == Point.Empty)
            {
                if (_gameBoardFacade.GetCellType(cellLocation) == CellType.Chicken)
                {
                    if (_gameBoardFacade.CanChickenMove(cellLocation))
                    {
                        _selectedPiece = cellLocation;
                        _selectedPictureBox = clickedCell;
                        HighlightSelectedPiece();
                    }
                    else
                    {
                        MessageBox.Show("No moves available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _selectedPiece = Point.Empty;
                        _selectedPictureBox = null; 
                    }
                }
                else
                {
                    MessageBox.Show("Choose a chicken.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (_gameBoardFacade.IsValidChickenMove(_selectedPiece, cellLocation))
                {
                    _gameBoardFacade.MoveChicken(_selectedPiece, cellLocation);
                    ClearHighlighting();
                    _selectedPiece = Point.Empty;
                    _selectedPictureBox = null; 
                    MakeFoxMove();
                }
                else
                {
                    MessageBox.Show("Inadmissible movement.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdateUi();
            CheckForGameOver();
        }

        private void HandleFoxMove(Point cellLocation, PictureBox clickedCell) 
        {
            if (_selectedPiece == Point.Empty)
            {
                if (_gameBoardFacade.GetCellType(cellLocation) == CellType.Fox)
                {
                    _selectedPiece = cellLocation;
                    _selectedPictureBox = clickedCell; 
                    HighlightSelectedPiece();
                }
                else
                    MessageBox.Show("Select a fox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (_gameBoardFacade.IsValidFoxMove(_selectedPiece, cellLocation))
                {
                    _gameBoardFacade.MoveFox(_selectedPiece, cellLocation);
                    ClearHighlighting();
                    _selectedPiece = Point.Empty;
                    _selectedPictureBox = null;

                    _gameBoardFacade.SwitchPlayer();
                }
                else
                {
                    MessageBox.Show("Inadmissible movement.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdateUi();
            CheckForGameOver();
        }

        private void MakeFoxMove()
        {
            bool foxMoved = true;
            while (foxMoved && _gameBoardFacade.CurrentPlayer == PlayerType.Fox)
            {
                foxMoved = _gameBoardFacade.MakeFoxMove();
                UpdateUi();
                CheckForGameOver();

                if (!foxMoved && _gameBoardFacade.CurrentPlayer == PlayerType.Fox)
                {
                    foxMoved = _gameBoardFacade.MakeRandomFoxMove();
                }

                if (!foxMoved && _gameBoardFacade.CurrentPlayer == PlayerType.Fox)
                {
                    MessageBox.Show("Winner winner chicken dinner!.", "Game over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetGame();
                    return;
                }

            }
        }

        private void UpdateUi()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (!(tableLayoutPanel1.GetControlFromPosition(col, row) is PictureBox cell)) continue;

                    CellType cellType = _gameBoardFacade.GetCellType(new Point(col, row));

                    if (cellType == CellType.Chicken)
                        cell.Image = Image.FromFile("chicken.png");
                    else if (cellType == CellType.Fox)
                        cell.Image = Image.FromFile("fox.png");
                    else
                        cell.Image = null;
                }
            }

            eatenChickenLabel.Text = "Eaten chickens: " + _gameBoardFacade.EatenChickensCount;
            chickensLeftLabel.Text = "Chickens: " + _gameBoardFacade.ChickenCount;
        }

        private void CheckForGameOver()
        {
            if (_gameBoardFacade.ChickenCount <= 8)
            {
                MessageBox.Show("Foxes won! Chickens don't have space.", "Game over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
                return;
            }
            if (_gameBoardFacade.CheckChickenWin())
            {
                MessageBox.Show("Chickens won! They filled top square.", "Game over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
                return;
            }
            if (_gameBoardFacade.CurrentPlayer == PlayerType.Chicken && !_gameBoardFacade.CanChickensMove())
            {
                MessageBox.Show("Foxes won! Chickens can't move it move it.", "Game over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
                return;
            }
            if (_gameBoardFacade.CurrentPlayer == PlayerType.Fox && !_gameBoardFacade.CanFoxesMove())
            {
                MessageBox.Show("Chickens won! Foxes can't move it move it.", "Game over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
                return;
            }
        }

        private void ResetGame()
        {
            InitializeGame();
            UpdateUi();
        }

        private void HighlightSelectedPiece()
        {
            if (_selectedPictureBox != null) 
                _selectedPictureBox.BorderStyle = BorderStyle.Fixed3D;
        }

        private void ClearHighlighting()
        {
            if (_selectedPictureBox != null)
                _selectedPictureBox.BorderStyle = BorderStyle.FixedSingle;
        }
    }

    public enum CellType
    {
        Empty,
        Chicken,
        Fox
    }

    public enum PlayerType
    {
        Chicken,
        Fox
    }
}
