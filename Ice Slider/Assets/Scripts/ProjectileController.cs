using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    public float speed;
    public GameObject explosion;

    private float lineLength;
	private float maxHeight;
    private Vector3 posMiddle;
	private Vector3 posStart;
    private Vector3 posTarget;
    private Vector3 target;
	

	public void Start () {
        posStart = transform.position;
        posTarget = explosion.transform.position;
        DeactivateExplosion();
        lineLength = MathHelp.LineLength(posStart.x, posStart.y, posTarget.x, posTarget.y);
        float maxHeight = -2000 / lineLength;
        DeterminePosMiddle(maxHeight);
        target = posMiddle;
	}

    private void DeterminePosMiddle(float maxHeight) {
        float x = MathHelp.Midpoint(posStart.x, posTarget.x);
        float y = MathHelp.Midpoint(posStart.y, posTarget.y);
        posMiddle = new Vector3(x, y, maxHeight);
    }
	
	public void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        UpdateTarget();
	}

    private void UpdateTarget() {
        if (transform.position == posMiddle) {
            target = posTarget;
        } else if (transform.position == posTarget) {
            Explode();
        }
    }
	
	private void Explode() {
		transform.gameObject.SetActive(false);
        explosion.SetActive(true);
        Invoke("DeactivateExplosion", 0.5f);   
        Invoke("ResetPosition", 3);
	}
	
	private void DeactivateExplosion() {
        explosion.SetActive(false);
    }
	
    private void ResetPosition() {
        target = posMiddle;
        transform.position = posStart;
        transform.gameObject.SetActive(true);
    }
  
}
