using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireController : MonoBehaviour {

	public float fireRate;

	private string fireButtonName;
	private float nextFireTime;

	// Use this for initialization
	void Start () {
		BasePlayerVariables vars = this.gameObject.GetComponent<BasePlayerVariables>();
		fireButtonName = vars.playerNumber + "Fire";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton(fireButtonName) && Time.time > nextFireTime) {
			nextFireTime = Time.time + fireRate;
			Debug.Log("lel");
		}
	}
}
