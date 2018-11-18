using System.Collections.Generic;

public static class GameStats {
    public static Dictionary<int, int> playerScores = new Dictionary<int, int>();
    //Maybe move to player stats? Why do I even need a dictionary
    public static Dictionary<int, int> playerBombs = new Dictionary<int, int>();
}

public static class PlayerStats {
    public static int playerNum = 0;
}