using UnityEngine;

public class SuperBullet : MonoBehaviour
{

    public int movementSpeed;

    private BaseBulletVariables bulletVars;

    private void Start()
    {
        bulletVars = GetComponent<BaseBulletVariables>();
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = rigidBody.velocity * movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This ish broke

        /*
        BaseBulletVariables collidingBulletVars = collision
            .gameObject
            .GetComponent(typeof(BaseBulletVariables)) as BaseBulletVariables;

        if(collidingBulletVars != null && collidingBulletVars.ownerPlayerId != bulletVars.ownerPlayerId) {
            Destroy(collision.gameObject);
        }
        */
    }
}
