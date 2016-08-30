using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public abstract class Trigger {
    public abstract void Activate(PlayerController pc, Collider2D other);

    protected void DeactivateOther(Collider2D other) {
        other.gameObject.SetActive(false);
    }
}

public class DeathTrigger : Trigger {
    public override void Activate(PlayerController pc, Collider2D other) {
        pc.KillPlayer();
    }
}

public class GoalTrigger : Trigger {
	public override void Activate(PlayerController pc, Collider2D other) {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex != 0) {
            Application.Quit();
        } else {
            SceneManager.LoadScene(sceneIndex + 1);
        }   
	}
}

public class GroundTrigger: Trigger {
    public override void Activate(PlayerController pc, Collider2D other) {
        pc.OnGround();
    }
}

public class IceTrigger : Trigger {
    public override void Activate(PlayerController pc, Collider2D other) {
        pc.OnIce();
    }
}

public class KeyTrigger : Trigger {
    public override void Activate(PlayerController pc, Collider2D other) {
        pc.IncreaseKeyCount();
        DeactivateOther(other);
    }
}

public class RespawnTrigger : Trigger {
    public override void Activate(PlayerController pc, Collider2D other) {
        pc.SetRespawnPoint(other.transform.position);
    }
}

public class SkatesTrigger : Trigger {
    public override void Activate(PlayerController pc, Collider2D other) {
        pc.EquipSkates();
        DeactivateOther(other);
    }
}

