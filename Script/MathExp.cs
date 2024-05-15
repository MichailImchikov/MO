using System.Collections.Generic;
using Client;
using org.matheval;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
public class MathExpression
{

    private string _expression;
    private int _entity;
    public MathExpression(string expression, int entity)
    {
        _expression = expression;
        _entity = entity;
    }
    public string GetExpression()
    {
        return _expression;
    }
    public float Calculation(Point point)
    {
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
                GameState.Instance.ecsWorld.GetPool<StopCountingEvent>().Add(_entity).exception="Валера сделай нормальные подсчеты";
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
            bufStr = bufStr.Replace(variable, "(" + point[i].ToString().Replace(",", ".") + ")");
        }
        return bufStr;
    }
}
