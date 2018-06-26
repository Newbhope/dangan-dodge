using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxController : MonoBehaviour {

	//only really useful if I ever want to change hitbox/modelsize

	public float modelScale;
	private Transform model;


	void Start () {
		model = GetComponent<Transform>();
	}
	
	void Update () {
		model.localScale = model.localScale * modelScale;
	}
}
