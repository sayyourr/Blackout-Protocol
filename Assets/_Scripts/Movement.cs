using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    public GameObject Crosshair;
    public GameObject Gun;
    public GameObject ClipSlider;
    public GameObject Clips;
    public GameObject BulletShooter;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jump;
    private InputAction run;
    // private InputAction damage;
    private InputAction fire;
    private InputAction reload;
    public Coroutine reloading;
    public bool Reloading;
    public int clips = -1;
    public float HP = 1;
    public float force = 60f;
    public float walk = 25f;
    public float sprint = 50f;
    public int Kills = 0;
    public GameObject health;
    public AudioClip reloadSound;
    public GameObject GunClip;
    public GameObject DeathCam;
    public GameObject WinCam;
    private AudioSource source;
    Animator animator;
    float speed;
    bool grounded;
    private Vector2 moveInput;


    void Start()
    {
        speed = walk;
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("move");
        jump = playerInput.actions.FindAction("Jump");
        run = playerInput.actions.FindAction("Run");
        // damage = playerInput.actions.FindAction("damage");
        fire = playerInput.actions.FindAction("Shoot");
        reload = playerInput.actions.FindAction("Reload");
        rb = GetComponent<Rigidbody>();
        HP = 1f;
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        health.GetComponent<Slider>().value = HP;
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.Find("Clips").gameObject.GetComponent<TextMeshProUGUI>().text = clips.ToString();
        if (transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.Find("Kills"))
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.Find("Kills").gameObject.GetComponent<TextMeshProUGUI>().text = Kills.ToString();
    }
    void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 moveVelocity = transform.TransformDirection(move) * speed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        if (jump.triggered && grounded)
        {
            Jump();
        }
        if (run.triggered)
        {
            if (speed == walk)
            {
                speed = sprint;
            }
            else speed = walk;
        }
        // if (damage.triggered)
        // {
        //     HP -= 0.34f;
        // }

        if (BulletShooter.activeInHierarchy)
        {
            if (fire.triggered)
            {
                if (!Reloading && BulletShooter.GetComponent<shoot>().clip > 0)
                {
                    BulletShooter.GetComponent<shoot>().Fire();
                }
                else if (Reloading && BulletShooter.GetComponent<shoot>().clip > 0)
                {
                    StopAllCoroutines();
                    source.Stop(); //cutting the reloading sound
                    //StopCoroutine(reloading);
                    clips++;
                    Reloading = false;
                    animator.SetBool("Reloading", false);
                    GunClip.SetActive(false);
                    Gun.transform.Find("Cube.012").gameObject.SetActive(true);
                    Gun.transform.Find("Cube.013").gameObject.SetActive(true);
                    Debug.Log("Stop");
                    BulletShooter.GetComponent<shoot>().Fire();
                    BulletShooter.GetComponent<shoot>().uIclip.GetComponent<Slider>().value = 1 - ((5 - BulletShooter.GetComponent<shoot>().clip) * 0.2f);
                }
            }

            if (reload.triggered && clips > 0 && BulletShooter.GetComponent<shoot>().clip < BulletShooter.GetComponent<shoot>().initialclip && !Reloading)
            {
                reloading = StartCoroutine(DelayedReload());
            }

            if (clips > 0 && BulletShooter.GetComponent<shoot>().clip == 0 && !Reloading)
            {
                reloading = StartCoroutine(DelayedReload());
            }


        }


        if (health.GetComponent<Slider>().value <= 0)
        {
            //StopAllCoroutines();
            StartCoroutine(Die());
            // transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).Find("Camera").gameObject.SetActive(false);
            // DeathCam.SetActive(true);
            // Cursor.lockState = CursorLockMode.Confined;
            // Cursor.visible = true;
            // transform.GetComponent<PlayerInput>().enabled = false;
            //transform.gameObject.SetActive(false);
        }

        if (Kills >= 10)
        {
            transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).Find("Camera").gameObject.SetActive(false);
            WinCam.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            gameObject.SetActive(false);
        }
    }

    IEnumerator DelayedReload()
    {
        Reloading = true;
        Debug.Log("Reloading");
        source.PlayOneShot(reloadSound);
        animator.SetBool("Reloading", true);
        Gun.transform.Find("Cube.012").gameObject.SetActive(false);
        Gun.transform.Find("Cube.013").gameObject.SetActive(false);
        StartCoroutine(ShowClip());
        //BulletShooter.GetComponent<shoot>().uIclip.GetComponent<Slider>().value = 0;
        Coroutine UI = StartCoroutine(UITimerRoutine());
        yield return new WaitForSeconds(1.5f);
        StopCoroutine(UI);
        BulletShooter.GetComponent<shoot>().Reload();
        Reloading = false;
        animator.SetBool("Reloading", false);
        GunClip.SetActive(false);
        Gun.transform.Find("Cube.012").gameObject.SetActive(true);
        Gun.transform.Find("Cube.013").gameObject.SetActive(true);
        Debug.Log("Done");
    }

    IEnumerator UITimerRoutine()
    {
        BulletShooter.GetComponent<shoot>().uIclip.GetComponent<Slider>().value = 0;
        clips--;

        while (BulletShooter.GetComponent<shoot>().uIclip.GetComponent<Slider>().value < 1.5f)
        {
            BulletShooter.GetComponent<shoot>().uIclip.GetComponent<Slider>().value += Time.deltaTime / 1.5f;
            yield return null;
        }
    }
    IEnumerator ShowClip()
    {
        yield return new WaitForSeconds(0.3f);
        GunClip.gameObject.SetActive(true);
    }

    IEnumerator Die()
    {
        animator.SetBool("Dead", true);
        transform.GetComponent<PlayerInput>().enabled = false;
        transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).Find("Camera").gameObject.SetActive(false);
        transform.GetChild(2).transform.gameObject.SetActive(true);
        //Debug.Log("DEAD");
        yield return new WaitForSeconds(3.5f);
        // Debug.Log("DEAD");
        DeathCam.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    private void Jump()
    {
        animator.SetBool("Jumping", true);
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        grounded = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        animator.SetBool("Jumping", false);
    }
}
