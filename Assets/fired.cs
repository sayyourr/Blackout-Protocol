using UnityEngine;

public class fired : MonoBehaviour
{
    public float velocity = 3000f;
    private float timeElapsed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * velocity, ForceMode.Impulse);

    }
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > 4)
            Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("Cube"))
        {
            collision.collider.name = "DEAD";
            collision.collider.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (collision.collider.name.StartsWith("P") || collision.collider.name.StartsWith("p"))
        {
            collision.collider.GetComponent<Movement>().HP -= 0.34f;
        }
        else if (collision.collider.name.StartsWith("Z"))
        {
            collision.collider.GetComponent<AI>().HP -= 0.34f;
        }
        Destroy(gameObject);
    }
}
