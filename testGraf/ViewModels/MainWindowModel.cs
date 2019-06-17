using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlotDemo.Annotations;
using OxyPlotDemo;

namespace testGraf.ViewModels
{
    class MainWindowModel : INotifyPropertyChanged
    {
        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }

        private DateTime lastUpdate = DateTime.Now;

        OxyPlot.Series.LineSeries punktySerii;

        public MainWindowModel()
        {
            this.plotModel = new PlotModel { Title = "Example 1" };
            //this.plotModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            
            punktySerii = new LineSeries();
            //punktySerii.Points.Add(new OxyPlot.DataPoint(5, 3));
            //punktySerii.Points.Add(new OxyPlot.DataPoint(15, 17));
            //punktySerii.Points.Add(new OxyPlot.DataPoint(25, 12));
            //punktySerii.Points.Add(new OxyPlot.DataPoint(35, 4));
            //punktySerii.Points.Add(new OxyPlot.DataPoint(45, 15));
            //punktySerii.Points.Add(new OxyPlot.DataPoint(55, 10));

            plotModel.Series.Add(punktySerii);

            //PlotModel = new PlotModel();
            //SetUpModel();
            //LoadData();
        }

        Random r = new Random();

        public void add()
        {
            punktySerii.Points.Add(new OxyPlot.DataPoint(r.Next(100), r.Next(100)));
        }

        public void add(OxyPlot.DataPoint point)
        {
            punktySerii.Points.Add(point);
            //punktySerii.Points.Add(new OxyPlot.DataPoint(r.Next(100), r.Next(100)));
        }

        private void LoadData()
        {
            List<Measurement> measurements = Data.GetData();

            var dataPerDetector = measurements.GroupBy(m => m.DetectorId).OrderBy(m => m.Key).ToList();

            foreach (var data in dataPerDetector)
            {
                var lineSerie = new LineSeries
                {
                    StrokeThickness = 2,
                    MarkerSize = 3,
                    MarkerStroke = OxyColors.Green,
                    MarkerType = MarkerType.Star,
                    CanTrackerInterpolatePoints = false,
                    Title = string.Format("Detector {0}", data.Key),
                    Smooth = false,
                };

                data.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(d.DateTime), d.Value)));
                PlotModel.Series.Add(lineSerie);
            }
            lastUpdate = DateTime.Now;
        }

        private void SetUpModel()
        {
            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;

            var dateAxis = new DateTimeAxis(AxisPosition.Bottom, "Date", "HH:mm") { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis(AxisPosition.Left, 0) { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            PlotModel.Axes.Add(valueAxis);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
