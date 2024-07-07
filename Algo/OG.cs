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
        private List<sbyte[]> GenerateOrderMove(sbyte[] game, sbyte[] location)
        {
            List<sbyte[]> moveListDeck = new List<sbyte[]>();
            List<sbyte[]> moveListBoard = new List<sbyte[]>();
            sbyte j;
            for(sbyte i=0; i<=8; ++i)
            {
                if(game[i] == 0)
                {
                    j=0;
                }
                else if(game[i]>=16)
                {
                    j=6;
                }
                else if(game[i]>=4)
                {
                    j=4;
                }
                else
                {
                    j=2;
                }
                while(j <= 5)
                {
                    if(location[j] == 9)
                    {
                        sbyte[] move = new sbyte[3];
                        move[0] = (sbyte)(j>>1);
                        move[1] = 9;
                        move[2] = i;
                        moveListDeck.Add(move);
                    }
                    else if(location[j] != -1)
                    {
                        sbyte[] move = new sbyte[3];
                        move[0] = (sbyte)(j>>1);
                        move[1] = location[j];
                        move[2] = i;
                        moveListBoard.Add(move);
                    }
                    j+=1;
                }
            }
            return moveListDeck.Concat(moveListBoard).ToList();
        }
        private List<sbyte[]> GenerateBetterMove(sbyte[] game)
        {
            sbyte[] location = WherePlayablePiece(game);
            return GenerateOrderMove(game, location);
        }
    }
}