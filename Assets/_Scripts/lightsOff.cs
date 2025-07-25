using UnityEngine;

public class lightsOff : MonoBehaviour
{
    public AudioClip glassAudio;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!(collision.collider.name.StartsWith("P") || collision.collider.name.StartsWith("p")))
        {
            source.PlayOneShot(glassAudio);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}

