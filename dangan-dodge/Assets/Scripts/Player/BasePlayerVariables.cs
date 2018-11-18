using UnityEngine;
using UnityEngine.Networking;

public class BasePlayerVariables : NetworkBehaviour {

    //TODO: maybe refactor all player variables into this?

    //TODO: remove for network version
    public string playerNumberString = "One";
	public int playerNumberInt;

    //Vector representing which way the player is facing
    //Needed because only the sprite is flipped
    //Rotating player makes controls weird
    public Vector2 playerVector;
}
