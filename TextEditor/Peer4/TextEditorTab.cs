using System;
using System.Windows.Forms;

namespace Peer4
{
    public class TextEditorTab : TabPage
    {
        /// <summary>
        /// Отвечает за то, сохранен ли файл
        /// </summary>
        private bool isSaved = true;
        /// <summary>
        /// Свойство, отвечающее за то, сохранен ли файл 
        /// </summary>
        public bool IsSaved
        {
            get => isSaved;
            set
            {
                isSaved = value;
                // Если файл не сохранен - к его имени на подписи должна быть добавлена точка
                if (isSaved == false)
                {
                    Text = Text.Trim('*');
                    Text += "*";
                }
                else
                    Text = Text.Trim('*');
            } 
        }
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName;
        /// <summary>
        /// Тип(расширение) файла
        /// </summary>
        public RichTextBoxStreamType Type = RichTextBoxStreamType.RichText;
        /// <summary>
        /// Само текстовое поле на вкладке
        /// </summary>
        public RichTextBox editorBox;

        /// <summary>
        /// Конструктор, добавляющий вкладку в табконтроллер
        /// </summary>
        public TextEditorTab()
        {
            editorBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
            };
            Controls.Add(editorBox);
            // Навешиваем обработчик
            editorBox.TextChanged += new EventHandler(onTextChange);
        }

        /// <summary>
        /// Обработчик событий, срабатывающий в момент изменения файла
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void onTextChange(object sender, EventArgs e)
        {
            RichTextBox rtb = sender as RichTextBox;
            ((TextEditorTab)rtb.Parent).IsSaved = false;
        }

    }
}
