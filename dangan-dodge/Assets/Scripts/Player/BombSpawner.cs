using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombSpawner : MonoBehaviour {

    public int horizontalPadding;
    public Image originalBombIcon;
    public GameObject particles;

    private List<Image> bombIcons = new List<Image>();
    private int bombsLeft;

    public void Start() {
        BasePlayerVariables vars = this.gameObject.GetComponent<BasePlayerVariables>();
        bombsLeft = vars.bombsLeft;
        Vector3 originalPosition = originalBombIcon.transform.position;
        bombIcons.Add(originalBombIcon);

        for (int i = 1; i < bombsLeft; i++) {
            Vector3 newIconPosition = new Vector3(
                originalPosition.x + (horizontalPadding * i * vars.playerVector.x),
                originalPosition.y,
                originalPosition.z);

            Image newIcon = Instantiate(originalBombIcon, newIconPosition, Quaternion.identity);
            Canvas canvas = FindObjectOfType<Canvas>();
            newIcon.transform.SetParent(canvas.transform);
            bombIcons.Add(newIcon);
        }
    }

    public void Spawn(int bombsLeft) {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) {
            Destroy(bullet);
        }
        Destroy(bombIcons[bombsLeft - 1]);

        GameObject particleGameObject = Instantiate(particles, gameObject.transform);
        particleGameObject.GetComponent<ParticleSystem>().Play();
    }
}
