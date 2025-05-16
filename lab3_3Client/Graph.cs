using ScottPlot;

namespace lab3_3Client
{
    public partial class Graph : Form
    {
        private readonly FormController controller = new();

        public Graph()
        {
            InitializeComponent();

            // оформили единственный график
            ConfigurePlot(chart,
                title: "Зависимость давления от температуры",
                xLabel: "Температура, °C",
                yLabel: "Давление, атм");

            controller.Errors += ShowError;
            controller.DataUpdated += UpdateGraph;
        }

        /* ------------ внешний вид графика ------------- */
        private void ConfigurePlot(FormsPlot plt, string title, string xLabel, string yLabel)
        {
            var pc = plt.Plot;

            pc.Title(title, size: 18, bold: true);

            pc.XAxis.Label(xLabel, size: 16);
            pc.YAxis.Label(yLabel, size: 16);

            pc.XAxis.TickLabelStyle(fontSize: 24);
            pc.YAxis.TickLabelStyle(fontSize: 24);

            pc.Grid(enable: true, lineStyle: LineStyle.Dot);
        }

        /* ------------- обработчики UI ------------------ */
        private void btnConnect_Click(object _, EventArgs __)
        {
            controller.ConnectToServer(tBoxIPAddress.Text.Trim());
            controller.StartGetData();
        }

        private void btn_disconnect_Click(object _, EventArgs __) => controller.Disconnect();

        private void ResetGraphs_Click(object _, EventArgs __)
        {
            controller.ClearValues();
            chart.Plot.Clear();
            chart.Refresh();
        }

        /* ------------- визуализация данных -------------- */
        /* ------------- визуализация данных -------------- */
        private void UpdateGraph(List<double> temps, List<double> presses)
        {
            if (InvokeRequired) { BeginInvoke(() => UpdateGraph(temps, presses)); return; }

            chart.Plot.Clear();

            // ❶ упаковали пары, отсортировали по температуре
            var pairs = temps.Zip(presses, (t, p) => (t, p))
                             .OrderBy(tp => tp.t)
                             .ToArray();

            double[] xs = pairs.Select(tp => tp.t).ToArray();   // T
            double[] ys = pairs.Select(tp => tp.p).ToArray();   // P

            // ❷ только точки (marker), без соединяющей ломаной
            // вместо AddScatter(... lineWidth: 0, markerSize: 6)
            chart.Plot.AddScatter(xs, ys,
                                  lineWidth: 2,      // >0  ─ появится линия
                                  markerSize: 0,      // маркеры можно оставить
                                  //markerShape: MarkerShape.filledCircle,
                                  color: Color.RoyalBlue);


            // ❸ фиксируем диапазоны (по условию Т 0-100 °C, P 0-6 атм)
            chart.Plot.SetAxisLimits(xMin: 0, xMax: 100,
                                     yMin: 0, yMax: 6);

            chart.Render();
        }


        /* ------------- показ ошибок -------------------- */
        private void ShowError(string msg) =>
            MessageBox.Show(this, msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
