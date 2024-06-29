Given a position, can you return a move? How about a good one?\
Of course you can brute force it try everything, but we don't have all day, you will be given 100ms\

To participate you will need to follow the format of the abstract class Engine and respect the convention of the move input.\

TODO operator counter
This will allow you to 'fit' into category. The category are base on the powers of 2.

There will some build in function that you can simply call. These function aren't free, they will have a fix cost that will be added to your final operation counter.

If times allows it I will add my original engine from Julia (OG) and maybe some inefficiant joke engine.

TODO explain the rule of the game

A move is a int[3].\
The first slot indicates the pieces size {0,1,2} where 0 is the smallest piece.\
The second slot indicates the location the piece was previously on. {0,...,8} for a piece on the board and -1 if its comes from the deck.\
The third slot indicates the location of where the piece will be place. {0,..,8}