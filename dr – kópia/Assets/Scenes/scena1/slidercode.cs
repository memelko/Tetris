using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class slidercode : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _slidertext;
    void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _slidertext.text = v.ToString("0");
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}