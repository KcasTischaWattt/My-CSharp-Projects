using System.Windows;

namespace Peer5
{
    /// <summary>
    /// Класс, определяющий геометрические фигуры
    /// </summary>
    public class GeometryObject
    {
        /// <summary>
        /// Точки геометрической фигуры
        /// </summary>
        public Point p1, p2, p3;
        /// <summary>
        /// Длина стороны квадрата
        /// </summary>
        public double length;

        /// <summary>
        /// Конструктор линии
        /// </summary>
        /// <param name="a">Первая точка линии</param>
        /// <param name="b">Вторая точка линии</param>
        public GeometryObject(Point a, Point b)
        {
            p1 = new Point(a.X, a.Y);
            p2 = new Point(b.X, b.Y);
        }

        /// <summary>
        /// Конструктор треугольника
        /// </summary>
        /// <param name="a">Вершина треугольника</param>
        /// <param name="b">Вершина треугольника</param>
        /// <param name="c">Вершина треугольника</param>
        public GeometryObject(Point a, Point b, Point c)
        {
            p1 = new Point(a.X, a.Y);
            p2 = new Point(b.X, b.Y);
            p3 = new Point(c.X, c.Y);
        }

        /// <summary>
        /// Конструктор квадрата
        /// </summary>
        /// <param name="a">Левая нижняя точка квадрата</param>
        /// <param name="len">Длина стороны квадрата</param>
        public GeometryObject(Point a, double len)
        {
            p1 = a;
            length = len;
        }

        /// <summary>
        /// Метод для создания линии
        /// </summary>
        /// <param name="a">Первая точка линии</param>
        /// <param name="b">Вторая точка линии</param>
        /// <returns></returns>
        public static GeometryObject Line(Point a, Point b)
        {
            return new GeometryObject(a, b);
        }

        /// <summary>
        /// Метод ля создания треугольника
        /// </summary>
        /// <param name="a">Вершина треугольника</param>
        /// <param name="b">Вершина треугольника</param>
        /// <param name="c">Вершина треугольника</param>
        /// <returns></returns>
        public static GeometryObject Triangle(Point a, Point b, Point c)
        {
            return new GeometryObject(a, b, c);
        }

        /// <summary>
        /// Метод для создания квадрата
        /// </summary>
        /// <param name="a">Левая нижняя точка квадрата</param>
        /// <param name="len">Длина стороны квадрата</param>
        /// <returns></returns>
        public static GeometryObject Square(Point a, double len)
        {
            return new GeometryObject(a, len);
        }
    }
}