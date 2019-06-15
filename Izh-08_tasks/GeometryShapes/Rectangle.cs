using System;

namespace Izh_08_tasks.GeometryShapes
{
    public class Rectangle : Shape
    {
        private double edgeA;
        private double edgeB;

        public Rectangle(double edgeA, double edgeB)
        {
            if (edgeA == 0 || edgeB == 0)
            {
                throw new ArgumentException("Одна из сторон равна нулю");
            }

            this.edgeA = edgeA;
            this.edgeB = edgeB;
        }

        public override double Area()
        {
            return this.edgeA * this.edgeB;
        }

        public override double Perimeter()
        {
            return (this.edgeA + this.edgeB) * 2;
        }
    }
}
