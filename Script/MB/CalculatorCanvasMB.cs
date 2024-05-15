using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorCanvasMB : CanvasMB
{
    public void OpenChart()
    {
        GameState.Instance.GetCanvas<ChartCanvasMB>().Open();
    }
    public void OpenMenu()
    {
        GameState.Instance.GetCanvas<MenuCanvasMB>().Open();
    }
}
