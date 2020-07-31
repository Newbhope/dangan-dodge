using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject player;
    public GameObject otherPlayer;
    public GameObject bulletPrefab;
    public float fireRate;
    public int movementSpeed;


    private float nextFireTime;


    void Start()
    {
        
    }

    void Update()
    {
        Vector3 targetPosition = otherPlayer.transform.position;
        Vector3 direction = (targetPosition - transform.position).normalized;
        //Debug.Log("direction vector: " + direction);
        //transform.localRotation.Set(direction.x, direction.y, direction.z, 0);
        Debug.Log(Quaternion.LookRotation(direction));
        //transform.LookAt(targetPosition, Vector3.forward);
        Debug.Log(Quaternion.LookRotation(direction, Vector3.up));
        //transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        //transform.rotation = new Quaternion(0, 0, Quaternion.LookRotation(direction, Vector3.up).x, 0);
        //transform.eulerAngles = new Vector3(direction.x, direction.y, direction.z);
        Debug.Log(transform.rotation);
        //()

        transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
        // study trig lol
        // get arc tangent or something

        if (Time.time > nextFireTime) {
            nextFireTime = Time.time + fireRate;

            Vector2 spawnDirection = new Vector2(1, 0) * movementSpeed;

            GameObject spawnedBulletObject = Instantiate(
                    bulletPrefab,
                    playerTransform.position,
                    Quaternion.identity);

            spawnedBulletObject.GetComponent<BaseBulletVariables>().ownerPlayerId = playerNumber;
            spawnedBulletObject.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
            spawnedBulletObject.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(spawnDirection);

        }

    }


}
