using System;
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

        if (readyToFire == true)
        {
            LerpMove();
        }

        //RawMove();
    }

    private void LerpMove()
    {
        // Move to end position
        if (Math.Abs(transform.position.x - end.position.x) > 0.1)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            Vector3 lerpPosition = Vector3.Lerp(start.position, end.position, fractionOfJourney);
            Vector3 newGatePosition = new Vector2(lerpPosition.x, transform.position.y);
            transform.position = newGatePosition;
        }

        // At end position
        if (Math.Abs(transform.position.x - end.position.x) <= 0.1)
        {
            if (bulletsFired < bulletsToFire && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                baseBulletSpawner.Spawn();
                bulletsFired++;
                middleTime = Time.time;
            }
        }

        // Move to start position
        if (bulletsFired == bulletsToFire)
        {
            float distCovered = (Time.time - middleTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            Vector3 lerpPosition = Vector3.Lerp(end.position, start.position, fractionOfJourney);
            Vector2 newGatePosition = new Vector2(lerpPosition.x, transform.position.y);
            transform.position = newGatePosition;

            // Reset
            if (Math.Abs(transform.position.x - start.position.x) < 0.1)
            {
                bulletsFired = 0;
                readyToFire = false;
            }
        }

    }

    public void ActivateGate()
    {
        readyToFire = true;
        bulletsFired = 0;
        startTime = Time.time;
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
