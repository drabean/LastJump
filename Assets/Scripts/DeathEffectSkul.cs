using UnityEngine;

public class DeathEffectSkul : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D[] frags;

    void Start()
    {
        for(int i = 0; i < 2; i++)
        {
            frags[i].AddForce(Vector3.right * 3 * (i ==0 ? 1 : -1) + Vector3.up * Random.Range(6f, 12f), ForceMode2D.Impulse);
            frags[i].AddTorque((i == 0 ? -1 : 1) *  Random.Range(360, 720));
        }
    }

}
