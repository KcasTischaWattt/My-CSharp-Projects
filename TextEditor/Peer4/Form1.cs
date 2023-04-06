using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peer4
{
    public partial class NotePad : Form
    {
        /// <summary>
        /// Количество созданных пользователем новых вкладок
        /// </summary>
        private static int UnnamedTabsCount = 1;
        /// <summary>
        /// Количество открытых форм (не работает, понял слишком поздно)
        /// </summary>
        private int formCounter = 0;
        /// <summary>
        /// Количество открытых вкладок (то же самое)
        /// </summary>
        private int tabCounter = 0;
        /// <summary>
        /// Таймер автосохранения
        /// </summary>
        private Timer autoSaveTimer = new Timer();

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public NotePad()
        {
            
            try
            {
                InitializeComponent();
                BackColor = Program.Settings.ColorScheme == "light" ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);
                tabCounter++;
                formCounter++;
                if (Program.Settings.OpenFileNames.Count == 0)
                {
                    newTab();
                }
                else
                {
                    openSavedFiles();
                }
                setColorScheme();
                // Устанавливаем интервалы автосохранения
                foreach (var i in AppSettings.AvailableAutosaveIntervals)
                {
                    var intervalMenuItem = new ToolStripMenuItem($"{i} min");
                    toolStripDropDownAutosaveInterval.DropDownItems.Add(intervalMenuItem);
                    intervalMenuItem.Click += new EventHandler(minToolStripMenuItem_Click);
                    if (i == Program.Settings.AutosaveInterval)
                        intervalMenuItem.Checked = true;
                }
                startAutosaveTimer();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла неизвестная ошибка. Повторите попытку.");
            }
        }

        /// <summary>
        /// Запуск таймера автосохранения
        /// </summary>
        private void startAutosaveTimer()
        {
            autoSaveTimer.Interval = 60000 * Program.Settings.AutosaveInterval;
            autoSaveTimer.Tick += new EventHandler(autoSaveAllTabs);
            autoSaveTimer.Start();
        }

        /// <summary>
        /// Автосохранение всех файлов в форме
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void autoSaveAllTabs(object sender, EventArgs e)
        {
            try
            {
                foreach (TextEditorTab tab in tabControlAllTabs.TabPages)
                    // Сохраняем каждый файл, если он существует(Его имя не null)
                    if (tab.FileName != null)
                    {
                        string extension = Path.GetExtension(tab.FileName);
                        switch (extension)
                        {
                            case ".rtf":
                                tab.editorBox.SaveFile(tab.FileName);
                                tab.Type = RichTextBoxStreamType.RichText;
                                break;
                            case ".txt":
                                tab.editorBox.SaveFile(tab.FileName, RichTextBoxStreamType.PlainText);
                                tab.Type = RichTextBoxStreamType.PlainText;
                                break;
                        }
                        tab.IsSaved = true;
                    }
            }
            // Если поймали исключение - замалчиваем
            catch (Exception)
            { 
            }

        }

        /// <summary>
        /// Открытие пустой вкладки при запуске приложения
        /// </summary>
        private void newTab()
        {
            var tab = new TextEditorTab
            {
                Text = $"New File{UnnamedTabsCount++}",

            };
            tabControlAllTabs.Controls.Add(tab);
            // Навешиваем контекстное меню
            tab.editorBox.ContextMenuStrip = contextMenuStrip1;
        }

        /// <summary>
        /// Загрузка раннее открытых файлов из json
        /// </summary>
        private void openSavedFiles()
        {
            foreach (var file in Program.Settings.OpenFileNames)
                if (File.Exists(file))
                    openTab(file);
            Program.Settings.OpenFileNames.Clear();
        }

        /// <summary>
        /// Установка темы приложения
        /// </summary>
        private void setColorScheme()
        {
            switch (Program.Settings.ColorScheme)
            {
                case "light":
                    setLightTheme();
                    break;
                case "dark":
                    setDarkTheme();
                    break;
            }
        }

        /// <summary>
        /// Установка темной темы
        /// </summary>
        private void setDarkTheme()
        {
            var backColor = Color.FromArgb(0, 0, 0);
            var foreColor = Color.FromArgb(255, 255, 255);
            foreach (Form f in Application.OpenForms)
                f.BackColor = backColor;
            foreach (Control c in Controls)
                UpdateColorControls(c, backColor, foreColor);
            Program.Settings.ColorScheme = "dark";
        }

        /// <summary>
        /// Установка светлой темы
        /// </summary>
        private void setLightTheme()
        {
            var backColor = Color.FromArgb(255, 255, 255);
            var foreColor = Color.FromArgb(0, 0, 0);
            foreach (Form f in Application.OpenForms)
                f.BackColor = backColor;
            foreach (Control c in Controls)
                UpdateColorControls(c, backColor, foreColor);
            Program.Settings.ColorScheme = "light";
        }

        /// <summary>
        /// Установка цвета определенного элемента формы
        /// </summary>
        /// <param name="c">Элемент формы</param>
        /// <param name="backColor">Цвкт элемента</param>
        /// <param name="foreColor">Цвет текста</param>
        private void UpdateColorControls(Control c, Color backColor, Color foreColor)
        {
            c.BackColor = backColor;
            c.ForeColor = foreColor;
            foreach (Control subC in c.Controls)
            {
                UpdateColorControls(subC, backColor, foreColor);
            }
        }

        /// <summary>
        /// Создение нового файла
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void tsbNewFileButton_Click(object sender, EventArgs e)
        {
            if (tabCounter < 1000)
            {
                var tab = new TextEditorTab
                {
                    Text = $"New File{UnnamedTabsCount++}",
                };
                tabCounter++;
                tabControlAllTabs.Controls.Add(tab);
                tab.editorBox.ContextMenuStrip = contextMenuStrip1;
            }
            else
                MessageBox.Show("Слишком много вкладок!");
        }

        /// <summary>
        /// Открытие существующего файла
        /// </summary>
        /// <param name="sender">объект</param>
        /// <param name="e">событие</param>
        private void tsbOpenFileButton_Click(object sender, EventArgs e)
        {
            try 
            { 
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.DefaultExt = "*.rtf";
            openFile.Filter = "All supported files|*.txt;*.rtf|RTF Files|*.rtf|txt files (*.txt)|*.txt";
                if (openFile.ShowDialog() == DialogResult.OK && openFile.FileName.Length > 0)
                {
                    openTab(openFile.FileName);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть файл");
            }
        }

        /// <summary>
        /// Открытие файла в вкладку
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        private void openTab(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            var tab = new TextEditorTab
            {
                Text = Path.GetFileName(fileName),
            };
            switch (extension)
            {
                case ".rtf":
                    tab.editorBox.LoadFile(fileName);
                    break;
                case ".txt":
                    tab.editorBox.LoadFile(fileName, RichTextBoxStreamType.PlainText);
                    tab.Type = RichTextBoxStreamType.PlainText;
                    break;
            }
            tab.IsSaved = true;
            tabCounter++;
            tab.FileName = fileName;
            tabControlAllTabs.Controls.Add(tab);
        }

        /// <summary>
        /// Простое сохранение файла
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void tsbSaveFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
                if (tab.FileName == null)
                    tsbSaveAsButton_Click(sender, e);
                else
                    saveToFile(tab.FileName);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сохранить файл");
            }
        }

        /// <summary>
        /// Сохранение файла с открытием диалогового окна(нажатие кнопки SaveAs)
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void tsbSaveAsButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.DefaultExt = "*.rtf";
                saveFile.Filter = "RTF Files|*.rtf|txt files (*.txt)|*.txt";
                if (saveFile.ShowDialog() == DialogResult.OK && saveFile.FileName.Length > 0)
                    saveToFile(saveFile.FileName);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сохранить файл");
            }
        }

        /// <summary>
        /// Непосредственно сохранение в файл
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        private void saveToFile(string fileName)
        {
            var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
            string extension = Path.GetExtension(fileName);
            switch (extension)
            {
                case ".rtf":
                    tab.editorBox.SaveFile(fileName);
                    tab.Type = RichTextBoxStreamType.RichText;
                    break;
                case ".txt":
                    var result = MessageBox.Show("При сохранении в формате .txt форматирование будет потеряно. Сохранить в .txt?", "Сохранение без форматирования", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        tab.editorBox.SaveFile(fileName, RichTextBoxStreamType.PlainText);
                        tab.Type = RichTextBoxStreamType.PlainText;
                    }
                    else
                    {
                        MessageBox.Show("Файл не сохранен");
                        return;
                    }
                    break;
            }
            tab.FileName = fileName;
            tab.Text = Path.GetFileName(fileName);
            tab.IsSaved = true;
        }


        /// <summary>
        /// Установка жирного шрифта
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void tsbBoldTextButton_Click(object sender, EventArgs e)
        {
            setFontStyle(FontStyle.Bold);
        }

        /// <summary>
        /// Установка курсива
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void tsbCursiveTextButton_Click(object sender, EventArgs e)
        {
            setFontStyle(FontStyle.Italic);
        }

        /// <summary>
        /// Установка подчеркнутого шрифта
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void tsbUndelineTextButton_Click(object sender, EventArgs e)
        {
            setFontStyle(FontStyle.Underline);
        }

        /// <summary>
        /// Установка зачеркнутого шрифта
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void tsbCrossedTextButton_Click(object sender, EventArgs e)
        {
            setFontStyle(FontStyle.Strikeout);
        }

        /// <summary>
        /// Установка определенного стиля
        /// </summary>
        /// <param name="style">Стиль текста</param>
        private void setFontStyle(FontStyle style)
        {
            try
            {
                var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
                int selectionStart = tab.editorBox.SelectionStart;
                int selectionLength = tab.editorBox.SelectionLength;
                int selectionEnd = selectionStart + selectionLength;
                int countRegular = 0;
                // Проходимся по выделенному фрагменту. Если есть хотя бы однин символ со стилем, отличным от устанавливаемого - установливаем стиль для всего фрагмента
                for (var i = selectionStart; i < selectionEnd; ++i)
                {
                    tab.editorBox.Select(i, 1);
                    if (tab.editorBox.SelectionFont.Style != (tab.editorBox.SelectionFont.Style | style))
                    {
                        ++countRegular;
                        break;
                    }
                }
                tab.editorBox.Select(selectionStart, selectionLength);
                for (var i = selectionStart; i < selectionEnd; ++i)
                {
                    tab.editorBox.Select(i, 1);
                    // Иначе - убираем его для всего фрагмента
                    if (countRegular == 0)
                        tab.editorBox.SelectionFont = new Font(tab.editorBox.SelectionFont.FontFamily, tab.editorBox.SelectionFont.Size, tab.editorBox.SelectionFont.Style & ~style);
                    else
                        tab.editorBox.SelectionFont = new Font(tab.editorBox.SelectionFont.FontFamily, tab.editorBox.SelectionFont.Size, tab.editorBox.SelectionFont.Style | style);
                }
                tab.editorBox.Select(selectionStart, selectionLength);
                tab.IsSaved = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось применить стиль");
            }
        }

        /// <summary>
        /// Установка timesNewRoman
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timesNewRomanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeOnFont("Times New Roman");
        }

        /// <summary>
        /// Установка calibri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calibriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeOnFont("Calibri");
        }

        /// <summary>
        /// Установка cambria
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void cambriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeOnFont("Cambria");
        }

        /// <summary>
        /// Установка garamond
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void garamondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeOnFont("Gramond");
        }

        /// <summary>
        /// Установка arial
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void arialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeOnFont("Arial");
        }

        /// <summary>
        /// Установка dejaVuSans
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void dejaVuSabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeOnFont("DejaVu Sans");
        }

        /// <summary>
        /// Установка tahoma
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void tahomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeOnFont("Tahoma");
        }

        /// <summary>
        /// Установка шрифта
        /// </summary>
        /// <param name="font">Шрифт</param>
        private void changeOnFont(string font)
        {
            try
            {
                var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
                int selectionStart = tab.editorBox.SelectionStart;
                int selectionLength = tab.editorBox.SelectionLength;
                int selectionEnd = selectionStart + selectionLength;
                // Проходимся по каждому элементу и устанавливаем ему шрифт
                for (int i = selectionStart; i < selectionEnd; ++i)
                {
                    tab.editorBox.Select(i, 1);
                    tab.editorBox.SelectionFont = new Font(font, tab.editorBox.SelectionFont.Size, tab.editorBox.SelectionFont.Style);
                }
                tab.editorBox.Select(selectionStart, selectionLength);
                tab.IsSaved = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось применить шрифт. Повторите попытку.");
            }
        }

        /// <summary>
        /// Закрытие формы - опрос о сохранении докуументов и запись их в json
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Спрашиваем о том, сохранить ли каждый документ
                foreach (TextEditorTab tab in tabControlAllTabs.TabPages)
                {
                    if (tab.IsSaved == false)
                    {
                        var fileName = (tab.FileName == null ? tab.Text : Path.GetFileName(tab.FileName));
                        var message = MessageBox.Show($"Хотите сохранить изменения в файле {fileName} ?", "Сохранить файл", MessageBoxButtons.YesNoCancel);
                        if (message == DialogResult.Yes)
                        {
                            tsbSaveFileButton_Click(sender, null);
                        }
                        if (message == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                }
                // Если форма все же будет закрыта - записать ее в json
                if (!e.Cancel)
                {
                    formCounter--;
                    tabCounter -= tabControlAllTabs.TabPages.Count;
                    foreach (TextEditorTab tab in tabControlAllTabs.TabPages)
                        if (tab.FileName != null)
                            Program.Settings.OpenFileNames.Add(tab.FileName);
                    Program.Settings.SaveToJson();
                }
            }
            catch (Exception)
            {
                e.Cancel = true;
                MessageBox.Show("Произошла неизвестная ошибка. Повторите попытку.");
            }
        }

        /// <summary>
        /// Установка жирного шрифта для контекстного меню
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setFontStyle(FontStyle.Bold);
        }

        /// <summary>
        /// Установка курсива для контекстного меню
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setFontStyle(FontStyle.Italic);
        }

        /// <summary>
        /// Установка подчекрнутого шрифта для контекстного меню
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setFontStyle(FontStyle.Underline);
        }

        /// <summary>
        /// Установка зачеркнутого шрифта для контекстного меню
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void strikeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setFontStyle(FontStyle.Strikeout);
        }

        /// <summary>
        /// Копирование текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
                tab.editorBox.Paste();
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат данных в буфере обмена");
            }

        }

        /// <summary>
        /// Выделение всего текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
                tab.editorBox.SelectAll();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла неизвестная ошибка. Повторите попытку");
            }
        }

        /// <summary>
        /// Вырезать текст
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
                tab.editorBox.Cut();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла неизвестная ошибка. Повторите попытку");
            }
        }

        /// <summary>
        /// Скопировать текст
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
                tab.editorBox.Copy();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла неизвестная ошибка. Повторите попытку");
            }
        }

        /// <summary>
        /// Смена цветовой темы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChangeScheme_Click(object sender, EventArgs e)
        {
            try
            {
                switch (Program.Settings.ColorScheme)
                {
                    case "dark":
                        setLightTheme();
                        break;
                    case "light":
                        setDarkTheme();
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла неизвестная ошибка. Повторите попытку");
            }
        }

        /// <summary>
        /// Интервал автосохранения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                Program.Settings.AutosaveInterval = AppSettings.AvailableAutosaveIntervals[toolStripDropDownAutosaveInterval.DropDownItems.IndexOf(item)];
                foreach (ToolStripMenuItem i in toolStripDropDownAutosaveInterval.DropDownItems)
                    i.Checked = false;
                item.Checked = true;
                autoSaveTimer.Stop();
                autoSaveTimer.Interval = 60000 * Program.Settings.AutosaveInterval;
                autoSaveTimer.Start();
            }
            catch (Exception)
            { 
            }
        }

        /// <summary>
        /// Реализация горячих клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S && !e.Shift)
            {
                tsbSaveFileButton_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.O)
            {
                tsbOpenFileButton_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.N && !e.Shift)
            {
                tsbNewFileButton_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.Shift && e.KeyCode == Keys.N)
            {
                if (formCounter < 100 && tabCounter < 1000)
                {
                    var form2 = new NotePad();
                    form2.Show();
                }
                else
                    MessageBox.Show("Слишком много вкладок!");
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.S && e.Shift)
            {
                foreach (var tab in tabControlAllTabs.TabPages)
                    if (((TextEditorTab)tab).FileName != null)
                        tsbSaveFileButton_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.W && !e.Shift)
            {
                ActiveForm.Close();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.B)
            {
                setFontStyle(FontStyle.Bold);
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.I)
            {
                setFontStyle(FontStyle.Italic);
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.U)
            {
                setFontStyle(FontStyle.Underline);
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.W && e.Shift)
            {
                var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
                var fileName = (tab.FileName == null ? tab.Text : Path.GetFileName(tab.FileName));
                var message = MessageBox.Show($"Хотите сохранить изменения в файле {fileName} ?", "Сохранить файл", MessageBoxButtons.YesNoCancel);
                if (message == DialogResult.Yes)
                {
                    tsbSaveFileButton_Click(sender, null);
                    tabControlAllTabs.TabPages.Remove(tab);
                    tabCounter--;
                }
                if (message == DialogResult.No)
                {
                    tabControlAllTabs.TabPages.Remove(tab);
                    tabCounter--;
                }
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.Z && !e.Shift)
            {
                var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
                tab.editorBox.Undo();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.Z && e.Shift)
            {
                var tab = (TextEditorTab)tabControlAllTabs.SelectedTab;
                tab.editorBox.Redo();
                e.SuppressKeyPress = true;
            }
        }
    }
}
