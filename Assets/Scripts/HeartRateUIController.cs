using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeartRateUIController : MonoBehaviour
{
    public HeartRateManager hrManager;
    public Slider hrSlider;
    public TextMeshProUGUI bpmDisplayText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hrManager = FindFirstObjectByType<HeartRateManager>();
        hrSlider.minValue =40f;
        hrSlider.maxValue =200f;

        hrSlider.value = hrManager.GetHeartRate();

        hrSlider.onValueChanged.AddListener(OnSliderValueChanged);
        OnSliderValueChanged(hrSlider.value);

    }
    public void OnSliderValueChanged(float value)
    {
        hrManager.SetHeartRate(value);
        if (bpmDisplayText != null)
        {
            bpmDisplayText.text =$"{Mathf.RoundToInt(value)} BPM";
        }
    }
}
