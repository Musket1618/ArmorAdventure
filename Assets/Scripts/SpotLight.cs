using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLight : MonoBehaviour
{
    public PlayerMovement playerMovementScript;
    private bool hasTriggered = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (hasTriggered) return;
        
        if (other.CompareTag("Player"))
        {
            if (!playerMovementScript.isDashing)
            {
                GameMgr.I.isCanMove = false;
                GameMgr.I.StartSequence.GameOver();
                hasTriggered = true;
            }
            else
            {
                return;
            }
        }
    }

}
