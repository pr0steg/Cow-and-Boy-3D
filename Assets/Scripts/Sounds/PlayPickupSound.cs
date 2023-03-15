using UnityEngine;

public class PlayPickupSound : MonoBehaviour
{
    // The AudioSource component that will play the sound
    public AudioSource pickupSound;
    public AudioSource pickupSoundBlue;
    public AudioSource pickupSoundYellow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Milk")
        {
            // Play the sound
            pickupSound.volume = PlayerPrefs.GetFloat("EffectsVolume");
            pickupSound.Play();
        }
        else if (other.gameObject.tag == "blueMilk")
        {
            // Play the sound
            pickupSoundBlue.volume = PlayerPrefs.GetFloat("EffectsVolume");
            pickupSoundBlue.Play();
        }
        else if (other.gameObject.tag == "yellowMilk")
        {
            // Play the sound
            pickupSoundYellow.volume = PlayerPrefs.GetFloat("EffectsVolume");
            pickupSoundYellow.Play();
        }
    }
}
