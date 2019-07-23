using System;

namespace Izh_08_tasks.GeometryShapes
{
    public class Circle : Shape
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public override double Area()
        {
            return Math.PI * this.radius * this.radius;
        }

        public override double Perimeter()
        {
            return 2 * Math.PI * this.radius;
        }
    }
}
