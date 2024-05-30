using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player movement")]
    [SerializeField] public float walkSpeed = 5.0f;
    [SerializeField] public float runSpeedMultiplier = 2.0f;
    [SerializeField] public float stepOffset = 0.5f; // Максимальная высота шага
    [SerializeField] public float stepSearchRange = 0.3f; // Расстояние поиска препятствия
    [SerializeField] public float stepSpeed = 5.0f; // Скорость подъема на преграду

    [SerializeField] private Camera mainCamera;

    [SerializeField] private Vector3 originalCameraPosition;

    [Header("Footstep Parametres")]
    [SerializeField] public AudioClip[] grassFootStepSounds;
    [SerializeField] public AudioClip[] asphaltFootStepSounds;
    [SerializeField] public float minMoveSpeedForFootsteps = 0.1f;
    [SerializeField] public float minTimeBetweenFootsteps = 0.5f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float lastFootstepTime;
    [SerializeField] private AudioClip[] currentFootStepSounds;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        Transform playerCameraTransform = transform.Find("Camera");

        if (playerCameraTransform != null)
        {
            mainCamera = playerCameraTransform.GetComponent<Camera>();

            if (mainCamera != null)
            {
                originalCameraPosition = mainCamera.transform.localPosition;
            }
            else
            {
                Debug.LogError("Camera component not found in the 'Camera' object!");
            }
        }
        else
        {
            Debug.LogError("Camera object not found inside the Player object!");
        }

        currentFootStepSounds = grassFootStepSounds;
        lastFootstepTime = -minTimeBetweenFootsteps;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (IsSteppingObstacle(ref moveDirection))
        {
            // Подъем на преграду
            moveDirection.y = stepSpeed;
        }
        else
        {
            // Гравитация
            moveDirection.y -= 9.18f * Time.deltaTime;
        }

        float currentSpeed = walkSpeed;
        bool isSprinting;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            currentSpeed *= runSpeedMultiplier;
        }
        else
        {
            isSprinting = false;
        }

        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        if ((controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            if (controller != null && controller.gameObject != null && controller.gameObject.transform != null && controller.gameObject.transform.parent != null)
            {
                if (controller.gameObject.transform.parent.gameObject.tag == "Grass")
                {
                    currentFootStepSounds = grassFootStepSounds;
                }
                else if (controller.gameObject.transform.parent.gameObject.tag == "Stone")
                {
                    currentFootStepSounds = asphaltFootStepSounds;
                }
            }
        }

        if (controller.velocity.magnitude > 0)
        {
            if (controller.velocity.magnitude > minMoveSpeedForFootsteps && currentFootStepSounds.Length > 0 && Time.time - lastFootstepTime > minTimeBetweenFootsteps)
            {
                PlayRandomFootstepSound();
                lastFootstepTime = Time.time;
            }

        }
        else
        {
            audioSource.Stop();
            mainCamera.transform.localPosition = originalCameraPosition;
        }
    }

    bool IsSteppingObstacle(ref Vector3 moveDirection)
    {
        RaycastHit hitLower;
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        Vector3 direction = moveDirection.normalized;
        float distance = stepSearchRange;

        if (Physics.Raycast(origin, direction, out hitLower, distance))
        {
            Vector3 upperOrigin = transform.position + Vector3.up * (stepOffset + 0.1f);

            if (!Physics.Raycast(upperOrigin, direction, distance))
            {
                moveDirection += Vector3.up * stepOffset;
                return true;
            }
        }
        return false;
    }

    void PlayRandomFootstepSound()
    {
        AudioClip randomFootstepSound = currentFootStepSounds[Random.Range(0, currentFootStepSounds.Length)];
        audioSource.PlayOneShot(randomFootstepSound);
    }

    public void SetFootstepSounds(AudioClip[] footStepSounds)
    {
        currentFootStepSounds = footStepSounds;
    }
}
