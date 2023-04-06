using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peer2
{
    // Это класс Matrix. Основной код в другом файле.
    public class Matrix
    {
        // Само тело матрицы, к которому будем обращаться по координатам M и N.
        public double[,] Body;
        // Столбцы.
        public uint M = 0;
        // Строки.
        public uint N = 0;

        /// <summary>
        /// Конструкторы:
        /// Создание матрицы размера 1 на 1 и единственным элементом ноль в случае отстутствия параметров.
        /// </summary>
        public Matrix()
        {
            Body = new double[1, 1];
            Body[0, 0] = 0.0;
            M = 1;
            N = 1;
        }

        /// <summary>
        /// Создание матрицы размера n на m.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        public Matrix(uint n, uint m)
        {
            M = m;
            N = n;
            Body = new double[n, m];
        }

        /// <summary>
        /// Создание матрицы путем чтения из файла.
        /// </summary>
        /// <param name="fileName"></param>
        public Matrix(string fileName)
        {
            // Чтение из файла.
            string[] sRows = System.IO.File.ReadAllLines(fileName);
            // Разделение прочитанных строк по разделителям.
            int columns = sRows[0].Split(' ', ';', '|', '\t').Length;
            int rows = sRows.Length;
            N = (uint)rows;
            M = (uint)columns;
            // Непосредственно создание матрицы и ее заполнение.
            Body = new double[N, M];
            for (uint i = 0; i < N; i++)
            {
                // Если ряд не удалось инициализировать - кинуть Exception.
                if (!InitRow(sRows[i], i))
                {
                    throw new Exception("");
                }
            }

        }

        /// <summary>
        /// Инициализация ряда.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public bool InitRow(string row, uint rowNumber)
        {
            // Разделение ряда по разделителям и запись этого в массив.
            string[] sNumbers = row.Split(' ', ';', '|', '\t');
            double[] elements = new double[M];
            // Если одна из строк больше остальных - вернуть false.
            if (sNumbers.Length != M)
            {
                return false;
            }
            // Инициализация строки и заполнение Body.
            for (var i = 0; i < M; i++)
            {
                if (double.TryParse(sNumbers[i], out double element))
                {
                    elements[i] = element;
                }
                else
                {
                    return false;
                }
            }
            for (var i = 0; i < M; i++)
            {
                Body[rowNumber, i] = elements[i];
            }
            return true;
        }

        /// <summary>
        /// Превращение матрицы в строку для вывода.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";
            for (var i = 0; i < N; i++)
            {
                result += "\n";
                for (var j = 0; j < M; j++)
                {
                    result += (Body[i, j] + " ");
                }
            }
            return result;
        }

        /// <summary>
        /// Перегрузка оператора сложения, которое происходит по элементам.
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            Matrix matrRes = new Matrix(m1.N, m1.M);
            for (var i = 0; i < m1.N; i++)
            {
                for (var j = 0; j < m1.M; j++)
                {
                    matrRes.Body[i, j] = m1.Body[i, j] + m2.Body[i, j];
                }
            }
            return matrRes;
        }

        /// <summary>
        /// Перегрузка оператора вычитания.
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            Matrix matrRes = new Matrix(m1.N, m1.M);
            for (var i = 0; i < m1.N; i++)
            {
                for (var j = 0; j < m1.M; j++)
                {
                    matrRes.Body[i, j] = m1.Body[i, j] - m2.Body[i, j];
                }
            }
            return matrRes;
        }

        /// <summary>
        /// Поиск следа матрицы. Возращается матрица размером 1 на 1 и единствекнным элементом - значением следа.
        /// </summary>
        /// <returns></returns>
        public Matrix Trace()
        {
            Matrix resultMatr = new Matrix();
            double result = 0;
            for (var i = 0; i < N; i++)
            {
                result += Body[i, i];
            }
            resultMatr.Body[0, 0] = result;
            return resultMatr;
        }

        /// <summary>
        /// Перегрузка оператора умножения числа на матрицу.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="m1"></param>
        /// <returns></returns>
        public static Matrix operator *(double a, Matrix m1)
        {
            Matrix matrRes = new Matrix(m1.N, m1.M);
            for (var i = 0; i < m1.N; i++)
            {
                for (var j = 0; j < m1.M; j++)
                {
                    matrRes.Body[i, j] = m1.Body[i, j] * a;
                }
            }
            return matrRes;
        }

        /// <summary>
        /// В случаве умножения матрицы на число - умножить число на матрицу.
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix m1, double a)
        {
            return a * m1;
        }

        /// <summary>
        /// Перегрузка оператора умножения матрицы на матрицу по определению умножения.
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix matrRes = new Matrix(m1.N, m2.M);
            // Умножение по определению.
            for (var i = 0; i < m1.N; i++)
            {
                for (var j = 0; j < m2.M; j++)
                {
                    for (var q = 0; q < m1.M; q++)
                    {
                        matrRes.Body[i, j] += (m1.Body[i, q] * m2.Body[q, j]);
                    }
                }
            }
            return matrRes;
        }

        /// <summary>
        /// Проверка матрицы на квадратность.
        /// </summary>
        /// <returns></returns>
        public bool IsSquare()
        {
            return M == N;
        }

        /// <summary>
        /// Транспонирование матрицы по определению.
        /// </summary>
        /// <returns></returns>
        public Matrix Transpose()
        {
            Matrix matrRes = new Matrix(M, N);
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < M; j++)
                {
                    matrRes.Body[j, i] = Body[i, j];
                }
            }
            return matrRes;
        }

        /// <summary>
        /// Решение СЛАУ методом Гаусса. Создается копия матрицы, потом выбирается первый ненулевой ведущий элемент.
        /// Далее под ним все зануляется, и ищется следующий ненулевой ведущий элемент. И так происходит, пока не будет достигнут ступенчатый вид.
        /// </summary>
        /// <returns></returns>
        public Matrix GaussianElimination()
        {
            // Создание копии матрицы.
            Matrix matrRes = Copy();
            for (var i = 0; i < N; i++)
            {
                // Сортировка для поиска первого ненулевого элемента.
                matrRes = matrRes.Sort();
                for (var j = 0; j < M; j++)
                {
                    if (matrRes.Body[i,j] != 0)
                    {
                        // Зануление всех элементов столбца под ненулевым элементом.
                        matrRes.MakeZeroesBelow(i, j);
                        break;
                    }
                }
            }
            return matrRes;
        }

        /// <summary>
        /// Сортировка матрицы для поиска ненулевого элемента.
        /// </summary>
        /// <returns></returns>
        private Matrix Sort()
        {
            // Создание копии матрицы.
            Matrix matrRes = Copy();
            // Смещение относительно главной диагонали в случае, если все элементы в ней - нули.
            int shift = 0;
            // Для матрицы замера 1*М метод Гаусса бессмыслен
            if (N == 1)
            {
                return matrRes;
            }
            // Проход по главной диагонали со смещением.
            for (var j = 0; j < Math.Min(N, M); j++)
            {
                if (matrRes.Body[j - shift, j] != 0)
                {
                    // Если найден ненулевой элемент, он становится ведущим, и  на него делится вся строка - для удобства.
                    matrRes.DivideRow(j - shift, matrRes.Body[j - shift, j]);
                }
                if (matrRes.Body[j - shift, j] == 0)
                {
                    // Если элемент нулевой, то ищем под ним первый ненулевой элемент.
                    for (var k = j - shift + 1; k < N; k++)
                    {
                        if (matrRes.Body[k, j] != 0)
                        {
                            // Если ненулевой элемент найден, то делим всю строку на него, и меняем эту строку со строкой с нулевым элементом.
                            matrRes.DivideRow(k, matrRes.Body[k, j]);
                            matrRes.SwapRows(j - shift, k);
                            break;
                        }
                        // Если под элементом не нашлось ненулевых - увеличиваем шаг смещения на 1.
                        if (k == (N - 1))
                        {
                            shift++;
                        }
                    }
                }
            
            }
            return matrRes;
        }

        /// <summary>
        /// Замена строк в матрице.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="k"></param>
        private void SwapRows(int i, int k)
        {
            for (var j = 0; j < M; j++)
            {
                // Стандартная замена через третью переменную.
                var temp = Body[i, j];
                Body[i, j] = Body[k, j];
                Body[k, j] = temp;
            }
        }

        /// <summary>
        /// Деление строки на число.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="divider"></param>
        private void DivideRow(int row, double divider)
        {
            for (var col = 0; col < M; col++)
            {
                Body[row, col] /= divider;
            }
        }

        /// <summary>
        /// Зануление всех элементов столбца ниже указанного.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void MakeZeroesBelow(int row, int col)
        {
            for (var i = row + 1; i < N; i++)
            {
                // Если первый элемент строки не нулевой - вычесть из строки заданную строку, умноженную на первый элемент.
                if (Body[i, col] != 0)
                {
                    // Первый элемент строки, на который будет умножать вычитаемая строка.
                    double divider = Body[i, col] / Body[row, col];

                    for (var j = 0; j < M; j++)
                    {
                        // Вычитание строки, умноженной на первый элемент.
                        Body[i, j] -= Body[row, j] * divider;
                    }
                }
            }
        }

        /// <summary>
        /// Создание копии матрицы по элементам.
        /// </summary>
        /// <returns></returns>
        public Matrix Copy()
        {
            Matrix result = new Matrix(N, M);
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < M; j++)
                {

                    result.Body[i, j] = Body[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Удаление колонки из матрицы.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private Matrix CreateMatrixWithoutColumn(int column)
        {
            var matrRes = new Matrix(N, M - 1);
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < M - 1; j++)
                {
                    // Если столбец, в котором стоит элемент, по индексу меньше удаляемой колонки - оставить, иначе - сместить на один влево.
                    if (j < column)
                    {
                        matrRes.Body[i, j] = Body[i, j];
                    }
                    else
                    {
                        matrRes.Body[i, j] = Body[i, j + 1];
                    }
                }
            }
            return matrRes;
        }

        /// <summary>
        /// Аналогично с вышеописанным методом - удалаем строку.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Matrix CreateMatrixWithoutRow(int row)
        {
            var matrRes = new Matrix(N - 1, M);
            for (var i = 0; i < N - 1; i++)
            {
                for (var j = 0; j < M; j++)
                {
                    if (i < row)
                    {
                        matrRes.Body[i, j] = Body[i, j];
                    }
                    else
                    {
                        matrRes.Body[i, j] = Body[i + 1, j];
                    }
                }
            }
            return matrRes;
        }

        /// <summary>
        /// Поиск детерминанта полстепенным приведением матрицы к матрице размера 2*2.
        /// </summary>
        /// <returns></returns>
        public double DoubDeterminant()
        {
            if (N == 2)
            {
                // Подсчет определителя для матрицы 2*2
                return Body[0, 0] * Body[1, 1] - Body[0, 1] * Body[1, 0];
            }
            double result = 0;
            // Удаление одной строки и столбца и рекурсивный вызов этого же метода.
            for (var j = 0; j < N; j++)
            {
                result += (j % 2 == 1 ? 1 : -1) * Body[1, j] *
                    CreateMatrixWithoutColumn(j).CreateMatrixWithoutRow(1).DoubDeterminant();
            }
            return result;
        }

        /// <summary>
        /// Так как метод в Мэйне не умеет возвращать числа, только матрицы,
        /// надо вернуть матрицу размера 1 на 1 и единственным элементом - значением определителя.
        /// </summary>
        /// <returns></returns>
        public Matrix MatrDeterminant()
        {
            Matrix matrRes = new Matrix();
            matrRes.Body[0, 0] = DoubDeterminant();
            return matrRes;
        }
    }
}
