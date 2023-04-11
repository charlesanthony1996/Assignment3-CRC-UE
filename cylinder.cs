using System;

class GeometryLibrary
{
    public struct Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public Point3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}

public class Cylinder
{
    public double Radius { get; set; }
    public Point3D Base1 { get; set; }
    public Point3D Base2 { get; set; }

    public Cylinder(double radius, Point3D base1, Point3D base2)
    {
        Radius = radius;
        Base1 = base1;
        Base2 = base2;
    }

    public static bool operator == (Cylinder a, Cylinder b)
    {
        if(ReferenceEquals(a, null))
        {
            return ReferenceEquals(b, null);
        }

        if(ReferenceEquals(b, null))
        {
            return false;
        }

        return a.radius == b.Radius && a.base1.Equals(b.Base1) && a.Base2.Equals(b.Base2);
    }

    public static bool operator !=(Cylinder a, Cylinder b)
    {
        return !=(a == b)
    }

    public double Height()
    {
        return Math.Sqrt(Math.Pow(Base2.X - Base1.X, 2 ) + (Math.Pow(Base2.Y - Base1.Y, 2) + Math.Pow(Base2.Z - Base1.Z, 2)));
    }

    public double BottomArea()
    {
        return Math.PI * Math.Pow(Radius, 2);
    }

    public double Volume()
    {
        return BottomArea() * Height();
    }

    public double SurfaceArea()
    {
        return 2 * Math.PI * Radius * (Radius + Height());
    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return this == (Cylinder)obj;
    }

    public override int GetHashCode()
    {
        
    }




}

