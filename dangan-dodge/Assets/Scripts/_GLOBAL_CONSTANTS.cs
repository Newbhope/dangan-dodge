using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary : MonoBehaviour {

	private static Boundary privBounds;
	public static Boundary Instance() {
		return new Boundary(-5, 5, -10, 10);
	}

	public float xMin, xMax, yMin, yMax;

	public Boundary(float xMin, float xMax, float yMin, float yMax) {
		this.xMin = xMin;
		this.xMax = xMax;
		this.yMin = yMin;
		this.yMax = yMax;
	}
}
	
public class _GLOBAL_CONSTANTS {
	public static Boundary ARENA_BOUNDARIES = new Boundary(-5, 5, -10, 10);
}

public class Singleton : MonoBehaviour {
	private static Singleton instance;

	public static Singleton Instance {
		get { return instance ?? 
			(instance = new GameObject("Singleton").AddComponent<Singleton>()); 
		}
	}
}