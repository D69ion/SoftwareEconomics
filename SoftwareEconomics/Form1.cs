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
            CreateDataGridTable();
        }
        private void buttonCalc_Click(object sender, EventArgs e)
        {
            double npv = NPV(Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value) / 100);
            textBoxResult.Text += "NPV = " + npv.ToString("F" + 2) + Environment.NewLine;
            double roi = ROI(npv);
            textBoxResult.Text += "ROI = " + roi.ToString("F" + 2) + Environment.NewLine;
            double pi = PI();
            textBoxResult.Text += "PI = " + pi.ToString("F" + 2) + Environment.NewLine;

            CalcSecondTable(npv);

            //IRR
            textBoxResult.Text += "IRR = " + (IRR() * 100).ToString("F" + 2) + '%'; //Math.Round(IRR(), 7);

            //построение графика
            FillChart();
        }

        private void FillChart()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[0].Color = Color.Green;
            chart1.Series[0].BorderWidth = 1;
            for (int i = 1; i < dataGridView2.Columns.Count; i++)
            {
                chart1.Series[0].Points.AddXY(i - 1, Convert.ToDouble(dataGridView2.Rows[2].Cells[i].Value));
            }
        }

        private void CalcSecondTable(double npv)
        {
            //заполнение второй таблицы
            //0 год
            dataGridView2.Rows[0].Cells[1].Value = dataGridView2.Rows[1].Cells[1].Value = dataGridView2.Rows[2].Cells[1].Value = -Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value);
            //1 год
            dataGridView2.Rows[0].Cells[2].Value = CFk(0);
            double r = Convert.ToDouble(dataGridView2.Rows[0].Cells[2].Value) / Math.Pow((1 + Convert.ToDouble(dataGridView1.Rows[2].Cells[1].Value) / 100), 1);
            dataGridView2.Rows[1].Cells[2].Value = r.ToString("F" + 2);
            dataGridView2.Rows[2].Cells[2].Value = (-(Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value)) + r).ToString("F" + 4);
            //2 и далее годы
            int year = 0;
            for (int i = 3; i < dataGridView2.ColumnCount; i++)
            {
                dataGridView2.Rows[0].Cells[i].Value = CFk(i - 2);
                r = Convert.ToDouble(dataGridView2.Rows[0].Cells[i].Value) / Math.Pow((1 + Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value) / 100), i - 1);
                dataGridView2.Rows[1].Cells[i].Value = r.ToString("F" + 2);
                npv = Convert.ToDouble(dataGridView2.Rows[2].Cells[i - 1].Value) + r;
                dataGridView2.Rows[2].Cells[i].Value = npv.ToString("F" + 2);
                if (npv > 0)
                    year = i - 1;
            }
            //срок окупаемости РВР
            textBoxResult.Text += "PBP = " + (year - 1 + Math.Abs(Convert.ToDouble(dataGridView2.Rows[2].Cells[year - 1].Value)) / Convert.ToDouble(dataGridView2.Rows[1].Cells[year].Value)).ToString("F" + 2) + Environment.NewLine;
        }

        private double PI()
        {
            double res = 0.0;
            int n = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value);
            for (int i = 0; i < n; i++)
            {
                res += CFk(i) / Math.Pow((1 + Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value) / 100), i + 1);
            }
            res /= Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value);
            return res;
        }

        private double ROI(double npv)
        {
            return npv / Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value);
        }

        private double NPV(double i)
        {
            double res = 0.0;
            for (int j = 0; j < Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value); j++)
            {
                res += CFk(j) / Math.Pow(1 + i, j + 1);
            }
            res -= Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value);
            return res;
        }

        private double IRR()
        {
            for (double i = 0; i < 1.0001; i += 0.0001)
            {
                if (NPV(i) < 1)
                {
                    return i;
                }
            }
            return -1;
        }
        private int CFk(int i)
        {
            return Convert.ToInt32(dataGridView1.Rows[3 + i].Cells[1].Value) -
                Convert.ToInt32(dataGridView1.Rows[6 + i].Cells[1].Value);
        }

        private void CreateDataGridTable()
        {
            /*
            dataGridView1.RowCount = 8;
            dataGridView1.Rows[0].Cells[0].Value = "";
            dataGridView1.Rows[0].Cells[1].Value = "";
            */
            //вторая таблица
            dataGridView1.RowCount = 9;
            dataGridView1.Rows[0].Cells[0].Value = "Стартовые инвестиции, Ic";
            dataGridView1.Rows[0].Cells[1].Value = 120000;
            dataGridView1.Rows[1].Cells[0].Value = "Ставка дисконтирования, i";
            dataGridView1.Rows[1].Cells[1].Value = 11;
            dataGridView1.Rows[2].Cells[0].Value = "Горизонт расчета проекта(кол-во лет), n";
            dataGridView1.Rows[2].Cells[1].Value = 3;
            dataGridView1.Rows[3].Cells[0].Value = "Приток средств в 1-й год, DP1";
            dataGridView1.Rows[3].Cells[1].Value = 70000;
            dataGridView1.Rows[4].Cells[0].Value = "Приток средств во 2-й год, DP2";
            dataGridView1.Rows[4].Cells[1].Value = 70000;
            dataGridView1.Rows[5].Cells[0].Value = "Приток средств в 3-й год, DP3";
            dataGridView1.Rows[5].Cells[1].Value = 70000;
            dataGridView1.Rows[6].Cells[0].Value = "Отток средств в 1-й год, Z1";
            dataGridView1.Rows[6].Cells[1].Value = 20000;
            dataGridView1.Rows[7].Cells[0].Value = "Отток средств во 2-й год, Z2";
            dataGridView1.Rows[7].Cells[1].Value = 15000;
            dataGridView1.Rows[8].Cells[0].Value = "Отток средств в 3-й год, Z3";
            dataGridView1.Rows[8].Cells[1].Value = 10000;
            //вторая таблица
            dataGridView2.RowCount = 3;
            dataGridView2.ColumnCount = 1; //Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value) + 1;
            dataGridView2.Rows[0].Cells[0].Value = "Денежный поток";
            dataGridView2.Rows[1].Cells[0].Value = "Дисконтированный денежный поток";
            dataGridView2.Rows[2].Cells[0].Value = "Накопленный денежный поток";
            for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value) + 1; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.HeaderText = i + " год";
                dataGridView2.Columns.Add(column);
            }
        }
    }
}
