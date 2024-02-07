using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace практика_производственная
{
    public partial class Serial : Form
    {
        public Serial()
        {
            InitializeComponent();
            PopulateComboBox1();
            PopulateComboBox2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            selection f = new selection();
            f.ShowDialog();
        }
        private void PopulateComboBox1()
        {
            string folderPath = @"C:\Users\Пользователь\Desktop\сериалы";
            string[] fileEntries = Directory.GetFiles(folderPath);
            foreach (string fileName in fileEntries)
            {
                comboBox1.Items.Add(Path.GetFileName(fileName));

            }
        }
        private void PopulateComboBox2()
        {
            string folderPath = @"C:\Users\Пользователь\Desktop\сериалы";
            string[] fileEntries = Directory.GetFiles(folderPath);
            foreach (string fileName in fileEntries)
            {
                comboBox2.Items.Add(Path.GetFileName(fileName));

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFileName = comboBox1.SelectedItem.ToString();
            string filePath = Path.Combine("C:\\Users\\Пользователь\\Desktop\\сериалы", selectedFileName);
            DataTable dt2 = new DataTable();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = sr.ReadLine();
                string[] values = line.Split(',');

                // Проходим по всем значениям, чтобы сформировать уникальные имена столбцов
                Dictionary<string, int> columnCounts = new Dictionary<string, int>();
                foreach (string header in values)
                {
                    string columnName = header;
                    if (columnCounts.ContainsKey(columnName))
                    {
                        columnCounts[columnName]++;
                        columnName = $"{columnName} ({columnCounts[columnName]})";
                    }
                    else
                    {
                        columnCounts.Add(columnName, 1);
                    }
                    // Добавляем столбец с уникальным именем
                    dt2.Columns.Add(columnName);
                }
            }

            dataGridView1.DataSource = dt2;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFileName = comboBox2.SelectedItem.ToString();
            string filePath = Path.Combine("C:\\Users\\Пользователь\\Desktop\\сериалы", selectedFileName);
            DataTable dt2 = new DataTable();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = sr.ReadLine();
                string[] values = line.Split(',');

                // Проходим по всем значениям, чтобы сформировать уникальные имена столбцов
                Dictionary<string, int> columnCounts = new Dictionary<string, int>();
                foreach (string header in values)
                {
                    string columnName = header;
                    if (columnCounts.ContainsKey(columnName))
                    {
                        columnCounts[columnName]++;
                        columnName = $"{columnName} ({columnCounts[columnName]})";
                    }
                    else
                    {
                        columnCounts.Add(columnName, 1);
                    }
                    // Добавляем столбец с уникальным именем
                    dt2.Columns.Add(columnName);
                }
            }

            dataGridView2.DataSource = dt2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text == "") && (comboBox2.Text == ""))
            {
                MessageBox.Show("Выберите сериалы", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(System.Windows.Forms.Application.ExecutablePath);
                Close();
            }
            else
          if (comboBox1.Text == "")
            {
                MessageBox.Show("Выберите первый сериал", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(System.Windows.Forms.Application.ExecutablePath);
                Close();
            }
            else
          if (comboBox2.Text == "")
            {
                MessageBox.Show("Выберите второй сериал", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(System.Windows.Forms.Application.ExecutablePath);
                Close();
            }
            else
            {
                string selectedFileName = comboBox1.SelectedItem.ToString();
                string filePath = Path.Combine("C:\\Users\\Пользователь\\Desktop\\сериалы", selectedFileName);

                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath); // Чтение содержимого файла
                    string[] numbers = content.Split(','); // Разделение содержимого по запятым
                    int[] values = new int[numbers.Length];

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if (int.TryParse(numbers[i], out int number))
                        {
                            values[i] = number; // Преобразование строк в числа
                        }
                    }
                    string selectedFileName1 = comboBox2.SelectedItem.ToString();
                    string filePath1 = Path.Combine("C:\\Users\\Пользователь\\Desktop\\сериалы", selectedFileName1);

                    if (File.Exists(filePath1))
                    {
                        string content1 = File.ReadAllText(filePath1); // Чтение содержимого файла
                        string[] numbers1 = content1.Split(','); // Разделение содержимого по запятым
                        int[] values1 = new int[numbers1.Length];

                        for (int i = 0; i < numbers1.Length; i++)
                        {
                            if (int.TryParse(numbers1[i], out int number1))
                            {
                                values1[i] = number1; // Преобразование строк в числа
                            }
                        }
                        DrawChart(values, values1); // Вызов метода для построения диаграммы
                    }

                }
            }
        }
        private void DrawChart(int[] values, int[] values1)
        {

            pictureBox1.Refresh();

            using (Graphics g = pictureBox1.CreateGraphics())
            {
                int startX = 70;
                int startY = pictureBox1.Height - 10;
                int barWidth = 80;
                int spacing = 50;


                using (Pen pen = new Pen(Color.Red))
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        int barHeight = values[i];
                        int x = startX + (barWidth + spacing) * i;
                        int y = startY - barHeight;


                        g.DrawRectangle(pen, x, y, barWidth, barHeight);
                    }
                }


                using (Pen pen1 = new Pen(Color.Blue))
                {
                    for (int i = 0; i < values1.Length; i++)
                    {
                        int barHeight = values1[i];
                        int x = startX + (barWidth + spacing) * i;
                        int y = startY - barHeight;


                        g.DrawRectangle(pen1, x, y, barWidth, barHeight);
                    }
                }
            }


            label2.ForeColor = Color.Red;
            label3.ForeColor = Color.Blue;
            label2.Text = "первый сериал";
            label3.Text = "второй сериал";
        }
        private void SaveChartToFile(int[] values, int[] values1, string saveFilePath)
        {
            using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    int startX = 70;
                    int startY = pictureBox1.Height - 10;
                    int barWidth = 80;
                    int spacing = 50;

                    using (Pen pen = new Pen(Color.Red))
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            int barHeight = values[i];
                            int x = startX + (barWidth + spacing) * i;
                            int y = startY - barHeight;
                            g.DrawRectangle(pen, x, y, barWidth, barHeight);
                        }
                    }

                    using (Pen pen1 = new Pen(Color.Blue))
                    {
                        for (int i = 0; i < values1.Length; i++)
                        {
                            int barHeight = values1[i];
                            int x = startX + (barWidth + spacing) * i;
                            int y = startY - barHeight;
                            g.DrawRectangle(pen1, x, y, barWidth, barHeight);
                        }
                    }
                }

                bmp.Save(saveFilePath, ImageFormat.Png);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text == "") && (comboBox2.Text == ""))
            {
                MessageBox.Show("Выберите сериалы", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(System.Windows.Forms.Application.ExecutablePath);
                Close();
            }
            else
               if (comboBox1.Text == "")
            {
                MessageBox.Show("Выберите первый сериал", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(System.Windows.Forms.Application.ExecutablePath);
                Close();
            }
            else
               if (comboBox2.Text == "")
            {
                MessageBox.Show("Выберите второй сериал", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(System.Windows.Forms.Application.ExecutablePath);
                Close();
            }
            else
            {
                string selectedFileName = comboBox1.SelectedItem.ToString();
                string filePath = Path.Combine("C:\\Users\\Пользователь\\Desktop\\сериалы", selectedFileName);

                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath); // Чтение содержимого файла
                    string[] numbers = content.Split(','); // Разделение содержимого по запятым
                    int[] values = new int[numbers.Length];

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if (int.TryParse(numbers[i], out int number))
                        {
                            values[i] = number; // Преобразование строк в числа
                        }
                    }
                    string selectedFileName1 = comboBox2.SelectedItem.ToString();
                    string filePath1 = Path.Combine("C:\\Users\\Пользователь\\Desktop\\сериалы", selectedFileName1);

                    if (File.Exists(filePath1))
                    {
                        string content1 = File.ReadAllText(filePath1); // Чтение содержимого файла
                        string[] numbers1 = content1.Split(','); // Разделение содержимого по запятым
                        int[] values1 = new int[numbers1.Length];

                        for (int i = 0; i < numbers1.Length; i++)
                        {
                            if (int.TryParse(numbers1[i], out int number1))
                            {
                                values1[i] = number1; // Преобразование строк в числа
                            }
                        }
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.Filter = "PNG Files (*.png)|*.png|All files (*.*)|*.*";

                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                string savePath = saveDialog.FileName;
                                SaveChartToFile(values, values1, savePath); // Вызов метода для сохранения графика в файл
                            }
                        }

                    }
                }
            }
        }

        
    }
}
