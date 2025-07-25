using System.Collections;
using UnityEngine;
using System;

public class coll : MonoBehaviour
{
    private bool Open;
    public AudioClip doorSound;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider player)
    {
        if (player.name.StartsWith("P") || player.name.StartsWith("p") && !Open)
        {
            StopAllCoroutines();
            StartCoroutine(OpenTheDoor());
        }
    }
    
    void OnTriggerExit(Collider player)
    {
        if (player.name.StartsWith("P") || player.name.StartsWith("p") && Open)
        {
            StopAllCoroutines();
            StartCoroutine(CloseTheDoor());
        }
        
    }

    IEnumerator OpenTheDoor()
    {
        Open = true;
        float time = 0;
        int index = 0;
        while (time < 2)
        {
            float timer = (float)Math.Round(time * 4, 1);
            if (timer == (int)timer && timer < 8)
            {
                index = (int)timer;
                transform.GetChild(index).gameObject.SetActive(false);
                source.PlayOneShot(doorSound);
            }
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CloseTheDoor()
    {
        Open = false;
        float time = 0;
        int index = 0;
        while (time < 2)
        {
            float timer = (float)Math.Round(time * 4, 1);
            if (timer == (int)timer && timer < 8)
            {
                index = 7 - (int)timer;
                transform.GetChild(index).gameObject.SetActive(true);
                source.PlayOneShot(doorSound);
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
}
