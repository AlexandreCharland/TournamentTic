//This is the abstract class you must implement

namespace TicTacToe
{
    public abstract class Engine : ToolBox
    {
        //When your engine is create pls don't start heavy calculation.
        //Also try not to take to much memory space.
        //I have no decided yet if transposition table are accepted or not.

        public abstract string Name();

        //This function ask you to duplicate yourself.
        public abstract Engine Duplicate();
        //If your algorithm has a random component, see RandomMove implementation.
        //If your algorithm is deterministic, see OG implementation.

        //This is where your creativity must shine.
        public abstract sbyte[] GiveMove(sbyte[] game);
        //You can't call another engine, run it and then return their move.
    }
}