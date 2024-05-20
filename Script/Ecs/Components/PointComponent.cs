using System.Collections.Generic;

namespace Client {
    struct PointComponent 
    {
        public List<Point> Polyhedron;
        public Point _lowPoint;
        public Point _highPoint;
        public Point _middlePoint;
        public Point _centerPoint;
        public Point _reflectionPoint;
        public void InitPoint()
        {
            _lowPoint = new Point();
            _highPoint = new Point();
            _middlePoint = new Point();
            _centerPoint = new Point();
            _reflectionPoint = new Point();
        }
    }
}