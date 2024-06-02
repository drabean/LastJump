using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sp;
    public IEnumerator co_Toggle()
    {
        sp.gameObject.SetActive(false);

        yield return new WaitForSeconds(2.5f);
        sp.gameObject.SetActive(true);
    }

    public void Togle()
    {
        StartCoroutine(co_Toggle());
    }
}
