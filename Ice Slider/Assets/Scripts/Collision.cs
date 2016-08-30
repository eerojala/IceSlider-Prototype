using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class Collision {
    public abstract void Collide(PlayerController pc, Collision2D other);
}

public class BlockCollision : Collision {
    public override void Collide(PlayerController pc, Collision2D other) {
        if (pc.KeyCount() > 0) {
            pc.DecreaseKeyCount();
            other.gameObject.SetActive(false);
        }
    }
}
