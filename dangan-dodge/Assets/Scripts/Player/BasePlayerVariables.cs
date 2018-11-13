using UnityEngine;
using UnityEngine.Networking;

public class BasePlayerVariables : NetworkBehaviour {

    //TODO: maybe refactor all player variables into this?

    public string playerNumberString = "One";
	public int playerNumberInt;

    //Vector representing which way the player is facing
    //TODO: Why do i need this? Because only the sprite is flipped. Rotating player makes controls weird
    public Vector2 playerVector;

    private void Start() {
        if (isServer) {

        } else {

        }
    }

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();

    }

    /*
    private void OnPlayerConnected(NetworkIdentity player) {
        
    }
    */

    public override void OnStartClient() {
        Debug.Log("Player Num: " + _GLOBAL_CONSTANTS.currentPlayerNum);
        switch (_GLOBAL_CONSTANTS.currentPlayerNum) {
            case 0:
                gameObject.transform.position = new Vector3(-15, 0, 0);
                playerNumberInt = 1;
                playerVector = new Vector2(1, 0);
                break;
            case 1:
                var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                spriteRenderer.material.color = Color.blue;
                spriteRenderer.flipX = true;

                gameObject.transform.position = new Vector3(15, 0, 0);
                playerNumberInt = 2;
                playerVector = new Vector2(-1, 0);

                break;
            case 2:
                //TODO: spectator
                //TODO: reconnects
                break;
        }
        _GLOBAL_CONSTANTS.currentPlayerNum++;
    }
}
