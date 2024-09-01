//Author Alexandre Charland

//This algorithm will evaluate a position base on the amount of possible move the
//opponent can play. The less the better
namespace TicTacToe
{
    public class Restrictor : Engine
    {
        public Restrictor(){}
        public override string Name() => "Anaconda";
        public override Restrictor Duplicate() => new Restrictor(); 
        public override sbyte[] GiveMove(sbyte[] game)
        {
            sbyte[] bestMove = new sbyte[3];
            sbyte eval = 64;
            List<sbyte[]> moveList = GenerateBetterMove(game);
            sbyte depth = 6;
            sbyte val;
            //List<sbyte[]> moveList = new List<sbyte[]>();
            //moveList.Add(new sbyte[3]{2,9,6});
            foreach(sbyte[] move in moveList)
            {
                //val = CrunchingNumbers(MakeMove(game, move), move[2], depth);
                val = Mx(MakeMove(game, move), move[2], depth, -1, eval);
                //System.Console.WriteLine($"{move[0]}{move[1]}{move[2]} {val} {depth}");
                if(val < eval)
                {
                    bestMove = move;
                    eval = val;
                    if(val<=depth)
                    {
                        depth = (sbyte)Math.Max(0, val-2);
                    }
                }
            }
            //System.Console.WriteLine($"{bestMove[0]}{bestMove[1]}{bestMove[2]} {eval}");
            return bestMove;
        }
        private sbyte Mn(sbyte[] game, sbyte square, sbyte depth, sbyte alpha, sbyte beta)
        {
            sbyte positionValue = 49;
            if(SomethingHasChange(game, square))
            {
                return positionValue;
            }
            List<sbyte[]> moveList = GenerateBetterMove(game);
            if(moveList.Count == 0)
            {
                return positionValue;
            }
            sbyte val;
            --depth;
            foreach(sbyte[] move in moveList)
            {
                val = Mx(MakeMove(game, move), move[2], depth, alpha, beta);
                if(val < positionValue)
                {
                    beta = val;
                    positionValue = val;
                    if(positionValue <= 6)
                    {
                        return (sbyte)(positionValue+2);
                    }
                }
                if(alpha >= beta)
                {
                    break;
                }
            }
            return positionValue;
        }
        private sbyte Mx(sbyte[] game, sbyte square, sbyte depth, sbyte alpha, sbyte beta)
        {
            sbyte positionValue = 0;
            if(SomethingHasChange(game, square))
            {
                return positionValue;
            }
            else if(depth == 0)
            {
                return CountingMoves(game);
            }
            List<sbyte[]> moveList = GenerateBetterMove(game);
            if(moveList.Count == 0)
            {
                return positionValue;
            }
            sbyte val;
            --depth;
            foreach(sbyte[] move in moveList)
            {
                val = Mn(MakeMove(game, move), move[2], depth, alpha, beta);
                if(val > positionValue)
                {
                    alpha = val;
                    positionValue = val;
                    if(positionValue >= 49)
                    {
                        return (sbyte)(positionValue-2);
                    }
                }
                if(alpha >= beta)
                {
                    break;
                }
            }
            return positionValue;
        }
        private sbyte CrunchingNumbers(sbyte[] game, sbyte square, sbyte depth)
        {
            sbyte positionValue = (sbyte)((depth&1)*(47));
            sbyte backup = (sbyte)((47)-positionValue);
            sbyte turn = ((depth&1)!=0) ? (sbyte)1 : (sbyte)-1;
            if(SomethingHasChange(game, square))
            {
                return positionValue;
            }
            else if(depth == 0)
            {
                return CountingMoves(game);
            }
            List<sbyte[]> moveList = GenerateBetterMove(game);
            if(moveList.Count == 0)
            {
                return positionValue;
            }
            sbyte val;
            --depth;
            foreach(sbyte[] move in moveList)
            {
                val = CrunchingNumbers(MakeMove(game, move), move[2], depth);
                if(val*turn<positionValue*turn)
                {
                    positionValue = val;
                    if(Math.Abs(val-backup) < 4)
                    {
                        break;
                    }
                }
            }
            return (sbyte)(positionValue+1);
        }
        public sbyte CountingMoves(sbyte[] game)
        {
            sbyte[] location = new sbyte[6];
            sbyte val = game[9+game[11]];
            for(sbyte i=0; i<=2; ++i)
            {
                if((val & 3) != 0)
                {
                    location[i] = 1;
                }
                val >>= 2;
            }
            bool myPiece;
            for(sbyte i = 0; i<=8; ++i)
            {
                myPiece = false;
                if(game[i] != 0)
                {
                    if(WhoIsOnTop(game[i]) == game[11])
                    {
                        myPiece = true;
                    }
                }
                switch (game[i])
                {
                    case 0:
                        location[3]+=1;location[4]+=1;location[5]+=1;break;
                    case >=16:
                        game[i] -= (sbyte)(16<<game[11]);
                        if(myPiece && !SomethingHasChange(game, i))
                        {
                            location[2]+=1;
                        }
                        game[i] += (sbyte)(16<<game[11]);break;
                    case >=4:
                        game[i] -= (sbyte)(4<<game[11]);
                        if(myPiece && !SomethingHasChange(game, i))
                        {
                            location[1]+=1;
                        }
                        game[i] += (sbyte)(4<<game[11]);
                        location[5]+=1;break;
                    default:
                        if(myPiece)
                        {
                            location[0]+=1;
                        }
                        location[4]+=1;location[5]+=1;break;
                }
            }
            return (sbyte)(location[0]*location[3]+location[1]*location[4]+location[2]*location[5]+6);
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
        public List<sbyte[]> GenerateBetterMove(sbyte[] game)
        {
            sbyte[] location = WherePlayablePiece(game);
            return GenerateOrderMove(game, location);
        }
    }
}