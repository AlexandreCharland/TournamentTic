namespace TicTacToe
{
    public class Arena
    {
        public static void Main(string[] args)
        {
            OG bot = new OG();
            //play(bot);
        }
        public static void play(Engine bot)
        {
            Arbiter referee = new Arbiter();
            bool gameInProgress = true;
            sbyte[] userMove;
            sbyte[] compMove;
            while(gameInProgress)
            {
                referee.ShowPosition();
                //Remove annoying warning messages
                #pragma warning disable 8604
                userMove = referee.TransformInt(System.Console.ReadLine());
                while(!referee.IsMoveLegal(userMove))
                {
                    System.Console.WriteLine("Invalid move");
                    userMove = referee.TransformInt(System.Console.ReadLine());
                }
                #pragma warning restore 8604
                if(!referee.IsPinPiece(userMove))
                {
                    System.Console.WriteLine("Get good");
                    break;
                }
                referee.UpdateGame(userMove);
                if(referee.SomeoneWon())
                {
                    System.Console.WriteLine("Congrats you have won");
                    break;
                }
                compMove = bot.GiveMove(referee.getGame());
                if(!referee.IsPinPiece(compMove))
                {
                    System.Console.WriteLine("The computer played an impossible move");
                    System.Console.WriteLine("Congrats you have won");
                    break;
                }
                //clrscr();
                System.Console.WriteLine($"The computer played {compMove[0]}{compMove[1]}{compMove[2]}");
                referee.UpdateGame(compMove);
                if(referee.SomeoneWon())
                {
                    System.Console.WriteLine("Get good");
                    gameInProgress = false;
                }
            }
        }
        public static void Colosseum(List<Engine> gladiators)
        {
            //Todo
        }
        public static byte fight(Engine a, Engine b)
        {
            //Todo
            return 1;
        }
        public static void CalculEloRating(sbyte[] result)
        {
            //Todo
        }
    }
}