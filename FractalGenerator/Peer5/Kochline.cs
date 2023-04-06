using System;
using System.Collections.Generic;
using System.Windows;

namespace Peer5
{
    /// <summary>
    /// Класс для отрисовки линии Коха
    /// </summary>
    class Kochline : Fractal
    {
        public override void Calculate(GeometryObject a, ref List<GeometryObject> koch, int depth)
        {
            // Сперва на основании одного отрезка высчитываем 5 точек для 4 отрезков
            Point p1 = new Point(a.p1.X, a.p1.Y);
            Point p2 = new Point(a.p1.X + (a.p2.X - a.p1.X) / 3, a.p1.Y + (a.p2.Y - a.p1.Y) / 3);
            Point p3 = new Point(0.5 * ((a.p2.X - a.p1.X) / 3) - Math.Sqrt(3) / 2 * ((a.p2.Y - a.p1.Y) / 3) + p2.X, 0.5 * ((a.p2.Y - a.p1.Y) / 3) + Math.Sqrt(3) / 2 * ((a.p2.X - a.p1.X) / 3) + p2.Y);
            Point p4 = new Point(a.p1.X + (a.p2.X - a.p1.X) / 1.5, a.p1.Y + (a.p2.Y - a.p1.Y) / 1.5);
            Point p5 = new Point(a.p2.X, a.p2.Y);
            if (depth >= MaxDepth)
            {
                koch.Add(GeometryObject.Line(p1, p2));
                koch.Add(GeometryObject.Line(p2, p3));
                koch.Add(GeometryObject.Line(p3, p4));
                koch.Add(GeometryObject.Line(p4, p5));
                return;
            }
            else
            {
                // И повторяем эту процедуру рекурсивно, пока не будет достигнут предел глубины
                Calculate(GeometryObject.Line(p1, p2), ref koch, depth + 1);
                Calculate(GeometryObject.Line(p2, p3), ref koch, depth + 1);
                Calculate(GeometryObject.Line(p3, p4), ref koch, depth + 1);
                Calculate(GeometryObject.Line(p4, p5), ref koch, depth + 1);
            }
        }

        public override void Draw()
        {
            s_canvas.Children.Clear();
            // Высчитываем масштаб и смещеения
            double scale = ScaleFactor(ListOfPoints, out double minX, out double maxX, out double minY, out double maxY);
            double dx = s_canvas.ActualWidth / 2 - (maxX + minX) / 2 * scale;
            double dy = s_canvas.ActualHeight / 2 - (maxY + minY) / 2 * scale;
            foreach (var i in ListOfPoints)
            {
                DrawLine(i.p1, i.p2, scale, dx, dy);
            }
        }
    }
}
