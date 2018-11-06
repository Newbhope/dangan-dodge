using UnityEngine;

public class BasePlayerVariables : MonoBehaviour {

	//TODO: maybe refactor all player variables into this?

    //Assigned in editor
	public string playerNumberString;
	public int playerNumberInt;
    public int bombsLeft;

    //Vector representing which way the player is facing
    //TODO: Why do i need this?
    public Vector2 playerVector;

	void Awake() {
	}
}
