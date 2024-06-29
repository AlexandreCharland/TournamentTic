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
        public override sbyte[] GiveMove(sbyte[] game)
        {
            List<sbyte[]> moveList = GenerateEveryMove(game);
            return moveList[generator.Next(moveList.Count)];
        }
    }
}