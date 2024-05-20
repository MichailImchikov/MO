using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.TestTools;
//using NUnit.Framework;
using System.Collections;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine.Assertions;
namespace Client {
    sealed class InitTest : IEcsInitSystem {
        public void Init (IEcsSystems systems) {
            var _world = systems.GetWorld();
            Point[] points = new Point[4] { new Point(new List<float> { 1, 3 }), new Point(new List<float> { -1, 2 }), new Point(new List<float> { 3, 0 }), new Point(new List<float> { 1, 3 }) };
            NewTypeTest(_world, TestType.Stretching, "2*x1^2+3*x2^2-1", points);
            points = new Point[4] { new Point(new List<float> { 3, 5 }), new Point(new List<float> { -3, 2 }), new Point(new List<float> { -5, 1 }), new Point(new List<float> { 1, 3 }) };
            NewTypeTest(_world, TestType.DoubleStretching, "5*x2^2+x1^2+10", points);
            points = new Point[4] { new Point(new List<float> { 2, 7 }), new Point(new List<float> { -2, -2 }), new Point(new List<float> { 3, 3 }), new Point(new List<float> { -4, 5 }) };
            NewTypeTest(_world, TestType.Compressions, "-3*x2^3+3*x1^2-7", points);
            points = new Point[4] { new Point(new List<float> { 1, 3 }), new Point(new List<float> { 9, -2 }), new Point(new List<float> { -5, 3 }), new Point(new List<float> { 2, -7 }) };
            NewTypeTest(_world, TestType.Reduction, "x1^2+6*x2^4", points);
            MathTest(_world, "100*(x2-x1^2)^2+(1-x1)^2", 0);
            MathTest(_world, "x1^2+x2^2", 0);
            MathTest(_world, "(x1+2*x2-7)^2 + (2*x1+x2-5)^2", 0);
        }
        public int NewTypeTest(EcsWorld _world, TestType testType, string expression, Point[] points)
        {
            var entity = _world.NewEntity();
            ref var _nelderComponent = ref _world.GetPool<NelderComponent>().Add(entity);
            _nelderComponent._compressionRatio = 0.5f;
            _nelderComponent._reflectionRatio = 1;
            _nelderComponent._stretchingRatio = 2;
            _nelderComponent.expression = new(expression, entity, null);
            ref var _pointCompoent = ref _world.GetPool<PointComponent>().Add(entity);
            _pointCompoent._lowPoint = points[0];
            _pointCompoent._middlePoint = points[1];
            _pointCompoent._highPoint = points[2];
            _pointCompoent._centerPoint = points[3];
            _world.GetPool<TestComponent>().Add(entity).Init(testType);
            return entity;
        }
        public int MathTest(EcsWorld _world, string expression, float realAnswer)
        {
            
            var entity = _world.NewEntity();
            ref var _nelderComponent = ref _world.GetPool<NelderComponent>().Add(entity);
            _nelderComponent._compressionRatio = 0.5f;
            _nelderComponent._reflectionRatio = 1;
            _nelderComponent._stretchingRatio = 2;
            _nelderComponent.expression = new(expression, entity, null);
            _world.GetPool<TestComponent>().Add(entity).Init(TestType.MathTest, realAnswer);
            return entity;
        }
    }
}