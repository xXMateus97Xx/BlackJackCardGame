using CardGame;

var game = new BlackJack();

var play = true;

while (play)
{
    game.StartGame();

    Console.Write("Your Cards: ");
    PrintCards(game.UserCards);

    var userCanPick = true;
    var engineCanPick = true;
    var pickCard = true;
    while (userCanPick && engineCanPick)
    {
        if (userCanPick && pickCard)
        {
            Console.Write("Pick new card? (Y/n) ");
            var pick = Console.ReadLine();
            pickCard = pick != "n" && pick != "N";

            if (pickCard)
            {
                userCanPick = game.PickCardForUser();
                Console.Write("Your Cards: ");
                PrintCards(game.UserCards);
            }
        }

        if (userCanPick && engineCanPick)
            engineCanPick = game.PickCardForEngine();
    }

    var result = game.GetResult();

    switch (result)
    {
        case GameResult.Win:
            Console.WriteLine("You Won");
            break;
        case GameResult.Lose:
            Console.WriteLine("You Lost!!!");
            break;
        case GameResult.Draw:
            Console.WriteLine("It's a Draw!!!");
            break;
    }

    Console.Write("Your Cards: ");
    PrintCards(game.UserCards);

    Console.Write("Computer Cards: ");
    PrintCards(game.EngineCards);

    Console.Write("Play Again? (Y/n) ");
    var key = Console.ReadLine();
    play = key != "n" && key != "N";
}

Console.WriteLine("Bye!");

Console.ReadLine();

static void PrintCards(IEnumerable<Card> cards)
{
    foreach (var card in cards)
        Console.Write("{0} ", card);

    Console.WriteLine();
}