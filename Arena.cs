namespace TicTacToe
{
    public class Arena
    {
        public static void Main(string[] args)
        {
            //play();
        }
        public static void imprime(List<sbyte[]> a)
        {
            for(int i=0; i<a.Count; ++i)
            {
                sbyte[] b = a[i];
                for(int j=0; j<=2; ++j)
                {
                    System.Console.Write(b[j]);
                }
                System.Console.WriteLine();
            }
        }
        public static bool MoveInList(List<sbyte[]> listMove, sbyte[] userMove)
        {
            bool containIt = false;
            foreach (sbyte[] move in listMove)
            {
                containIt = containIt || Enumerable.SequenceEqual(move, userMove);
            }
            return containIt;
        }
        public static void play()
        {
            Engine bot = new RandomMove();
            bool gameInProgress = true;
            sbyte[] game = new sbyte[12];
            game[9] = 42;
            game[10] = 42;
            List<sbyte[]> listMove;
            sbyte[] userMove;
            List<sbyte[]> listBadMove;
            sbyte[] compMove;
            while(gameInProgress)
            {
                bot.ShowPosition(game);
                listMove = bot.GenerateEveryMove(game);
                userMove = bot.TransformInt(System.Console.ReadLine());
                while(!MoveInList(listMove, userMove))
                {
                    System.Console.WriteLine("Invalid move");
                    userMove = bot.TransformInt(System.Console.ReadLine());
                }
                listBadMove = bot.GenerateBetterMove(game);
                if(!MoveInList(listBadMove, userMove))
                {
                    System.Console.WriteLine("Get good");
                    break;
                }
                game = bot.MakeMove(game, userMove);
                if(bot.SomeoneWon(game))
                {
                    System.Console.WriteLine("Congrats you have won");
                    break;
                }
                compMove = bot.GiveMove(game);
                //clrscr();
                System.Console.WriteLine($"The computer played {compMove[0]}{compMove[1]}{compMove[2]}");
                game = bot.MakeMove(game, compMove);
                if(bot.SomeoneWon(game))
                {
                    System.Console.WriteLine("Get good");
                    gameInProgress = false;
                }
            }
        }
    }
}