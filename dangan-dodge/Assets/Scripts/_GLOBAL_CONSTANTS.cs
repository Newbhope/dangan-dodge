using UnityEngine.Networking;

public struct Boundary {

	public float xMin, xMax, yMin, yMax;

	public Boundary(float xMin, float xMax, float yMin, float yMax) {
		this.xMin = xMin;
		this.xMax = xMax;
		this.yMin = yMin;
		this.yMax = yMax;
	}
}

public class _GLOBAL_CONSTANTS {

	public static Boundary getPlayerBoundary(BasePlayerVariables vars) {
        switch (vars.playerNumberInt) {
            case 1:
                return new Boundary(-17.775f, 0f, -10f, 10f);
            case 2:
                return new Boundary(0f, 17.775f, -10f, 10f);
            default:
                return new Boundary(0, 0, 0, 0);
        }
	}
}