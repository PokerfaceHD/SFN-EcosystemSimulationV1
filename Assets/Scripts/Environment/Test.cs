using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public float radius = 1;
	public Vector2 regionSize = Vector2.one;
	public int rejectionSamples = 30;
	public float displayRadius =1;
    public GameObject food;
    Vector3 point3D;
	List<Vector2> points;

	void OnValidate() {
		points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSamples);
	}

	void Start() {

		if (points != null) {
			foreach (Vector2 point in points) {
                point3D.x = point.x - regionSize.x/2;
                point3D.z = point.y - regionSize.y/2;
                Instantiate(food, point3D, new Quaternion(0,0,0,0));
			}
		}
	}
}
