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

    private void Start()
    {
        _isButtonClicked = false;
        _text.text = "0";

        StartCoroutine(CountUp(_delay));
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
        _isButtonClicked = !_isButtonClicked;
        ChangeButtonText();
    }

    private IEnumerator CountUp(float delay)
    {
        var wait = new WaitForSeconds(delay);
        var waitCondition = new WaitUntil(() => GetState());
        int step = 0;

        while (true)
        {
            yield return waitCondition;
            yield return wait;
            step++;
            DisplayValue(step);
        }
    }

    private bool GetState() => _isButtonClicked;

    private void DisplayValue(int value)
    {
        _text.text = value.ToString();
    }

    private void ChangeButtonText()
    {
        string startCounter = "Запустить счётчик";
        string stopCounter = "Остановить счётчик";

        TextMeshProUGUI buttonText = _button.GetComponentInChildren<TextMeshProUGUI>();

        if (_isButtonClicked)
            buttonText.text = stopCounter;
        else
            buttonText.text = startCounter;
    }
}
