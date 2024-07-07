namespace TicTacToe
{
    public abstract class Engine : ToolBox
    {
        //Function  you have to implement
        public abstract string Name();
        public abstract Engine Duplicate();
        public abstract sbyte[] GiveMove(sbyte[] game);
        //You can't call another engine, run it and then return their move
    }
}