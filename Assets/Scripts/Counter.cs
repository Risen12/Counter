using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;

    private float _delay = 0.5f;
    private bool _isButtonClicked;
    private WaitUntil _waitUntilButtonActivates;
    private TextMeshProUGUI _buttonText;

    private void Start()
    {
        _isButtonClicked = false;
        _text.text = "0";
        _waitUntilButtonActivates = new WaitUntil(() => GetButtonState());
        StartCoroutine(CountUp(_delay, _waitUntilButtonActivates));
        _buttonText = _button.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonState);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonState);
    }

    private void HandleButtonState()
    {
        if(_isButtonClicked)
            _isButtonClicked = false;
        else
            _isButtonClicked = true;

        ChangeButtonText(_isButtonClicked, _buttonText);
        SwitchConditionCoroutine(_isButtonClicked);
    }

    private IEnumerator CountUp(float delay, WaitUntil waitCondition)
    {
        var wait = new WaitForSeconds(delay);
        int step = 0;

        while (true)
        {
            yield return waitCondition;
            yield return wait;
            step++;
            DisplayValue(step);
        }
    }

    private void SwitchConditionCoroutine(bool state)
    {
        if (state == true)
            StartCoroutine(_waitUntilButtonActivates);
        else
            StopCoroutine(_waitUntilButtonActivates);
    }

    private bool GetButtonState() => _isButtonClicked;

    private void DisplayValue(int value)
    {
        _text.text = value.ToString();
    }

    private void ChangeButtonText(bool state, TextMeshProUGUI buttonText)
    {
        string startCounter = "Запустить счётчик";
        string stopCounter = "Остановить счётчик";

        if (state == true)
            buttonText.text = stopCounter;
        else
            buttonText.text = startCounter;
    }
}
