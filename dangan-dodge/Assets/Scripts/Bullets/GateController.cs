using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject otherPlayer;
    public int bulletsToFire;
    public float fireRate;

    private int bulletsFired = 0;
    private BaseBulletSpawner baseBulletSpawner;

    private float nextFireTime;

    public void Start() {
        baseBulletSpawner = GetComponent<BaseBulletSpawner>();
    }

    void Update() {
        if (otherPlayer != null) {
            // Face towards enemy
            Vector3 targetPosition = otherPlayer.transform.position;
            Vector3 direction = (targetPosition - transform.position).normalized;
            //Debug.Log("direction vector: " + direction);
            //transform.localRotation.Set(direction.x, direction.y, direction.z, 0);
            //transform.LookAt(targetPosition, Vector3.forward);
            //transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            //transform.rotation = new Quaternion(0, 0, Quaternion.LookRotation(direction, Vector3.up).x, 0);
            //transform.eulerAngles = new Vector3(direction.x, direction.y, direction.z);
            //()

            transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
            // study trig lol
            // get arc tangent or something
        }


        if (transform.localPosition.x <= 2)
        {
            transform.Translate(0.005f, 0, 0);
        }

        if (transform.localPosition.x >= 2)
        {
            // shoot
            if (bulletsFired < bulletsToFire && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                baseBulletSpawner.Spawn();
                bulletsFired++;
            }
        }

        if (bulletsFired == bulletsToFire)
        {
            transform.Translate(-0.025f, 0, 0);
        }
    }


}
