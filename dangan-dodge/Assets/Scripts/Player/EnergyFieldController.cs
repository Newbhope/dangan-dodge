using UnityEngine;

public class EnergyFieldController : MonoBehaviour {

    public int energyPerFrame;

    private GameController gameController;
    private BasePlayerVariables vars;

    void Start () {
		vars = GetComponentInParent<BasePlayerVariables>();
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        BaseBulletVariables bulletVars = collision.GetComponent<BaseBulletVariables>();
        if (bulletVars != null && bulletVars.ownerPlayerId != vars.playerId) {
            vars.Energy += energyPerFrame;
        }
    }

}
