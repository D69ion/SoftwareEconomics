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
            CreateResultTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] vs = new int[3];
            vs[0] = FirstMethod();
            vs[1] = SecondMethod();
            vs[2] = ThirdMethod();
            int min = 0;
            for (int i = 1; i < vs.Length; i++)
            {
                if (vs[i] < vs[min])
                    min = i;
            }
            textBox1.Text += "Наименее затратные данные получены ";
            switch (min)
            {
                case 0:
                    textBox1.Text += "прямым методом" + Environment.NewLine;
                    break;
                case 1:
                    textBox1.Text += "методом определения на основе размерности БД программной системы" + Environment.NewLine;
                    break;
                case 2:
                    textBox1.Text += "методом функциональных точек" + Environment.NewLine;
                    break;

            }
            int T = Convert.ToInt32(dataGridView2.Rows[min].Cells[1].Value),
                Z = Convert.ToInt32(dataGridView2.Rows[min].Cells[3].Value),
                D = Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value);
            int SalaryFund = CalcSalaryFund(T, Z, D);
        }

        private int CalcSalaryFund(int T, int Z, int D)
        {
            //табл 1.7
            dataGridView3.RowCount = 4;
            dataGridView3.Rows[0].Cells[0].Value = "Анализ предметной области и разработка требований";
            dataGridView3.Rows[1].Cells[0].Value = "Проектирование";
            dataGridView3.Rows[2].Cells[0].Value = "Программирование";
            dataGridView3.Rows[3].Cells[0].Value = "Тестирование и комплексные испытания";
            dataGridView3.Rows[0].Cells[1].Value = Math.Floor((0.1 * T) / (0.1 * D));
            dataGridView3.Rows[1].Cells[1].Value = Math.Floor((0.22 * T) / (0.3 * D));
            dataGridView3.Rows[2].Cells[1].Value = Math.Floor((0.405 * T) / (0.35 * D));
            dataGridView3.Rows[3].Cells[1].Value = Math.Floor((0.275 * T) / (0.25 * D));
            dataGridView3.Rows[0].Cells[2].Value = Math.Floor(0.1 * D);
            dataGridView3.Rows[1].Cells[2].Value = Math.Floor(0.3 * D);
            dataGridView3.Rows[2].Cells[2].Value = Math.Floor(0.35 * D);
            dataGridView3.Rows[3].Cells[2].Value = Math.Floor(0.25 * D);

            //проблемы в кол-ве людейю они бывают 0
            //табл 1.9
            dataGridView4.RowCount = 4;
            dataGridView4.Rows[0].Cells[0].Value = "Анализ предметной области и разработка требований";
            dataGridView4.Rows[1].Cells[0].Value = "Проектирование";
            dataGridView4.Rows[2].Cells[0].Value = "Программирование";
            dataGridView4.Rows[3].Cells[0].Value = "Тестирование и комплексные испытания";

            dataGridView4.Rows[0].Cells[1].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[0].Cells[1].Value) * 0.4);
            dataGridView4.Rows[1].Cells[1].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[1].Cells[1].Value) * 0.35);
            dataGridView4.Rows[2].Cells[1].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[2].Cells[1].Value) * 0.1);
            dataGridView4.Rows[3].Cells[1].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[3].Cells[1].Value) * 0.15);

            dataGridView4.Rows[0].Cells[2].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[0].Cells[1].Value) * 0.2);
            dataGridView4.Rows[1].Cells[2].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[1].Cells[1].Value) * 0.35);
            dataGridView4.Rows[2].Cells[2].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[2].Cells[1].Value) * 0.65);
            dataGridView4.Rows[3].Cells[2].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[3].Cells[1].Value) * 0.6);

            dataGridView4.Rows[0].Cells[3].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[0].Cells[1].Value) * 0.4);
            dataGridView4.Rows[1].Cells[3].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[1].Cells[1].Value) * 0.3);
            dataGridView4.Rows[2].Cells[3].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[2].Cells[1].Value) * 0.25);
            dataGridView4.Rows[3].Cells[3].Value = Math.Floor(Convert.ToInt32(dataGridView3.Rows[3].Cells[1].Value) * 0.25);

            return 0;
        }

        private int FirstMethod()
        {
            //P - 220 строк/чел-месяц
            double P = 220;
            double R = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value);
            double T = R / P;
            double Z = T / Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value);
            dataGridView2.Rows[0].Cells[1].Value = Math.Floor(T);
            dataGridView2.Rows[0].Cells[2].Value = dataGridView1.Rows[1].Cells[1].Value;
            dataGridView2.Rows[0].Cells[3].Value = Math.Floor(Z);
            return (int)Math.Floor(T) * (int)Math.Floor(Z);
        }

        private int SecondMethod()
        {
            int R = 100 * Convert.ToInt32(dataGridView1.Rows[3].Cells[1].Value) * Convert.ToInt32(dataGridView1.Rows[4].Cells[1].Value) * Convert.ToInt32(dataGridView1.Rows[5].Cells[1].Value);
            double O = 0.0;
            if (R < 90000)
                O = 0.00566;
            else if (R >= 90000 && R < 200000)
                O = 0.00808;
            else if (R >= 200000 && R < 500000)
                O = 0.01537;
            double T = 0.01 * R * O;
            double Z = T / Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value);
            dataGridView2.Rows[1].Cells[1].Value = Math.Floor(T);
            dataGridView2.Rows[1].Cells[2].Value = dataGridView1.Rows[1].Cells[1].Value;
            dataGridView2.Rows[1].Cells[3].Value = Math.Floor(Z);
            return (int)Math.Floor(T) * (int)Math.Floor(Z);
        }

        private int ThirdMethod()
        {
            double W = 0.65 + 0.01 * Convert.ToInt32(dataGridView1.Rows[7].Cells[1].Value);
            double RF = W * Convert.ToInt32(dataGridView1.Rows[6].Cells[1].Value);
            int LOC = 0;
            switch (dataGridView1.Rows[0].Cells[1].Value.ToString())
            {
                case "Basic Assembler":
                    LOC = 320;
                    break;
                case "Macro Assembler ":
                    LOC = 213;
                    break;
                case "Basic":
                    LOC = 107;
                    break;
                case "Pascal":
                    LOC = 91;
                    break;
                case "C++":
                    LOC = 53;
                    break;
                case "C#":
                    LOC = 53;
                    break;
                case "Java":
                    LOC = 53;
                    break;
                case "Oracle":
                    LOC = 40;
                    break;
                case "Sybase":
                    LOC = 40;
                    break;
                case "Access":
                    LOC = 38;
                    break;
                case "Delphi":
                    LOC = 29;
                    break;
                case "Oracle Developer/2000":
                    LOC = 23;
                    break;
                case "Perl":
                    LOC = 20;
                    break;
                case "HTML 3.0":
                    LOC = 15;
                    break;
                case "SQL (ANSI)":
                    LOC = 13;
                    break;
                case "Excel":
                    LOC = 6;
                    break;
            }
            double Rloc = RF * LOC;
            double A = 3;
            double E = 1.12;
            double T = A * Math.Pow(Rloc / 1000, E) / 12;
            double Z = T / Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value);
            dataGridView2.Rows[2].Cells[1].Value = Math.Floor(T);
            dataGridView2.Rows[2].Cells[2].Value = dataGridView1.Rows[1].Cells[1].Value;
            dataGridView2.Rows[2].Cells[3].Value = Math.Floor(Z);
            return (int)Math.Floor(T) * (int)Math.Floor(Z);
        }

        private void FillInitialValues()
        {
            //тип системы исс
            //сложность системы простая до 30000 строк
            dataGridView1.RowCount = 9;
            dataGridView1.Rows[0].Cells[0].Value = "Язык программирования";
            dataGridView1.Rows[0].Cells[1].Value = "Oracle";
            dataGridView1.Rows[1].Cells[0].Value = "Срок разработки (мес.)";
            dataGridView1.Rows[1].Cells[1].Value = 15;
            dataGridView1.Rows[2].Cells[0].Value = "Размерность системы определенная экспертами";
            dataGridView1.Rows[2].Cells[1].Value = 15000;
            dataGridView1.Rows[3].Cells[0].Value = "БД - N";
            dataGridView1.Rows[3].Cells[1].Value = 13;
            dataGridView1.Rows[4].Cells[0].Value = "БД - K1";
            dataGridView1.Rows[4].Cells[1].Value = 20;
            dataGridView1.Rows[5].Cells[0].Value = "БД - M";
            dataGridView1.Rows[5].Cells[1].Value = 15;
            dataGridView1.Rows[6].Cells[0].Value = "Кол-во функциональных точек";
            dataGridView1.Rows[6].Cells[1].Value = 3000;
            dataGridView1.Rows[7].Cells[0].Value = "V - коэффициент внешней среды";
            dataGridView1.Rows[7].Cells[1].Value = 53;
            dataGridView1.Rows[8].Cells[0].Value = "Ставка программиста (руб.)";
            dataGridView1.Rows[8].Cells[1].Value = 16000;
        }

        private void CreateResultTable()
        {
            dataGridView2.RowCount = 3;
            dataGridView2.Rows[0].Cells[0].Value = "Прямой метод";
            dataGridView2.Rows[1].Cells[0].Value = "На основе Размерности БД системы";
            dataGridView2.Rows[2].Cells[0].Value = "Метод Функциональных точек";
        }
    }
}
