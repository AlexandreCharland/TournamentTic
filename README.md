# Introduction
Given a position, can you return a move? How about a good one?\
Of course you can brute force it try everything, but we don't have all day, you will be given 100ms

To participate you will need to follow the format of the abstract class Engine and respect the convention of the move input.

# Counter mechanics
TODO operator counter\
This will allow you to 'fit' into category. The category are base on the powers of 2.

There is an example to cut the operator amount. The branch Julia original implementation shows the OG Engine with a "naive" encoding. The current version has far less operator.

There will some build in function that you can simply call. These function aren't free, they will have a fix cost that will be added to your final operation counter.

# How the game works
Each player has two small pieces, two mediums, and two larges. The goal remains the same: make three in a row.
When it is their turn to play, a player can place a piece or move an available piece.
The piece can be placed on a square if it is bigger than what is already on it. Every piece can be placed on an empty square.
A player can't move a piece that is under another one.
If a player moves a piece and reveals a tictactoe for the opponent, he automatically loses. Even if he intended to place his piece back to block it.
This rule makes it very important to keep track of what is under what.
If a player is out of moves, it loses.\
If a position repeat once, the game is declare a draw.\
The added rules make the game a lot more difficult and interesting.
