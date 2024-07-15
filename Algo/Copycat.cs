//Author Alexandre Charland

//This algorithm will evaluate a position base on how symetrical a position is
namespace TicTacToe
{
    public class Copycat : Engine
    {
        private sbyte[,] instruction = new sbyte[4,4] {{1,3,5,7},{3,7,1,5},{0,2,6,8},{6,0,8,2}};
        public Copycat(){}
        public override string Name() => "Copycat";
        public override Copycat Duplicate() => new Copycat();
        public override sbyte[] GiveMove(sbyte[] game)
        {
            List<sbyte[]> moveList = GenerateEveryMove(game);
            sbyte[] bestMove = new sbyte[3];
            short mostSym = 421;//upper bound (I think it should be closer to 295)
            short eval;
            foreach(sbyte[] move in moveList)
            {
                eval = HowSym(MakeMove(game, move));
                if(mostSym > eval)
                {
                    mostSym = eval;
                    bestMove = move;
                }
            }
            return bestMove;
        }
        private short HowSym(sbyte[] game)
        {
            short eval = 0;
            if(SomeoneWon(game))
            {
                return -1;
            }
            for(sbyte i=0; i<=3; ++i)
            {
                for(sbyte j=0; j<=2; ++j)
                {
                    eval += (short)(Math.Abs(game[instruction[i,j]] - game[instruction[i,j+1]]));
                }
            }
            return eval;
        }
    }
}