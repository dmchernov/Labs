using System;

namespace Izh_08_tasks.GeometryShapes
{
    public class Triangle : Shape
    {
        private double a;
        private double b;
        private double c;

        public Triangle(double a, double b, double c)
        {
            if ((a + b) <= c || (a + c) <= b || (b + c) <= a)
            {
                throw new ArgumentException("Одна из сторон треугольника больше или равна сумме двух других");
            }

            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override double Area()
        {
            double p = this.Perimeter() / 2;
            return Math.Sqrt(p * (p - this.a) * (p - this.b) * (p - this.c));
        }

        public override double Perimeter()
        {
            return this.a + this.b + this.c;
        }
    }
}
