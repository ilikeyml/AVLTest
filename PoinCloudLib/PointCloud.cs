using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvlNet;

namespace PoinCloudLib
{

    [Serializable]
    public class PointCloud
    {
        long width;
        long height;
        float xResolution;
        float yResolution;
        float zResolution;
        float xOffset;
        float yOffset;
        float zOffset;
        Dictionary<long, List<AvlNet.Point3D>> profileDict;
        AvlNet.Point3D[] point3DArray;

        public long Width { get => width; set => width = value; }
        public long Height { get => height; set => height = value; }
        public float XResolution { get => xResolution; set => xResolution = value; }
        public float YResolution { get => yResolution; set => yResolution = value; }
        public float ZResolution { get => zResolution; set => zResolution = value; }
        public float XOffset { get => xOffset; set => xOffset = value; }
        public float YOffset { get => yOffset; set => yOffset = value; }
        public float ZOffset { get => zOffset; set => zOffset = value; }
        public Point3D[] Point3DArray { get => point3DArray; set => point3DArray = value; }
        public Dictionary<long, List<AvlNet.Point3D>> ProfileDict { get => profileDict; set => profileDict = value; }
    }

}
