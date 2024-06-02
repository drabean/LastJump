using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public List<Stage> StageLis;

    public Stage CurStage;
    public Stage PrevStage;

    Camera cam;

    public PlayerMove playerPrefab;
    public Ghost ghostPrefab;
    public static GameManager Inst;


    public float time;

    public void Awake()
    {
        cam = Camera.main;
        Inst = this;
    }
    private void Start()
    {
        StartCoroutine(co_Timer());
        FadeOut();
        SoundManager.Inst.PlayBGM("Main");
    }
    IEnumerator co_Timer()
    {
        while(true)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }
    public void GetStage(Stage curStage)
    {
        Stage newStage = Instantiate(StageLis[curStage.curStageIdx + 1]);
        newStage.curStageIdx = curStage.curStageIdx + 1;
        newStage.Init(this);

        if(PrevStage != null) Destroy(PrevStage.gameObject);
        PrevStage = CurStage;
        CurStage = newStage;

    }

    float cameraMoveSpeed = 25.0f;
    public void CamMove(Transform dest)
    {
        SoundManager.Inst.Play("StageMove");
        StartCoroutine(co_CamMove(dest));
    }

    IEnumerator co_CamMove(Transform dest)
    {
        while(Vector3.Distance(cam.transform.position, dest.position) > 0.1f)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, dest.position, cameraMoveSpeed * Time.deltaTime);
            yield return null;
        }

        cam.transform.position = dest.position;
    }


    #region Á×À½Ã³¸®
    public void SpawnGhost(Vector3 deathPos)
    {
        StartCoroutine(co_SpawnGhost(deathPos));
    }

    Ghost curGhost;
    IEnumerator co_SpawnGhost(Vector3 deathPos)
    {
        yield return new WaitForSeconds(0.1f);
        if (curGhost != null) yield break;
        curGhost = Instantiate(ghostPrefab, deathPos, Quaternion.identity);
    }

    public void GetItem(Vector3 revPos)
    {
        StartCoroutine(co_GetItem(revPos));
    }
    IEnumerator co_GetItem(Vector3 revPos)
    {
        yield return new WaitForSeconds(0.1f);
        if (curPlayer != null) yield break;
        curPlayer = Instantiate(playerPrefab, revPos, Quaternion.identity);
        curPlayer.Revive();
    }

    public void Revive()
    {
        StartCoroutine(co_Revive());
    }
    PlayerMove curPlayer;
    IEnumerator co_Revive()
    {
        yield return new WaitForSeconds(1f);
        if (curPlayer != null) yield break;
        curPlayer = Instantiate(playerPrefab, CurStage.RevivePos.position, Quaternion.identity);
        curPlayer.Revive();
    }

    #endregion

    #region UI
    float fadeTime = 0.6f;
    public Image fadeImage;

    IEnumerator Fade(bool isFadeIn)
    {
        float fadeAmount = 1;

        while (fadeAmount >= 0)
        {
            fadeAmount -= Time.deltaTime * (1 / fadeTime);
            float curProgress = isFadeIn ? fadeAmount : 1 - fadeAmount;
            fadeImage.color = new Color(0, 0, 0, curProgress);
            yield return null;
        }
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(true));
    }

    #endregion
}
