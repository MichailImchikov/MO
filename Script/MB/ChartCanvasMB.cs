using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChartCanvasMB : CanvasMB
{
    [SerializeField] private Transform _historyZone;
    [HideInInspector] private List<Point> points = new();
    [HideInInspector] private List<float> answers = new();
    [SerializeField] private HistoryMB history;
    [SerializeField] private ScrollRect scrollRect;
    override public void Open()
    {
        _thisCanvas.enabled = true;
        for (int i = 0; i < _historyZone.childCount; i++)
        {
            Destroy(_historyZone.GetChild(i).gameObject);
        }
        for(int i=0;i<points.Count;i++)
        {
            var historyMB = Instantiate(history, _historyZone);
            historyMB.Init(points[i], answers[i]);
        }
        scrollRect.verticalNormalizedPosition = 1f;
    }
    public void OpenCalculator()
    {
        GameState.Instance.GetCanvas<CalculatorCanvasMB>().Open();
    }
    public void ClearLists()
    {
        points.Clear();
        answers.Clear();
    }
    public void SetPoint(Point point, float answer)
    {
        points.Add(point);
        answers.Add(answer);
    }
    public void OpenSecret()
    {
        GameState.Instance.GetCanvas<SecretCanvasMB>().Open();
        Close();
    }
}
