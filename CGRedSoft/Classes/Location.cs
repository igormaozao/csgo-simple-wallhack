namespace CGRedSoft.Classes {
    public class Location {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Location(float x, float y, float z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override string ToString() {
            return string.Format("{0}, {1}, {2}", X, Y, Z);
        }
    }
}
