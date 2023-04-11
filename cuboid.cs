using System;
using System.Linq;
using GeometryLibrary;

namespace GeometryLibrary
{
    public struct Point3D
    {
        public double X { get; set;}
        public double Y { get; set;}
        public double Z { get; set;}

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class Cuboid
    {
        private Point3D[] vertices;

        public Cuboid(Point3D[] vertices)
        {
            if (vertices.Length != 8)
            {
                throw new ArgumentException("A cuboid must have 8 vertices");
            }

            this.vertices = vertices;
        }

        public static bool operator == (Cuboid a, Cuboid b)
        {
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }

            if (ReferenceEquals(b, null))
            {
                return false;
            }

            // assuming two cuboids are equal if they have the same vertices in any order
            return a.vertices.All(v1 => b.vertices.Any(v2 => v1.Equals(v2))) && 
                b.vertices.All(v1=> a.vertices.Any(v2 => v1.Equals(v2)));
        }


        public static bool operator !=(Cuboid a, Cuboid b)
        {
            return !(a == b);
        }

        public Point3D Centroid()
        {
            double x = 0;
            double y = 0;
            double z = 0;

            for (int i = 0; i < 8; i++)
            {
                x += vertices[i].X;
                y += vertices[i].Y;
                z += vertices[i].Z;
            }

            return new Point3D(x /8, y/ 8, z /8);
        }

        public double Volume()
        {
            double width = Math.Abs(vertices[1].X - vertices[0].X);
            double height = Math.Abs(vertices[3].Y - vertices[0].Y);
            double depth = Math.Abs(vertices[4].Z - vertices[0].Z);
            
            return width * depth * height; 
        }

        public double SurfaceArea()
        {
            double width = Math.Abs(vertices[1].X - vertices[0].X);
            double height = Math.Abs(vertices[3].Y - vertices[0].Y);
            double depth = Math.Abs(vertices[4].Z - vertices[0].Z);

            return 2 * (width * height + width * depth + height * depth);
        }

        // 

        public override bool Equals(object obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this == (Cuboid)obj;
        }

        // 
        public override int GetHashCode()
        {
            int hashCode = 0;
            for(int i = 0; i < 8; i++)
            {
                hashCode ^= vertices[i].GetHashCode();
            }
            return hashCode;

        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Point3D p1 = new Point3D(0, 0, 0);
        Point3D p2 = new Point3D(1, 0, 0);
        Point3D p3 = new Point3D(1, 1, 0);
        Point3D p4 = new Point3D(0, 1, 0);
        Point3D p5 = new Point3D(0, 0, 1);
        Point3D p6 = new Point3D(1, 0, 1);
        Point3D p7 = new Point3D(1, 1, 1);
        Point3D p8 = new Point3D(0, 1, 1);

        Cuboid cuboid = new Cuboid(new Point3D[] { p1, p2, p3, p4, p5, p6, p7, p8});

        Point3D centroid = cuboid.Centroid();
        double Volume = cuboid.Volume();
        double SurfaceArea = cuboid.SurfaceArea();

        Console.WriteLine($"Centroid: ({centroid.X}, {centroid.Y}, {centroid.Z})");
        Console.WriteLine($"Volume: {Volume}");
        Console.WriteLine($"Surface Area: {SurfaceArea}");
    }
}