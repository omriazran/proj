using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2
{
    class pearsonCalc
    {
        // constructor
        public pearsonCalc() { }

		//methods
		double avg(List<double> x, int size)
		{
			double sum = 0;
			for (int i = 0; i < size; sum += x[i], i++) ;
			return sum / size;
		}

		// returns the variance of X and Y
		double var(List<double> x, int size)
		{
			double av = avg(x, size);
			double sum = 0;
			for (int i = 0; i < size; i++)
			{
				sum += x[i] * x[i];
			}
			return sum / size - av * av;
		}

		// returns the covariance of X and Y
		double cov(List<double> x, List<double> y, int size)
		{
			double sum = 0;
			for (int i = 0; i < size; i++)
			{
				sum += x[i] * y[i];
			}
			sum /= size;

			return sum - avg(x, size) * avg(y, size);
		}

		


		// returns the Pearson correlation coefficient of X and Y
		double pearson(List<double> x, List<double> y, int size)
		{
			return cov(x, y, size) / (Math.Sqrt(var(x, size)) * Math.Sqrt(var(y, size)));
		}

		// return the column that has the max pearson with given one
		 public int maxPearson(int index,CSVReader reader,int size)
		{
			List<double> col = reader.getSubCol(index, size);
			List<double> otherCol = null;
			int colNum = reader.getFieldsNames().Count() / 2;
			double currentPearson = 0;
			double maxPearson = 0;
			int maxIndex = 0;

			for (int i =0; i < colNum; i++)
			{
				currentPearson = 0;
				if(i != index)
				{
					otherCol = reader.getSubCol(i, size);
					currentPearson = pearson(col, otherCol,size);
					if (currentPearson > maxPearson)
					{
						maxPearson = currentPearson;
						maxIndex = i;
						Console.WriteLine($"max index = {i}");

					}

				}
			}

			return maxIndex;
		}

		// linear reg methods

		// performs a linear regression and returns the line equation
		public Line linear_reg(List<Point> points, int size)
		{
			List<double> x = new List<double>();
			List<double> y = new List<double>();
			for (int i = 0; i < size; i++)
			{
				x[i] = points[i].getX();
				y[i] = points[i].getY();
			}
			double a = cov(x, y, size) / var(x, size);
			double b = avg(y, size) - a * (avg(x, size));

			Line l = new Line(a, b);
			return l;
		}

		// returns the deviation between point p and the line equation of the points
		double dev(Point p, List<Point> points, int size)
		{
			Line l = linear_reg(points, size);
			return dev(p, l);
		}

		// returns the deviation between point p and the line
		double dev(Point p, Line l)
		{
			return Math.Abs(p.getY() - l.f(p.getX()));
		}

		public List<Point> columnToPoints(List<double> x, List<double> y)
		{
			List<Point> ps = new List<Point>();
			for (int i = 0; i < x.Count(); i++)
			{
				ps[i] = new Point(x[i], y[i]);
			}
			return ps;
		}

	}

	class Line
	{
	   double a, b;
		public Line(){
			this.a = 0;
			this.b = 0;
			}
		public Line(double a, double b)
		{
			this.a = a;
			this.b = b;
		}
		public double f(double x)
		{
			return this.a * x + this.b;
		}
		public double getA()
		{
			return this.a;
		}
		public double getB()
		{
			return this.b;
		}
		public bool cmp(Line l)
		{
			if(this.a == l.getA() && this.b == l.getB())
			{
				return true;
			}
			return false;
		}
	};

	class Point
	{
		double x, y;

		public Point(double x, double y)
			{
			this.x = x;
			this.y = y;
			}
		//methods
		public double getX()
		{
			return this.x;
		}

		 public double getY()
		{
			return this.y;
		}
	};

}
