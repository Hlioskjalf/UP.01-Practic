class Program
{
    static void Main()
    {
        const int crystalPrice = 10;

        Console.Write("                   <---------- CRYSTAL SHOP ---------->     \n");
        Console.Write("\n<------------------------ REALISE YOUR POVERTY ------------------------>\n");
        Console.Write("\nEnter the initial amount of gold: ");
        int gold = int.Parse(Console.ReadLine());

        Console.Write($"How many crystals do you want to buy? (price {crystalPrice} gold per crystal): ");
        int crystalsToBuy = int.Parse(Console.ReadLine());

        Console.Write("\n<---------------------------------------------------------------------->\n");

        int maxAffordable = gold / crystalPrice;
        int crystalsPurchased = Math.Min(crystalsToBuy, maxAffordable);
        int goldSpent = crystalsPurchased * crystalPrice;
        int remainingGold = gold - goldSpent;
        int totalCrystals = crystalsPurchased;

        Console.WriteLine($"\nOperation result:");
        Console.WriteLine($"The rest of the gold: {remainingGold}");
        Console.WriteLine($"Bought crystals: {totalCrystals}");

        Console.Write("\n<---------------------------------------------------------------------->\n");
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}