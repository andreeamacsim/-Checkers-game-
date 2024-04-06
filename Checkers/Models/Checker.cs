using Checkers.Services;

namespace Checkers.Models
{
    public static class Checker
    {
        public static PlayerType GetPlayerTypeFromChecker(CheckerTypes checkerType)
        {
            switch (checkerType)
            {
                case CheckerTypes.RedPawn:
                case CheckerTypes.RedKing:
                    return PlayerType.Red;

                case CheckerTypes.WhitePawn:
                case CheckerTypes.WhiteKing:

                    return PlayerType.White;

                default:
                    return PlayerType.None;

            }
        }
    }
}
