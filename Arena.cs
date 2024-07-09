//This is the "main"
//This is where the battles will take place.

namespace TicTacToe
{
    public class Arena
    {
        public static void Main(string[] args)
        {
            OG bot1 = new OG();
            RandomMove bot2 = new RandomMove();
            List<Engine> gladiators = new List<Engine>();
            gladiators.Add(bot1);
            gladiators.Add(bot2);
            Colosseum(gladiators);
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
            List<sbyte[]> battleField = GenerateBattleField();
            int howMany = gladiators.Count;
            sbyte[,] result = new sbyte[howMany, howMany];
            for(int i=0; i<howMany; ++i)
            {
                for(int j=0; j<howMany; ++j)
                {
                    if(i==j){continue;}
                    for(int k=0; k<battleField.Count; ++k)
                    {
                        if(Fight(gladiators[i], gladiators[j], battleField[k]))
                        {
                            result[i,j] += 1;
                        }
                        else
                        {
                            result[j,i] += 1;
                        }
                    }
                }
            }
            System.Console.WriteLine(result[0,1]);
            System.Console.WriteLine(result[1,0]);
        }
        public static bool Fight(Engine a, Engine b, sbyte[] game)
        {
            Arbiter referee = new Arbiter(game);
            Engine playerA = a.Duplicate();
            Engine playerB = b.Duplicate();
            sbyte[] move;
            while(true)
            {
                move = playerA.GiveMove(referee.getGame());
                if(!referee.IsPinPiece(move))
                {
                    return false;
                }
                referee.UpdateGame(move);
                if(referee.SomeoneWon())
                {
                    return true;
                }
                move = playerB.GiveMove(referee.getGame());
                if(!referee.IsPinPiece(move))
                {
                    return true;
                }
                referee.UpdateGame(move);
                if(referee.SomeoneWon())
                {
                    return false;
                }
            }
        }
        public static List<sbyte[]> GenerateBattleField()
        {
            List<sbyte[]> battleField = new List<sbyte[]>();
            sbyte[] game = new sbyte[12];
            game[9] = 42;
            game[10] = 42;
            battleField.Add((sbyte[])(game.Clone()));
            Arbiter helper = new Arbiter();
            List<sbyte[]> moveList = helper.GenerateEveryMove(game);
            foreach (sbyte[] move in moveList)
            {
                battleField.Add(helper.MakeMove(game, move));
            }
            return battleField;
        }
        public static void CalculEloRating(sbyte[] result)
        {
            //Todo
        }
    }
}