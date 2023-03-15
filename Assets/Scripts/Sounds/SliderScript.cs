using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) => { 
            _slider.value = v;
        });
    }
}
