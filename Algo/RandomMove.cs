//This is the classic random algorithm.
//There is no excuses to lose to him.

//Weakness: Has no clue what is going on.

namespace TicTacToe
{
    public class RandomMove : Engine
    {
        Random generator;
        public RandomMove()
        {
            generator = new Random();
        }
        public RandomMove(Random Generator)
        {
            generator = Generator;
        }
        public override string Name() => "Random";
        public override RandomMove Duplicate() => new RandomMove(generator);

        public override sbyte[] GiveMove(sbyte[] game)
        {
            sbyte[] location = WherePlayablePiece(game);
            List<sbyte[]> moveList = GenerateMove(game, location);
            return moveList[generator.Next(0,moveList.Count)];
            //return GenerateEveryMove(game)[generator.Next(0,moveList.Count)];
        }
    }
}