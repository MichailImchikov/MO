using Client;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculatorCanvasMB : CanvasMB
{
    [SerializeField] private TextMeshProUGUI Input;
    [SerializeField] private TextMeshProUGUI Answer;
    int Index;
    public void AddChar(string element)
    {
        Input.text += element;
    }

    public void _Start(bool MinMax)
    {

        if (StaplesVariables(Input.text) == 0)
        {
            var entity = GameState.Instance.ecsWorld.NewEntity();
            ref var _nelderComponent = ref GameState.Instance.ecsWorld.GetPool<NelderComponent>().Add(entity);
            Client.Point point = new Client.Point();
            _nelderComponent._compressionRatio = 0.5f;
            _nelderComponent._reflectionRatio = 1;
            _nelderComponent._stretchingRatio = 2;
            string expression = Input.text;
            _nelderComponent.Degree = new();
            for (int i = 0; i < point.Coordinates.Count; i++)
            {
                string variable = "x" + (i + 1);
                expression = expression.Replace(variable, "(" + variable + ")");
            }
            for (int i = 0; i < expression.Length; i++)
            {
                string resultStr = "";
                int Staple = 0;
                int left = i - 1;
                string leftStr = "";
                int right = i + 1;
                string rightStr = "";
                if (expression[i] == '^')
                {
                    if (Char.IsDigit(expression[left]))
                    {
                        while (Char.IsDigit(expression[left]))
                        {
                            leftStr += expression[left];
                            left--;
                            if (left == -1) break;
                        }
                    }
                    else
                    {
                        do
                        {
                            leftStr += expression[left];
                            if (expression[left] == ')') Staple--;
                            if (expression[left] == '(') Staple++;
                            left--;
                        } while (Staple != 0);
                    }
                    if (Char.IsDigit(expression[right]))
                    {
                        while (Char.IsDigit(expression[right]))
                        {
                            rightStr += expression[right];
                            right++;
                            if (right == expression.Length) break;
                        }
                    }
                    else
                    {
                        do
                        {
                            rightStr += expression[right];
                            if (expression[right] == '(') Staple++;
                            if (expression[right] == ')') Staple--;
                            right++;
                        } while (Staple != 0);
                    }
                    string Left = "";
                    for (int l = 0; l < leftStr.Length; l++)
                    {
                        Left += leftStr[leftStr.Length - 1 - l];
                    }
                    resultStr = Left + '^' + rightStr;
                    for (int j = 0; j < point.Coordinates.Count; j++)
                    {
                        string variable = "x" + (j + 1);
                        resultStr = resultStr.Replace(variable, point.Coordinates[j].ToString().Replace(",", "."));
                    }
                    _nelderComponent.Degree.Add(resultStr);
                }
            }
            if (MinMax) _nelderComponent.expression = new(Input.text, entity, _nelderComponent.Degree);
            else _nelderComponent.expression = new("-(" + Input.text + ")", entity, _nelderComponent.Degree);
            GameState.Instance.answer = Answer;
            //Count(Input.text);
            var canvas = GameState.Instance.GetCanvas<ChartCanvasMB>() as ChartCanvasMB;
            canvas.ClearLists();
        }
        else if (StaplesVariables(Input.text) == 1)
        {
            Answer.text = "Неправильные скобки";
        }
        else if (StaplesVariables(Input.text) == 2)
        {
            Answer.text = "Неправильный x";
        }
        else if (StaplesVariables(Input.text) == 3)
        {
            Answer.text = "Конец выражения неправелен";
        }
        else if (StaplesVariables(Input.text) == 4)
        {
            Answer.text = "Два оператора подряд";
        }
        else if (StaplesVariables(Input.text) == 5)
        {
            Answer.text = "Закрывающая скобка и неправильный следующий символ";
        }
        else if (StaplesVariables(Input.text) == 6)
        {
            Answer.text = "Нет х";
        }
    }
    int StaplesVariables(string expression)
    {
        if (!expression.Contains("x")) return 6;
        int Staple = 0;
        Count(expression);
        if (expression[expression.Length - 1] == '^' || expression[expression.Length - 1] == '*' || expression[expression.Length - 1] == '+' || expression[expression.Length - 1] == '-' || expression[expression.Length - 1] == '/') return 3;
        for (int index = 0; index < expression.Length; index++)
        {
            if (expression[index] == '(') Staple++;
            else if (expression[index] == ')') Staple--;
            if (expression[index] == 'x')
            {
                if (index == expression.Length - 1) return 2;
                int indexX = index + 1;
                string check = "";
                while (expression[indexX] != '^' && expression[indexX] != '(' && expression[indexX] != ')' && expression[indexX] != '*' && expression[indexX] != '+' && expression[indexX] != '-' && expression[indexX] != '/')
                {
                    check += expression[indexX];
                    if (indexX == expression.Length - 1) break;
                    indexX++;
                }
                if (int.TryParse(check, out int numericValue))
                {
                    if (numericValue > Index) return 2;
                }
                else return 2;
            }
        }
        for (int index = 0; index < expression.Length - 1; index++)
        {
            if (expression[index].ToString() + expression[index + 1].ToString() == "()") return 1;
            if (expression[index] == ')' && (Char.IsDigit(expression[index + 1]) || expression[index + 1] == 'x')) return 5;
            if ((expression[index] == '^' || expression[index] == '*' || expression[index] == '+' || expression[index] == '-' || expression[index] == '/') && (expression[index + 1] == '^' || expression[index + 1] == '*' || expression[index + 1] == '+' || expression[index + 1] == '-' || expression[index + 1] == '/')) return 4;
        }
        if (Staple == 0) return 0;
        else return 1;
    }
    public void Count(string expression)
    {
        GameState.Instance.CountVariables = 0;
        bool flag = true;
        Index = 1;
        while (flag)
        {
            string variable = "x" + Index.ToString();
            if (expression.Contains(variable))
            {
                GameState.Instance.CountVariables++;
                Index++;
            }
            else
            {
                flag = false;
            }
        }
    }
    public void ClearLostChar()
    {
        if(Input.text.Length>0)
        {
            var str = Input.text.Remove(Input.text.Length - 1);
            Input.text = str;
        }
    }
    public void ClearFull()
    {
        Input.text = "";
    }
    public void OpenChart()
    {
        GameState.Instance.GetCanvas<ChartCanvasMB>().Open();
    }
    public void OpenMenu()
    {
        GameState.Instance.GetCanvas<MenuCanvasMB>().Open();
    }
}
