namespace TicTacToe
{
    public class Arbiter : ToolBox
    {
        private sbyte[] game;
        public Arbiter()
        {
            game = new sbyte[12];
            game[9] = 42;
            game[10] = 42;
        }
        public Arbiter(sbyte[] Game)
        {
            game = Game;
        }
        public sbyte[] getGame()
        {
            return (sbyte[])(game.Clone());
        }
        public void UpdateGame(sbyte[] move)
        {
            game = MakeMove(game, move);
        }
        public bool SomeoneWon()
        {
            return SomeoneWon(game);
        }
        private bool MoveInList(List<sbyte[]> listMove, sbyte[] theMove)
        {
            bool containIt = false;
            foreach (sbyte[] move in listMove)
            {
                containIt = containIt || Enumerable.SequenceEqual(move, theMove);
            }
            return containIt;
        }
        public bool IsMoveLegal(sbyte[] move)
        {
            return MoveInList(GenerateEveryMove(game), move);
        }
        public bool IsPinPiece(sbyte[] move)
        {
            sbyte[] location = WherePlayablePiece(game);
            return MoveInList(GenerateMove(game, location), move);
        }
        private sbyte Decode(sbyte val)
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
        public void ShowPosition()
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
    }
}