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

    public class ChartDataPoints
    {
        public int ID { get; set; }
        public DateTime TimeStamp { get; set; }
        public float Value { get; set; }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Random rand = new Random();
            DateTime currentTime = DateTime.Today.AddDays( -2 );
            List<ChartDataPoints> chartDataPoints = new List<ChartDataPoints>();
            for (int i = 0; i < 100; i++)
            {
                ChartDataPoints dataPoint = new ChartDataPoints()
                {
                    ID = i,
                    TimeStamp = currentTime.AddHours(1),
                    Value = rand.Next( 1000 )
                };
                chartDataPoints.Add(dataPoint);
            }
            
            chart1.Series.Add("linechart");
            chart1.DataSource = chartDataPoints;
            chart1.Series[1].XValueMember = "TimeStamp";
            chart1.Series[1].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            chart1.Series[1].YValueMembers = "Value";
            chart1.DataBind();


        }
    }
}
