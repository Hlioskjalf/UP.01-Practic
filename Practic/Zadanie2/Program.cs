using System;

/** Задание 2.
     Поле шахматной доски определяется парой символов состоящей из буквы от a до h, и цифры от 1 до 8. 
     На поле x1y1 расположена белая фигура, на поле x2y2 - черная. Определить, может ли белая фигура дойти до поля x3y3, 
     не попав при этом под удар черной фигуры (черная фигура остается неподвижной). 
     В качестве пар следует использовать различные сочетания из следующих фигур: ладья, конь, слон, ферзь, король
     В качестве входных данных используется строка состоящая из: название белой фигуры, пробел, координаты белой фигуры, 
     пробел, название черной фигуры, пробел, координаты черной фигуры, пробел, координаты конечной точки. **/

class Program
{
    static void Main()
    {
        Console.Write("\n                  <------------- CHESTS ------------->     \n");
        Console.Write("\nEnter the raw data: ");
        string input = Console.ReadLine();

        Console.Write("\n<---------------------------------------------------------------------->\n");

        string[] parts = input.Split(' ');
        if (parts.Length != 5)
        {
            Console.WriteLine("\nData Entry Error.");
            Console.Write("\n<---------------------------------------------------------------------->\n");
            return;
        }

        string whitePiece = parts[0];
        string whitePos = parts[1];
        string blackPiece = parts[2];
        string blackPos = parts[3];
        string targetPos = parts[4];

        (int x, int y) whiteCoord = ParsePosition(whitePos);
        (int x, int y) blackCoord = ParsePosition(blackPos);
        (int x, int y) targetCoord = ParsePosition(targetPos);

        bool canReach = CanReachTarget(whitePiece, whiteCoord, targetCoord, blackPiece, blackCoord);

        if (canReach)
        {
            Console.WriteLine($"\n{whitePiece} will come to {targetPos}");
            Console.Write("\n<---------------------------------------------------------------------->\n");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine($"\n{whitePiece} can't get to {targetPos}");
            Console.Write("\n<---------------------------------------------------------------------->\n");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    static (int x, int y) ParsePosition(string pos)
    {
        int x = pos[0] - 'a' + 1;
        int y = pos[1] - '0';
        return (x, y);
    }

    static bool CanReachTarget(string whitePiece, (int x, int y) whiteCoord, (int x, int y) targetCoord,
                               string blackPiece, (int x, int y) blackCoord)
    {
        if (!IsValidMove(whitePiece, whiteCoord, targetCoord))
        {
            return false;
        }

        if (IsUnderAttack(blackPiece, blackCoord, targetCoord))
        {
            return false;
        }

        if (IsPathUnderAttack(whitePiece, whiteCoord, targetCoord, blackPiece, blackCoord))
        {
            return false;
        }

        return true;
    }

    static bool IsValidMove(string piece, (int x, int y) start, (int x, int y) end)
    {
        switch (piece.ToLower())
        {
            case "Rook":
                return start.x == end.x || start.y == end.y;
            case "Elephant":
                return Math.Abs(start.x - end.x) == Math.Abs(start.y - end.y);
            case "Queen":
                return start.x == end.x || start.y == end.y || Math.Abs(start.x - end.x) == Math.Abs(start.y - end.y);
            case "Horse":
                return (Math.Abs(start.x - end.x) == 2 && Math.Abs(start.y - end.y) == 1) ||
                       (Math.Abs(start.x - end.x) == 1 && Math.Abs(start.y - end.y) == 2);
            case "King":
                return Math.Abs(start.x - end.x) <= 1 && Math.Abs(start.y - end.y) <= 1;
            default:
                return false;
        }
    }

    static bool IsUnderAttack(string piece, (int x, int y) attacker, (int x, int y) target)
    {
        return IsValidMove(piece, attacker, target);
    }

    static bool IsPathUnderAttack(string whitePiece, (int x, int y) start, (int x, int y) end,
                                  string blackPiece, (int x, int y) blackCoord)
    {
        int dx = Math.Sign(end.x - start.x);
        int dy = Math.Sign(end.y - start.y);

        int x = start.x + dx;
        int y = start.y + dy;

        while (x != end.x || y != end.y)
        {
            if (IsUnderAttack(blackPiece, blackCoord, (x, y)))
            {
                return true;
            }
            x += dx;
            y += dy;
        }

        return false;
    }
}