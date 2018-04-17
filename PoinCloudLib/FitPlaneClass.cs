using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using System.IO;

namespace PoinCloudLib
{

    /// <summary>
    /// Caculate flatness
    /// </summary>
    public class FitPlaneClass
    {

        //var A = Matrix<double>.Build.DenseOfArray(new double[,] {
        //    { 3, 2, -1 },
        //    { 2, -2, 4 },
        //    { -1, 0.5, -1 }
        //});
        //        var b = Vector<double>.Build.Dense(new double[] { 1, -2, 0 });
        //        var x = A.Solve(b);


        /// <summary>
        /// Calculate plane equation Ax+By+C = Z
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

        public List<double> FitPlaneCalculate(Point3D[] point3D)
        {

            double[] zVectorData = new double[point3D.Length];
            double[,] xyMatrixData = new double[point3D.Length, 3];

            for (int i = 0; i < point3D.Length; i++)
            {
                zVectorData[i] = point3D[i].Z;
                xyMatrixData[i, 0] = point3D[i].X;
                xyMatrixData[i, 1] = point3D[i].Y;
                xyMatrixData[i, 2] = 1;
            }
            var zVector = Vector<double>.Build.Dense(zVectorData);
            var xyMatrix = Matrix<double>.Build.DenseOfArray(xyMatrixData);
            var factors = xyMatrix.Solve(zVector);

            List<double> result = new List<double>();
            result.Add(factors.At(0));
            result.Add(factors.At(1));
            result.Add(factors.At(2));

            return result;
        }


        /// <summary>
        /// calculate Matrix base value 
        /// </summary>
        /// <param name="point3D"></param>
        /// <returns></returns>
        List<double> XYZValueSigma(Point3D[] point3D)
        {
            double twiceXValue = 0;
            double twiceYValue = 0;
            double twiceZValue = 0;
            double XYValue = 0;
            double XZValue = 0;
            double YZValue = 0;
            double XSigma = 0;
            double ZSigma = 0;
            double YSigma = 0;
            double constValue = 0;

            for (int i = 0; i < point3D.Length; i++)
            {
                twiceXValue += point3D[i].X * point3D[i].X;
                twiceYValue += point3D[i].Y * point3D[i].Y;
                twiceZValue += point3D[i].Z * point3D[i].Z;
                XYValue += point3D[i].X * point3D[i].Y;
                XZValue += point3D[i].X * point3D[i].Z;
                YZValue += point3D[i].Y * point3D[i].Z;
                XSigma += point3D[i].X;
                YSigma += point3D[i].Y;
                ZSigma += point3D[i].Z;
                constValue++;


            }

            List<double> MatrixBase = new List<double>();
            MatrixBase.Add(twiceXValue);//0
            MatrixBase.Add(twiceYValue);//1
            MatrixBase.Add(twiceZValue);//2
            MatrixBase.Add(XYValue);//3
            MatrixBase.Add(XZValue);//4
            MatrixBase.Add(YZValue);//5
            MatrixBase.Add(XSigma);//6 
            MatrixBase.Add(YSigma);//7
            MatrixBase.Add(ZSigma);//8
            MatrixBase.Add(constValue);//9

            return MatrixBase;

        }


        public List<double> FitPlaneAlter(Point3D[] point3D)
        {
            //SaveData(point3D);

            List<double> MatrixBase = new List<double>();
            MatrixBase = XYZValueSigma(point3D);

            double[] zVectorData = new double[3];
            double[,] xyMatrixData = new double[3, 3];
            zVectorData[0] = MatrixBase[4];
            zVectorData[1] = MatrixBase[5];
            zVectorData[2] = MatrixBase[8];
            xyMatrixData[0, 0] = MatrixBase[0];
            xyMatrixData[0, 1] = MatrixBase[3];
            xyMatrixData[0, 2] = MatrixBase[6];
            xyMatrixData[1, 0] = MatrixBase[3];
            xyMatrixData[1, 1] = MatrixBase[1];
            xyMatrixData[1, 2] = MatrixBase[7];
            xyMatrixData[2, 0] = MatrixBase[6];
            xyMatrixData[2, 1] = MatrixBase[7];
            xyMatrixData[2, 2] = MatrixBase[9];

            var zVector = Vector<double>.Build.Dense(zVectorData);
            var xyMatrix = Matrix<double>.Build.DenseOfArray(xyMatrixData);

            var factors = xyMatrix.Inverse() * zVector;


            double factorA = factors.At(0);
            double factorB = factors.At(1);
            double factorC = factors.At(2);

            //Fit quality – Coefficient of determination = R^2

            double TipAngle = Math.Atan(factors.At(1));
            double TiltAngle = Math.Atan(-factors.At(0));


            //R^2 calcu
            double zDistance = 0;
            double zAverage = 0;
            double zValueAvg = 0;
            double zSum = 0;
            foreach (var item in point3D)
            {
                zSum += item.Z;
            }
            zValueAvg = zSum / point3D.Length;
            for (int i = 0; i < point3D.Length; i++)
            {
                zDistance += Math.Pow((point3D[i].Z - (factorA * point3D[i].X + factorB * +point3D[i].Y + factorC)), 2);

                zAverage += Math.Pow(point3D[i].Z - zValueAvg, 2);
            }
            double RR = 1 - (zDistance / zAverage);

            List<double> result = new List<double>();
            result.Add(factorA);//A
            result.Add(factorB);//B
            result.Add(factorC);//C
            result.Add(RR);
            return result;

        }




        void SaveData(Point3D[] point3D)
        {
            string fileName = @"C:\points.csv";
            string dataStr = "";
            Stream stream = File.OpenWrite(fileName);
            StreamWriter sw = new StreamWriter(stream);
            foreach (var item in point3D)
            {
                dataStr = "";
                dataStr = item.X + "," + item.Y + "," + item.Z + Environment.NewLine;
                sw.Write(dataStr);
                
            }
            sw.Flush();
            stream.Close();

        }

    }

}
