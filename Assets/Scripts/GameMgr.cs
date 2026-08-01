using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr I;
    public PlayerMovement playermovementScript;
    public ArmourEdit armoureditScript;
    public bool isCanMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        I = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
