using UnityEngine;

public class Stage : MonoBehaviour
{
     public GameManager mgr;

    public int curStageIdx;

    public Transform CamPos;

    public Transform RevivePos;
    public GameObject StageLock;
    [ContextMenu("INIT")]
    public void Init(GameManager mgr)
    {
        Debug.Log("INIT STAGE" + curStageIdx);
        this.mgr = mgr;
        transform.position = curStageIdx * (Vector3.right) * 18;

        mgr.CamMove(CamPos);
    }

    public void CloseStage()
    {
        StageLock.SetActive(true);
    }




}
