//This is my original algorithm translated from julia to c#
//I hope that most engine has a positive score against it.

//Tech
//It has the minimax algorithm implemented. (To see a more readable version, check the branch in the rep)
//It is unable to evaluate a position.
//It only consider the move that aren't pin.
//It orders the move that bring a piece in the game first.
//In the minimax algorithm the order of the move matters. Finding a winning
// continuation early in the search reduces the calculation time.

//Weakness: Is unable to differenciate two move that doesn't lose or doesn't win
// within a certain depth.
//It has a hard time navigating the opening phase of the game.

namespace TicTacToe
{
    public class OG : Engine
    {
        private sbyte strength;
        public OG(sbyte Strength){strength = Strength;}
        public override string Name() => "OG";
        public override OG Duplicate() => new OG(strength);
        
        public override sbyte[] GiveMove(sbyte[] game)
        {
            sbyte[] bestMove = new sbyte[3];
            sbyte turn = (game[11]==0) ? (sbyte)1 : (sbyte)-1;
            sbyte eval = turn;
            List<sbyte[]> moveList = GenerateBetterMove(game);
            sbyte depth = strength;
            foreach(sbyte[] move in moveList)
            {
                sbyte val = CrunchingNumbers(MakeMove(game, move), move[2], depth);
                if(val*turn>=1)
                {
                    //This was change. Before it simply return a move that win.
                    //It would skip a one move win and could get stuck in a loop.
                    bestMove = move;
                    eval=0;
                    depth=1;
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
                //If you want to evaluate a position this is where you should call it.
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
                switch (game[i])
                {
                    case 0:
                        j=0;break;
                    case >=16:
                        j=6;break;
                    case >=4:
                        j=4;break;
                    default:
                        j=2;break;
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