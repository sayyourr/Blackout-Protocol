using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [Header("References")]
    public Transform cameraHolder;

    public Transform headBone;
    public Transform rightArmBone;
    public Transform leftArmBone;

    [Header("Settings")]
    public float sensitivity = 0.2f;
    private const string SensitivityKey = "MouseSensitivity";
    public float verticalClamp = 30f;
    PlayerInput playerInput;

    private float verticalRotation;
    private InputAction lookAction;
    private InputAction sensUp;
    private InputAction sensDown;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        sensDown = playerInput.actions.FindAction("Sens-");
        sensUp = playerInput.actions.FindAction("Sens+");
        lookAction = playerInput.actions.FindAction("Look");

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // sensitivity = PlayerPrefs.GetFloat(SensitivityKey, 1.0f);
    }

    void Update()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>() * sensitivity;

        // Horizontal rotation (yaw): rotate the player
        transform.Rotate(Vector3.up * lookInput.x);

        // Vertical rotation (pitch): rotate the camera holder
        verticalRotation -= lookInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);
        cameraHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        //Ak.localRotation = Quaternion.Euler(-verticalRotation, 0f, 0f);
    }
    public void FixedUpdate()
    {
        if (sensUp.triggered && sensitivity < 10)
        {
            sensitivity += 0.5f;
            // PlayerPrefs.SetFloat(SensitivityKey, sensitivity);
            // PlayerPrefs.Save();
        }
        if (sensDown.triggered && sensitivity > 0)
        {
            sensitivity -= 0.2f;
            // PlayerPrefs.SetFloat(SensitivityKey, sensitivity);
            // PlayerPrefs.Save();
        }
    }
    void LateUpdate()
    {
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>();

        verticalRotation -= mouseDelta.y * sensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);

        Quaternion verticalRot = Quaternion.Euler(verticalRotation, 0f, 0f);
        Quaternion negverticalRot = Quaternion.Euler(0f, -verticalRotation, 0f);
        Quaternion rightverticalRot = Quaternion.Euler(0f, verticalRotation, 0f);

        // HEAD: Add vertical tilt on top of animation
        if (headBone != null)
        {
            headBone.localRotation = verticalRot * headBone.localRotation;

        }

        // RIGHT HAND: Follow tilt
        if (rightArmBone != null)
        {
            rightArmBone.localRotation = rightverticalRot * rightArmBone.localRotation;

        }

        // LEFT HAND: Same
        if (leftArmBone != null)
        {
            leftArmBone.localRotation = negverticalRot * leftArmBone.localRotation;

            float goRight = Mathf.Clamp(verticalRotation, -verticalClamp, 20);

            float leftArmOffset = Mathf.Sin(Mathf.Deg2Rad * goRight) * -0.17f;
            leftArmBone.localPosition = new Vector3(leftArmOffset, leftArmBone.localPosition.y, leftArmBone.localPosition.z);

            float lefty = Mathf.Sin(Mathf.Deg2Rad * goRight) * 0.06f;
            leftArmBone.localPosition = new Vector3(leftArmBone.localPosition.x, lefty,  leftArmBone.localPosition.z);
        }

    }

}
