
using UnityEngine;

public class ArmourEdit : MonoBehaviour
{
    public GameObject ArmourEditView;
    private bool isReadytoEdit = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {            
            isReadytoEdit = true;
        }       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isReadytoEdit = false;
        }
    }

    private void Start()
    {
        ArmourEditView.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameMgr.I.isCanMove = true;
            ArmourEditView.SetActive(false);            
        }
    }

    private void ArmourEditing()
    {
        GameMgr.I.isCanMove = false;
        ArmourEditView.SetActive(true);
    }
}
