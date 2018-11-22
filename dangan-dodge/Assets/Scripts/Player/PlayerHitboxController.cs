using UnityEngine;

public class PlayerHitboxController : MonoBehaviour {
    //TOOD: kinda weird that this is here

    private BasePlayerVariables vars;

    void Start () {
		vars = GetComponentInParent<BasePlayerVariables>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        vars.CheckDamage(other);
    }
}
