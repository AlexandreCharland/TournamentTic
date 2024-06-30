//This is the classic random algorithm.
//There is no excuses to lose to him.
namespace TicTacToe
{
    public class RandomMove : Engine
    {
        Random generator;
        public RandomMove()
        {
            generator = new Random();
        }
        public override string Name()
        {
            return "Random";
        }
        public override RandomMove Duplicate()
        {
            return new RandomMove();
        }
        public override sbyte[] GiveMove(sbyte[] game)
        {
            sbyte[] location = WherePlayablePiece(game);
            List<sbyte[]> moveList = GenerateMove(game, location);
            return moveList[generator.Next(0,moveList.Count)];
        }
    }
}