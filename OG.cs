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
        public override sbyte[] GiveMove(sbyte[] position)
        {
            sbyte[] move = new sbyte[3];
            return move;
        }
        public void test()
        {
            sbyte[] a = new sbyte[12];
            a[9] = 42;
            a[10] = 42;
            ShowPosition(a);
        }
    }
}