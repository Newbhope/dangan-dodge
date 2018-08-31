using UnityEngine;

public class BasePlayerVariables : MonoBehaviour {

	//TODO: maybe refactor all player variables into this?

    //Assigned in editor
	public string playerNumberString;
	public int playerNumberInt;
    //Vector representing which way the player is facing
    public Vector2 playerVector;

    //Assigned programatically
	public Transform spriteTransform;

	void Awake() {
		var childComponents = GetComponentsInChildren<Transform>();

		foreach (Transform transform in childComponents) {
			if (transform.tag.Equals("PlayerSprite")) {
				spriteTransform = transform;
			}
		}
	}
}
