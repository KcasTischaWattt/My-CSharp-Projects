using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Peer5
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Текущий фрактал, нарисованный на канвасе
        /// </summary>
        public Fractal CurrentFractal;

        /// <summary>
        /// Инициализация формы и установка дефолтных значений
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Fractal.Canvas = canvas;
            Fractal.MaxDepth = 1;
            Fractal.Multiplier = 1;
            Pifagor.AngleLeft = Pifagor.AngleRight = Math.PI * 0.25;
            Pifagor.Length = 1/Math.Sqrt(2);
            Cantor.Distance = 10;
            angleRightTextBox.Text = Math.Round(Pifagor.AngleRight * 180 / Math.PI, 4).ToString();
            angleLeftTextBox.Text = Math.Round(Pifagor.AngleLeft * 180 / Math.PI, 4).ToString();
            renderDepthTextBox.Text = Fractal.MaxDepth.ToString();
            stepTextBox.Text = Math.Round(Pifagor.Length, 4).ToString();
            distanceTextBox.Text = Cantor.Distance.ToString();
        }

        /// <summary>
        /// Отрисовка линии коха при нажатии на соответствующую кнопку
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        public void KochLineButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Fractal.MaxDepth > 7)
                {
                    MessageBoxResult dialogResult = MessageBox.Show("Заданная глубина рекурсии превышает максимально разрешенную для этого фрактала(7). Глубина рекурсии будет изменена", "Внимание", MessageBoxButton.OKCancel);
                    if (dialogResult == MessageBoxResult.OK)
                    {
                        renderDepthTextBox.Text = "7";
                        DrawKochline();
                    }
                    else
                    {
                        return;
                    }
                }
                DrawKochline();
            }
            catch (Exception ex)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Произошла ошибка. Нажмите ОК, чтобы ознакомиться с информацией", "У вас эксекпшн!", MessageBoxButton.OKCancel);
                if (dialogResult == MessageBoxResult.OK)
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);              
            }
        }

        /// <summary>
        /// Расчет и отрисовка линии Коха
        /// </summary>
        public void DrawKochline()
        {
            Kochline koch = new Kochline();
            koch.Calculate(GeometryObject.Line(new Point(0, 0), new Point(1000, 0)), ref koch.ListOfPoints, 0);
            koch.Draw();
            CurrentFractal = koch;
        }

        /// <summary>
        /// Отрисовка пифагорова дерева при нажатии на соответствующую кнопку
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        public void PifagorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Fractal.MaxDepth > 11)
                {
                    MessageBoxResult dialogResult = MessageBox.Show("Заданная глубина рекурсии превышает максимально разрешенную для этого фрактала(11). Глубина рекурсии будет изменена", "Внимание", MessageBoxButton.OKCancel);
                    if (dialogResult == MessageBoxResult.OK)
                    {
                        renderDepthTextBox.Text = "11";
                        DrawTree();
                    }
                    else
                    {
                        return;
                    }
                }
                DrawTree();
            }
            catch (Exception ex)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Произошла ошибка. Нажмите ОК, чтобы ознакомиться с информацией", "У вас эксекпшн!", MessageBoxButton.OKCancel);
                if (dialogResult == MessageBoxResult.OK)
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// Рассчет и отрисовка пифагорова дерева
        /// </summary>
        public void DrawTree()
        {
            Pifagor pifagor = new Pifagor();
            pifagor.Calculate(GeometryObject.Line(new Point(0, 0), new Point(0, 1000)), ref pifagor.ListOfPoints, 0);
            pifagor.Draw();
            CurrentFractal = pifagor;
        }

        /// <summary>
        /// Отрисовка коврика Серпинского при нажатии на соответствующую кнопку
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        public void CarpetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Fractal.MaxDepth > 5)
                {
                    MessageBoxResult dialogResult = MessageBox.Show("Заданная глубина рекурсии превышает максимально разрешенную для этого фрактала(5). Глубина рекурсии будет изменена", "Внимание", MessageBoxButton.OKCancel);
                    if (dialogResult == MessageBoxResult.OK)
                    {
                        renderDepthTextBox.Text = "5";
                        DrawCarpet();
                    }
                    else
                    {
                        return;
                    }
                }
                DrawCarpet();
            }
            catch (Exception ex)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Произошла ошибка. Нажмите ОК, чтобы ознакомиться с информацией", "У вас эксекпшн!", MessageBoxButton.OKCancel);
                if (dialogResult == MessageBoxResult.OK)
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// Отрисовка ковра Серпинского
        /// </summary>
        public void DrawCarpet()
        {
            SerpCarp carpet = new SerpCarp();
            carpet.ListOfPoints.Add(GeometryObject.Square(new Point(0, 0), 100));
            carpet.Calculate(carpet[0], ref carpet.ListOfPoints, 0);
            carpet.Draw();
            CurrentFractal = carpet;
        }

        /// <summary>
        /// Отрисовка треугольника Серпинского при нажатии на соответствующую кнопку
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        public void TriangleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Fractal.MaxDepth > 7)
                {
                    MessageBoxResult dialogResult = MessageBox.Show("Заданная глубина рекурсии превышает максимально разрешенную для этого фрактала(10). Глубина рекурсии будет изменена", "Внимание", MessageBoxButton.OKCancel);
                    if (dialogResult == MessageBoxResult.OK)
                    {
                        renderDepthTextBox.Text = "7";
                        DrawTriangle();
                    }
                    else
                    {
                        return;
                    }
                }
                DrawTriangle();
            }
            catch (Exception ex)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Произошла ошибка. Нажмите ОК, чтобы ознакомиться с информацией", "У вас эксекпшн!", MessageBoxButton.OKCancel);
                if (dialogResult == MessageBoxResult.OK)
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        // Отрисовка треугольника Серпинского
        public void DrawTriangle()
        {
            SerpTrg triangle = new SerpTrg();
            triangle.ListOfPoints.Add(GeometryObject.Triangle(new Point(0, 0), new Point(50, 100 * Math.Sin(Math.PI / 3)), new Point(100, 0)));
            triangle.Calculate(triangle[0], ref triangle.ListOfPoints, 0);
            triangle.Draw();
            CurrentFractal = triangle;
        }

        /// <summary>
        /// Отрисовка множества Кантора при нажатии на соответствующую кнопку
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        public void CantorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Fractal.MaxDepth > 10)
                {
                    MessageBoxResult dialogResult = MessageBox.Show("Заданная глубина рекурсии превышает максимально разрешенную для этого фрактала(10). Глубина рекурсии будет изменена", "Внимание", MessageBoxButton.OKCancel);
                    if (dialogResult == MessageBoxResult.OK)
                    {
                        renderDepthTextBox.Text = "10";
                        DrawCantor();
                    }
                    else
                    {
                        return;
                    }
                }
                DrawCantor();
            }
            catch (Exception ex)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Произошла ошибка. Нажмите ОК, чтобы ознакомиться с информацией", "У вас эксекпшн!", MessageBoxButton.OKCancel);
                if (dialogResult == MessageBoxResult.OK)
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// Отрисовка множества Кантора
        /// </summary>
        public void DrawCantor()
        {
            Cantor cantor = new Cantor();
            cantor.ListOfPoints.Add(GeometryObject.Line(new Point(0, 0), new Point(554, 0)));
            cantor.Calculate(cantor[0], ref cantor.ListOfPoints, 0);
            cantor.Draw();
            CurrentFractal = cantor;
        }

        /// <summary>
        /// Считывание информации при вводе глубины отрисовки
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        public void DepthTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if ((!int.TryParse(renderDepthTextBox.Text, out int value) && renderDepthTextBox.Text != "") || value < 0)
                {
                    renderDepthTextBox.BorderBrush = Brushes.Red;
                    renderDepthTextBox.BorderThickness = new Thickness(1);
                }
                else
                {
                    Fractal.MaxDepth = value;
                    renderDepthTextBox.BorderBrush = Brushes.Black;
                    renderDepthTextBox.BorderThickness = new Thickness(0.5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что то пошло не так. Повторите попытку. Информация об ошибке:" + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Считывание информации при вводе правого угла в дереве Пифагора
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void angleRightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if ((!double.TryParse(angleRightTextBox.Text, out double value) && angleRightTextBox.Text != "") || value < -180 || value > 180)
                {
                    angleRightTextBox.BorderBrush = Brushes.Red;
                    angleRightTextBox.BorderThickness = new Thickness(1);
                }
                else
                {
                    Pifagor.AngleRight = value * (Math.PI / 180);
                    angleRightTextBox.BorderBrush = Brushes.Black;
                    angleRightTextBox.BorderThickness = new Thickness(0.5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что то пошло не так. Повторите попытку. Информация об ошибке:" + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Считывание информации при вводе левого угла в дереве Пифагора
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void angleLeftTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if ((!double.TryParse(angleLeftTextBox.Text, out double value) && angleLeftTextBox.Text != "") || value < -180 || value > 180)
                {
                    angleLeftTextBox.BorderBrush = Brushes.Red;
                    angleLeftTextBox.BorderThickness = new Thickness(1);
                }
                else
                {
                    Pifagor.AngleLeft = value * (Math.PI / 180);
                    angleLeftTextBox.BorderBrush = Brushes.Black;
                    angleLeftTextBox.BorderThickness = new Thickness(0.5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что то пошло не так. Повторите попытку. Информация об ошибке:" + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Считывание информации при вводе отношения в дереве пифагора
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void stepTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if ((!double.TryParse(stepTextBox.Text, out double value) && stepTextBox.Text != "") || value < 0 || value > 1)
                {
                    stepTextBox.BorderBrush = Brushes.Red;
                    stepTextBox.BorderThickness = new Thickness(1);
                }
                else
                {
                    Pifagor.Length = value;
                    stepTextBox.BorderBrush = Brushes.Black;
                    stepTextBox.BorderThickness = new Thickness(0.5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что то пошло не так. Повторите попытку. Информация об ошибке:" + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Считывание информации при вводе расстояния между линиями
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void distanceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if ((!double.TryParse(distanceTextBox.Text, out double value) && distanceTextBox.Text != "" && distanceTextBox.Text != "-") || value < 0)
                {
                    distanceTextBox.BorderBrush = Brushes.Red;
                    distanceTextBox.BorderThickness = new Thickness(1);
                }
                else
                {
                    Cantor.Distance = value;
                    distanceTextBox.BorderBrush = Brushes.Black;
                    distanceTextBox.BorderThickness = new Thickness(0.5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что то пошло не так. Повторите попытку. Информация об ошибке:" + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Перерисовка фрактала при изменении размеров окна
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                if (CurrentFractal != null)
                    CurrentFractal.Draw();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Сохранение изображения в файл
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void renderButton_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание битмапа
                RenderTargetBitmap rtb = new RenderTargetBitmap(2 * (int)canvas.ActualWidth, 2 * (int)canvas.ActualHeight, 144d, 144d, PixelFormats.Default);
                rtb.Render(canvas);

                // Конвертация в png
                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                // Сохранение в файл
                pngEncoder.Save(ms);
                ms.Close();
                System.IO.File.WriteAllBytes("fractal.png", ms.ToArray());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось сохранить картинку. Повторите попытку. Информация об ошибке:" + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Вывод информации о программе
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Общие сведения о программе: " + Environment.NewLine +
                "1.Я ограничил максимальную глубину рекурсии индивидуально для каждого фрактала, чтобы программа не вылетала на сложных, но и была возможность чуть более деьально прорисовывать легкие " + Environment.NewLine +
                "2.Максимальные параметры фракталов " + Environment.NewLine +
                "1) Правый угол: от - 180 до 180, только целые" + Environment.NewLine +
                "2) Левый угол: от - 180 до 180, только целые" + Environment.NewLine +
                "3) Отношение(отношение длин отрезков на разных итерациях): от 0 до 1" + Environment.NewLine +
                "4) Расстояние: от 0 до int_MaxValue, только целые; " + Environment.NewLine +
                "Если при вводе параметр подсвечивается красной рамкой - значит, его значение неверно" + Environment.NewLine +
                "Если не получается использовать как разделитель точку, попробуйте запятую" + Environment.NewLine +
                "3.Отрисовка фракталов происходит при нажатии на кнопку с сответствующим фракталом." + Environment.NewLine +
                "4.Кнопка render - сохранение фрактала в память.Фрактал сохраняется в папку с программой, Peer5\\bin\\Debug\\net5.0 - windows\\fractal.png" + Environment.NewLine +
                "5.При изменении размеров окна фрактал перерисовывается автоматически, но учтите - при большом количестве итераций его отрисовка займет довольно продолжительное время");
        }

        /// <summary>
        /// Создание эффекта при заходе курсора на кнопки отрисовки
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void onButton_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                button.Effect = new System.Windows.Media.Effects.DropShadowEffect()
                {
                    BlurRadius = 10,
                    ShadowDepth = 5
                };
            }
            catch
            {
            }
        }

        /// <summary>
        /// Убирание эффекта в случае, еслим курсор уходит с кнопки
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void onButton_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                button.Effect = null;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Изменение текста в случае, если курсор зашел на кнопку Информация
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void infoButton_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                infoButton.Content = "readme.txt";
            }
            catch
            {
            }
        }

        /// <summary>
        /// Возврат текста к дефолтному
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void infoButton_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                infoButton.Content = "Информация";
            }
            catch
            {
            }
        }

        /// <summary>
        /// Появление подсказки, когда курсор заходит на надпись
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Label label = sender as Label;
                ToolTip toolTip = new ToolTip();
                StackPanel toolTipPanel = new StackPanel();
                toolTipPanel.Children.Add(new TextBlock { Text = "Угол наклона левого отрезка", FontSize = 10.5 });
                toolTipPanel.Children.Add(new TextBlock { Text = "От -180 до 180 градусов, только целые значения", FontSize = 9.5 });
                toolTip.Content = toolTipPanel;
                label.ToolTip = toolTip;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Появление подсказки, когда курсор заходит на надпись
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void Label_MouseEnter_1(object sender, MouseEventArgs e)
        {
            try
            {
                Label label = sender as Label;
                ToolTip toolTip = new ToolTip();
                StackPanel toolTipPanel = new StackPanel();
                toolTipPanel.Children.Add(new TextBlock { Text = "Угол наклона правого отрезка", FontSize = 10.5 });
                toolTipPanel.Children.Add(new TextBlock { Text = "От -180 до 180 градусов, только целые значения", FontSize = 9.5 });
                toolTip.Content = toolTipPanel;
                label.ToolTip = toolTip;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Появление подсказки, когда курсор заходит на надпись
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void Label_MouseEnter_2(object sender, MouseEventArgs e)
        {
            try
            {
                Label label = sender as Label;
                ToolTip toolTip = new ToolTip();
                StackPanel toolTipPanel = new StackPanel();
                toolTipPanel.Children.Add(new TextBlock { Text = "Коэффициент, определяющий отношение длин" + Environment.NewLine + "отрезков на текущей и последующей итерации ", FontSize = 9.5 });
                toolTipPanel.Children.Add(new TextBlock { Text = "От 0 до 1", FontSize = 8 });
                toolTip.Content = toolTipPanel;
                label.ToolTip = toolTip;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Появление подсказки, когда курсор заходит на надпись
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void Label_MouseEnter_3(object sender, MouseEventArgs e)
        {
            try
            {
                Label label = sender as Label;
                ToolTip toolTip = new ToolTip();
                StackPanel toolTipPanel = new StackPanel();
                toolTipPanel.Children.Add(new TextBlock { Text = "Расстояние между отрезками," + Environment.NewLine + "отрисованными на разных итерациях", FontSize = 10.5 });
                toolTipPanel.Children.Add(new TextBlock { Text = "От 0 до int_MaxValue, только целые", FontSize = 9.5 });
                toolTip.Content = toolTipPanel;
                label.ToolTip = toolTip;
            }
            catch
            {
            }

        }

        /// <summary>
        /// Появление подсказки, когда курсор заходит на надпись
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void renderButton_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                ToolTip toolTip = new ToolTip();
                StackPanel toolTipPanel = new StackPanel();
                toolTipPanel.Children.Add(new TextBlock { Text = "Сохранить фрактал в png", FontSize = 10.5 });
                toolTip.Content = toolTipPanel;
                button.ToolTip = toolTip;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Появление подсказки, когда курсор заходит на надпись
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Событие</param>
        private void Label_MouseEnter_4(object sender, MouseEventArgs e)
        {
            try
            {
                Label label = sender as Label;
                ToolTip toolTip = new ToolTip();
                StackPanel toolTipPanel = new StackPanel();
                toolTipPanel.Children.Add(new TextBlock { Text = "Количество итераций", FontSize = 10.5 });
                toolTipPanel.Children.Add(new TextBlock { Text = "Для каждого фрактала индивидуально", FontSize = 9.5 });
                toolTip.Content = toolTipPanel;
                label.ToolTip = toolTip;
            }
            catch
            {
            }
        }
    }
}