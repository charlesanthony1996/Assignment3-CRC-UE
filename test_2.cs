using System;
using System.Linq;
using GeometryLibrary;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GeometryLibrary
{
    
    public struct Point3D
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

        public Tetrahedron(Point3D v1, Point3D v2, Point3D v3, Point3D v4)
        {
            vertices = new Point3D[] { v1, v2, v3, v4};
        }

        public static bool operator ==(Tetrahedron t1, Tetrahedron t2)
        {
            // the logic to compare the two tetrahedra here
            if(ReferenceEquals(t1, null))
            {
                return ReferenceEquals(t2, null);
            }

            if(ReferenceEquals(t2, null))
            {
                return false;
            }

            // assuming two tetrahedra are equal if they have the same vertices in any order
            return t1.vertices.All(v1 => t2.vertices.Any(v2 => v1.Equals(v2))) 
            && t2.vertices.All(v1 => t1.vertices.Any(v2 => v1.Equals(v2)));
        }

        public static bool operator !=(Tetrahedron t1, Tetrahedron t2)
        {
            return !(t1 == t2);
        }

        public Point3D Centroid()
        {
            double x = (vertices[0].X + vertices[1].X + vertices[2].X + vertices[3].X) / 4;
            double y = (vertices[0].Y + vertices[1].Y + vertices[2].Y + vertices[3].Y) / 4;
            double z = (vertices[0].Z + vertices[1].Z + vertices[2].Z + vertices[3].Z) / 4;
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
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
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

        public static Tetrahedron GenerateRandom(Random random)
        {
            Point3D p1 = new Point3D(random.NextDouble(), random.NextDouble(), random.NextDouble());
            Point3D p2 = new Point3D(random.NextDouble(), random.NextDouble(), random.NextDouble());
            Point3D p3 = new Point3D(random.NextDouble(), random.NextDouble(), random.NextDouble());
            Point3D p4 = new Point3D(random.NextDouble(), random.NextDouble(), random.NextDouble());

            return new Tetrahedron(p1, p2, p3, p4);
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

        public static Cuboid GenerateRandom(Random random)
        {
            // Generate 8 random points here
            Point3D[] vertices = new Point3D[8];
            for (int i = 0; i < 8; i++)
            {
                vertices[i] = new Point3D(random.NextDouble(), random.NextDouble(), random.NextDouble());
            }

            return new Cuboid(vertices);
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

            return a.Radius == b.Radius && a.Base1.Equals(b.Base1) && a.Base2.Equals(b.Base2);
        }

        public static bool operator !=(Cylinder a, Cylinder b)
        {
            return !(a == b);
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
            return Radius.GetHashCode() ^ Base1.GetHashCode() ^ Base2.GetHashCode();
        }

        public static Cylinder GenerateRandom(Random random)
        {
            Point3D base1 = new Point3D(random.NextDouble(), random.NextDouble(), random.NextDouble());
            Point3D base2 = new Point3D(random.NextDouble(), random.NextDouble(), random.NextDouble());
            double radius = random.NextDouble() * 5;

            return new Cylinder(radius, base1, base2);
        }
    }


}


class Program
{

    static void TetrahedronExample()
    {
        Point3D p1 = new Point3D(0, 0, 0);
        Point3D p2 = new Point3D(1, 0, 0);
        Point3D p3 = new Point3D(0, 1, 0);
        Point3D p4 = new Point3D(0, 0, 1);

        Tetrahedron tetra = new Tetrahedron(p1, p2, p3, p4);

        Point3D centroid = tetra.Centroid();
        double surfaceArea = tetra.SurfaceArea();

        Console.WriteLine($"Centroid: ({centroid.X}, {centroid.Y}, {centroid.Z})");
        Console.WriteLine($"Surface Area: {surfaceArea}");
    }

    static void CuboidExample()
    {
        Point3D p1 = new Point3D(0, 0, 0);
        Point3D p2 = new Point3D(1, 0, 0);
        Point3D p3 = new Point3D(1, 1, 0);
        Point3D p4 = new Point3D(0, 1, 0);
        Point3D p5 = new Point3D(0, 0, 1);
        Point3D p6 = new Point3D(1, 0, 1);
        Point3D p7 = new Point3D(1, 1, 1);
        Point3D p8 = new Point3D(0, 1, 1);

        Cuboid cuboid = new Cuboid(new Point3D[] { p1, p2, p3, p4, p5, p6, p7, p8 });

        Point3D centroid = cuboid.Centroid();
        double volume = cuboid.Volume();
        double surfaceArea = cuboid.SurfaceArea();

        Console.WriteLine($"Centroid: ({centroid.X}, {centroid.Y}, {centroid.Z})");
        Console.WriteLine($"Volume: {volume}");
        Console.WriteLine($"Surface Area: {surfaceArea}");
    }

    static void CylinderExample()
    {
        Point3D base1 = new Point3D(0, 0, 0);
        Point3D base2 = new Point3D(0, 0, 5);
        double radius = 2;

        Cylinder cylinder = new Cylinder(radius, base1, base2);

        double height = cylinder.Height();
        double bottomArea = cylinder.BottomArea();
        double volume = cylinder.Volume();
        double surfaceArea = cylinder.SurfaceArea();

        Console.WriteLine($"Height: {height}");
        Console.WriteLine($"Bottom Area: {bottomArea}");
        Console.WriteLine($"Volume: {volume}");
        Console.WriteLine($"Surface Area: {surfaceArea}");
    }

    // G

    static void Main(string[] args)
    {

        var random = new Random();
        Tetrahedron[] tetrahedrons = new Tetrahedron[5];
        Cuboid[] cuboids = new Cuboid[5];
        Cylinder[] cylinders = new Cylinder[5];

        // generate instances for each shape/solid
        for(int i = 0; i < 5; i++)
        {
            tetrahedrons[i] = Tetrahedron.GenerateRandom(random);
            cuboids[i] = Cuboid.GenerateRandom(random);
            cylinders[i] = Cylinder.GenerateRandom(random);
        }

        Console.WriteLine("using the synchronous method");
        Stopwatch syncStopwatch = new Stopwatch();
        var syncStopWatch = Stopwatch.StartNew();

        SynchronousPrint(tetrahedrons, cuboids, cylinders);
        syncStopWatch.Stop();
        Console.WriteLine($"Synchronous method took: {syncStopwatch.ElapsedMilliseconds} ms\n");


        // 
        Console.WriteLine("Using the asynchronous method: ");
        Stopwatch asyncStopwatch = new Stopwatch();
        var asyncStopWatch = Stopwatch.StartNew();

        AsyncPrint(tetrahedrons, cuboids, cylinders).Wait();
        asyncStopwatch.Stop();
        Console.WriteLine($"Asynchronous method took: {asyncStopwatch.ElapsedMilliseconds} ms\n");

        Console.WriteLine("Done ");



        // synchronous method
        static void SynchronousPrint(Tetrahedron[] tetrahedrons, Cuboid[] cuboids, Cylinder[] cylinders)
        {
            
            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Cuboid {i + 1} Surface Area: {cuboids[i].SurfaceArea()}");
                Thread.Sleep(1000);

                Console.WriteLine($"Cylinder {i + 1} Surface Area: {cylinders[i].SurfaceArea()}");
                Thread.Sleep(1000);
            }

            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Tetrahedron {i + 1} Surface Area: {tetrahedrons[i].SurfaceArea()}");
                Thread.Sleep(1000);
            }
        }

        // Console.WriteLine("using the synchronous method");
        // Stopwatch syncStopwatch = new Stopwatch();
        // var syncStopWatch = Stopwatch.StartNew();

        // SynchronousPrint(tetrahedrons, cuboids, cylinders);
        // syncStopWatch.Stop();
        // Console.WriteLine($"Synchronous method took: {syncStopwatch.ElapsedMilliseconds} ms\n");


        // asynchronous method
        static async Task AsyncPrint(Tetrahedron[] tetrahedrons, Cuboid[] cuboids, Cylinder[] cylinders)
        {
            for(int i = 0; i < 5; i++)
            {
                int currentCuboidIndex = i;
                int currentCylinderIndex = i;

                var cuboidSurfaceAreaTask = Task.Run(() =>
            {
                return (index: currentCuboidIndex, surfaceArea: cuboids[currentCuboidIndex].SurfaceArea());
            });

            var cylinderSurfaceAreaTask = Task.Run(() =>
            {
                return (index: currentCylinderIndex, surfaceArea: cylinders[currentCylinderIndex].SurfaceArea());
            });

            var results = await Task.WhenAll(cuboidSurfaceAreaTask, cylinderSurfaceAreaTask);

            Console.WriteLine($"Cuboid {results[0].index + 1} Surface Area: {results[0].surfaceArea}");
            Console.WriteLine($"Cylinder {results[1].index + 1} Surface Area: {results[1].surfaceArea}");

            await Task.Delay(1000);
        }
        for (int i = 0; i < 5; i++)
        {
            int currentTetrahedronIndex = i;

            var tetrahedronSurfaceArea = await Task.Run(() =>
            {
                return (index: currentTetrahedronIndex, surfaceArea: tetrahedrons[currentTetrahedronIndex].SurfaceArea());
            });
            

            Console.WriteLine($"Tetrahedron {tetrahedronSurfaceArea.index + 1} Surface Area: {tetrahedronSurfaceArea.surfaceArea}");

            await Task.Delay(1000);
        }
    }

        // Console.WriteLine("Using the asynchronous method: ");
        // Stopwatch asyncStopwatch = new Stopwatch();
        // var asyncStopWatch = Stopwatch.StartNew();

        // AsyncPrint(tetrahedrons, cuboids, cylinders).Wait();
        // asyncStopwatch.Stop();
        // Console.WriteLine($"Asynchronous method took: {asyncStopwatch.ElapsedMilliseconds} ms\n");

        // Console.WriteLine("Done ");
}
}