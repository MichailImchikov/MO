using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonInputMB : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputText;
    public void Click()
    {
        GameState.Instance.GetCanvas<CalculatorCanvasMB>().AddChar(inputText.text);
    }
}
