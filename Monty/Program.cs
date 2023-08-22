// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
using System.Transactions;

Random rnd = new Random();
int WinStick = 0;
int WinSwitch = 0;
int totalRuns = 0;
const int numberOfDoors = 100;

while (true)
{
    int testCount = 1000000;
    totalRuns += testCount;
    Console.WriteLine($"Monty Hall with {numberOfDoors} doors");

    for (int i = 0; i < testCount; i++)
    {
        WinStick += test(true) ? 1 : 0;
    }
    Console.WriteLine($"\nIf you Stick:  {WinStick}/{totalRuns} ({((double)WinStick/ (double)totalRuns) * 100.0}%)");

    for (int i = 0; i < testCount; i++)
    {
        WinSwitch += test(false) ? 1 : 0;
    }
    Console.WriteLine($"If you Switch: {WinSwitch}/{totalRuns} ({((double)WinSwitch / (double)totalRuns) * 100.0}%)");

    Thread.Sleep(1000);
    Console.Clear();
}


bool test(bool stickWithFirstChoice)
{
    // Populate Doors
    string[] doors = new string[numberOfDoors];
    for (int i = 0; i < numberOfDoors; i++)
    {
        doors[i] = "GOAT";
    }

    doors[rnd.Next() % numberOfDoors] = "CAR";

    // Pick Your First Door
    int firstChoice = rnd.Next() % numberOfDoors;

    // Reveal a Goat
    int j = 0;
    for (int i = 0; i < numberOfDoors && j < numberOfDoors - 2; i++)
    {
        if (i != firstChoice && doors[i] != "CAR")
        {
            doors[i] = "REVEALED";
            j++;
        }
    }

    // Stick with Choice
    if (stickWithFirstChoice)
    {
        return doors[firstChoice] == "CAR";
    }
    // Switch Doors
    else
    {
        int secondChoice = firstChoice;
        while (true)
        {
            secondChoice = (secondChoice + 1) % numberOfDoors;
            if (doors[secondChoice] != "REVEALED")
            {
                return doors[secondChoice] == "CAR";
            }
        }
    }
}