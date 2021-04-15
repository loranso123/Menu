using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Slider))]
public class HealtBar : MonoBehaviour
{
    [SerializeField] private float fillSpeed = 50.0f;
    [SerializeField] private Text _textSlider;

    private Slider _slider;
    private RectTransform _fillRect;
    private float _targetValue = 0f;
    private float _curValue = 0f;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(ValueChange);
        _fillRect = _slider.fillRect;
        _targetValue = _curValue = _slider.value;
    }

    private void ValueChange(float newValue)
    {
        _targetValue = newValue;
        _textSlider.text = _slider.value.ToString();
    }

    private void Update()
    {
        _curValue = Mathf.MoveTowards(_curValue, _targetValue, Time.deltaTime * fillSpeed);

        Vector2 fillAnchor = _fillRect.anchorMax;
        fillAnchor.x = Mathf.Clamp01(_curValue / _slider.maxValue);
        _fillRect.anchorMax = fillAnchor;
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(ValueChange);
    }

    public void Attack()
    {
        _slider.value -= 10f;
    }

    public void Heal()
    {
        _slider.value += 10f;
    }
}