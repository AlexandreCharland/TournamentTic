//This is my original algorithm translated from julia to c#
namespace TicTacToe
{
    public class OG : Engine
    {
        public OG(){}
        public override string Name() => "OG";
        public override OG Duplicate() => new OG();
        
        public override sbyte[] GiveMove(sbyte[] game)
        {
            sbyte[] bestMove = new sbyte[3];
            sbyte turn = (game[11]==0) ? (sbyte)1 : (sbyte)-1;
            sbyte eval = turn;
            List<sbyte[]> moveList = GenerateBetterMove(game);
            foreach(sbyte[] move in moveList)
            {
                sbyte val = CrunchingNumbers(MakeMove(game, move), move[2], 2);
                if(val*turn>=1)
                {
                    return move;
                }
                else if(((val == 0) || (val*turn < eval*turn)) && (eval != 0))
                {
                    bestMove = move;
                    eval = val;
                }
            }
            return bestMove;
        }
        private sbyte CrunchingNumbers(sbyte[] game, sbyte square, sbyte depth)
        {
            sbyte turn = (game[11]==0) ? (sbyte)1 : (sbyte)-1;
            if(SomethingHasChange(game, square))
            {
                return (sbyte)(-turn);
            }
            else if(depth == 0)
            {
                return 0;
            }
            List<sbyte[]> moveList = GenerateBetterMove(game);
            if(moveList.Count == 0)
            {
                return (sbyte)(-turn);
            }
            sbyte positionValue = turn;
            foreach(sbyte[] move in moveList)
            {
                sbyte val = CrunchingNumbers(MakeMove(game, move), move[2], (sbyte)(depth-1));
                if(val*turn>=1)
                {
                    return (sbyte)(val+turn);
                }
                else if(((val == 0) || (val*turn < positionValue*turn)) && (positionValue != 0))
                {
                    positionValue = val;
                }
            }
            return positionValue;
        }
    }
}