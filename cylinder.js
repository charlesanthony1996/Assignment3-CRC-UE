class GeometryLibrary {
    // helper function to calculate the distance between two points
    static distanceBetweenPoints(a, b) {
        return Math.sqrt(Math.pow(b.x - a.x, 2) + Math.pow(b.y - a.y, 2) + Math.pow(b.z - a.z, 2));
    }
}

class Cylinder {
    constructor(radius, base1, base2) {
        this.radius = radius
        this.base1 = base1
        this.base2 = base2
    }

    // override the == operator
    static equals(a, b) {
        return a.radius === b.radius &&
        a.base1.x === b.base1.x && a.base1.y === b.base1.y && a.base1.z === b.base1.z &&
        a.base2.x === b.base2.x && a.base2.y === b.base2.y && a.base2.z === b.base2.z
    }

    // height() to calculate the height
    height() {
        return GeometryLibrary.distanceBetweenPoints(this.base1, this.base2)
    }

    // bottom area to calculate the area of the bottom circle
    bottomArea() {
        return Math.PI * Math.pow(this.radius, 2)
    }

    // volume to calculate the volume of the cylinder
    volume() {
        return this.bottomArea() * this.height()
    }

    // surfaceArea() to calcualte the surface area of the cylinder
    surfaceArea() {
        const lateralArea = 2 * Math.PI * this.radius * this.height()
        const topAndBottomArea = 2 * this.bottomArea()
        return lateralArea + topAndBottomArea
    }
}


const base1 = { x: 0, y: 0, z: 0 }
const base2 = { x: 0, y: 0, z: 10}
const cylinder1 = new Cylinder(5, base1, base2)
const cylinder2 = new Cylinder(5, base1, base2)

console.log(Cylinder.equals(cylinder1, cylinder2))
console.log(cylinder1.height())
console.log(cylinder1.bottomArea())
console.log(cylinder1.volume())
console.log(cylinder1.surfaceArea())



