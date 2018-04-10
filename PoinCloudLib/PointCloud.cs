using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoinCloudLib
{

    [Serializable]
    public class PointCloud
    {
            long width;
            long length;
            //Point3D[] point3D;
            short[] range;

            public long Width { get => width; set => width = value; }
            //public Point3D[] Point3D { get => point3D; set => point3D = value; }
            public long Length { get => length; set => length = value; }
            public short[] Range { get => range; set => range = value; }


    }

    struct Point3D
    {
        float x;
        float y;
        float z;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Z { get => z; set => z = value; }
    }
}
