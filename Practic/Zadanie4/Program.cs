﻿using System;

/** Задание 4.
     Сделать игровую карту(лабиринт) с помощью двумерного массива.Сделать функцию рисования карты.
     Помимо этого, дать пользователю возможность перемещаться по карте и взаимодействовать с элементами (например, пользователь не может пройти сквозь стену)
     Карта считывается из текстового файла.На игровом поле должны случайным образом появляться враги.
     Враги должны двигаться случайным образом. Исполнитель игрока управляется клавишами. 
     Также необходимо сделать некий бар, показывающий, например, остаток жизненных сил исполнителя (в процентах). 
     Все элементы являются обычными символами Если карта представляет собой лабиринт, то необходимо предусмотреть возможность,
     по нажатию определенной клавиши, показать правильный маршрут. Для данной задачи следует разработать функцию, 
     которая рисует некий бар(Healthbar, Manabar) в определённой позиции. Она также принимает некий закрашенный процент. **/

class Program
{
    private static char[,] _map;
    private static int _playerX;
    private static int _playerY;
    private static int _playerHealth = 100;
    private static List<(int X, int Y)> _enemies = new List<(int X, int Y)>();
    private static List<(int X, int Y)> _path = new List<(int X, int Y)>();
    private static int _mapWidth;
    private static int _mapHeight;
    private static Random _random = new Random();
    private static string _mapFilePath = "maze.txt";
    private static (int X, int Y) _endPoint;
    private static bool _gameOver = false;

    static void Main(string[] args)
    {
        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string solutionDirectory = Path.Combine(projectDirectory, "..", "..", "..");
        string fullPath = Path.Combine(solutionDirectory, _mapFilePath);


        LoadMap(fullPath);
        SpawnPlayer();
        SpawnEnemies(5);

        Thread enemyThread = new Thread(MoveEnemies);
        enemyThread.Start();

        while (!_gameOver)
        {
            Console.Clear();
            DrawMap();
            DrawHealthBar(1, _mapHeight + 1, _playerHealth);

            if (_playerHealth <= 0)
            {
                Console.SetCursorPosition(1, _mapHeight + 2);
                Console.WriteLine("You lost!");
                _gameOver = true;
                break;
            }

            if (_playerX == _endPoint.X && _playerY == _endPoint.Y)
            {
                Console.SetCursorPosition(1, _mapHeight + 2);
                Console.WriteLine("You've been through the maze!");
                _gameOver = true;
                break;
            }

            ProcessInput();
            Thread.Sleep(100);
        }
    }

    static void LoadMap(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            _mapHeight = lines.Length;
            _mapWidth = lines[0].Length;
            _map = new char[_mapHeight, _mapWidth];

            for (int i = 0; i < _mapHeight; i++)
            {
                if (lines[i].Length != _mapWidth)
                {
                    Console.WriteLine($"Error: String {i + 1} has an incorrect length (all strings should have the same length).");
                    Environment.Exit(1);
                }
                for (int j = 0; j < _mapWidth; j++)
                {
                    _map[i, j] = lines[i][j];
                    if (_map[i, j] == 'F')
                    {
                        _endPoint = (j, i);
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Maze file not found!");
            Console.WriteLine($"Check to see if the file {filePath} exists.");
            Environment.Exit(1);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error when loading the map: {e.Message}");
            Environment.Exit(1);
        }
    }

    static void SpawnPlayer()
    {
        for (int i = 0; i < _mapHeight; i++)
        {
            for (int j = 0; j < _mapWidth; j++)
            {
                if (_map[i, j] == 'S')
                {
                    _playerX = j;
                    _playerY = i;
                    _map[i, j] = ' ';
                    return;
                }
            }
        }

        Console.WriteLine("No (S) player's spawn point was found!");
        Environment.Exit(1);
    }

    static void SpawnEnemies(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int enemyX, enemyY;
            do
            {
                enemyX = _random.Next(0, _mapWidth);
                enemyY = _random.Next(0, _mapHeight);
            } while (_map[enemyY, enemyX] != ' ' || (enemyX == _playerX && enemyY == _playerY));

            _enemies.Add((enemyX, enemyY));
        }
    }

    static void MoveEnemies()
    {
        while (!_gameOver)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                (int enemyX, int enemyY) = _enemies[i];
                List<(int X, int Y)> possibleMoves = new List<(int X, int Y)>();

                if (CanMoveTo(enemyX + 1, enemyY)) possibleMoves.Add((enemyX + 1, enemyY));
                if (CanMoveTo(enemyX - 1, enemyY)) possibleMoves.Add((enemyX - 1, enemyY));
                if (CanMoveTo(enemyX, enemyY + 1)) possibleMoves.Add((enemyX, enemyY + 1));
                if (CanMoveTo(enemyX, enemyY - 1)) possibleMoves.Add((enemyX, enemyY - 1));

                if (possibleMoves.Count > 0)
                {
                    (int newEnemyX, int newEnemyY) = possibleMoves[_random.Next(possibleMoves.Count)];
                    _enemies[i] = (newEnemyX, newEnemyY);

                    if (newEnemyX == _playerX && newEnemyY == _playerY)
                    {
                        _playerHealth -= 10;
                    }
                }
            }
            Thread.Sleep(500);
        }
    }

    static void DrawMap()
    {
        for (int i = 0; i < _mapHeight; i++)
        {
            for (int j = 0; j < _mapWidth; j++)
            {
                if (i == _playerY && j == _playerX)
                    Console.Write("P"); //Player
                else if (_enemies.Any(e => e.X == j && e.Y == i))
                    Console.Write("E"); //Enemy
                else
                    Console.Write(_map[i, j]);
            }
            Console.WriteLine();
        }
    }

    static void DrawHealthBar(int x, int y, int percentage)
    {
        Console.SetCursorPosition(x, y);
        Console.Write("HP [");

        int filled = percentage / 10;
        for (int i = 0; i < filled; i++)
            Console.Write("#");

        for (int i = 0; i < 10 - filled; i++)
            Console.Write("_");

        Console.Write("]");
        Console.Write($" {percentage}%");
    }

    static void ProcessInput()
    {
        if (_gameOver) return;

        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            int newX = _playerX;
            int newY = _playerY;

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    newY--;
                    break;
                case ConsoleKey.DownArrow:
                    newY++;
                    break;
                case ConsoleKey.LeftArrow:
                    newX--;
                    break;
                case ConsoleKey.RightArrow:
                    newX++;
                    break;
            }

            if (CanMoveTo(newX, newY))
            {
                _playerX = newX;
                _playerY = newY;
            }
        }
    }

    static bool CanMoveTo(int x, int y)
    {
        return x >= 0 && x < _mapWidth && y >= 0 && y < _mapHeight && _map[y, x] != '#';
    }

    static bool FindPath(int x, int y, int targetX, int targetY, List<(int X, int Y)> currentPath)
    {
        if (!CanMoveTo(x, y) || currentPath.Contains((x, y)))
            return false;

        currentPath.Add((x, y));

        if (x == targetX && y == targetY)
        {
            _path = currentPath;
            return true;
        }

        //Pathfinding
        if (FindPath(x + 1, y, targetX, targetY, currentPath.ToList()) ||
            FindPath(x - 1, y, targetX, targetY, currentPath.ToList()) ||
            FindPath(x, y + 1, targetX, targetY, currentPath.ToList()) ||
            FindPath(x, y - 1, targetX, targetY, currentPath.ToList()))
        {
            _path = currentPath;
            return true;
        }

        return false;
    }
}