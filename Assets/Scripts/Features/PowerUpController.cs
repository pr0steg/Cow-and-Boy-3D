using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour
{
    public TouchAndGo touchAndGo;
    public float speedBoostMultiplier = 2f;
    public Image speedPowerupImage;
    public Image protectPowerupImage;

    private Coroutine powerUpCoroutine;

    private bool isSpeedBoostActive = false;
    private float originalMovementSpeed = 0f;

    private void Start()
    {
        speedPowerupImage.enabled = false;
        protectPowerupImage.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("yellowMilk") && gameObject.GetComponent<Renderer>().material.color != Color.green)
        {
            if (powerUpCoroutine != null)
            {
                StopCoroutine(powerUpCoroutine);

                if (isSpeedBoostActive)
                {
                    isSpeedBoostActive = false;
                    touchAndGo.movementSpeed = originalMovementSpeed;
                }
            }
            powerUpCoroutine = StartCoroutine(SetPowerUpColor(Color.green, 10f));
            speedPowerupImage.enabled = false;
            protectPowerupImage.enabled = true;
        }

        if (other.gameObject.CompareTag("blueMilk") && gameObject.GetComponent<Renderer>().material.color != Color.blue)
        {
            if (powerUpCoroutine != null)
            {
                StopCoroutine(powerUpCoroutine);
            }
            powerUpCoroutine = StartCoroutine(SetPowerUpColor(Color.blue, 10f));
            speedPowerupImage.enabled = true;
            protectPowerupImage.enabled = false;
        }
    }

    private IEnumerator SetPowerUpColor(Color color, float duration)
    {
        var renderer = gameObject.GetComponent<Renderer>();
        var originalColor = renderer.material.color;

        renderer.material.color = color;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Wolf"), color == Color.green);

        if (color == Color.blue && !isSpeedBoostActive)
        {
            originalMovementSpeed = touchAndGo.movementSpeed;
            touchAndGo.movementSpeed *= speedBoostMultiplier;
            isSpeedBoostActive = true;
        }

        yield return new WaitForSeconds(duration);

        renderer.material.color = originalColor;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Wolf"), false);

        if (isSpeedBoostActive)
        {
            touchAndGo.movementSpeed = originalMovementSpeed;
            isSpeedBoostActive = false;
        }

        powerUpCoroutine = null;
        speedPowerupImage.enabled = false;
        protectPowerupImage.enabled = false;
    }
}
