using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PoinCloudLib
{
    public class RegionClass
    {
        #region private


        long regionSize;
        RegionStride[] regionStrides;

        public long RegionSize { get => regionSize; set => regionSize = value; }
        public RegionStride[] RegionStrides { get => regionStrides; set => regionStrides = value; }
        #endregion

        RegionClass GetRegionFromRect(Rectangle rect)
        {

            RegionClass rc = new RegionClass();
            regionSize = 0;
            rc.regionStrides = new RegionStride[rect.Height];
            //rc.regionSize = rc.RegionStrides.Length;

            for (int i = 0; i < rect.Height; i++)
            {
                rc.RegionStrides[i].StartPoint = new Point(rect.Left, rect.Top + i);
                rc.RegionStrides[i].EndPoint = new Point(rect.Right, rect.Top + i);
                rc.RegionStrides[i].RunLength = rc.RegionStrides[i].EndPoint.X - rc.RegionStrides[i].StartPoint.X;
                regionSize += rc.RegionStrides[i].RunLength;
            }
            return rc;
        }

        public struct RegionStride
        {
            Point startPoint;
            Point endPoint;
            long runLength;

            public Point StartPoint { get => startPoint; set => startPoint = value; }
            public Point EndPoint { get => endPoint; set => endPoint = value; }
            public long RunLength { get => runLength; set => runLength = value; }
        }



    }
}
