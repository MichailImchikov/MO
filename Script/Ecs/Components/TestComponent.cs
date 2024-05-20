using System.Collections.Generic;

namespace Client {
    struct TestComponent {
        public TestType _type;
        public bool Stretching;
        public bool DoubleStretching;
        public bool Compressions;
        public bool Reduction;
        public float Result;
        public float RealResult;
        public void Init(TestType type, float realResult = 0)
        {
            Stretching = false;
            DoubleStretching = false;
            Compressions = false;
            _type = type;
            RealResult = realResult;
        }
    }        
    enum TestType
    {
        Stretching,
        DoubleStretching,
        Compressions,
        Reduction,
        MathTest
    }
}