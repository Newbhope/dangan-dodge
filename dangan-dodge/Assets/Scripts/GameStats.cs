using System.Collections.Generic;

public static class GameStats {
    public static Dictionary<int, int> playerScores = new Dictionary<int, int>();
    //Maybe move to player stats? Why do I even need a dictionary
    //Static dictionary is for persisting across scenes
    public static Dictionary<int, int> playerBombs = new Dictionary<int, int>();

    //TODO: may need to reset back to false when a game is restarted
    public static bool localGameStarted = false;
}

public static class PlayerStats {
    public static int playerNum = 0;
}