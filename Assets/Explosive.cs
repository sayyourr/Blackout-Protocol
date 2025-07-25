using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public AudioClip explosionSound;
    private AudioSource source;
    private List<GameObject> inRangePlayers = new List<GameObject>();

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void Explode()
    {
        source.PlayOneShot(explosionSound);
        foreach (Transform c in transform)
        {
            c.gameObject.SetActive(false);
        }
        foreach (GameObject player in inRangePlayers)
        {
            if (player != null)
            {
                Debug.Log(player.name + " exploded into pieces.");
                if (player.name.StartsWith("Z"))
                {
                    player.GetComponent<AI>().HP = 0;
                }
                    else player.GetComponent<Movement>().HP = 0;
            }
        }

        inRangePlayers.Clear(); // reset after explosion
        
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.name.StartsWith("P") || other.name.StartsWith("p") || other.name.StartsWith("Z")) && !inRangePlayers.Contains(other.gameObject))
        {
            Debug.Log(other.name + " entered.");
            inRangePlayers.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (inRangePlayers.Contains(other.gameObject))
        {
            Debug.Log(other.name + " exited.");
            inRangePlayers.Remove(other.gameObject);
        }
    }
    
}
