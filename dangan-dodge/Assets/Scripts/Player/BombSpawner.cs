using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombSpawner : MonoBehaviour {

    public int horizontalPadding;
    public Image originalBombIcon;

    private List<Image> bombIcons = new List<Image>();
    private int bombsLeft;

    public void Start() {
        BasePlayerVariables vars = this.gameObject.GetComponent<BasePlayerVariables>();
        bombsLeft = vars.bombsLeft;
        Vector3 originalPosition = originalBombIcon.transform.position;
        bombIcons.Add(originalBombIcon);

        for (int i = 1; i <= bombsLeft; i++) {
            Vector3 newIconPosition = new Vector3(
                originalPosition.x + horizontalPadding * i,
                originalPosition.y,
                originalPosition.z);

            Image newIcon = Instantiate(originalBombIcon, newIconPosition, Quaternion.identity);
            bombIcons.Add(newIcon);
        }
    }

    public void Spawn(int bombsLeft) {

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) {
            Destroy(bullet);
        }

        Destroy(originalBombIcon);


        //TODO: Some visual effect
    }
}
