using UnityEngine;

public class ParticleController : MonoBehaviour
{

    // Currently attached to ExplosionParticles prefab

    private ParticleSystem pSystem;

    // Use this for initialization
    void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pSystem.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
