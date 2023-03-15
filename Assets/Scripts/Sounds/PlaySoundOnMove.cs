using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnMove : MonoBehaviour
{
    // The AudioSource component that will play the sound
    public AudioSource audioSource;

    // The minimum distance the object must move before the sound is played
    public float minMoveDistance = 0.1f;

    // The position of the object in the previous frame
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the object
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance the object has moved since the last frame
        float moveDistance = Vector3.Distance(transform.position, previousPosition);

        // If the object has moved more than the minimum distance
        if (moveDistance >= minMoveDistance)
        {
            // Play the sound
            audioSource.volume = PlayerPrefs.GetFloat("EffectsVolume");
            audioSource.Play();

            // Store the current position of the object for the next frame
            previousPosition = transform.position;
        }
    }
}
