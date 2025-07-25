using UnityEngine;

public class ExplosiveTriggerer : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("ullet"))
        {
            transform.GetComponentInParent<Explosive>().Explode();
        }
    }
}
