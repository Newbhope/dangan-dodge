using UnityEngine;

public class EnergyFieldController : MonoBehaviour {

    private GameController gameController;
    private BasePlayerVariables vars;

    void Start () {
		vars = GetComponentInParent<BasePlayerVariables>();
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        BaseBulletVariables bulletVars = collision.GetComponent<BaseBulletVariables>();
        if (bulletVars != null && bulletVars.ownerPlayerId != vars.playerId) {
            vars.energy += 1;
            gameController.UpdateEnergyUi(vars.playerId, vars.energy);
        }
    }

}
