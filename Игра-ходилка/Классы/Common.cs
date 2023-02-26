namespace Игра_ходилка.Классы
{
    public static class Common
    {
        public static class Strings
        {
            public static class Errors
            {
                public static string negativeArgument = "Аргумент не может быть меньше нуля!";
                public static string negativeArgumentFormated = "Аргумент '{0}' не может быть меньше нуля!";
                public static string leftBiggerRight = "Аргумент слева больше аргумента справа!";
                public static string leftBiggerRightFormated = "Аргумент '{0}' больше аргумента '{1}'!";
                public static string noVertexToMove = "Невохможно переместить фишку игрока!";
            }
            public static class Messages
            {
                public static string gameStarted = "Игра началась";
                public static string diceRollFormated = "Бросок кубика! Выпало {0}";
                public static string currentPlayerMoveFormated = "Ход игрока '{0}'";
                public static string playerWonFormatted = "Игрок '{0}' победил!";
                public static string gameNotStarted = "Игра не запущена!";
                public static string playerReachFinish = "Игрок достиг финишной клетки!";
            }
        }
    }
}
