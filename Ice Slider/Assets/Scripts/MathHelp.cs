using UnityEngine;
using System.Collections;

public class MathHelp {

	public static float Distance(float a, float b) {
        return Mathf.Abs(a - b);
    }
	
	public static float ExtendCoordinate(float targetCoordinate, float startCoordinate, float lineLength, float factor) {
		return targetCoordinate + (targetCoordinate - startCoordinate) / lineLength * factor;
	}
	
    public static float LineLength(float x1, float y1, float x2, float y2) {
        float x = PowerOfTwo(x2 - x1);
        float y = PowerOfTwo(y2 - y1);
        return Sqrt(x + y);
    }

	public static float Midpoint(float a, float b) {
        return (a + b) / 2;
    }
	
    public static float PowerOfTwo(float f) {
        return Mathf.Pow(f, 2);
    }
	
	public static float Sqrt(float x) {
		return Mathf.Sqrt(x);
	}
}
