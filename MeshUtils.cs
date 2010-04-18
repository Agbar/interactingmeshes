﻿//20-02-2010

using System;
using System.Collections.Generic;
using System.Text;
using Direct3D = Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;
using InteractingMeshes.BSP;

namespace InteractingMeshes
{
    /// <summary>
    /// Mesh utils (changing color, getting points, getting polygons, etc.)
    /// </summary>
    public class MeshUtils
    {
        #region --- Static methods ---

        /// <summary>
        /// Changing the mesh color
        /// </summary>
        /// <param name="_mesh"></param>
        /// <param name="_color"></param>
        /// <param name="_device"></param>
        /// <returns></returns>
        public static Direct3D.Mesh ChangeMeshColor(Direct3D.Mesh _mesh, Color _color, Direct3D.Device _device)
        {
            Direct3D.Mesh tempMesh = _mesh.Clone(_mesh.Options.Value, Vertex.FVF_Flags, _device);
            Vertex[] vertData =
                (Vertex[])tempMesh.VertexBuffer.Lock(0, typeof(Vertex),
                                                       Direct3D.LockFlags.None,
                                                       tempMesh.NumberVertices);

            for (int i = 0; i < vertData.Length; ++i)
            {
                vertData[i].color = _color.ToArgb();
            }

            tempMesh.VertexBuffer.Unlock();
            return tempMesh;
        }

        /// <summary>
        /// Get vertices from mesh
        /// </summary>
        /// <param name="_mesh"></param>
        /// <returns></returns>
        public static Vertex[] GetVertexes(Direct3D.Mesh _mesh)
        {
            Vertex[] vertData =
                (Vertex[])_mesh.VertexBuffer.Lock(0, typeof(Vertex),
                                                       Direct3D.LockFlags.None,
                                                       _mesh.NumberVertices);
            _mesh.VertexBuffer.Unlock();
            return vertData;
        }

        /// <summary>
        /// Get points from mesh
        /// </summary>
        /// <param name="_mesh"></param>
        /// <param name="_position"></param>
        /// <returns></returns>
        public static List<Vector3> GetPoints(Direct3D.Mesh _mesh, Vector3 _position)
        {
            Vertex[] vertices = GetVertexes(_mesh);
            List<Vector3> points = new List<Vector3>();

            foreach (Vertex v in vertices)
            {
                if (!points.Contains(v.Vector + _position))
                {
                    points.Add(v.Vector + _position);
                }
            }
            return points;
        }

        /// <summary>
        /// Get triangle polygons from a mesh
        /// </summary>
        /// <param name="_mesh"></param>
        /// <returns></returns>
        public static List<Polygon> GetPolygons(Direct3D.Mesh _mesh)
        {
            Vertex[] vertices = GetVertexes(_mesh);
            //List<Vector3> points = new List<Vector3>();
            List<Polygon> polygonLst = new List<Polygon>();

            for (int i = 0; i < vertices.Length; i+=3)
            {
                Vector3 p0 = vertices[3 * i].Vector;
                Vector3 p1 = vertices[3 * i + 1].Vector;
                Vector3 p2 = vertices[3 * i + 2].Vector;
                Polygon poly = new Polygon();
                poly.Add(p0); 
                poly.Add(p1); 
                poly.Add(p2);
                polygonLst.Add(poly);
            }

            return polygonLst;
        }

        #endregion
    }
}
