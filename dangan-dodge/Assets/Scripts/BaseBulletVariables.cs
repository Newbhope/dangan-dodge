using UnityEngine;
using UnityEngine.Networking;

public class BaseBulletVariables : NetworkBehaviour {
    [SyncVar]
	public int playerNumberInt;

    private void OnCollisionEnter2D(Collision2D collision) {
        //TODO: evaluate
        Destroy(gameObject);
    }
}
