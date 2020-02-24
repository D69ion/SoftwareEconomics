using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareEconomics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "26";
        }
        private void buttonCalc_Click(object sender, EventArgs e)
        {
            //label2.Text = dataGridView1.Rows[3].Cells[0].Value.ToString();
            CreateDataGrids();
            int res = 0;
            for (int i = 0; i < 13; i++)
            {
                res += Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);
            }
            textBoxResult.Text += "Прямые затраты (ПЗ) = " + res + Environment.NewLine;

        }

        private void CreateDataGrids()
        {
            //коэффициент варианта
            double k = Convert.ToDouble(textBox1.Text) / 100.0 + 1.0;
            //заполнение статистики организации
            //dataGridView1.Rows[0].Cells[0].Value = "";
            //dataGridView1.Rows[0].Cells[1].Value = Math.Round(*k);

            dataGridView1.RowCount = 4;
            dataGridView1.Rows[0].Cells[0].Value = "Кол-во ПК в организации";
            dataGridView1.Rows[0].Cells[1].Value = Math.Round(150 * k);
            dataGridView1.Rows[1].Cells[0].Value = "Кол-во пользователей в организации";
            dataGridView1.Rows[1].Cells[1].Value = Math.Round(170 * k);
            dataGridView1.Rows[2].Cells[0].Value = "Годовой валовой доход компании, руб.";
            dataGridView1.Rows[2].Cells[1].Value = Math.Round(75650000 * k);
            dataGridView1.Rows[3].Cells[0].Value = "Средняя зарплата пользователя";
            dataGridView1.Rows[3].Cells[1].Value = Math.Round(12000 * k);
            //заполнение ит-бюджет
            dataGridView2.RowCount = 13;
            dataGridView2.Rows[0].Cells[0].Value = "Затраты на закупку оборудования";
            dataGridView2.Rows[0].Cells[1].Value = Math.Round(400000 * k);
            dataGridView2.Rows[1].Cells[0].Value = "Затраты на ПО";
            dataGridView2.Rows[1].Cells[1].Value = Math.Round(150000 * k);
            dataGridView2.Rows[2].Cells[0].Value = "Затраты на комплектующие";
            dataGridView2.Rows[2].Cells[1].Value = Math.Round(130000 * k);
            dataGridView2.Rows[3].Cells[0].Value = "Затраты на зарплату персонала по категориям";
            //dataGridView2.Rows[3].Cells[1].Value = Math.Round(*k);
            dataGridView2.Rows[4].Cells[0].Value = "Системный администратор - 1 ед.";
            dataGridView2.Rows[4].Cells[1].Value = Math.Round(190000 * k);
            dataGridView2.Rows[5].Cells[0].Value = "ИТ- менеджер - 1 ед.";
            dataGridView2.Rows[5].Cells[1].Value = Math.Round(260000 * k);
            dataGridView2.Rows[6].Cells[0].Value = "Программист - 1 ед.";
            dataGridView2.Rows[6].Cells[1].Value = Math.Round(100000 * k);
            dataGridView2.Rows[7].Cells[0].Value = "Персонал технической поддержки - 2 ед.";
            dataGridView2.Rows[7].Cells[1].Value = Math.Round(380000 * k);
            dataGridView2.Rows[8].Cells[0].Value = "Затраты на обучение";
            dataGridView2.Rows[8].Cells[1].Value = Math.Round(70000 * k);
            dataGridView2.Rows[9].Cells[0].Value = "Затраты на внешнюю поддержку (outsourcing)";
            dataGridView2.Rows[9].Cells[1].Value = Math.Round(180000 * k);
            dataGridView2.Rows[10].Cells[0].Value = "Затраты на разработку/внедрение ИТ- проектов";
            dataGridView2.Rows[10].Cells[1].Value = Math.Round(300000 * k);
            dataGridView2.Rows[11].Cells[0].Value = "Затраты на телефонию";
            dataGridView2.Rows[11].Cells[1].Value = Math.Round(140000 * k);
            dataGridView2.Rows[12].Cells[0].Value = "Затраты на Интернет";
            dataGridView2.Rows[12].Cells[1].Value = Math.Round(100000 * k);
            //заполнение данных для расчета затрат пользователей на ит
            dataGridView3.RowCount = 4;
            dataGridView3.Rows[0].Cells[0].Value = "Кол-во пользователей в организации, Кп";
            dataGridView3.Rows[0].Cells[1].Value = Math.Round(170 * k);
            dataGridView3.Rows[1].Cells[0].Value = "Средняя зарплата пользователя, Зп";
            dataGridView3.Rows[1].Cells[1].Value = Math.Round(12000 * k);
            dataGridView3.Rows[2].Cells[0].Value = "Среднее кол-во рабочих часов в месяце, Рч";
            dataGridView3.Rows[2].Cells[1].Value = Math.Round(168 * k);
            dataGridView3.Rows[3].Cells[0].Value = "Кол-во часов в месяц, затрачиваемых одним пользователем на самообучение, обслуживание компьютера, файлов и программ, Пч";
            dataGridView3.Rows[3].Cells[1].Value = Math.Round(8 * k);
            //заполенение данных для расчета стоимости простоев ит системы
            dataGridView4.RowCount = 4;
            dataGridView4.Rows[0].Cells[0].Value = "Кол-во отключений системы в месяц, Ко";
            dataGridView4.Rows[0].Cells[1].Value = Math.Round(2 * k);
            dataGridView4.Rows[1].Cells[0].Value = "Средняя продолжительность отключений, часов, Чо";
            dataGridView4.Rows[1].Cells[1].Value = Math.Round(3 * k);
            dataGridView4.Rows[2].Cells[0].Value = "Количество отключенных пользователей, По";
            dataGridView4.Rows[2].Cells[1].Value = Math.Round(20 * k);
            dataGridView4.Rows[3].Cells[0].Value = "Годовой валовой доход компании, руб., Гд";
            dataGridView4.Rows[3].Cells[1].Value = Math.Round(78880000 * k);
            //вычисление промежуточных показателей
            dataGridView5.RowCount = 3;
            dataGridView5.Rows[0].Cells[0].Value = "Часовая оплата пользователя, руб./ч, ЧОп";
            dataGridView5.Rows[0].Cells[1].Value = Convert.ToDouble(dataGridView3.Rows[1].Cells[1].Value) / Convert.ToDouble(dataGridView3.Rows[2].Cells[1].Value);
            dataGridView5.Rows[1].Cells[0].Value = "Доход на каждого работника, руб./ч, Чд";
            dataGridView5.Rows[1].Cells[1].Value = Convert.ToDouble(dataGridView4.Rows[3].Cells[1].Value) / 12.0 / Convert.ToDouble(dataGridView3.Rows[2].Cells[1].Value) / Convert.ToDouble(dataGridView3.Rows[0].Cells[1].Value);
            dataGridView5.Rows[2].Cells[0].Value = "Простои, часов в год, Гп";
            dataGridView5.Rows[2].Cells[1].Value = Convert.ToInt32(dataGridView4.Rows[0].Cells[1].Value) * Convert.ToInt32(dataGridView4.Rows[1].Cells[1].Value) * 12;

        }
    }
}
