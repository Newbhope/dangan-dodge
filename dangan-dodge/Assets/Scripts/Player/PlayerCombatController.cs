using UnityEngine;
using Rewired;

/**
 * Class that calls into each bullet spawner with the fire key
 * Each bullet spawner will have its own configurable fire rate
 * 
 * Fire rate is the period in seconds between the next time that type of bullet fires
 * 
 * Also calls into bomb spawner with bomb key.
 **/
public class PlayerCombatController : MonoBehaviour {

    private Player player;

    private BaseBulletSpawner baseBulletSpawner;

    private BombSpawner bombSpawner;
    private int bombsLeft;

    void Awake() {
        BasePlayerVariables vars = this.gameObject.GetComponent<BasePlayerVariables>();
        player = ReInput.players.GetPlayer(vars.playerId);

        baseBulletSpawner = GetComponent<BaseBulletSpawner>();

        bombSpawner = GetComponent<BombSpawner>();
        bombsLeft = vars.bombsLeft;
    }

    void Update() {
        // To prevent actions while paused
        if (Time.timeScale > 0.1) {
            if (player.GetButton("Fire")) {
                baseBulletSpawner.Spawn();
            }

            if (player.GetButtonDown("Bomb") && bombsLeft > 0) {
                bombSpawner.Spawn(bombsLeft);
                bombsLeft--;
                Debug.Log(bombsLeft);
            }
        }
    }
}
