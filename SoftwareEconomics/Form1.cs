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
            FillInitialValues();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillTable21();
            double s = Math.Round(Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value) * 0.05, 2);
            FillTable22(s);

        }

        private void FillTable21()
        {
            dataGridView3.RowCount = 5;
            dataGridView3.Rows[0].Cells[0].Value = "Накладные расходы по проекту";
            dataGridView3.Rows[0].Cells[1].Value = 10000;
            dataGridView3.Rows[1].Cells[0].Value = "Плановое ежемесячное гашение кредита";
            dataGridView3.Rows[1].Cells[1].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value) * Convert.ToDouble(dataGridView2.Rows[0].Cells[1].Value) / 100, 2);
            dataGridView3.Rows[2].Cells[0].Value = "Выплата среднего банковского процента";
            dataGridView3.Rows[2].Cells[1].Value = 0;
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
            dataGridView1.Rows[0].Cells[1].Value = 1000;
            dataGridView1.Rows[1].Cells[0].Value = "Срок выплаты";
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
