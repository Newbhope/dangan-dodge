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
	public static Boundary ARENA_BOUNDARIES = new Boundary(-17.775f, 17.775f, -10f, 10);

	//TODO: maybe use int parameter
	public static Boundary getPlayerBoundary(string playerNumber) {
		if (playerNumber.Equals("One")) {
			return new Boundary(-17.775f, 0f, -10f, 10f);
		} else if (playerNumber.Equals("Two")) {
			return new Boundary(0f, 17.775f, -10f, 10f);
		} else {
			return ARENA_BOUNDARIES;
		}
	}

}