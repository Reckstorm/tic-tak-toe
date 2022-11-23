using Classwork_19._11;

ConsoleKeyInfo key;
string? playerOne;
string? playerTwo;
bool flag;
do
{
    flag = false;
    Console.Clear();
    Console.WriteLine("Welcome!\n1 - Start new game;\nEsc - Exit");
    key = Console.ReadKey(true);
    if (key.KeyChar == '1')
    {
        Console.Clear();
        Console.WriteLine("Enter first player name:");
        playerOne = Console.ReadLine();
        Console.WriteLine("Enter second player name:");
        playerTwo = Console.ReadLine();
        Game game = new Game(playerOne, playerTwo);
        game.PlayerMove += ChooseMove;
        game.GameOver += GameOver;
        Console.WriteLine($"{game.PlayerOne.Name} moves first");
        key = Console.ReadKey(true);
        do
        {
            do
            {
                Console.Clear();
                Console.WriteLine(game + "\n\n");
                Console.WriteLine($"Choose your move");
                Console.WriteLine(game.PrintCoords());
                key = Console.ReadKey(true);
                if (key.KeyChar == 27)
                {
                    Console.Clear();
                    Console.WriteLine("Bye!");
                    Environment.Exit(0);
                }
            } while (key.KeyChar < 49 || key.KeyChar > 57);
            if (!flag && game.MakeMove(int.Parse(key.KeyChar.ToString()), game.PlayerOne))
            {
                flag = true;
            }
            else if(flag && game.MakeMove(int.Parse(key.KeyChar.ToString()), game.PlayerTwo))
            {
                flag = false;
            }
            if (game.WinCheck())
            {
                break;
            }
        } while (true);
    }
    else if (key.KeyChar == 27)
    {
        Console.Clear();
        Console.WriteLine("Bye!");
        Environment.Exit(0);
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Invalid command");
        Console.ReadKey(true);
    }
} while (true);


void ChooseMove(string m)
{
    Console.WriteLine(m);
    Console.ReadKey(true);
}

void GameOver(string m, Game game)
{
    Console.Clear();
    Console.WriteLine(game);
    Console.WriteLine(m);
    Console.ReadKey(true);
}