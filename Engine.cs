namespace TicTacToe
{
    public abstract class Engine
    {
        //Function  you have to implement
        public abstract string Name();
        public abstract sbyte[] GiveMove(sbyte[] game);

        //Function that I give you
        //Using them will add a cost to your number of operator
        //You can also reimplemented them if you don't like how I did it.

        public sbyte Decode(sbyte val)
        {
            sbyte code = 0;
            for(sbyte i = 1; i <= 6; ++i)
            {
                if((val & 1) == 1)
                {
                    code = i;
                }
                val = (sbyte) (val >> 1);//In c# the bitwise operation transform a sbyte into an int
            }
            return code;
        }
        public void ShowPosition(sbyte[] game)
        {
            char[] pieceSet = new char[] {'x','o','+','0','X','O'};
            char stuff = ' ';
            string line = " ";
            for(int i = 0; i<=8; ++i)
            {
                sbyte code = Decode(game[i]);
                if(code != 0)
                {
                    stuff = pieceSet[code-1];
                }
                line += stuff;
                if(i%3 == 2)
                {
                    System.Console.WriteLine(line);
                    if(i!=8)
                    {
                        System.Console.WriteLine("---+---+---");
                    }
                    line = " ";
                }
                else
                {
                    line += " | ";
                }
                stuff = ' ';
            }
        }
        public sbyte[] TransformInt(string s)
        {
            int len = s.Length;
            sbyte[] newVec = new sbyte[len];
            for(int i=0; i<len; ++i)
            {
                newVec[i] = (sbyte)Char.GetNumericValue(s[i]);
            }
            return newVec;
        }
        public int WhoIsOnTop(sbyte val)
        {
            return (val & 42)>(val & 21) ? 1 : 0;
        }
        public bool VerifyWin(sbyte a, sbyte b, sbyte c)
        {
            return (a != 0) && (b != 0) && (c != 0) && ((WhoIsOnTop(a)+WhoIsOnTop(b)+WhoIsOnTop(c))%3==0);
        }
        public bool SomeoneWon(sbyte[] game)
        {
            return (VerifyWin(game[0],game[1],game[2]) ||
                    VerifyWin(game[0],game[3],game[6]) ||
                    VerifyWin(game[0],game[4],game[8]) ||
                    VerifyWin(game[1],game[4],game[7]) ||
                    VerifyWin(game[2],game[4],game[6]) ||
                    VerifyWin(game[2],game[5],game[8]) ||
                    VerifyWin(game[3],game[4],game[5]) ||
                    VerifyWin(game[6],game[7],game[8]));
        }
        public bool SomethingHasChange(sbyte[] game, sbyte square)
        {
            if((square & 1)==1)
            {
                return (VerifyWin(game[(square & 4)<<1],game[square],game[((square & 2) | 1)<<1]) ||
                        VerifyWin(game[square],game[4],game[8-square]));
            }
            else if(square == 4)
            {
                return (VerifyWin(game[0],game[4],game[8]) ||
                        VerifyWin(game[1],game[4],game[7]) ||
                        VerifyWin(game[2],game[4],game[6]) ||
                        VerifyWin(game[3],game[4],game[5]));
            }
            else
            {
                int stupidIf = (square < 4) ? 1 : 7;
                return (VerifyWin(game[square],game[4],game[8-square]) ||
                        VerifyWin(game[square],game[(square%6)+3],game[6+2*(square%3)-square]) ||
                        VerifyWin(game[square],game[stupidIf],game[2+square-((square%3)*2)]));
            }
        }
        public sbyte[] MakeMove(sbyte[] game, sbyte[] move)
        {
            sbyte[] newGame = (sbyte[])(game.Clone());
            sbyte piece = (sbyte)(1<<(move[0]<<1));
            newGame[move[2]] += (sbyte)(piece<<newGame[11]);
            if (move[1] == -1)
            {
                newGame[9^newGame[11]] -= piece;
            }
            else
            {
                newGame[move[2]] -= (sbyte)(piece<<newGame[11]);
            }
            newGame[11] ^= 1;//Bitwise xor
            return newGame;
        }
        public sbyte[] FoundOne(sbyte[] location, sbyte piece, sbyte from)
        {
            int a = (location[piece<<1] == 9) ? 0 : 1;
            location[(piece<<1)+a] = from;
            return location;
        }
        public sbyte[] WhoIsOnTheBench(sbyte[] game, sbyte[] location)
        {
            sbyte val = game[9^game[11]];
            for(sbyte i=0; i<=2; ++i)
            {
                if((val & 3) != 0)
                {
                    location = FoundOne(location, i, 0);
                }
                val >>= 2;
            }
            return location;
        }
        public sbyte[] WhereEveryPiece(sbyte[] game)
        {
            sbyte[] location = new sbyte[6];
            Array.Fill(location, (sbyte)9);
            for(sbyte i = 0; i<=8; ++i)
            {
                if(game[i] != 0)
                {
                    if(WhoIsOnTop(game[i]) == game[11])
                    {
                        if(game[i]>=16)
                        {
                            location = FoundOne(location, 2, i);
                        }
                        else if(game[i]>=4)
                        {
                            location = FoundOne(location, 1, i);
                        }
                        else
                        {
                            location = FoundOne(location, 0, i);
                        }
                    }
                }
            }
            return WhoIsOnTheBench(game, location);
        }
        public sbyte[] WherePlayablePiece(sbyte[] game)
        {
            sbyte[] location = new sbyte[6];
            Array.Fill(location, (sbyte)9);
            for(sbyte i = 0; i<=8; ++i)
            {
                if(game[i] != 0)
                {
                    if(WhoIsOnTop(game[i]) == game[11])
                    {
                        if(game[i]>=16)
                        {
                            game[i] -= (sbyte)(16<<game[11]);
                            if(!SomethingHasChange(game, i))
                            {
                                location = FoundOne(location, 2, i);
                            }
                            game[i] += (sbyte)(16<<game[11]);
                        }
                        else if(game[i]>=4)
                        {
                            game[i] -= (sbyte)(4<<game[11]);
                            if(!SomethingHasChange(game, i))
                            {
                                location = FoundOne(location, 1, i);
                            }
                            game[i] += (sbyte)(4<<game[11]);
                        }
                        else
                        {
                            location = FoundOne(location, 0, i);
                        }
                    }
                }
            }
            return WhoIsOnTheBench(game, location);
        }
        public List<sbyte[]> GenerateMove(sbyte[] game, sbyte[] location)
        {
            List<sbyte[]> moveList = new List<sbyte[]>();
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
                    j=0;
                }
                while(j <= 5)
                {
                    if(location[j] == 9)
                    {
                        j+=(sbyte)(1+(j&1));
                    }
                    else
                    {
                        sbyte[] move = new sbyte[3];
                        move[0] = (sbyte)((j-1)>>1);
                        move[1] = location[j];
                        move[2] = i;
                        moveList.Add(move);
                        j = (sbyte)((location[j] == -1) ? j + (j & 1) + 1 : j+1);
                    }
                }
            }
            return moveList;
        }
        public List<sbyte[]> GenerateOrderMove(sbyte[] game, sbyte[] location)
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
                    j=0;
                }
                while(j <= 5)
                {
                    if(location[j] == 9)
                    {
                        j+=(sbyte)(1+(j&1));
                    }
                    else if(location[j] == -1)
                    {
                        sbyte[] move = new sbyte[3];
                        move[0] = (sbyte)((j-1)>>1);
                        move[2] = i;
                        moveListDeck.Add(move);
                        j = (sbyte)(1+(j&1));
                    }
                    else
                    {
                        sbyte[] move = new sbyte[3];
                        move[0] = (sbyte)((j-1)>>1);
                        move[1] = location[j];
                        move[2] = i;
                        moveListBoard.Add(move);
                        j += 1;
                    }
                }
            }
            return moveListDeck.Concat(moveListBoard).ToList();
        }
        public List<sbyte[]> GenerateEveryMove(sbyte[] game)
        {
            sbyte[] location = WhereEveryPiece(game);
            return GenerateMove(game, location);
        }
        public List<sbyte[]> GenerateBetterMove(sbyte[] game)
        {
            sbyte[] location = WherePlayablePiece(game);
            return GenerateOrderMove(game, location);
        }
    }
}