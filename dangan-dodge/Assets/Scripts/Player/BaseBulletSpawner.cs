using System;
using UnityEngine;
using UnityEngine.Networking;

/**
 * Class used to make base "Bullets". 
 * Will use its own fire rate separate from other spawner types
 **/
public class BaseBulletSpawner : NetworkBehaviour {

	public float fireRate;
	public int movementSpeed;
    public GameObject bulletPrefab;

	private float nextFireTime;

	private Transform playerTransform;
	private BasePlayerVariables vars;
	private int playerNumber;
    private SpriteRenderer spriteRenderer;

    private int angle = 0;
    private bool shouldMovePatternDown = true;

	public void Start() {
		playerTransform = GetComponentInParent<Transform>();
		vars = GetComponentInParent<BasePlayerVariables>();
		playerNumber = vars.playerNumberInt;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    [Command]
	public void CmdSpawn() {
		if (Time.time > nextFireTime) {
			nextFireTime = Time.time + fireRate;
            SpawnBullet(angle);
            for (int i = 1; i < 3; i++) {
                SpawnBullet(angle + i * 2);
                SpawnBullet(angle - i * 2);
            }

            if (shouldMovePatternDown) {
                angle -= 1;
            } else {
                angle += 1;
            }

            if (angle >= 10) {
                shouldMovePatternDown = true;
            }
            if (angle <= -10) {
                shouldMovePatternDown = false;
            }
        }
    }

    private void SpawnBullet(int angle) {
        Vector2 spawnDirection = vars.playerVector * movementSpeed;
        spawnDirection.y += angle;

        GameObject spawnedBulletObject = Instantiate(
            bulletPrefab,
            playerTransform.position,
            Quaternion.identity);

        spawnedBulletObject.GetComponent<BaseBulletVariables>().playerNumberInt = playerNumber;

        spawnedBulletObject.GetComponent<SpriteRenderer>().color = spriteRenderer.color;

        Rigidbody2D bulletBody = spawnedBulletObject
            .GetComponent<Rigidbody2D>();
        bulletBody.velocity = transform.TransformDirection(spawnDirection);

        NetworkServer.Spawn(spawnedBulletObject);
    }
}
