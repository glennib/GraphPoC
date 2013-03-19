using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GraphPoC
{
    public partial class Form1 : Form
    {
        // Alle  datapunkt, og nåværende tidspunkt
        List<ChartDataPoint> allDataPoints = ChartDataPoint.GenerateDataPoints();
        DateTime currentTime = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
            
            // Sett opp grafen
            SetupGraph();

            // Hent ut initielle verdier og kjør databind
            InitializeGraph( currentTime );
        }

        public void SetupGraph()
        {
            // Sett opp grafen og sett Timestamp og Value som members av klassen ChartDataPoint som skal være de som er henhodsvis akse X og Y
            chart1.Series.Add("linechart");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            chart1.Series[0].XValueMember = "TimeStamp";
            chart1.Series[0].YValueMembers = "Value";           
        }

        public IEnumerable<ChartDataPoint> SelectDataPoints( DateTime date )
        {
            // Returner fra allDataPoints de datapunktene som er innenfor tidsrammen som vi ønsker
            return allDataPoints.Where(c => c.TimeStamp >= date.AddDays(-1) && c.TimeStamp <= date.AddDays(1));
        }

        public void InitializeGraph( DateTime date )
        {
            // Hent ut datapunktene som er ønskelige
            var selectedDataPoints = SelectDataPoints( date );

            // Legg datapunktene til som DataSource, og kjør databindingen
            chart1.DataSource = selectedDataPoints; 
            chart1.DataBind();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Legg til en time, og oppdater graf
            currentTime = currentTime.AddHours(1);
            
            InitializeGraph(currentTime);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Trekk fra en time, og oppdater graf
            currentTime = currentTime.AddHours(-1);

            InitializeGraph(currentTime);
        }
    }
    
    // Klasse som beskriver et datapunkt. 
    public class ChartDataPoint
    {        
        public int ID { get; set; }
        public DateTime TimeStamp { get; set; }
        public float Value { get; set; }

        // Genererer opp 100000 datapunkt med tilfeldig verdi og som starter fra for -5 dager fra nå
        public static List<ChartDataPoint> GenerateDataPoints()
        {
            Random rand = new Random();
            DateTime currentTime = DateTime.Today.AddDays(-5);
            List<ChartDataPoint> chartDataPoints = new List<ChartDataPoint>();
            for (int i = 0; i < 100000; i++)
            {
                currentTime = currentTime.AddMinutes(25);
                ChartDataPoint dataPoint = new ChartDataPoint()
                {
                    ID = i,
                    TimeStamp = currentTime,
                    Value = rand.Next(1000)
                };
                chartDataPoints.Add(dataPoint);
            }

            return chartDataPoints;
        }
    }
}
