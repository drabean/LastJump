using UnityEngine;
using TMPro;

public class GoldenTombstone : MonoBehaviour
{

    public GameObject TMPGroup;

    public TextMeshPro TMP_Score;


    public GameObject ClearEffect;

    private void Start()
    {
        SoundManager.Inst.BGMFadeout();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            TMPGroup.SetActive(true);
            TMP_Score.text = "you found a place to rest..\n..\n in " + (int)GameManager.Inst.time + "  Seconds!";

            Destroy(collision.gameObject);
            Vector3 pos = collision.gameObject.transform.position;

            for(int i = 0; i < 3; i++)
            {
                Instantiate(ClearEffect, pos, Quaternion.identity);
            }

            SoundManager.Inst.Play("WIN");
        }

    }
}
