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

	public void Start() {
		playerTransform = GetComponentInParent<Transform>();
		vars = GetComponentInParent<BasePlayerVariables>();
		playerNumber = vars.playerNumberInt;
	}

    [Command]
	public void CmdSpawn() {
		if (Time.time > nextFireTime) {
			nextFireTime = Time.time + fireRate;
            //TODO: shot direction uses server script all the time
            Vector2 spawnDirection = vars.playerVector * movementSpeed;

			GameObject spawnedBulletObject = Instantiate(
				bulletPrefab,
				playerTransform.position,
				Quaternion.identity);

            spawnedBulletObject.GetComponent<BaseBulletVariables>().playerNumberInt = vars.playerNumberInt;

			BaseBulletVariables bulletsVars = spawnedBulletObject
				.GetComponent(typeof(BaseBulletVariables)) as BaseBulletVariables;
			bulletsVars.playerNumberInt = playerNumber;

            Rigidbody2D bulletBody = spawnedBulletObject
				.GetComponent<Rigidbody2D>();
			bulletBody.velocity = transform.TransformDirection(spawnDirection);

            NetworkServer.Spawn(spawnedBulletObject);
        }
    }
}
