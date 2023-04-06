using System;
using System.Collections.Generic;
using System.Windows;

namespace Peer5
{
    /// <summary>
    /// Класс для отрисовки пифагорова дерева
    /// </summary>
    public class Pifagor : Fractal
    {
        /// <summary>
        /// Угол наклона первого отрезка
        /// </summary>
        public static double AngleLeft;
        /// <summary>
        /// Угол наклона второго отрезка
        /// </summary>
        public static double AngleRight;
        /// <summary>
        /// Коэффициент, определяющий отношение длин отрезков на текущей и последующей итерации
        /// </summary>
        public static double Length;

        public override void Calculate(GeometryObject a, ref List<GeometryObject> pifagor, int depth)
        {
            // Сперва добавляем переданный отрезок в список
            pifagor.Add(a);
            // Потом рассчитываем 2 прямые на основании данной
            Point p2 = new Point(a.p2.X, a.p2.Y);
            Point p3 = new Point(Length * (a.p2.X - a.p1.X) * Math.Cos(AngleLeft) - Length * (a.p2.Y - a.p1.Y) * Math.Sin(AngleLeft) + a.p2.X, Length * (a.p2.Y - a.p1.Y) * Math.Cos(AngleLeft) + Length * (a.p2.X - a.p1.X) * Math.Sin(AngleLeft) + a.p2.Y);
            Point p4 = new Point(Length * (a.p2.X - a.p1.X) * Math.Cos(-AngleRight) - Length * (a.p2.Y - a.p1.Y) * Math.Sin(-AngleRight) + a.p2.X, Length * (a.p2.Y - a.p1.Y) * Math.Cos(-AngleRight) + Length * (a.p2.X - a.p1.X) * Math.Sin(-AngleRight) + a.p2.Y);
            var p2p3 = GeometryObject.Line(p2, p3);
            var p2p4 = GeometryObject.Line(p2, p4);
            if (depth >= MaxDepth)
                return;
            else
            {
                // Рекурсивно повторяем алгоритм для каждой прямой, пока не будет достигнут лимит итераций
                Calculate(p2p3, ref pifagor, depth + 1);
                Calculate(p2p4, ref pifagor, depth + 1);
            }
        }

        public override void Draw()
        {
            s_canvas.Children.Clear();
            // Вычисляем масштаб, а так же смещения
            double scale = ScaleFactor(ListOfPoints, out double minX, out double maxX, out double minY, out double maxY);
            double dx = s_canvas.ActualWidth / 2 - (maxX + minX) / 2 * scale;
            double dy = s_canvas.ActualHeight / 2 - (maxY + minY) / 2 * scale;
            // Рисуем каждую линию с учетом высчитанного
            foreach (var i in ListOfPoints)
            {
                DrawLine(i.p1, i.p2, scale*Multiplier, dx, dy);
            };
        }
    }
}
