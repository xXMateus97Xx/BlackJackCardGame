using BlackJackCardGame;

var game = new BlackJack();

var play = true;

while (play)
{
    game.StartGame();

    Console.Write("Your Cards: ");
    PrintCards(game.UserCards);

    var userState = RoundState.Continue;
    var engineState = RoundState.Continue;
    while (userState == RoundState.Continue || engineState == RoundState.Continue)
    {
        if (userState == RoundState.Continue)
        {
            Console.Write("Pick new card? (Y/n) ");
            var pick = Console.ReadLine();
            var pickCard = pick != "n" && pick != "N";

            if (pickCard)
            {
                userState = game.PickCardForUser();
                Console.Write("Your Cards: ");
                PrintCards(game.UserCards);
            }
            else
            {
                userState = RoundState.Stop;
            }
        }

        if (userState == RoundState.Lose)
            break;

        if (engineState == RoundState.Continue)
            engineState = game.PickCardForEngine();

        if (engineState == RoundState.Lose)
            break;
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

static void PrintCards(ReadOnlySpan<Card> cards)
{
    Span<char> cardsString = stackalloc char[cards.Length * 2 + (cards.Length - 1)];

    var currentPos = cardsString;

    for (var i = 0; i < cards.Length; i++)
    {
        cards[i].WriteToSpan(currentPos);
        if (currentPos.Length > 2)
        {
            currentPos[2] = ' ';
            currentPos = currentPos[3..];
        }
    }

    Console.Out.WriteLine(cardsString);
}