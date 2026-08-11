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
    public bool isDashing = false;
    public bool CutSceneActived = false;
    public bool isCounting = true;
    public float CountTime;
    public TextMeshProUGUI Timer;
    public GameSequenceMgr StartSequence;

    // Start is called before the first frame update

    private void Awake()
    {
        I = this;
        //DOTween.SetTweensCapacity(10000, 1000);
        //if (I == null)
        //{
        //    I = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    public void Nextscene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void Start()
    {
        StartSequence.SceneStart();
    }

    private void Update()
    {
        if (isCounting && CountTime > 0)
        {
            CountTime -= Time.deltaTime;

            if (CountTime <= 0)
            {
                StopTimer();
                CountTime = 0f;
                StartSequence.GameOver();
            }           
            Timer.text = Mathf.Max(0f, CountTime).ToString("F1");
        }
    }

    public void SceneReload(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }

    public void GotoNextScene()
    {
        StartSequence.NextSceneLoading();
    }

    public void StopTimer()
    {
        isCounting = false;
    }
}
