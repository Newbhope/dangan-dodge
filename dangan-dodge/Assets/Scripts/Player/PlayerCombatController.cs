using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Class that calls into each bullet spawner with the fire key
 * Each bullet spawner will have its own configurable fire rate
 * 
 * Fire rate is the period in seconds between the next time that type of bullet fires
 * 
 * Also calls into bomb spawner with bomb key.
 **/
public class PlayerCombatController : MonoBehaviour {

    private string fireButtonName;
    private BaseBulletSpawner baseBulletSpawner;

    private string bombButtonName;
    private BombSpawner bombSpawner;
    private int bombsLeft;

    void Start() {
        BasePlayerVariables vars = this.gameObject.GetComponent<BasePlayerVariables>();

        fireButtonName = vars.playerNumberString + "Fire";
        baseBulletSpawner = GetComponent<BaseBulletSpawner>();

        bombButtonName = vars.playerNumberString + "Bomb";
        bombsLeft = vars.bombsLeft;
        bombSpawner = GetComponent<BombSpawner>();
    }

    public void OnFire(InputValue value) {
        Debug.Log("yay");
        baseBulletSpawner.Spawn();

    }

    private void HandleFire(InputAction.CallbackContext obj) {
        baseBulletSpawner.Spawn();
    }

    void Update() {
        /*
        if (Time.timeScale > 0.1) {
            if (Input.GetButton(fireButtonName)) {
                baseBulletSpawner.Spawn();
            }

            if (Input.GetButtonDown(bombButtonName) && bombsLeft > 0) {
                bombSpawner.Spawn(bombsLeft);
                bombsLeft--;
                Debug.Log(bombsLeft);
            }
        }
        */
    }
}
