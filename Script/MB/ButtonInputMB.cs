using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonInputMB : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputText;
    [SerializeField] private AreaInputMB areaInputMB;
    public void Click()
    {
        areaInputMB.AddChar(inputText.text);
    }
}
