//This is the "main"
//This is where the battles will take place.

namespace TicTacToe
{
    public class Arena
    {
        public static void Main(string[] args)
        {

        }
        public static void play(Engine bot)
        {
            Arbiter referee = new Arbiter();
            sbyte isItOver;
            sbyte[] userMove;
            sbyte[] compMove;
            while(true)
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
                isItOver = referee.UpdateGame(userMove);
                if(isItOver == 1)
                {
                    System.Console.WriteLine("Congrats you have won");
                    break;
                }
                else if(isItOver == -1)
                {
                    System.Console.WriteLine("Draw");
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
                isItOver = referee.UpdateGame(compMove);
                if(isItOver == 1)
                {
                    System.Console.WriteLine("Get good");
                    break;
                }
                else if(isItOver == -1)
                {
                    System.Console.WriteLine("Draw");
                    break;
                }
            }
        }
        public static void Colosseum(List<Engine> gladiators)
        {
            List<sbyte[]> battleField = GenerateBattleField();
            int howMany = gladiators.Count;
            sbyte[,] result = new sbyte[howMany, howMany];
            sbyte ending;
            for(int i=0; i<howMany; ++i)
            {
                for(int j=0; j<howMany; ++j)
                {
                    if(i==j){continue;}
                    for(int k=0; k<battleField.Count; ++k)
                    {
                        ending = Fight(gladiators[i], gladiators[j], battleField[k]);
                        if(ending == 1)
                        {
                            result[i,j] += 1;
                        }
                        else if(ending == -1)
                        {
                            result[j,i] += 1;
                        }
                    }
                }
            }
            for(int i=0; i<howMany;++i)
            {
                for(int j=0; j<howMany;++j)
                {
                    System.Console.Write($"{result[i,j]} ");
                }
                System.Console.WriteLine();
            }
            //Todo elo (needs more test bot)
            //Todo ShowResult
        }
        public static sbyte Fight(Engine a, Engine b, sbyte[] game)
        {
            Arbiter referee = new Arbiter(game);
            Engine playerA = a.Duplicate();
            Engine playerB = b.Duplicate();
            sbyte[] move;
            sbyte isItOver;
            while(true)
            {
                move = playerA.GiveMove(referee.getGame());
                if(!referee.IsPinPiece(move))
                {
                    return -1;
                }
                isItOver = referee.UpdateGame(move);
                if(isItOver == 1)
                {
                    return 1;
                }
                else if(isItOver == -1)
                {
                    return 0;
                }
                move = playerB.GiveMove(referee.getGame());
                if(!referee.IsPinPiece(move))
                {
                    return 1;
                }
                isItOver = referee.UpdateGame(move);
                if(isItOver == 1)
                {
                    return -1;
                }
                else if(isItOver == -1)
                {
                    return 0;
                }
            }
        }
        public static List<sbyte[]> GenerateBattleField()
        {
            List<sbyte[]> battleField = new List<sbyte[]>();
            sbyte[] game = new sbyte[12];
            game[9] = 42;
            game[10] = 42;
            Arbiter helper = new Arbiter();
            List<sbyte[]> moveList = helper.GenerateEveryMove(game);
            foreach (sbyte[] move in moveList)
            {
                battleField.Add(helper.MakeMove(game, move));
            }
            return battleField;
        }
        public static void CalculEloRating(sbyte[,] result)
        {
            //Todo
        }
    }
}