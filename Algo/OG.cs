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
        public override sbyte[] GiveMove(sbyte[] game)
        {
            sbyte[] move = new sbyte[3];
            return move;
        }
        public void test()
        {
            sbyte[] a = new sbyte[12];
            a[9]=42;a[10]=42;
            sbyte[] move = new sbyte[3];
            move[1]=-1;
            sbyte[] b = MakeMove(a, move);
            ShowPosition(b);
            //System.Console.WriteLine(SomethingHasChange(a,0));
        }
    }
}