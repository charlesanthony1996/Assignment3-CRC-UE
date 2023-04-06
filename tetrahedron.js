class GeometryLibrary {

}

class Point {
    constructor(x, y, z) {
        this.x = x
        this.y = y
        this.z = z

    }

    equals(other) {
        return this.x === other.x && this.y === other.y && this.z === other.z
    }   
}

class Tetrahedron extends GeometryLibrary {
    constructor(p1, p2, p3, p4) {
        super()
        this.p1 = p1
        this.p2 = p2
        this.p3 = p3
        this.p4 = p4
    }

    equals(other) {
        return (this.p1.equals(other.p1) && this.p2.equals(other.p2) && this.p3.equals(other.p3) && this.p4.equals(other.p4))
    }

    distance(p1, p2) {
        return Math.sqrt((p1.x - p2.x) ** 2 + (p1.y - p2.y) ** 2 + (p1.z - p2.z) ** 2)
    }

    // function for the centroid here
    // check center of mass for a tetrahedron
    //  equation for the center of mass for a solid tetrahedron = 1/4 * (v1 + v2 + v3 + v4)
    // v1 to v4 being vertices 
    //  link for the calculation -> https://math.stackexchange.com/questions/1592128/finding-center-of-mass-for-tetrahedron
    centroid() {
        const x = (this.p1.x + this.p2.x + this.p3.x + this.p4.x) / 4
        const y = (this.p2.y + this.p2.y + this.p3.y + this.p4.y) / 4
        const z = (this.p1.z + this.p2.y + this.p3.y + this.p4.z) / 4
        return new Point(x, y, z)
    }


    // surface area of a tetrahedron
    surfaceArea() {
        const a = this.distance(this.p1, this.p2)
        const b = this.distance(this.p1, this.p3)
        const c = this.distance(this.p1, this.p4)
        const d = this.distance(this.p2, this.p3)
        const e = this.distance(this.p2, this.p4)
        const f = this.distance(this.p3, this.p4)

        const s1 = (a + d + e) / 2
        const s2 = (a + b + f) / 2
        const s3 = (b + d + c) / 2
        const s4 = (c + e + f) / 2

        const area1 = Math.sqrt(s1 * (s1 - a) * (s1 - d) * (s1 - e))
        const area2 = Math.sqrt(s2 * (s2 - a) * (s2 - b) * (s2 - f))
        const area3 = Math.sqrt(s3 * (s3 - b) * (s3 - d) * (s3 - c))
        const area4 = Math.sqrt(s4 * (s4 - c) * (s4 - e) * (s4 - f))

        return area1 + area2 + area3 + area4

    }
}


// applying the class while creating a new instance
const p1 = new Point(0, 0, 0)
const p2 = new Point(1, 0, 0)
const p3 = new Point(0, 1, 0)
const p4 = new Point(0, 0, 1)

// all point classes are params here
// this makes the instance of the tetrahedron complete
const tetrahedron = new Tetrahedron(p1, p2, p3, p4)

// centroid use here
const centroid = tetrahedron.centroid()
console.log(`Centroid: ${centroid.x}, ${centroid.y}, ${centroid.z}`)


const surfaceArea = tetrahedron.surfaceArea()

console.log(`Surface Area: ${surfaceArea}`)