using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public AudioClip tickSound;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    IEnumerator Respawn(int a)
    {
        //if (!gameObject.name.StartsWith("H"))
            source.PlayOneShot(tickSound); 
        yield return new WaitForSeconds(a);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Resp(int a)
    {
        StartCoroutine(Respawn(a));
    }
}
