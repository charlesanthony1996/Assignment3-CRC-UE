import math

class GeometryLibrary:
    pass

class Point:
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z


    def __eq__(self, other):
        return self.x == other.x and self.y == other.y and self.z == other.z


class Cuboid(GeometryLibrary):
    def __init__(self, vertices):
        if len(vertices) != 8:
            raise ValueError("A cuboid must have only 8 vertices")
        self.vertices = vertices


    def __eq__(self, other):
        return all(v1 == v2 for v1, v2 in zip(self.vertices, other.vertices))

    
    def centroid(self):
        x = sum(vertex.x for vertex in self.vertices) / 8
        y = sum(vertex.y for vertex in self.vertices) / 8
        z = sum(vertex.z for vertex in self.vertices) / 8
        return Point(x, y, z)


    def volume(self):
        d1 = math.sqrt(sum(((v1.x - v2.x) ** 2 + (v1.y - v2.y) ** 2 + (v1.z - v2.z) ** 2) for v1, v2 in zip(self.vertices[:4], self.vertices[4:])))
        d2 = math.sqrt(sum(((v1.x - v2.x) ** 2 + (v1.y - v2.y) ** 2 + (v1.z - v2.z) ** 2) for v1, v2 in zip(self.vertices[:2] + self.vertices[6:8], self.vertices[2:6])))
        d3 = math.sqrt(sum(((v1.x - v2.x) ** 2 + (v1.y - v2.y) ** 2 + (v1.z - v2.z) ** 2) for v1, v2 in zip(self.vertices[:1] + self.vertices[3:5] + self.vertices[6:], self.vertices[1:3] + self.vertices[5:6])))
        return d1 * d2 * d3

    def surface_area(self):
        d1 = math.sqrt(sum(((v1.x - v2.x) ** 2 + (v1.y - v2.y) ** 2 + (v1.z - v2.z) ** 2) for v1, v2 in zip(self.vertices[:4], self.vertices[4:])))
        d2 = math.sqrt(sum(((v1.x - v2.x) ** 2 + (v1.y - v2.y) ** 2 + (v1.z - v2.z) ** 2) for v1, v2 in zip(self.vertices[:2] + self.vertices[6:8], self.vertices[2:6])))
        d3 = math.sqrt(sum(((v1.x - v2.x) ** 2 + (v1.y - v2.y) ** 2 + (v1.z - v2.z) ** 2) for v1, v2 in zip(self.vertices[:1] + self.vertices[3:5] + self.vertices[6:], self.vertices[1:3] + self.vertices[5:6])))
        return 2 * (d1 * d2 + d1 * d3 + d2 * d3)
    


def main():
    # define the vertices of the cuboid
    vertices = [
        Point(0, 0, 0),
        Point(1, 0, 0),
        Point(1, 1, 0),
        Point(0, 1, 0),
        Point(0, 0, 1),
        Point(1, 0, 1),
        Point(1, 1, 1),
        Point(0, 1, 1)
    ]

    # create a cuboid object
    cuboid = Cuboid(vertices)
    # print(cuboid)

    # calculate the print the centroid, volume and surface area of the cuboid
    centroid = cuboid.centroid()
    volume = cuboid.volume()
    surface_area = cuboid.surface_area()


    print(f"Centroid: ({centroid.x} {centroid.y} {centroid.z})")
    print(f"Volume: {volume}")
    print(f"Surface Area: {surface_area}")




    


if __name__ == "__main__":
    main()
