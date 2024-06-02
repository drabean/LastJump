using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sp;
    public IEnumerator co_Toggle()
    {
        sp.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.0f);
        sp.gameObject.SetActive(true);
    }
}
