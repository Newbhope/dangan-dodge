using UnityEngine;

/**
 * Class used to make base "Bullets". 
 * Will use its own fire rate separate from other spawner types
 **/
public class BaseBulletSpawner : MonoBehaviour
{
    public float fireRate;
    public int movementSpeed;
    public int additionalBulletAngleSpread;
    public int extraBulletsPerSide;

    public GameObject bulletPrefab;
    public GameObject superBulletPrefab;

    private Transform objectTransform;
    private BasePlayerVariables vars;
    private int playerNumber;
    private SpriteRenderer spriteRenderer;

    private float nextFireTime;

    public void Start()
    {
        objectTransform = GetComponentInParent<Transform>();
        vars = GetComponentInParent<BasePlayerVariables>();
        playerNumber = vars.playerId;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Spawn()
    {
        if (Time.time > nextFireTime && objectTransform != null)
        {
            nextFireTime = Time.time + fireRate;

            SpawnBullet(additionalBulletAngleSpread);
            for (int i = 1; i <= extraBulletsPerSide; i++)
            {
                SpawnBullet(additionalBulletAngleSpread + i * 2);
                SpawnBullet(additionalBulletAngleSpread - i * 2);
            }
        }
    }

    private void SpawnBullet(int angle)
    {
        Vector2 spawnDirection = new Vector2(1, 0) * movementSpeed;
        spawnDirection.y += angle;

        GameObject spawnedBulletObject = Instantiate(
                bulletPrefab,
                objectTransform.position,
                Quaternion.identity);

        spawnedBulletObject.GetComponent<BaseBulletVariables>().ownerPlayerId = playerNumber;
        spawnedBulletObject.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        spawnedBulletObject.GetComponent<Rigidbody2D>().velocity = base.transform.TransformDirection(spawnDirection);
    }

    internal void SpawnUltra() {
        for (int bulletNumber = 1; bulletNumber <= 100; bulletNumber++) {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
        }
        GameObject spawnedBulletObject = Instantiate(
                superBulletPrefab,
                objectTransform.position,
                Quaternion.identity);
    }

    internal void SpawnSuperBullet() {
        GameObject superBullet = Instantiate(
            superBulletPrefab,
            objectTransform.position,
            Quaternion.identity);

        superBullet.GetComponent<BaseBulletVariables>().ownerPlayerId = playerNumber;
        superBullet.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        superBullet.GetComponent<Rigidbody2D>().velocity = base.transform.TransformDirection(new Vector2(1, 0) * movementSpeed / 2);
    }
}
