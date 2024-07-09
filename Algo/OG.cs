//This is my original algorithm translated from julia to c#
namespace TicTacToe
{
    public class OG : Engine
    {
        public OG()
        {

        }
        public override string Name()
        {
            return "OG";
        }
        public override OG Duplicate()
        {
            return new OG();
        }
        public override sbyte[] GiveMove(sbyte[] game)
        {
            sbyte[] move;
            sbyte eval = 0;
            if(game[11] == 0)
            {
                (move, eval) = X(game, new sbyte[3], 3);
            }
            else
            {
                (move, eval) = O(game, new sbyte[3], 3);
            }
            return move;
        }
        private (sbyte[], sbyte) X(sbyte[] game, sbyte[] prevMove, sbyte depth)
        {
            if(SomethingHasChange(game, prevMove[2]))
            {
                //This branch leads to a loss.
                return (prevMove, -1);
            }
            else if(depth == 0)
            {
                //If you want to evalute a position this is where you should call it.
                return (prevMove, 0);
            }
            else
            {
                List<sbyte[]> moveList = GenerateBetterMove(game);
                if(moveList.Count == 0)
                {
                    //Out of move, this branch leads to a loss.
                    return (prevMove, -1);
                }
                else
                {
                    sbyte positionValue = 1;
                    sbyte[] bestMove = new sbyte[3];
                    foreach (sbyte[] move in moveList)
                    {
                        (sbyte[] curMove, sbyte val) = O(MakeMove(game, move),move, (sbyte)(depth-1));
                        if(val>=1)
                        {
                            //This move leads to a winning position.
                            return (move, (sbyte)(val+1));
                        }
                        else if((val == 0) && (positionValue != 0))
                        {
                            //A move that doesn't lose just got found.
                            bestMove = move;
                            positionValue = val;
                        }
                        else if((val < positionValue) && (positionValue != 0))
                        {
                            //This move loses, but slower.
                            bestMove = move;
                            positionValue = val;
                        }
                    }
                    return (bestMove, positionValue);
                }
            }
        }
        private (sbyte[], sbyte) O(sbyte[] game, sbyte[] prevMove, sbyte depth)
        {
            if(SomethingHasChange(game, prevMove[2]))
            {
                return (prevMove, 1);
            }
            else if(depth == 0)
            {
                return (prevMove, 0);
            }
            else
            {
                List<sbyte[]> moveList = GenerateBetterMove(game);
                if(moveList.Count == 0)
                {
                    return (prevMove, 1);
                }
                else
                {
                    sbyte positionValue = -1;
                    sbyte[] bestMove = new sbyte[3];
                    foreach (sbyte[] move in moveList)
                    {
                        (sbyte[] curMove, sbyte val) = X(MakeMove(game, move),move, (sbyte)(depth-1));
                        if(val<=-1)
                        {
                            return (move, (sbyte)(val-1));
                        }
                        else if((val == 0) && (positionValue != 0))
                        {
                            bestMove = move;
                            positionValue = val;
                        }
                        else if((val > positionValue) && (positionValue != 0))
                        {
                            bestMove = move;
                            positionValue = val;
                        }
                    }
                    return (bestMove, positionValue);
                }
            }
        }
    }
}
