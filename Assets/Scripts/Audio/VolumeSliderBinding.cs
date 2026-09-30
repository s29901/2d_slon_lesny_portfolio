using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Connects a UI slider to one volume channel. Attach to the Slider itself and
/// pick the channel — no OnValueChanged wiring needed in the inspector.
/// </summary>
[RequireComponent(typeof(Slider))]
public class VolumeSliderBinding : MonoBehaviour
{
    [SerializeField] private AudioChannel channel = AudioChannel.Music;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.wholeNumbers = false;
    }

    private void OnEnable()
    {
        slider.SetValueWithoutNotify(VolumeManager.Instance.Get(channel));
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        VolumeManager.Instance.Set(channel, value);
    }
}
