using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine.Assertions;
using org.matheval.Functions;
using System;
namespace Client {
    sealed class StopTestSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<TestComponent>> _filter1 = default;
        readonly EcsPoolInject<TestComponent> _testCompoent = default;
        [Test]
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter1.Value)
            {//да знаю реалиовал очень очень ќќчень плохо, на работе запара
                ref var testComponent = ref _testCompoent.Value.Get(entity);
                if(testComponent._type==TestType.Stretching)
                {
                    Assert.AreEqual(testComponent.Stretching, true);
                }
                if (testComponent._type == TestType.DoubleStretching)
                {
                    Assert.AreEqual(testComponent.DoubleStretching, true);
                }
                if (testComponent._type == TestType.Reduction)
                {
                    Assert.AreEqual(testComponent.Reduction, true);
                }
                if (testComponent._type == TestType.Compressions)
                {
                    Assert.AreEqual(testComponent.Compressions, true);
                }
                TestCalculator("x1+x2", new Point(new List<float> { 1, 1 }), 2);
                TestCalculator("x1^2+x2^2", new Point(new List<float> { 2, 2 }), 8);
                TestCalculator("x1*x2-1", new Point(new List<float> { 2, 2 }), 3);
                if(testComponent._type==TestType.MathTest)
                {
                    Assert.IsTrue(Math.Abs(testComponent.Result - testComponent.RealResult) < 1);
                }
                if (testComponent._type != TestType.MathTest)
                {
                    _testCompoent.Value.Del(entity);
                }
            }
        }
        public void TestCalculator(string Expression,Point point,float TestAnswer)
        {
            var expression = new MathExpression(Expression, 0,null);
            var answer = expression.Calculation(point);
            Assert.AreEqual(answer, TestAnswer);
        }
    }
}