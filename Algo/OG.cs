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
            sbyte[] move = new sbyte[3];
            return move;
        }
        public void test()
        {
            
        }
    }
}