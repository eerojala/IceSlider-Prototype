using UnityEngine;
using System.Collections;

public class PatrolController : MonoBehaviour {

    private float speed;
    private Vector3 pointA;
	public Vector3 pointB;	
    private Vector3 target;

	public void Start () {
        speed = 1000;
        pointA = transform.position;
        target = pointB;
	}
	
	
	public void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        UpdateTarget();
	}

    private void UpdateTarget() {
        if (transform.position == target) {
            if (target == pointA) {
                target = pointB;
            } else {
                target = pointA;
            }
        }
    }
}
