using UnityEngine;
using UnityEngine.UI;

/**
 * Class that calls into each bullet spawner with the fire key
 * Each bullet spawner will have its own configurable fire rate
 * 
 * Fire rate is the period in seconds between the next time that type of bullet fires
 **/
public class PlayerBombController : MonoBehaviour {

    public Image bombIndicator;

    private string bombButtonName;
    private int bombsLeft;

    void Start() {
		BasePlayerVariables vars = this.gameObject.GetComponent<BasePlayerVariables>();
        bombsLeft = vars.bombsLeft;
		bombButtonName = vars.playerNumberString + "Bomb";
	}
	
	void Update() {
		if (Input.GetButton(bombButtonName) && bombsLeft > 0) {
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bullet in bullets) {
                Destroy(bullet);
            }
            Destroy(bombIndicator);
            bombsLeft--;
		}
	}
}
