﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombSpawner : MonoBehaviour
{

    // TODO: UI stuff doesnt belong in a bomb spawner class
    public Image originalBombIcon;
    private List<Image> bombIcons = new List<Image>();

    public GameObject particles;

    private int bombsLeft;

    public void Start()
    {
        BasePlayerVariables vars = this.gameObject.GetComponentInParent<BasePlayerVariables>();
        bombsLeft = vars.bombsLeft;
        // TODO: fix this bs
        Vector3 originalPosition = originalBombIcon.transform.position;
        bombIcons.Add(originalBombIcon);

        float length = originalBombIcon.transform.localScale.x;

        float horizontalPadding = originalBombIcon.rectTransform.rect.width + 2;

        if (vars.playerId == 1)
        {
            horizontalPadding = -horizontalPadding;
        }

        for (int i = 1; i < bombsLeft; i++)
        {
            Vector3 newIconPosition = new Vector3(
                originalPosition.x + (horizontalPadding * i),
                originalPosition.y,
                originalPosition.z);

            Image newIcon = Instantiate(originalBombIcon, newIconPosition, Quaternion.identity);
            Canvas canvas = FindObjectOfType<Canvas>();
            newIcon.transform.SetParent(canvas.transform);
            bombIcons.Add(newIcon);
        }
    }

    public void Spawn(int bombsLeft)
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        Destroy(bombIcons[bombsLeft - 1]);

        GameObject particleGameObject = Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
        particleGameObject.GetComponent<ParticleSystem>().Play();
    }
}
