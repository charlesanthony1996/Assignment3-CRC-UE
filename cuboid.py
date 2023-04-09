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
        d1 = math.sqrt(sum((v1.x - v2.x) ** 2) )

    



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

    # calculate the 

    


if __name__ == "__main__":
    main()
