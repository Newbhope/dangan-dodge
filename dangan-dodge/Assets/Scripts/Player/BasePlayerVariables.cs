using UnityEngine;
using UnityEngine.Networking;

public class BasePlayerVariables : NetworkBehaviour {

    //TODO: maybe refactor all player variables into this?

    //TODO: remove for network version
    public string playerNumberString = "One";

    [SyncVar]
	public int playerNumberInt;
    //Vector representing which way the player is facing
    //Needed because only the sprite is flipped
    //Rotating player makes controls weird
    [SyncVar]
    public Vector2 playerVector;
    [SyncVar]
    public bool isFlipped;

    void Start() {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.flipX = isFlipped;

        if (playerNumberInt == 2) {
            sprite.material.color = Color.blue;
        }
    }
}
