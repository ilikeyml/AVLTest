using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace PoinCloudLib
{

    /// <summary>
    /// Caculate flatness
    /// </summary>
    class FitPlaneClass
    {       
//var A = Matrix<double>.Build.DenseOfArray(new double[,] {
//    { 3, 2, -1 },
//    { 2, -2, 4 },
//    { -1, 0.5, -1 }
//});
//        var b = Vector<double>.Build.Dense(new double[] { 1, -2, 0 });
//        var x = A.Solve(b);


            /// <summary>
            /// 
            /// (x1,y1,1) * (A
            ///              B   =   z1
            ///              C)
            /// 
            /// varA = (xi,yi,1)
            /// var b = vector(z1,z2,...zi)
            /// var x = A.Solve(b)
            /// 
            /// 
            /// </summary>

        public static object FitPlaneCalculate(Point3D[] point3D)
        {
            List<double> _xData = new List<double>();
            List<double> _yData = new List<double>();
            List<double> _zData = new List<double>();

            foreach (var item in point3D)
            {
                _xData.Add(item.X);
                _yData.Add(item.Y);
                _zData.Add(item.Z);

            }
            double[] zVectorData = new double[_xData.Count];
            double[,] xyMatrixData = new double[3,_xData.Count];

            for (int i = 0; i < _xData.Count; i++)
            {
                zVectorData[i] = _zData[i];
                xyMatrixData[0, i] = _xData[i];
                xyMatrixData[1, i] = _yData[i];
                xyMatrixData[2, i] = 1;
            }
            var zVector = Vector<double>.Build.Dense(zVectorData);
            var xyMatrix = Matrix<double>.Build.DenseOfArray(xyMatrixData);
            var factors = xyMatrix.Solve(zVector);
            return factors;
        }
    }
}
