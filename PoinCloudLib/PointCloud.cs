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
      
        int width;
        int height;
        float xResolution;
        float yResolution;
        float zResolution;
        float xOffset;
        float yOffset;
        float zOffset;
        float zStart;
        float xStart;
        float zRange;
        List<List<Point3D>> profileList;
        Point3D[] point3DArray;

        public float XResolution { get => xResolution; set => xResolution = value; }
        public float YResolution { get => yResolution; set => yResolution = value; }
        public float ZResolution { get => zResolution; set => zResolution = value; }
        public float XOffset { get => xOffset; set => xOffset = value; }
        public float YOffset { get => yOffset; set => yOffset = value; }
        public float ZOffset { get => zOffset; set => zOffset = value; }
        public Point3D[] Point3DArray { get => point3DArray; set => point3DArray = value; }
        public List<List<Point3D>> ProfileList { get => profileList; set => profileList = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public float ZStart { get => zStart; set => zStart = value; }
        public float XStart { get => xStart; set => xStart = value; }
        public float ZRange { get => zRange; set => zRange = value; }
    }

    [Serializable]
    public struct Point3D
    {
        float x;
        float y;
        float z;


        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Z { get => z; set => z = value; }
    }

}
