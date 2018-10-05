using UnityEngine;

/**
 * Class used to make base "Bullets". 
 * Will use its own fire rate separate from other spawner types
 **/
public class BaseBulletSpawner : MonoBehaviour {

	public float fireRate;
	public int movementSpeed;

	private Rigidbody2D bullet;
	private float nextFireTime;

	private Transform playerTransform;
	private BasePlayerVariables vars;
	private int playerNumber;

	private GameObject bulletObject;

	public void Start() {
		bulletObject = (GameObject) Resources.Load("BaseBullet", typeof(GameObject));
		playerTransform = GetComponentInParent<Transform>();
		vars = GetComponentInParent<BasePlayerVariables>();
		playerNumber = vars.playerNumberInt;
	}

	public void Spawn() {
		if (Time.time > nextFireTime) {
			nextFireTime = Time.time + fireRate;
            Vector2 spawnDirection = vars.playerVector * movementSpeed;

			GameObject spawnedBulletObject = Instantiate(
				bulletObject,
				playerTransform.position,
				Quaternion.identity) as GameObject;

			BaseBulletVariables bulletsVars = spawnedBulletObject
				.GetComponent(typeof(BaseBulletVariables)) as BaseBulletVariables;
			bulletsVars.playerNumberInt = playerNumber;

			Rigidbody2D bulletBody = spawnedBulletObject
				.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
			bulletBody.velocity = transform.TransformDirection(spawnDirection);
		}
	}

}
