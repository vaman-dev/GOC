using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] AudioClip backgroundsound;
    private AudioSource audioSource;

    void Start()
    {
        //
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = backgroundsound;
        audioSource.Play();
    }   

    void Update()
    {

    }
}