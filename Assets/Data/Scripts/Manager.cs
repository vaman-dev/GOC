using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip backgroundsound;
    [SerializeField] private Movement playerMovement;

    private AudioSource audioSource;
    private float lastSpeed = 0f;
    private float basePitch = 1f;
    private float maxPitch = 1.5f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = backgroundsound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.pitch = basePitch;
        audioSource.Play();
    }

    void Update()
    {
        if (playerMovement == null) return;

        float currentSpeed = playerMovement.CurrentSpeed;

        if (currentSpeed > lastSpeed)
        {
            float speedRatio = Mathf.InverseLerp(playerMovement.BaseSpeed, playerMovement.BaseSpeed * 3.5f, currentSpeed);
            float targetPitch = Mathf.Lerp(basePitch, maxPitch, speedRatio);
            audioSource.pitch = targetPitch;
        }

        lastSpeed = currentSpeed;
    }
}
