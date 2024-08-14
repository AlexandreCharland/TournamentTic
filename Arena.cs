//This is the "main"
//This is where the battles will take place.
using System.Diagnostics;//for the usage of the stopwatch
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
            //Stopwatch sw = new Stopwatch();
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
                //sw.Start();
                compMove = bot.GiveMove(referee.getGame());
                //sw.Stop();
                //System.Console.WriteLine("Elapsed={0}",sw.Elapsed);
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
            sbyte[,,] result = new sbyte[howMany, howMany, 27];
            for(int i=0; i<howMany; ++i)
            {
                for(int j=0; j<howMany; ++j)
                {
                    if(i==j){continue;}
                    for(int k=0; k<27; ++k)
                    {
                        result[i,j,k] = Fight(gladiators[i], gladiators[j], battleField[k]);
                    }
                }
            }
            CalculEloRating(result, howMany);
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
        public static void CalculEloRating(sbyte[,,] result, int numberBot)
        {
            int k = 10;//La constante va changer
            Random gen = new Random();
            int change = 10;
            double[] ratings = new double[numberBot];
            List<sbyte[]> order = new List<sbyte[]>();
            for(sbyte i=0; i<numberBot; ++i)
            {
                for(sbyte j=0; j<numberBot; ++j)
                {
                    if(i==j){continue;}
                    for(sbyte l=0; l<27; ++l)
                    {
                        order.Add(new sbyte[]{i,j,l});
                    }
                }
            }
            int amount = order.Count;int ind;
            sbyte a; sbyte b;sbyte res; double prob;
            while(amount>0)
            {
                ind = gen.Next(0,amount);
                sbyte[] triple = order[ind];
                order.RemoveAt(ind);
                a = triple[0];
                b = triple[1];
                res = result[triple[0], triple[1], triple[2]];
                if(res == 1)//A won
                {
                    prob = Prob(ratings[a], ratings[b]);
                    ratings[a] += k*prob;
                    ratings[b] -= k*prob;
                }
                else if(res == -1)//B won
                {
                    prob = Prob(ratings[b], ratings[a]);
                    ratings[a] -= k*prob;
                    ratings[b] += k*prob;
                }
                else
                {
                    prob = Prob(ratings[a], ratings[b]);
                    ratings[a] += k*(0.5-prob);
                    prob = Prob(ratings[b], ratings[a]);
                    ratings[b] += k*(0.5-prob);
                }
                --amount;
            }
            for(int i=0; i<numberBot; ++i)
            {
                System.Console.WriteLine(ratings[i]);
            }
        }
        public static double Prob(double ratingA, double ratingB)
        {
            int s = 100;//La constante va changer
            return 1/(1+Math.Pow(10,(ratingB-ratingA)/s));
        }
    }
}