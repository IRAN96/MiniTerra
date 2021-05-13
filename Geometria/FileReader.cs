using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Geometria.Layers;
using Geometria.Enumerates;
using Geometria.Geometry;

namespace Geometria.FileIO
{
    public static class FileReader
    {
        /// <summary>
        /// 读取指定位置的简化E00文件，导入一个GISSimpleUniqueLayer中。
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static GISSimpleUniqueLayer LoadE00FileToSimpleUniqueLayer(string path)
        {
            GISSimpleUniqueLayer outLayer = new GISSimpleUniqueLayer("new layer", GeometryObjectType.Polygon);

            //打开E00文件并分割为列表
            FileStream filestream = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader streamreader = new StreamReader(filestream);
            string[] separators = new string[] { " ", "\n" };
            string[] data = streamreader.ReadToEnd().Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //暂存用的列表
            List<GISPolyline> lineList = new List<GISPolyline>();
            //读取data变量
            int index = 0;
            while(index < data.Length)
            {
                //折线段
                if (data[index].Equals("ARC"))
                {
                    index += 2; //跳过ARC行
                    while (true)
                    {
                        //跳出条件：-1 0 0 0 0 0 0
                        if (data[index].Equals("-1"))
                        {
                            break;
                        }

                        GISPolyline line = new GISPolyline();

                        //向lineList添加折线段信息
                        line.id = Convert.ToUInt32(data[index]);
                        index += 6;
                        int numPts = Convert.ToInt32(data[index]);
                        index += 1;
                        for (int n = 0; n < numPts; index += 2, ++n)
                        {
                            double x = Convert.ToDouble(data[index]);
                            double y = Convert.ToDouble(data[index + 1]);
                            line.AddPoint(new GeoPoint(x, y));
                        }

                        lineList.Add(line);
                    }

                }
                //多边形
                else if (data[index].Equals("PAL"))
                {
                    index += 2; //跳过PAL行
                    uint id = 1;
                    while (true)
                    {
                        //跳出条件：-1 0 0 0 0 0 0
                        if (data[index].Equals("-1"))
                        {
                            break;
                        }

                        List<GeoPoint> outerRingPts = new List<GeoPoint>();
                        GISPolygon polygon = new GISPolygon();

                        //得到多边形外环的点列信息
                        int numLines = Convert.ToInt32(data[index]);
                        index += 5; //跳过包围盒

                        //录入外环
                        for (int n = 0; n < numLines; index += 3, ++n)
                        {
                            int lineID = Convert.ToInt32(data[index]);
                            if(lineID > 0)
                            {
                                //偷个懒，因为lineList的ID是从1开始无遗漏的，直接用下标来找到对应的折线
                                outerRingPts.AddRange(lineList[lineID - 1].GetAllPoints());
                            }
                            else if(lineID < 0)
                            {
                                outerRingPts.AddRange(lineList[-lineID - 1].Reversed().GetAllPoints());
                            }
                            
                        }

                        //构建多边形
                        polygon.id = id;
                        GISRing outer = new GISRing(0, outerRingPts);
                        polygon.SetOuterRing(outer);
                        //把第一个多边形无视掉，因为第一个的数据很有问题。。。
                        if(id > 1)
                        {
                            //将多边形录入图层
                            Feature f = new Feature(polygon, null);
                            outLayer.AddFeature(f);
                        }
                        ++id;
                    }
                }

                ++index;
            }

            return outLayer;
        }
    }
}
