using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombSpawner : MonoBehaviour {

    public int horizontalPadding;
    public Image originalBombIcon;
    public GameObject particles;

    public void Spawn(int bombsLeft) {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) {
            Destroy(bullet);
        }
        GameObject particleGameObject = Instantiate(particles, gameObject.transform);
        particleGameObject.GetComponent<ParticleSystem>().Play();
    }
}
