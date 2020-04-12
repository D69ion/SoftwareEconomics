using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SoftwareEconomics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FillInitialValues();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillTable21();
            double s = Math.Round(Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value) * 0.05, 2);
            FillTable22(s);
            int a = Convert.ToInt32(dataGridView3.Rows[4].Cells[1].Value),
                b = Convert.ToInt32(dataGridView4.Rows[4].Cells[1].Value);
            double x0 = a / (s - b);
            textBox1.Text += string.Format("В течение месяца фирме необходимо подготовить и продать миниммум {0} копий " +
                "программного продукта по цене {1} руб., чтобы окупить постоянные и переменные расходыв рамках " +
                "ее деятельности на создание программного продукта" + Environment.NewLine + Environment.NewLine, Math.Ceiling(x0), s);
            FillGraph(x0, s);

            int xp = Convert.ToInt32(dataGridView2.Rows[1].Cells[1].Value);
            double sm = Math.Round((double)(a + b * xp) / xp, 2);
            double discount = Math.Round((1 - sm / s) * 100, 2);
            textBox1.Text += string.Format("При гарантированном объеме рынка продаж в количестве {0} копий цена " +
                "тиражируемого продукта может быть снижена относительно начальной до {1} руб. за копию, что " +
                "позволяет установить скидку покупателю в размере {2}%" + Environment.NewLine + Environment.NewLine, xp, sm, discount);

            int pd = Convert.ToInt32(dataGridView2.Rows[2].Cells[1].Value) * 1000;
            int xd = (int)Math.Ceiling((pd + a) / (s - b));
            textBox1.Text += string.Format("Объем продаж для получения дополнительной прибыли в размере {0} тыс. " +
                "рублей составляет {1} копий продукта в месяц при условии, что постоянные и переменные издержки фирмы неизменны" +
                Environment.NewLine + Environment.NewLine, pd, xd);

            int xn = (int)Math.Ceiling((Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value) * a) / (s - b));
            int Cok = (int)Math.Ceiling(xn / x0);
            textBox1.Text += string.Format("Срок окупаемости проекта при продаже не менее {0} копий продукта в месяц " +
                "(точка безубыточности) и рыночной стоимости {1} руб. за копию составит {2} месяцев." + Environment.NewLine +
                "Для того чтобы окупить все расходы на реализацию проекта, необходимо продать {3} копий программного продукта" +
                Environment.NewLine, Math.Ceiling(x0), s, Cok, xn);
        }

        private void FillGraph(double x0, double s)
        {
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            chart1.ChartAreas[0].AxisY.LabelAutoFitMaxFontSize = 8;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 15;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].Position.Width = 66;
            chart1.Series.Clear();

            Series series = new Series();
            series.LegendText = "Постоянные издержки";
            series.ChartType = SeriesChartType.Line;
            series.Points.AddXY(0, Convert.ToInt32(dataGridView3.Rows[4].Cells[1].Value));
            series.Points.AddXY(x0, Convert.ToInt32(dataGridView3.Rows[4].Cells[1].Value));
            chart1.Series.Add(series);
            
            series = new Series();
            series.LegendText = "Переменные издержки";
            series.ChartType = SeriesChartType.Line;
            series.Points.AddXY(0, 0);
            series.Points.AddXY(x0, Convert.ToInt32(dataGridView4.Rows[4].Cells[1].Value));
            chart1.Series.Add(series);

            series = new Series();
            series.LegendText = "Общие издержки";
            series.ChartType = SeriesChartType.Line;
            series.Points.AddXY(0, Convert.ToInt32(dataGridView3.Rows[4].Cells[1].Value));
            series.Points.AddXY(x0, Convert.ToInt32(dataGridView3.Rows[4].Cells[1].Value) + Convert.ToInt32(dataGridView4.Rows[4].Cells[1].Value));
            chart1.Series.Add(series);

            series = new Series();
            series.LegendText = "Точка безубыточности";
            series.ChartType = SeriesChartType.Line;
            series.Points.AddXY(x0, 0);
            series.Points.AddXY(x0, Convert.ToInt32(dataGridView3.Rows[4].Cells[1].Value) + Convert.ToInt32(dataGridView4.Rows[4].Cells[1].Value));
            chart1.Series.Add(series);

            series = new Series();
            series.LegendText = "Доход от продаж";
            series.ChartType = SeriesChartType.Line;
            series.Points.AddXY(0, 0);
            series.Points.AddXY(x0, x0 * s - Convert.ToInt32(dataGridView3.Rows[4].Cells[1].Value) - Convert.ToInt32(dataGridView4.Rows[4].Cells[1].Value));
            chart1.Series.Add(series);

        }

        private void FillTable21()
        {
            dataGridView3.RowCount = 5;
            dataGridView3.Rows[0].Cells[0].Value = "Накладные расходы по проекту";
            dataGridView3.Rows[0].Cells[1].Value = 10000;
            dataGridView3.Rows[1].Cells[0].Value = "Плановое ежемесячное гашение кредита";
            dataGridView3.Rows[1].Cells[1].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value) * Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value) / 100 / 12, 2);
            dataGridView3.Rows[2].Cells[0].Value = "Выплата среднего банковского процента";
            dataGridView3.Rows[2].Cells[1].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value) * Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value) / 100, 2);
            dataGridView3.Rows[3].Cells[0].Value = "Прочие расходы";
            dataGridView3.Rows[3].Cells[1].Value = 1000;
            dataGridView3.Rows[4].Cells[0].Value = "Итого";
            dataGridView3.Rows[4].Cells[1].Value = Convert.ToInt32(dataGridView3.Rows[3].Cells[1].Value) + Convert.ToInt32(dataGridView3.Rows[2].Cells[1].Value) +
                Convert.ToInt32(dataGridView3.Rows[1].Cells[1].Value) + Convert.ToInt32(dataGridView3.Rows[0].Cells[1].Value);
        }

        private void FillTable22(double s)
        {
            dataGridView4.RowCount = 5;
            dataGridView4.Rows[0].Cells[0].Value = "Основная зарплата специалистов";
            dataGridView4.Rows[0].Cells[1].Value = Math.Round(s * 0.45, 2);
            dataGridView4.Rows[1].Cells[0].Value = "Страховые взносы от фонда зарплаты";
            dataGridView4.Rows[1].Cells[1].Value = Math.Round(Convert.ToDouble(dataGridView4.Rows[0].Cells[1].Value) * 0.3, 2);
            dataGridView4.Rows[2].Cells[0].Value = "Комплектующие и расходные материалы";
            dataGridView4.Rows[2].Cells[1].Value = Math.Round(s * 0.01, 2);
            dataGridView4.Rows[3].Cells[0].Value = "Накладные расходы отдела маркетинга";
            dataGridView4.Rows[3].Cells[1].Value = Math.Round(s * 0.015, 2);
            dataGridView4.Rows[4].Cells[0].Value = "Итого";
            dataGridView4.Rows[4].Cells[1].Value = Convert.ToInt32(dataGridView4.Rows[3].Cells[1].Value) + Convert.ToInt32(dataGridView4.Rows[2].Cells[1].Value) +
                Convert.ToInt32(dataGridView4.Rows[1].Cells[1].Value) + Convert.ToInt32(dataGridView4.Rows[0].Cells[1].Value);

        }

        private void FillInitialValues()
        {
            dataGridView1.RowCount = 3;
            dataGridView1.Rows[0].Cells[0].Value = "Размер кредита";
            dataGridView1.Rows[0].Cells[1].Value = 1280582;
            dataGridView1.Rows[1].Cells[0].Value = "Срок кредита";
            dataGridView1.Rows[1].Cells[1].Value = 15;
            dataGridView1.Rows[2].Cells[0].Value = "% годовых";
            dataGridView1.Rows[2].Cells[1].Value = 15;

            dataGridView2.RowCount = 4;
            dataGridView2.Rows[0].Cells[0].Value = "Процент банковского кредита";
            dataGridView2.Rows[0].Cells[1].Value = 15;
            dataGridView2.Rows[1].Cells[0].Value = "Заданный объем рынка продаж";
            dataGridView2.Rows[1].Cells[1].Value = 20;
            dataGridView2.Rows[2].Cells[0].Value = "Доп. прибыль (тыс. руб.)";
            dataGridView2.Rows[2].Cells[1].Value = 120;
            dataGridView2.Rows[3].Cells[0].Value = "Зарплата специалистов отдела маркетинга (%)";
            dataGridView2.Rows[3].Cells[1].Value = 45;
        }

    }
}
