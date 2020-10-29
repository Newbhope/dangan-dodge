using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject otherPlayer;
    public int bulletsToFire;
    public float fireRate;

    public Transform start;
    public Transform end;

    public float speed = 2.0f;

    private float startTime;
    private float middleTime;
    private float journeyLength;

    private int bulletsFired = 0;
    private BaseBulletSpawner baseBulletSpawner;

    private float nextFireTime;

    private bool readyToFire = false;
    private bool doneFiring = false;

    public void Start() {
        baseBulletSpawner = GetComponent<BaseBulletSpawner>();

        startTime = Time.time;
        journeyLength = Vector3.Distance(start.position, end.position);
    }

    void Update()
    {
        if (otherPlayer != null)
        {
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

        LerpMove();

        //RawMove();
    }

    private void LerpMove()
    {
        if (transform.position.x < end.position.x)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            Vector3 test = Vector3.Lerp(start.position, end.position, fractionOfJourney);
            Vector3 newPosition = new Vector2(test.x, transform.position.y);
            transform.position = newPosition;
        }

        if (transform.position.x >= end.position.x)
        {
            if (bulletsFired < bulletsToFire && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                baseBulletSpawner.Spawn();
                bulletsFired++;
                middleTime = Time.time;
            }
        }

        if (bulletsFired == bulletsToFire)
        {
            float distCovered = (Time.time - middleTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            Vector3 test = Vector3.Lerp(end.position, start.position, fractionOfJourney);
            Vector2 newPosition = new Vector2(test.x, transform.position.y);
            transform.position = newPosition;

            // TODO: reset gates
        }

    }

    private void RawMove()
    {
        if (transform.localPosition.x <= 2)
        {
            transform.Translate(0.025f, 0, 0);
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
            Debug.Log("BRUH");
            transform.Translate(-0.025f, 0, 0);
        }
    }
}
