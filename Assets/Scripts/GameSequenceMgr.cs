using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GameSequenceMgr : MonoBehaviour
{
    public GameObject Visor;
    // Start is called before the first frame update
    public void GameOver()
    {
        StartCoroutine(co_GameOver());
    }

    public IEnumerator co_GameOver()
    {
        Sequence GameOver = DOTween.Sequence();
        GameOver
            .AppendCallback(() =>
            {
                
            });

        yield return GameOver.WaitForCompletion();
        Loading();
    }

    public void Loading()
    {
        StartCoroutine(co_Loading());
    }

    public IEnumerator co_Loading()
    {
        Sequence Loading = DOTween.Sequence();
        Loading
            .AppendCallback(() =>
            {
                
            })
            .Append(Visor.transform.DOMoveY(540f, 0.8f).SetEase(Ease.InOutCubic));

        yield return Loading.WaitForCompletion();
        GameMgr.I. SceneReload("Stage1");

    }

    public void SceneStart()
    {
        StartCoroutine(co_SceneStart());
    }

    public IEnumerator co_SceneStart()
    {
        Sequence SceneStart = DOTween.Sequence();
        SceneStart
            .AppendCallback(() =>
            {
                Visor.SetActive(true);
            })
        .Append(Visor.transform.DOMoveY(1740f, 0.7f).SetEase(Ease.InCubic));

        yield return SceneStart.WaitForCompletion();

    }
}
