using UnityEngine;

public class BulletDespawner : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.tag == "Bullet")
        {
            Destroy(other.gameObject);
        }
    }
}
