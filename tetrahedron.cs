using System;
using System.Linq;

namespace GeometryLibrary
{
    public class Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class Tetrahedron
    {
        private Point3D[] vertices;
        
        public Tetrahedron(Point3D v1,Point3D v2, Point3D v3, Point3D v4)
        {
            vertices = new Point3D[] { v1, v2, v3, v4};

        }

        public static bool operator == (Tetrahedron t1, Tetrahedron t2)
        {
            // the logic to compare the two tetrahedra here
            if (ReferenceEquals(t1, null))
            {
                return ReferenceEquals(t2, null);
            }

            if (ReferenceEquals(t2, null))
            {
                return false;
            }

        // Assuming two tetrahedra are equal if they have the same vertices in any order
        return t1.vertices.All(v1 => t2.vertices.Any(v2 => v1.Equals(v2))) &&
               t2.vertices.All(v1 => t1.vertices.Any(v2 => v1.Equals(v2)));
        }

        public static bool operator !=(Tetrahedron t1, Tetrahedron t2)
        {
            return !(t1 == t2);
        }

        public Point3D Centroid()
        {
            double x = (vertices[0].X + vertices[1].X + vertices[2].X + vertices[3].X ) / 4;
            double y = (vertices[0].Y + vertices[1].Y + vertices[2].Y + vertices[3].Y ) / 4;
            double z = (vertices[0].Z + vertices[1].Z + vertices[2].Z + vertices[3].Z ) / 4;
            return new Point3D(x, y, z);
        }

        public double Area(Point3D p1, Point3D p2, Point3D p3)
        {
            double a = Distance(p1, p2);
            double b = Distance(p1, p3);
            double c = Distance(p2, p3);

            double s = (a + b + c) / 2.0;
            
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        private double Distance(Point3D p1, Point3D p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X , 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
        }

        public double SurfaceArea()
        {
            double area1 = Area(vertices[0], vertices[1], vertices[2]);
            double area2 = Area(vertices[0], vertices[1], vertices[3]);
            double area3 = Area(vertices[0], vertices[2], vertices[3]);
            double area4 = Area(vertices[1], vertices[2], vertices[3]);

            return area1 + area2 + area3 + area4;
        }

        public override bool Equals(object obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this == (Tetrahedron)obj;
        }

        public override int GetHashCode()
        {
            return vertices[0].GetHashCode() ^ vertices[1].GetHashCode() ^ vertices[2].GetHashCode() ^ vertices[3].GetHashCode();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GeometryLibrary.Point3D p1 = new GeometryLibrary.Point3D(0, 0, 0);
        GeometryLibrary.Point3D p2 = new GeometryLibrary.Point3D(1, 0, 0);
        GeometryLibrary.Point3D p3 = new GeometryLibrary.Point3D(0, 1, 0);
        GeometryLibrary.Point3D p4 = new GeometryLibrary.Point3D(0, 0, 1);

        GeometryLibrary.Tetrahedron tetra = new GeometryLibrary.Tetrahedron(p1, p2, p3, p4);

        GeometryLibrary.Point3D centroid = tetra.Centroid();
        double surfaceArea = tetra.SurfaceArea();

        Console.WriteLine($"Centroid: ({centroid.X}, {centroid.Y}, {centroid.Z})");
        Console.WriteLine($"Surface Area: {surfaceArea}");

    }
}