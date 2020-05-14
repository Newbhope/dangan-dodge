using UnityEngine;

public class SuperBullet : MonoBehaviour {

    private BaseBulletVariables bulletVars;

    private void Start() {
        bulletVars = GetComponent<BaseBulletVariables>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        BaseBulletVariables collidingBulletVars = collision
            .gameObject
            .GetComponent(typeof(BaseBulletVariables)) as BaseBulletVariables;

        if(collidingBulletVars != null && collidingBulletVars.ownerPlayerId != bulletVars.ownerPlayerId) {
            Destroy(collision.gameObject);
        }
    }
}
