using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PoinCloudLib
{
    public class ImageGenerator
    {


        /// <summary>
        /// Generate Raw Image
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public static Bitmap GrayScaleImageGenerator(PointCloud pc)
        {

            Bitmap bitmap = new Bitmap(pc.Width, pc.Height, PixelFormat.Format24bppRgb);
            //BitmapData _bmpData = bitmap.LockBits(new Rectangle(0, 0, pc.Width, pc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //IntPtr intPtr = _bmpData.Scan0;
            int[] imageRawData = GrayScaleData(pc);
            //Marshal.Copy(imageRawData, 0, intPtr, imageRawData.Length);
            //bitmap.UnlockBits(_bmpData);
            int index = 0;
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    bitmap.SetPixel(j, i, Color.FromArgb(255, imageRawData[index], imageRawData[index], imageRawData[index]));
                    index++;
                }
            }
            return bitmap;
        }

        /// <summary>
        /// Generate Image Keep Ratio
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>


        public static Bitmap GrayScaleImageGeneratorKeepRatio(PointCloud pc)
        {

            int[] imageRawData = GrayScaleDataKeepRatio(pc);
            //int[] imageRawData = GrayScaleData(pc);
            Bitmap bitmap = new Bitmap(pc.Width, pc.Height, PixelFormat.Format24bppRgb);


            BitmapData _bmpData = bitmap.LockBits(new Rectangle(0, 0, pc.Width, pc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int index = 0;
            unsafe
            {
                byte* ptr = (byte*)(_bmpData.Scan0);
                for (int x = 0; x < _bmpData.Width; x++)
                {
                    for (int y = 0; y < _bmpData.Height; y++)
                    {
                        ptr[0] = ptr[1] = ptr[2] = (byte)imageRawData[index];
                        ptr += 3;
                        index++;
                    }
                    ptr += _bmpData.Stride - _bmpData.Width * 3;
                }
            }
            bitmap.UnlockBits(_bmpData);

            //int index = 0;
            //for (int i = 0; i < bitmap.Height; i++)
            //{
            //    for (int j = 0; j < bitmap.Width; j++)
            //    {
            //        bitmap.SetPixel(j, i, Color.FromArgb(255, imageRawData[index], imageRawData[index], imageRawData[index]));
            //        index++;
            //    }
            //}
            return bitmap;
        }


        public static Bitmap RGBImageGenerator(PointCloud pc)
        {

            return null;
        }

        /// <summary>
        /// RawData
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>

        static int[] GrayScaleData(PointCloud pc)
        {

            int[] data = new int[pc.Height * pc.Width];
            long index = 0;
            foreach (var item in pc.Point3DArray)
            {
                data[index] = GrayDataTrans(item.Z, pc.ZRange);
                index ++;
            }

            return data;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        static int[] GrayScaleDataKeepRatio(PointCloud pc)
        {

            int ratio = (int)Math.Floor( pc.YResolution / pc.XResolution);

            List<List<Point3D>> newProfileList = new List<List<Point3D>>();
            for (int i = 0; i < pc.ProfileList.Count; i++)
            {
                for (int j = 0; j < ratio-1; j++)
                {
                    newProfileList.Add( pc.ProfileList[i]);
                }

            }

            pc.ProfileList.Clear();
            pc.ProfileList = newProfileList;
            pc.Width = pc.ProfileList[0].Count;
            pc.Height = pc.ProfileList.Count;
            pc.Point3DArray = new Point3D[pc.Width*pc.Height];
            long index = 0;
            for (int k = 0; k < pc.Height; k++)
            {
                for (int p = 0; p < pc.Width; p++)
                {
                    pc.Point3DArray[index].Z = pc.ProfileList[k][p].Z;
                    pc.Point3DArray[index].X = pc.ProfileList[k][p].X;
                    pc.Point3DArray[index].Y = pc.ProfileList[k][p].Y;
                    index++;
                }
            }


            int[] data = new int[pc.Height * pc.Width];
            long indexColor = 0;
            foreach (var item in pc.Point3DArray)
            {
                data[indexColor] = GrayDataTrans(item.Z, pc.ZRange);
                indexColor++;
            }


            //int[] data = new int[pc.Height * pc.Width *3];
            //long indexColor = 0;
            //foreach (var item in pc.Point3DArray)
            //{
            //    data[indexColor] = GrayDataTrans(item.Z, pc.ZRange);
            //    data[indexColor+1] = GrayDataTrans(item.Z, pc.ZRange);
            //    data[indexColor+2] = GrayDataTrans(item.Z, pc.ZRange);
            //    indexColor += 3;
            //}
            return data;
        }

        static short GrayDataTrans(float zData, float range)
        {
            return (short)Math.Floor(zData * (Math.Pow(2, 8) / range));
         
        }

        static short[] RGBData(PointCloud pc)
        {

            return null;
        }

        static int ColorDataTrans(float zData, float range)
        {
            return (int)Math.Floor(zData * (Math.Pow(2,16) / range));

        }



    }
}
