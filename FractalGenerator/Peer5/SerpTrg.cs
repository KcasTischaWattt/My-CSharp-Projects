using System;
using System.Collections.Generic;
using System.Windows;

namespace Peer5
{
    /// <summary>
    /// Класс для отрисовки треугольника Серпинского
    /// </summary>
    class SerpTrg : Fractal
    {
        public override void Calculate(GeometryObject a, ref List<GeometryObject> trg, int depth)
        {
            // Рассчитываем и добавляем в список центральный треугольник
            Point point12 = new Point((a.p1.X + a.p2.X) / 2, (a.p1.Y + a.p2.Y) / 2);
            Point point23 = new Point((a.p3.X + a.p2.X) / 2, (a.p3.Y + a.p2.Y) / 2);
            Point point31 = new Point((a.p3.X + a.p1.X) / 2, (a.p3.Y + a.p1.Y) / 2);
            trg.Add(GeometryObject.Triangle(point12, point23, point31));
            if (depth >= MaxDepth)
                return;
            else
            {
                // Запускаем расчет рекурсовно для каждого из полцчившихся треугольников
                Calculate(GeometryObject.Triangle(a.p1, point12, point31), ref trg, depth + 1);
                Calculate(GeometryObject.Triangle(a.p2, point23, point12), ref trg, depth + 1);
                Calculate(GeometryObject.Triangle(a.p3, point31, point23), ref trg, depth + 1);
            }
        }

        public override void Draw()
        {
            s_canvas.Children.Clear();
            double scale = Math.Min(s_canvas.ActualWidth / 100, s_canvas.ActualHeight / (100 * Math.Sin(Math.PI / 3)));
            double dx = s_canvas.ActualWidth / 2 - 50 * scale;
            double dy = s_canvas.ActualHeight / 2 - 50 * Math.Sin(Math.PI / 3) * scale;
            foreach (var i in ListOfPoints)
            {
                DrawLine(i.p1, i.p2, scale*Multiplier, dx, dy);
                DrawLine(i.p2, i.p3, scale*Multiplier, dx, dy);
                DrawLine(i.p3, i.p1, scale*Multiplier, dx, dy);
            }
        }
    }
}