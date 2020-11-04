using Rewired;
using UnityEngine;

/**
 * Class that calls into each bullet spawner with the fire key
 * Each bullet spawner will have its own configurable fire rate
 * 
 * Fire rate is the period in seconds between the next time that type of bullet fires
 * 
 * Also calls into bomb spawner with bomb key.
 **/
public class PlayerCombatController : MonoBehaviour
{

    public int superOneCost;
    public int ultraCost;

    public GameObject gatesOfBabylon;

    private Player player;
    private BasePlayerVariables vars;

    private BaseBulletSpawner baseBulletSpawner;

    private BombSpawner bombSpawner;
    private int bombsLeft;

    void Awake() {
        vars = GetComponentInParent<BasePlayerVariables>();
        player = ReInput.players.GetPlayer(vars.playerId);

        baseBulletSpawner = GetComponent<BaseBulletSpawner>();

        bombSpawner = GetComponent<BombSpawner>();
        bombsLeft = vars.bombsLeft;
    }

    // Player inputs should always be in Update() since FixedUpdate() doesn't poll fast enough
    void Update()
    {
        // To prevent actions while paused
        if (Time.timeScale > 0.1)
        {
            if (player.GetButton("Fire"))
            {
                baseBulletSpawner.Spawn();
            }

            if (player.GetButtonDown("Bomb") && bombsLeft > 0)
            {
                bombSpawner.Spawn(bombsLeft);
                bombsLeft--;
                Debug.Log(bombsLeft);
            }

            // Metered moves
            if (player.GetButtonLongPressUp("Super1") && vars.Energy >= ultraCost) {
                GatesOfBabylon gateController = gatesOfBabylon.GetComponent<GatesOfBabylon>();
                gateController.ActivateGates();
                vars.Energy -= ultraCost;
            } else if (player.GetButtonUp("Super1") && vars.Energy >= superOneCost) {
                baseBulletSpawner.SpawnSuperBullet();
                vars.Energy -= superOneCost;
                Debug.Log("short hold");
            }

        }
    }
}
