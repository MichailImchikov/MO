using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenuCanvasMB : CanvasMB
{
    public void OpenCalculator()
    {
        Close();
        GameState.Instance.GetCanvas<CalculatorCanvasMB>().Open();
    }
    public void OpenHistory()
    {
        Close();
        GameState.Instance.GetCanvas<HistoryCanvasMB>().Open();
    }
}
