using System;
using System.Collections.Generic;
using System.Windows;

namespace Peer5
{
    class Cantor : Fractal
    {
        /// <summary>
        /// Расстояние между отрезками, отрисованными на разных итерациях
        /// </summary>
        public static double Distance;

        public override void Calculate(GeometryObject line, ref List<GeometryObject> cantor, int depth)
        {
            // Высчитываем 2 отрезка, получающиеся из одного, и запускаем функцию рекуррентно для них
            var yCoord = line.p1.Y - Distance;
            Point point1 = new Point(line.p1.X, yCoord);
            Point point2 = new Point((2 * line.p1.X + line.p2.X) / 3, yCoord);
            Point point3 = new Point((line.p1.X + 2 * line.p2.X) / 3, yCoord);
            Point point4 = new Point(line.p2.X, yCoord);
            cantor.Add(GeometryObject.Line(point1, point2));
            cantor.Add(GeometryObject.Line(point3, point4));
            if (depth >= MaxDepth)
                return;
            else
            {
                Calculate(GeometryObject.Line(point1, point2), ref cantor, depth + 1);
                Calculate(GeometryObject.Line(point3, point4), ref cantor, depth + 1);
            }
        }

        public override void Draw()
        {
            s_canvas.Children.Clear();
            double scale = Math.Min(s_canvas.ActualWidth / 554, s_canvas.ActualHeight / (Distance * MaxDepth));
            double dx = s_canvas.ActualWidth / 2 - (277 * scale);
            double dy = s_canvas.ActualHeight / 2 + (Distance * MaxDepth / 2 * scale);
            foreach (var i in ListOfPoints)
            {
                DrawLine(i.p1, i.p2, scale*Multiplier, dx, dy);
            };
        }
    }
}