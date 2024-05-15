
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using UnityEngine;
namespace Client
{
    //public class PointMath:Point
    //{
    //    public float _valueFunc;
    //    public PointMath(Point point, float valueFunc) : base(point.Coordinates)
    //    {
    //        _valueFunc = valueFunc;
    //    }
    //}
    public class Point
    {
        public List<float> Coordinates = new();
        public Point(List<float> Coordinates)
        {
            this.Coordinates = Coordinates;
        }
        public Point GetCoarseningPoint()
        {
            List<float> bufList = new();
            foreach(var value in Coordinates)
            {
                bufList.Add(MathF.Round(value * 10f) / 10f);
            }
            return new Point(bufList);
        }
        public Point(float min,float max)
        {
            for (int i = 0; i < GameState.Instance.CountVariables; i++)
            {
                Coordinates.Add(UnityEngine.Random.Range(min,max));
            }
        }
        public Point()
        {
            for(int i=0;i<GameState.Instance.CountVariables;i++)
            {
                Coordinates.Add(0);
            }
        }
        public static Point operator +(Point p1, Point p2)
        {
            if (p1.Coordinates.Count == GameState.Instance.CountVariables && p2.Coordinates.Count == GameState.Instance.CountVariables)//защита от долбаеба
            {
                var result = p1.Coordinates.Zip(p2.Coordinates, (x, y) => x + y).ToList();
                return new Point(result);
            }
            return null;
        }
        public static Point operator -(Point p1, Point p2)
        {
            if (p1.Coordinates.Count == GameState.Instance.CountVariables && p2.Coordinates.Count == GameState.Instance.CountVariables)//защита от долбаеба
            {
                var result = p1.Coordinates.Zip(p2.Coordinates, (x, y) => x - y).ToList();
                return new Point(result);
            }
            return null;
        }
        public static Point operator *(Point p1, Point p2)
        {
            if (p1.Coordinates.Count == GameState.Instance.CountVariables && p2.Coordinates.Count == GameState.Instance.CountVariables)//защита от долбаеба
            {
                var result = p1.Coordinates.Zip(p2.Coordinates, (x, y) => x * y).ToList();
                return new Point(result);
            }
            return null;
        }
        public static Point operator /(Point p1, Point p2)
        {
            if (p1.Coordinates.Count == GameState.Instance.CountVariables && p2.Coordinates.Count == GameState.Instance.CountVariables)//защита от долбаеба
            {
                var result = p1.Coordinates.Zip(p2.Coordinates, (x, y) => x / y).ToList();
                return new Point(result);
            }
            return null;
        }
        public static Point operator /(Point p1, float value)
        {
            if (p1.Coordinates.Count == GameState.Instance.CountVariables)//защита от долбаеба
            {
                List<float> result = new();
                foreach (var point in p1.Coordinates)
                {
                    result.Add(point / value);
                }
                return new Point(result);
            }
            return null;
        }
        public static Point operator *(float value, Point p1)
        {
            if (p1.Coordinates.Count == GameState.Instance.CountVariables)//защита от долбаеба
            {
                List<float> result = new();
                foreach (var point in p1.Coordinates)
                {
                    result.Add(point * value);
                }
                return new Point(result);
            }
            return null;
        }
    }
}
