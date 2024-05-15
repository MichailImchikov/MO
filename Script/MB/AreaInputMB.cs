using Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class AreaInputMB : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Input;
    [SerializeField] private TextMeshProUGUI Answer;
    public void AddChar(string element)
    {
        Input.text += element;
    }
    public void MinStart()
    {
        var entity = GameState.Instance.ecsWorld.NewEntity();
        ref var _nelderComponent=ref GameState.Instance.ecsWorld.GetPool<NelderComponent>().Add(entity);
        _nelderComponent._compressionRatio = 0.5f;
        _nelderComponent._reflectionRatio = 1;
        _nelderComponent._stretchingRatio = 2;
        _nelderComponent.expression = new(Input.text,entity);
        GameState.Instance.answer = Answer;
        Count(Input.text);
        var canvas = GameState.Instance.GetCanvas<ChartCanvasMB>() as ChartCanvasMB;
        canvas.ClearLists();
    }
    public void MaxStart()
    {
        var entity = GameState.Instance.ecsWorld.NewEntity();
        ref var _nelderComponent = ref GameState.Instance.ecsWorld.GetPool<NelderComponent>().Add(entity);
        _nelderComponent._compressionRatio = 0.5f;
        _nelderComponent._reflectionRatio = 1;
        _nelderComponent._stretchingRatio = 2;
        _nelderComponent.expression = new("-("+Input.text+")",entity);
        GameState.Instance.answer = Answer;
        Count(Input.text);
        var canvas = GameState.Instance.GetCanvas<ChartCanvasMB>() as ChartCanvasMB;
        canvas.ClearLists();
    }
    public void Count(string expression)
    {
        GameState.Instance.CountVariables = 0;
        bool flag = true;
        int index = 1;
        while (flag)
        {
            string variable = "x" + index.ToString();
            if (expression.Contains(variable))
            {
                GameState.Instance.CountVariables++;
                index++;
            }
            else
            {
                flag = false;
            }
        }
    }
    public void ClearLostChar()
    {
        var str=Input.text.Remove(Input.text.Length - 1);
        Input.text=str;
    }
    public void ClearFull()
    {
        Input.text = "";
    }
}
