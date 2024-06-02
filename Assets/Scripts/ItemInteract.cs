using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public Item Owner;
    public bool isSkul;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost"))
        {
            if (isSkul)
            {
                Destroy(collision.gameObject);
                GameManager.Inst.GetItem(transform.position);
                Owner.Togle();
            }
            else
            {
                collision.GetComponent<Ghost>().SoulDeath();
            }
        }
    }

}
