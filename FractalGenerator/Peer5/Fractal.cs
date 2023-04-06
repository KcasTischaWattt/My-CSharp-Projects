using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Peer5
{
    /// <summary>
    /// Базовый класс
    /// </summary>
    public abstract class Fractal
    {
        /// <summary>
        /// Глубина отрисовки
        /// </summary>
        public static int MaxDepth;
        /// <summary>
        /// Непосредсвенно canvas, на котором рисуют
        /// </summary>
        protected static Canvas s_canvas;
        /// <summary>
        /// Множитель приближения(остался в мечтах)
        /// </summary>
        public static int Multiplier;
        /// <summary>
        /// Массив геометрических объектов для отрисовки
        /// </summary>
        public List<GeometryObject> ListOfPoints = new List<GeometryObject>();
        /// <summary>
        /// Перегруженный индексатор, возвращающий объект из массива ListOfPoints
        /// </summary>
        /// <param name="index">Индекс элемента в массиве</param>
        /// <returns></returns>
        public GeometryObject this[int index]
        {
            get => ListOfPoints[index];
            set => ListOfPoints[index] = value;
        }
        /// <summary>
        /// Свойство для доступа к канвасу
        /// </summary>
        public static Canvas Canvas 
        { 
            get => s_canvas; 
            set => s_canvas = value; 
        }

        /// <summary>
        /// Метод расчета фрактала
        /// </summary>
        /// <param name="obj">Геометрический объект, на основе которого происходит расчет</param>
        /// <param name="lst">Список объестов фрактала, уже рассчитанных</param>
        /// <param name="depth">Текущая итерация</param>
        public abstract void Calculate(GeometryObject obj, ref List<GeometryObject> lst, int depth);

        /// <summary>
        /// Метод отрисовки фрактала
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Метод, рисующий линию
        /// </summary>
        /// <param name="p1">Первая точка</param>
        /// <param name="p2">Вторая точка</param>
        /// <param name="scale">Масштаб</param>
        /// <param name="dx">Смещение по x</param>
        /// <param name="dy">Смещение по y</param>
        public void DrawLine(Point p1, Point p2, double scale, double dx, double dy)
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.X1 = p1.X * scale + dx;
            line.X2 = p2.X * scale + dx;
            line.X1 = (line.X1 - Canvas.ActualWidth / 2) * Multiplier + Canvas.ActualWidth / 2;
            line.X2 = (line.X2 - Canvas.ActualWidth / 2) * Multiplier + Canvas.ActualWidth / 2;
            line.Y1 = s_canvas.ActualHeight - p1.Y * scale - dy;
            line.Y2 = s_canvas.ActualHeight - p2.Y * scale - dy;
            line.Y1 = (line.Y1 - Canvas.ActualHeight / 2) * Multiplier + Canvas.ActualHeight / 2;
            line.Y2 = (line.Y2 - Canvas.ActualHeight / 2) * Multiplier + Canvas.ActualHeight / 2;
            s_canvas.Children.Add(line);
        }

        /// <summary>
        /// Метод для отрисовки квадрата
        /// </summary>
        /// <param name="x">x-координата левой нижней точки</param>
        /// <param name="y">y-координата левой нижней точки</param>
        /// <param name="length">Длина стороны</param>
        /// <param name="color">Цвет</param>
        public void DrawSquare(double x, double y, double length, Color color)
        {
            Rectangle GeometryObject = new Rectangle();
            GeometryObject.Width = GeometryObject.Height = length;
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = color;
            GeometryObject.Fill = brush;
            s_canvas.Children.Add(GeometryObject);
            Canvas.SetTop(GeometryObject, y);
            Canvas.SetLeft(GeometryObject, x);
        }

        /// <summary>
        /// Метод, рассчитывающий масштаб фрактала для масштабирования
        /// </summary>
        /// <param name="lst">Список точек объекта</param>
        /// <param name="minX">Точка с минимальной x-координатой из найденных</param>
        /// <param name="maxX">Точка с максимальной x-координатой из найденных</param>
        /// <param name="minY">Точка с минимальной y-координатой из найденных</param>
        /// <param name="maxY">Точка с максимальной y-координатой из найденных</param>
        /// <returns></returns>
        public double ScaleFactor(List<GeometryObject> lst, out double minX, out double maxX, out double minY, out double maxY)
        {
            minX = lst[0].p1.X;
            maxX = minX;
            minY = lst[0].p1.Y;
            maxY = minY;
            foreach (var k in lst)
            {
                minX = k.p2.X < minX ? k.p2.X : minX;
                minY = k.p2.Y < minY ? k.p2.Y : minY;
                maxX = k.p2.X > maxX ? k.p2.X : maxX;
                maxY = k.p2.Y > maxY ? k.p2.Y : maxY;
            }
            double width = maxX - minX;
            double height = maxY - minY;
            return Math.Min(s_canvas.ActualHeight / height, s_canvas.ActualWidth / width);
        }
    }
}
