
using UnityEngine;

public class ArmourEdit : MonoBehaviour
{
    private bool isReadytoEdit = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {            
            isReadytoEdit = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (isReadytoEdit)
            {
                ArmourEditing();
            }
        }
    }

    private void ArmourEditing()
    {
        print("1");
    }
}
