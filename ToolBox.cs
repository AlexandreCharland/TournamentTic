/*
Visualisation of the board 
 0 | 1 | 2
---+---+--
 3 | 4 | 5
---+---+---
 6 | 7 | 8
game[9] deck of the X player
game[10] deck of the O player
game[11] 0 if X turn 1 if O turn

 I | B | D
" "| 0 |
 x | 1 | 1
 o | 2 | 1
 + | 4 | 4
 0 | 8 | 4
 X | 16| 16
 O | 32| 16

This is the notation of I(image), B(Board or game[i] i∈{0,...,8}) D(Deck of game[i] i∈{9,10})
*/

//This class will contain useful function.
//I haven't decided yet if all of these function are free or not.
namespace TicTacToe
{
    public abstract class ToolBox
    {
        //This function take a non zero value
        //It returns 1 if O control the square and 1 if X controls the square
        //For example val=33 = 32+1 then O control the square so return 1
        public int WhoIsOnTop(sbyte val)
        {
            return (val & 42)>(val & 21) ? 1 : 0;
        }
        //This function takes three values of squares
        //It will return true if all three square are control by the same player.
        public bool VerifyWin(sbyte a, sbyte b, sbyte c)
        {
            return (a != 0) && (b != 0) && (c != 0) && ((WhoIsOnTop(a)+WhoIsOnTop(b)+WhoIsOnTop(c))%3==0);
        }
        //This function return true if there is a TictacToe on the board
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
        //This function return true if there is a TicTacToe on the board after changing the value of a square.
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
        //This function takes a game and a move and return the game after the move is played.
        //It assumes that both the game and move are valid.
        public sbyte[] MakeMove(sbyte[] game, sbyte[] move) //cost 0
        {
            sbyte[] newGame = (sbyte[])(game.Clone());
            sbyte piece = (sbyte)(1<<(move[0]<<1));
            newGame[move[2]] += (sbyte)(piece<<newGame[11]);
            if (move[1] == 9)
            {
                newGame[9+newGame[11]] -= piece;
            }
            else
            {
                newGame[move[1]] -= (sbyte)(piece<<newGame[11]);
            }
            newGame[11] ^= 1;//Bitwise xor
            return newGame;
        }
        //This function changes the location array when a piece is found
        public sbyte[] FoundOne(sbyte[] location, sbyte piece, sbyte from)
        {
            int a = (location[piece<<1] == -1) ? 0 : 1;
            location[(piece<<1)+a] = from;
            return location;
        }
        //This function adds the piece on the deck in the location array
        public sbyte[] WhoIsOnTheBench(sbyte[] game, sbyte[] location)
        {
            sbyte val = game[9+game[11]];
            for(sbyte i=0; i<=2; ++i)
            {
                if((val & 3) != 0)
                {
                    location = FoundOne(location, i, 9);
                }
                val >>= 2;
            }
            return location;
        }
        //This function return a location array of every piece.
        //The format of the location array is the following.
        //[Small, Small, Medium, Medium, Big, Big]
        //The possible values inside are, 0,...,8 the square of the piece
        //and -1 if it is impossible to move the piece (stuck under something or both piece are on the deck)
        public sbyte[] WhereEveryPiece(sbyte[] game)
        {
            sbyte[] location = new sbyte[6];
            Array.Fill(location, (sbyte)-1);
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
        //Similar to the previous function except that it won't show the pin pieces.
        //A piece is consider pin if moving it reveal a TicTacToe for the oponent.
        public sbyte[] WherePlayablePiece(sbyte[] game)
        {
            sbyte[] location = new sbyte[6];
            Array.Fill(location, (sbyte)-1);
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
        //This function generate a list of every move given a location array
        public List<sbyte[]> GenerateMove(sbyte[] game, sbyte[] location)
        {
            List<sbyte[]> moveList = new List<sbyte[]>();
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
                    if(location[j] != -1)
                    {
                        sbyte[] move = new sbyte[3];
                        move[0] = (sbyte)(j>>1);
                        move[1] = location[j];
                        move[2] = i;
                        moveList.Add(move);
                    }
                    j+=1;
                }
            }
            return moveList;
        }
        //This function take a game and return every move in a position.
        public List<sbyte[]> GenerateEveryMove(sbyte[] game) //Cost of 0
        {
            sbyte[] location = WhereEveryPiece(game);
            return GenerateMove(game, location);
        }
    }
}