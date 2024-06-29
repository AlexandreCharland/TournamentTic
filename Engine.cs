namespace TicTacToe
{
    public abstract class Engine
    {
        //Function  you have to implement
        public abstract string Name();
        public abstract sbyte[] GiveMove(sbyte[] position);

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
        //Todo SomethingHasChange

    }
}