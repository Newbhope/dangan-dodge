﻿using UnityEngine;
using UnityEngine.Networking;

/**
 * Class that calls into each bullet spawner with the fire key
 * Each bullet spawner will have its own configurable fire rate
 * 
 * Fire rate is the period in seconds between the next time that type of bullet fires
 * 
 * Also calls into bomb spawner with bomb key.
 **/
public class PlayerCombatController : NetworkBehaviour {

    private string fireButtonName;
    private BaseBulletSpawner baseBulletSpawner;

    private string bombButtonName;
    private BombSpawner bombSpawner;
    private int bombsLeft;
    private BasePlayerVariables vars;

    private ArenaController arenaController;


    void Start() {
        vars = this.gameObject.GetComponent<BasePlayerVariables>();

        fireButtonName = vars.playerNumberString + "Fire";
        baseBulletSpawner = GetComponent<BaseBulletSpawner>();

        bombButtonName = vars.playerNumberString + "Bomb";
        bombSpawner = GetComponent<BombSpawner>();

        arenaController = FindObjectOfType<ArenaController>();
    }

    void Update() {
        if (isLocalPlayer) {
            //TODO better way to do these calls?
            if (Time.timeScale > 0.1) {
                if (Input.GetButton(fireButtonName)) {
                    baseBulletSpawner.CmdSpawn();
                }

                var bombsLeft = GameStats.playerBombs[vars.playerNumberInt];
                if (Input.GetButtonDown(bombButtonName) && bombsLeft > 0) {
                    bombSpawner.Spawn(bombsLeft);
                    bombsLeft--;
                    GameStats.playerBombs[vars.playerNumberInt] = bombsLeft;

                    arenaController.UpdateBombUi();

                    Debug.Log("Bombs left: " + bombsLeft);
                }
            }
        }
    }
}