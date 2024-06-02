using UnityEngine;

public class StageMover : MonoBehaviour
{
    public Stage nextStage; // TODO: 매니저만들기

    public Stage Owner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Owner.mgr.GetStage(Owner);

            Owner.CloseStage();
            Destroy(gameObject);
        }
    }
}
