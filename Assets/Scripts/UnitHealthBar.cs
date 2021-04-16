using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UnitHealthBar : MonoBehaviour
{
    [SerializeField] private Unit _target;
    [SerializeField]private float fillSpeed = 50.0f;
    private float _targetValue;
    private Slider _bar;
    private Coroutine _updateValueCoroutine;

    private void Awake()
    {
        _bar = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _target.OnHealthChanged += UpdateValue;
        UpdateValue(_target.Health);
    }

    private void OnDisable()
    {
        _target.OnHealthChanged -= UpdateValue;
    }

    private void UpdateValue(int value)
    {
        _targetValue = value;
        if (_updateValueCoroutine == null)
        {
           _updateValueCoroutine = StartCoroutine(UpdateValueCoroutine());
        }
    }

    private IEnumerator UpdateValueCoroutine()
    {
        while (_bar.value != _targetValue)
        {
            _bar.value = Mathf.MoveTowards(_bar.value, _targetValue, Time.deltaTime * fillSpeed);
            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }
        _updateValueCoroutine = null;
    }
}
