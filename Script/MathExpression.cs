using System.Collections.Generic;
using Client;
using org.matheval;
using UnityEngine;
public class MathExpression1
{
    //private string _expression;
    //public MathExpression(string expression)
    //{
    //    _expression = expression;
    //}
    //public float Calculation(Point point)
    //{

    //    foreach (var value in point.Coordinates)
    //    {
    //        if(value>=float.MaxValue||value<=float.MinValue)
    //        {
    //            Debug.Log("’ьюстон у нас проблемы"); return 0;
    //        }
    //    }
    //    int h = 0;
    //    Expression expression = new Expression(ReturnStringExpression(point.Coordinates));
    //    var result = expression.Eval();
    //    return float.Parse(result.ToString());
    //    //DataTable table = new DataTable();
    //    //var result = table.Compute(ReturnStringExpression(point.Coordinates), "");
    //    //return float.Parse(result.ToString());
    //}
    //public string ReturnStringExpression(List<float> point)
    //{
    //    string bufStr = _expression;
    //    for (int i = 0; i < point.Count; i++)
    //    {
    //        string variable = "x" + (i + 1);
    //        bufStr = bufStr.Replace(variable, "(" + point[i].ToString().Replace(",", ".") + ")");
    //    }
    //    return bufStr;
    //}
}
