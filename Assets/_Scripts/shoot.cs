using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class shoot : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public GameObject uIclip;
    public GameObject FlashLight;
    private PlayerInput playerInput;
    public int clip = 5;
    public int initialclip;
    private InputAction fire;
    private InputAction reload;
    private InputAction flash;
    bool check;
    GameObject infront;
    public AudioClip shootSound;
    public AudioClip flashSound;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        fire = playerInput.actions.FindAction("Shoot");
        reload = playerInput.actions.FindAction("Reload");
        flash = playerInput.actions.FindAction("Flash");
        initialclip = clip;
        source = GetComponent<AudioSource>();
        
    }
    void Update()
    {
        // uIclip.GetComponent<Slider>().value = 1 - ((5 - clip) * 0.2f);
    }

    void FixedUpdate()
    {
        // if (fire.triggered && clip > 0)
        // {
        //     Instantiate(bullet, transform.position, transform.rotation);
        //     clip--;
        //     uIclip.GetComponent<Slider>().value -= 0.2f;
        // }
        // if (reload.triggered)
        // {
        //     clip = initialclip;
        //     uIclip.GetComponent<Slider>().value = 1;
        // }
        if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 80f) && hit.collider.name.Contains("Cube"))
        {
            infront = hit.collider.gameObject;
            hit.collider.GetComponent<Renderer>().material.color = new Color(1f, 0.5f, 0f);
            check = true;
        }
        else if (check && infront.GetComponent<Renderer>().material.color != Color.red)
        {
            infront.GetComponent<Renderer>().material.color = Color.gray;
            check = false;
        }
        if (flash.triggered)
        {
            if (FlashLight.activeInHierarchy)
            {
                FlashLight.SetActive(false);
                source.PlayOneShot(flashSound);
            }
            else
            {
                FlashLight.SetActive(true);
                source.PlayOneShot(flashSound);
            }
        }
    }
    public void Reload()
    {
        clip = initialclip;
        uIclip.GetComponent<Slider>().value = 1;
    }
    public void Fire()
    {
        if (clip > 0)
        {
            StopAllCoroutines();
            StartCoroutine(ShootSound());
            Instantiate(bullet, transform.position, transform.rotation);
            clip--;
            uIclip.GetComponent<Slider>().value -= 0.2f;
        }
    }

    IEnumerator ShootSound()
    {
        source.PlayOneShot(shootSound);
        yield return null;
    }
}