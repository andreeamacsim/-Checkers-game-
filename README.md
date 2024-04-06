# -Checkers-game-

The game is a checkers game implemented in C# with a WPF user interface, following the MVVM design pattern. It features two sets of pieces: white and red. The game board consists of 8 rows and 8 columns.
Initial Setup:The player with the red pieces makes the first move, followed by alternating moves between players.
Types of Moves: Simple Move: A piece moves one square diagonally forward. If a piece reaches the opponent's back row, it becomes a "king" and gains the ability to move both forward and backward diagonally. Jump Over Opponent's Piece: If a player's piece has an opponent's piece diagonally adjacent to it and an empty square immediately beyond, the player can jump over the opponent's piece, capturing it.Multiple Jumps: If a player captures an opponent's piece and another opponent's piece is immediately adjacent and can be captured, the player can continue jumping in a chain until no further captures are possible.Players can only perform multiple jumps if the option is enabled at the beginning of the game.
End of Game:The game ends when one player has no more pieces on the board. The opponent is declared the winner.
