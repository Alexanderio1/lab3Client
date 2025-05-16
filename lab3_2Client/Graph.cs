using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.WinForms;
using System.Drawing;

namespace lab3_2Client
{
    public partial class Graph : Form
    {
        private readonly FormController controller = new();

        public Graph()
        {
            InitializeComponent();

            // настраиваем оба графика одинаково
            ConfigurePlot(Temperature, "Температура процесса", "Время (с)", "Температура (°C)");
            ConfigurePlot(Pressure, "Давление процесса", "Время (с)", "Давление (атм)");

            controller.Errors += ShowError;
            controller.DataUpdated += UpdateGraph;
        }

        private void ConfigurePlot(FormsPlot plt, string title, string xLabel, string yLabel)
        {
            var pc = plt.Plot;

            pc.Title(title, size: 28);

            pc.XAxis.Label(xLabel, size: 26);
            pc.YAxis.Label(yLabel, size: 26);

            pc.XAxis.TickLabelStyle(fontSize: 24);
            pc.YAxis.TickLabelStyle(fontSize: 24);

            // пунктирная сетка
            pc.Grid(enable: true, lineStyle: LineStyle.Dot);
        }

        private void ShowError(string msg)
        {
            MessageBox.Show(this, msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void UpdateGraph(List<double> temps, List<double> presses)
        {
            // очищаем
            Temperature.Plot.Clear();
            Pressure.Plot.Clear();

            // рисуем сигналы
            Temperature.Plot.AddSignal(temps.ToArray(), sampleRate: 1);
            Pressure.Plot.AddSignal(presses.ToArray(), sampleRate: 1);

            // автоподгонка по Y
            Temperature.Plot.AxisAutoY();
            Pressure.Plot.AxisAutoY();

            // окно последних 10 точек по X
            int n = temps.Count;
            double xMin = Math.Max(0, n - 10);
            double xMax = Math.Max(n, 10);
            Temperature.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);
            Pressure.Plot.SetAxisLimits(xMin: xMin, xMax: xMax);

            // обновляем рендер
            Temperature.Render();
            Pressure.Render();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            controller.ConnectToServer(tBoxIPAddress.Text.Trim());
            controller.StartGetData();
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            controller.Disconnect();
        }

        private void ResetGraphs_Click(object sender, EventArgs e)
        {
            controller.ClearValues();

            Temperature.Plot.Clear();
            Pressure.Plot.Clear();
            Temperature.Plot.AxisAuto();
            Pressure.Plot.AxisAuto();
            Temperature.Render();
            Pressure.Render();
        }
    }
}
