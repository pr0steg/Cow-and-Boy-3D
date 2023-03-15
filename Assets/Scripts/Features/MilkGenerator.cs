using System.Collections.Generic;
using UnityEngine;

public class MilkGenerator : MonoBehaviour
{
    public GameObject whiteMilkPrefab;
    public GameObject blueMilkPrefab;
    public GameObject yellowMilkPrefab;

    private float totalMilk = 100;
    private float milkGenerated = 0;

    private Dictionary<GameObject, int> milkChances = new Dictionary<GameObject, int>();

    // The AudioSource component that will play the sound
    public AudioSource placeMilkSound;

    private void Start()
    {
        InvokeRepeating("GenerateMilk", 0f, 5f);
        milkChances.Add(whiteMilkPrefab, 70);
        milkChances.Add(blueMilkPrefab, 15);
        milkChances.Add(yellowMilkPrefab, 15);
    }

    private void GenerateMilk()
    {
        if (milkGenerated >= totalMilk)
        {
            CancelInvoke("GenerateMilk");
            return;
        }

        // Generate a random number between 0 and 100
        int randomChance = Random.Range(0, 100);

        foreach (var kvp in milkChances)
        {
            if (randomChance < kvp.Value)
            {
                GameObject milkPrefab = kvp.Key;
                Vector3 milkPosition = new Vector3(transform.position.x, 0.1f, transform.position.z);
                Quaternion milkRotation = Quaternion.Euler(90f, 0f, 0f);
                Instantiate(milkPrefab, milkPosition, milkRotation);
                placeMilkSound.volume = PlayerPrefs.GetFloat("EffectsVolume");
                placeMilkSound.Play();
                milkGenerated++;

                break;
            }

            randomChance -= kvp.Value;
        }
    }
}