Given a position, can you return a move? How about a good one?\
Of course you can brute force it try everything, but we don't have all day, you will be given 100ms

To participate you will need to follow the format of the abstract class Engine and respect the convention of the move input.

TODO operator counter
This will allow you to 'fit' into category. The category are base on the powers of 2.

There is an example to cut the operator amount. The branch Julia original implementation shows the OG Engine with a "naive" encoding. The current version has far less operator.

There will some build in function that you can simply call. These function aren't free, they will have a fix cost that will be added to your final operation counter.

If times allows it I will add my original engine from Julia (OG) and maybe some inefficiant joke engine.

Each player has two small pieces, two mediums, and two larges. The goal remains the same: make three in a row.
When it is their turn to play, a player can place a piece or move an available piece.
The piece can be placed on a square if it is bigger than what is already on it. Every piece can be placed on an empty square.
A player can't move a piece that is under another one.
If a player moves a piece and reveals a tictactoe for the opponent, he automatically loses. Even if he intended to place his piece back to block it.
This rule makes it very important to keep track of what is under what.
If a player is out of moves, it loses.
The added rules make the game a lot more difficult and interesting.


A lot of the function will give you as an input sbyte[] game. This array will store all of the information relevent to the current position of the game.\
game[0], ..., game[8] will store a number revealing what is on the square. The number inside it is the sum of every piece in the square (See piece reference).\
For example, game[5] = 18, implie that on the 5 square there is oX

A move is a sbyte[3].\
The first slot indicates the pieces size {0,1,2} where 0 is the smallest piece.\
The second slot indicates the location the piece was previously on. {0,...,8} for a piece on the board and 9 if its comes from the deck.\
The third slot indicates the location of where the piece will be place. {0,..,8}
