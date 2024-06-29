namespace TicTacToe
{
    public abstract class Engine
    {
        public abstract string Name();
        public abstract int[] GiveMove(int[] position);
    }
}