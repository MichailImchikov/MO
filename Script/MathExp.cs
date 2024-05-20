using System.Collections.Generic;
using System;
using Client;
using org.matheval;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
public class MathExpression
{

    private string _expression;
    private int _entity;
    private List<string> _degree;
    public MathExpression(string expression, int entity, List<string> degree)
    {
        _expression = expression;
        _entity = entity;
        _degree = degree;
    }
    public string GetExpression()
    {
        return _expression;
    }
    public float Calculation(Point point)
    {
        if(_degree !=null)
        {
            try
            {
                for (int i = 0; i < _degree.Count; i++)
                {
                    Expression expDegree = new Expression(ReturnStringExpressionWithout(point.Coordinates, _degree[i]));
                    var result = expDegree.Eval();
                }
            }
            catch (System.Exception e)
            {
                if (!GameState.Instance.ecsWorld.GetPool<StopCountingEvent>().Has(_entity))
                {
                    GameState.Instance.ecsWorld.GetPool<StopCountingEvent>().Add(_entity).exception = "Ошибка степени";
                }
                return 0;
            }
        }
        
        try
        {        
            Expression expression = new Expression(ReturnStringExpression(point.Coordinates));
            var result = expression.Eval();
            return float.Parse(result.ToString());
        }
        catch (System.Exception e)
        {
            if (!GameState.Instance.ecsWorld.GetPool<StopCountingEvent>().Has(_entity))
            {
                GameState.Instance.ecsWorld.GetPool<StopCountingEvent>().Add(_entity).exception="Бесконечность";
            }
            return 0;
        }

    }
    public string ReturnStringExpression(List<float> point)
    {
        string bufStr = _expression;
        for (int i = 0; i < point.Count; i++)
        {
            string variable = "x" + (i + 1);
            bufStr = bufStr.Replace(variable, '('+point[i].ToString().Replace(",", ".")+')');
        }
        return bufStr;
    }
    public string ReturnStringExpressionWithout(List<float> point,string str)
    {
        string bufStr = str;
        for (int i = 0; i < point.Count; i++)
        {
            string variable = "x" + (i + 1);
            bufStr = bufStr.Replace(variable, point[i].ToString().Replace(",", "."));
        }
        return bufStr;
    }
}
