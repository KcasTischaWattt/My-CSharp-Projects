using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Peer5
{
    /// <summary>
    /// Класс для отрисовки Ковра Серпинского
    /// </summary>
    class SerpCarp : Fractal
    {
        public override void Calculate(GeometryObject a, ref List<GeometryObject> serp, int depth)
        {
            // Добавляем в список середину квадрата
            serp.Add(GeometryObject.Square(new Point(a.p1.X + a.length / 3, a.p1.Y + a.length / 3), a.length / 3));
            if (depth >= MaxDepth)
                return;
            else
            {
                // Для каждого участка ковра рекурсивно запускаем метод
                Calculate(GeometryObject.Square(new Point(a.p1.X, a.p1.Y), a.length / 3), ref serp, depth + 1);
                Calculate(GeometryObject.Square(new Point(a.p1.X + a.length / 3, a.p1.Y), a.length / 3), ref serp, depth + 1);
                Calculate(GeometryObject.Square(new Point(a.p1.X, a.p1.Y + a.length / 3), a.length / 3), ref serp, depth + 1);
                Calculate(GeometryObject.Square(new Point(a.p1.X + 2 * a.length / 3, a.p1.Y), a.length / 3), ref serp, depth + 1);
                Calculate(GeometryObject.Square(new Point(a.p1.X, a.p1.Y + 2 * a.length / 3), a.length / 3), ref serp, depth + 1);
                Calculate(GeometryObject.Square(new Point(a.p1.X + a.length / 3, a.p1.Y + 2 * a.length / 3), a.length / 3), ref serp, depth + 1);
                Calculate(GeometryObject.Square(new Point(a.p1.X + 2 * a.length / 3, a.p1.Y + a.length / 3), a.length / 3), ref serp, depth + 1);
                Calculate(GeometryObject.Square(new Point(a.p1.X + 2 * a.length / 3, a.p1.Y + 2 * a.length / 3), a.length / 3), ref serp, depth + 1);
            }
        }

        public override void Draw()
        {
            s_canvas.Children.Clear();
            double scale = Math.Min(s_canvas.ActualWidth / 100, s_canvas.ActualHeight / 100);
            double dx = s_canvas.ActualWidth / 2 - 50 * scale;
            double dy = s_canvas.ActualHeight / 2 - 50 * scale;
            foreach (var i in ListOfPoints)
            {
                if (ListOfPoints.IndexOf(i) == 0)
                    // Если квадрат самый первый - рисуем синим, остальные - белым
                    DrawSquare(i.p1.X * scale + dx, i.p1.Y * scale + dy, i.length * scale, Colors.Blue);
                else
                    DrawSquare(i.p1.X * scale + dx, i.p1.Y * scale + dy, i.length * scale , Colors.White);
            }
        }
    }
}