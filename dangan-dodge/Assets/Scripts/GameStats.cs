using System.Collections.Generic;
using UnityEngine.Networking;

public static class GameStats {
    [SyncVar]
    public static Dictionary<int, int> playerScores = new Dictionary<int, int>();
    [SyncVar]
    public static Dictionary<int, int> playerBombs = new Dictionary<int, int>();
}
