using System;
using System.IO;

/** Задание 3.
    Перед вами босс, у которого есть определенное количество жизней и определенный ответный урон. 
    У вас есть не менее 5-и заклинаний для нанесения урона боссу. 
    Программа завершается только после смерти босса или смерти пользователя. **/

class Program
{
    static void Main()
    {
        int playerHP = 500;
        int bossHP = 1000;
        bool shieldUsed = false;
        bool shadowSpiritSummoned = false;

        Random random = new Random();

        while (playerHP > 0 && bossHP > 0)
        {
            Console.Write("\n                  <------------- GAME ------------->     \n");

            Console.WriteLine($"\nPlayer HP: {playerHP}; \nBoss HP: {bossHP}");

            Console.Write("\n<---------------------------------------------------------------------->\n");

            Console.WriteLine("\nYour Turn. Choose a spell:\n");
            Console.WriteLine(" 1. Fireball");
            Console.WriteLine(" 2. Ice strike");
            Console.WriteLine(" 3. Heal");
            Console.WriteLine(" 4. Lightning");
            Console.WriteLine(" 5. Shield");

            Console.Write("\n<---------------------------------------------------------------------->\n");

            int playerChoice = int.Parse(Console.ReadLine());

            switch (playerChoice)
            {
                case 1:
                    bossHP -= 50;
                    Console.Write("\n<---------------------------------------------------------------------->\n");
                    Console.WriteLine("You have dealt 50 damage to the boss.");
                    break;
                case 2:
                    if (playerChoice == 1)
                    {
                        bossHP -= 75;
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("You have dealt 75 damage to the boss.");
                    }
                    else
                    {
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("Ice Strike can only be used after Fireball.");
                    }
                    break;
                case 3:
                    if (playerHP < 250)
                    {
                        playerHP += 100;
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("You have recovered 100 xp.");
                    }
                    else
                    {
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("Healing can only be used if you have less than 50% xp.");
                    }
                    break;
                case 4:
                    if (bossHP > 500)
                    {
                        bossHP -= 150;
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("You have dealt 150 damage to the boss.");
                    }
                    else
                    {
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("Lightning can only be used if the boss has more than 50% xp.");
                    }
                    break;
                case 5:
                    if (!shieldUsed)
                    {
                        shieldUsed = true;
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("You used a shield. The next attack of the boss will be reduced by 50%.");
                    }
                    else
                    {
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("The shield may only be used once per game.");
                    }
                    break;
                default:
                    Console.Write("\n<---------------------------------------------------------------------->\n");
                    Console.WriteLine("Wrong choice.");
                    break;
            }

            if (bossHP <= 0) break;


            //boss logick 
            int bossChoice = random.Next(1, 6);
            Console.Write("\n<---------------------------------------------------------------------->\n");
            Console.WriteLine($"\nThe boss uses a spell {bossChoice}:");

            switch (bossChoice)
            {
                case 1:
                    playerHP -= 100;
                    shadowSpiritSummoned = true;
                    Console.Write("\n<---------------------------------------------------------------------->\n");
                    Console.WriteLine("The boss summoned a shadow spirit and dealt 100 damage to you.");
                    break;
                case 2:
                    if (shadowSpiritSummoned)
                    {
                        playerHP -= 100;
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("The boss used Houganzakura and dealt 100 damage to you.");
                    }
                    else
                    {
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("Houganzakura can only be used after summoning a shadow spirit.");
                    }
                    break;
                case 3:
                    if (bossHP < 500)
                    {
                        bossHP += 250;
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("The boss has regained 250 xp.");
                    }
                    else
                    {
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("Interdimensional Rift can only be used if the boss has less than 50% xp.");
                    }
                    break;
                case 4:
                    if (playerHP > 250)
                    {
                        playerHP -= 120;
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("The boss used Dark magic and dealt 120 damage to you.");
                    }
                    else
                    {
                        Console.Write("\n<---------------------------------------------------------------------->\n");
                        Console.WriteLine("Dark magic can only be used if you have more than 50% xp.");
                    }
                    break;
                case 5:
                    Console.Write("\n<---------------------------------------------------------------------->\n");
                    Console.WriteLine("The boss stunned you. You miss your next move.");
                    continue;
            }

            if (shieldUsed)
            {
                playerHP += 50;
                shieldUsed = false;
                Console.Write("\n<---------------------------------------------------------------------->\n");
                Console.WriteLine("Shield triggered, damage reduced by 50%.");
            }
        }

        if (playerHP > 0)
        {
            Console.Write("\n<---------------------------------------------------------------------->\n");
            Console.WriteLine("Victory! You beat the boss!");
        }
        else
        {
            Console.Write("\n<---------------------------------------------------------------------->\n");
            Console.WriteLine("You lost. The boss won.");
        }
    }
}