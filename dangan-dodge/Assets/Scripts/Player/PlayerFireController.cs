using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that calls into each bullet spawner with the fire key
 * Each bullet spawner will have its own configurable fire rate
 * 
 * Fire rate is the period in seconds between the next time that type of bullet fires
 **/
public class PlayerFireController : MonoBehaviour {

	private string fireButtonName;
	private BaseBulletSpawner baseBulletSpawner;

	void Start() {
		BasePlayerVariables vars = this.gameObject.GetComponent<BasePlayerVariables>();
		fireButtonName = vars.playerNumberString + "Fire";
		baseBulletSpawner = GetComponent<BaseBulletSpawner>();
	}
	
	void Update() {
		if (Input.GetButton(fireButtonName)) {
			baseBulletSpawner.Spawn();
		}
	}
}
