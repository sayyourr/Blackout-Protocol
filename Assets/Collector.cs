using UnityEngine;

public class Collector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PS;
    public GameObject KM;

    void OnTriggerEnter(Collider player)
    {

        if (transform.parent.name.StartsWith("H"))
        {
            // Debug.Log(KM.GetComponent<Movement>().HP);
            // Debug.Log(KM.GetComponent<Movement>().clips);
            if (player.name.StartsWith("b"))
            {
                PS.GetComponent<Movement>().HP = 1;
                gameObject.SetActive(false);
            }
            else
            {
                KM.GetComponent<Movement>().HP = 1;
                gameObject.SetActive(false);
            }

        }
        else if (transform.parent.name.StartsWith("C"))
        {
            GetComponentInParent<Spawner>().Resp(10);

            if (player.name.StartsWith("b") || player.name.StartsWith("p"))
            {
                PS.GetComponent<Movement>().clips += 1;
                gameObject.SetActive(false);
            }
            else if (player.name.StartsWith("B") || player.name.StartsWith("P"))
            {
                KM.GetComponent<Movement>().clips += 1;
                gameObject.SetActive(false);
            }
        }    
            else if (player.name.StartsWith("P") || player.name.StartsWith("p"))
            {
            if (player.GetComponent<Movement>().Gun.activeInHierarchy)
            {
                player.GetComponent<Movement>().clips += 1;
                gameObject.SetActive(false);
                GetComponentInParent<Spawner>().Resp(5);
            }
            else
            {
                player.GetComponent<Movement>().Gun.SetActive(true);
                player.GetComponent<Movement>().BulletShooter.SetActive(true);
                player.GetComponent<Movement>().Crosshair.SetActive(true);
                player.GetComponent<Movement>().ClipSlider.SetActive(true);
                player.GetComponent<Movement>().Clips.SetActive(true);
                player.gameObject.GetComponent<Animator>().SetBool("Pulled", true);
                gameObject.SetActive(false);
                GetComponentInParent<Spawner>().Resp(5);
            }
        }

    }
}
