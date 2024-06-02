using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public Item Owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost"))
        {
            Destroy(collision.gameObject);
            GameManager.Inst.GetItem(transform.position);
            StartCoroutine(Owner.co_Toggle());
        }
    }

}
