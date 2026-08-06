using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr I;
    public PlayerMovement playermovementScript;
    public ArmourEdit armoureditScript;
    public bool isCanMove = true;
    public float CountTime;
    public TextMeshProUGUI Timer;

    // Start is called before the first frame update

    private void Awake()
    {
        DOTween.SetTweensCapacity(10000, 1000);
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Nextscene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void Update()
    {
        CountTime -= Time.deltaTime;
        Timer.text = CountTime.ToString("F1");
    }

}
