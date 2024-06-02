using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitModule : MonoBehaviour
{
    SpriteRenderer[] sps;


    Material originMat;
    Material whiteMat;
    public void Awake()
    {
        sps = GetComponentsInChildren<SpriteRenderer>();

        originMat = sps[0].material;
        whiteMat = Resources.Load<Material>("Materials/FlashWhite");
    }

    public void FlashWhite(float time)
    {
        StartCoroutine(co_FlashWhite(time))
;
    }

    IEnumerator co_FlashWhite(float time)
    {
        for (int i = 0; i < sps.Length; i++)
        {
            sps[i].material = whiteMat;
        }

        yield return new WaitForSecondsRealtime(time);

        for (int i = 0; i < sps.Length; i++)
        {
            sps[i].material = originMat;
        }
    }

}
