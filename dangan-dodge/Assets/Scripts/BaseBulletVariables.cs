using UnityEngine;

public class BaseBulletVariables : MonoBehaviour {
	public int playerNumberInt;

    private void OnCollisionEnter2D(Collision2D collision) {
        //TODO: evaluate
        Destroy(gameObject);
    }
}
