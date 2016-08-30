using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private bool onIce;
    private bool skates;
	private Dictionary<string, Collision> collisions;
    private Dictionary<string, Trigger> triggers;
	private float currentSpeed;
    private float groundSpeed;
    private float iceSpeed;  
	private int keyCount;
    private Vector3 respawnPoint;
	private Vector3 startingPoint;
    private Vector3 target;
    

	void Start () {
        groundSpeed = 500;
        currentSpeed = groundSpeed;
        iceSpeed = 1000;
        target = transform.position;
        onIce = false;
        skates = false;
        keyCount = 0;
        SetUpTriggers();
        SetUpCollisions();
        UpdateStartingPoint();  
	}

    private void SetUpTriggers() {
        triggers = new Dictionary<string, Trigger>();
        triggers.Add("Death", new DeathTrigger());
		triggers.Add("Goal", new GoalTrigger());
        triggers.Add("Ground", new GroundTrigger());
        triggers.Add("Ice", new IceTrigger());
        triggers.Add("Key", new KeyTrigger());
        triggers.Add("Respawn", new RespawnTrigger());
        triggers.Add("Skates", new SkatesTrigger());
    }

    private void SetUpCollisions() {
        collisions = new Dictionary<string, Collision>();
        collisions.Add("Block", new BlockCollision());
    }

    private void UpdateStartingPoint() {
        startingPoint = transform.position;
    }

    public void Update() {
        ExitOnEsc();
        OnMouseClick();  
        if (onIce) {
            KeepSliding();
        }	
		transform.position = Vector3.MoveTowards(transform.position, target, currentSpeed * Time.deltaTime);
    }

    private void ExitOnEsc() {
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }

    private void OnMouseClick() {
        if (Input.GetMouseButtonDown(0) && !(onIce && !skates)) {
            SetNewTarget();
            UpdateStartingPoint();
        }
    }

    private void SetNewTarget() {
        Vector3 pos = Input.mousePosition;
        pos.z = transform.position.z - Camera.main.transform.position.z;
        target = Camera.main.ScreenToWorldPoint(pos);
    }

    private void KeepSliding() {
        if (IsPositionNearTarget()) {
            ExtendSlide(20f);
        }
    }

    private bool IsPositionNearTarget() {
        return MathHelp.Distance(transform.position.x, target.x) < 75 
            && MathHelp.Distance(transform.position.y, target.y) < 75;
    }

    private void ExtendSlide(float factor) {
        float lineLength = MathHelp.LineLength(target.x, target.y, startingPoint.x, startingPoint.y);
        target.x = MathHelp.ExtendCoordinate(target.x, startingPoint.x, lineLength, factor);
        target.y = MathHelp.ExtendCoordinate(target.y, startingPoint.y, lineLength, factor);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        try {
            triggers[other.tag].Activate(this, other); 
        } catch (KeyNotFoundException e) {}
            
    }

    public void OnCollisionEnter2D(Collision2D other) {
        try {
            collisions[other.gameObject.tag].Collide(this, other);
        } catch (KeyNotFoundException e) {}
    }

    public void SetRespawnPoint(Vector3 position) {
        respawnPoint = position;
    }

    public void KillPlayer() {
        gameObject.SetActive(false);
        Invoke("Respawn", 3);
    }

    private void Respawn() {
        gameObject.SetActive(true);
        transform.position = respawnPoint;
        target = transform.position;
    }

    public int KeyCount() {
        return keyCount;
    }

    public void IncreaseKeyCount() {
        keyCount++;
    }

    public void DecreaseKeyCount() {
        keyCount--;
    }

    public void OnIce() {
        currentSpeed = iceSpeed;
        onIce = true;
    }

    public void OnGround() {
        if (onIce) {
            currentSpeed = groundSpeed;
            onIce = false;
        }
    }
	
	public void EquipSkates() {
		skates = true;
	}
	
	public void UnequipSkates() {
		skates = false;
	}

}
