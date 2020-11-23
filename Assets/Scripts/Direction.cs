namespace FlatMango.Maze
{
    using Vector2Int = UnityEngine.Vector2Int;


    public struct Direction
    {
        private byte value;


        public Direction Opposite => ShiftRightLooped(2);
        

        public Vector2Int Delta
        {
            get
            {
                Vector2Int delta = Vector2Int.zero;

                if (Contains(Up))
                    delta.y = 1;

                if (Contains(Down))
                    delta.y = -1;

                if (Contains(Right))
                    delta.x = 1;

                if (Contains(Left))
                    delta.x = -1;

                return delta;
            }
        }

        public bool Contains(Direction direction)
        {
            return (value & direction) == direction;
        }

        private Direction ShiftLeftLooped(int n)
        {
            n = n % 4;

            return (byte)(value << n >> 4 | value << n);
        }

        private Direction ShiftRightLooped(int n)
        {
            n = n % 4;

            return (byte)(value << 4 >> n | value >> n);
        }

        public override string ToString()
        {
            string result = System.Convert.ToString(value, 2);
            return new string('0', 4 - result.Length) + result;
        }

        public static Direction None => 0b_0000;
        public static Direction Up => 0b_0001;
        public static Direction Right => 0b_0010;
        public static Direction Down => 0b_0100;
        public static Direction Left => 0b_1000;
        public static Direction All => 0b_1111;


        public static implicit operator Direction(int value)
        {
            // Here we use 0B_1111 as a mask to drop all bits except 1st tetrad.
            return new Direction() { value = (byte)(value & 0B_1111) };
        }

        public static implicit operator Direction(byte value)
        {
            // Here we use 0B_1111 as a mask to drop all bits in left tetrad.
            return new Direction() { value = (byte)(value & 0B_1111) };
        }

        public static implicit operator int(Direction direction) => direction.value;

        public static implicit operator byte(Direction direction) => direction.value;

        /// <summary>
        /// Turns current direction clockwise.
        /// </summary>
        public static Direction operator ++(Direction dir)
        {
            dir.value = dir.ShiftLeftLooped(1).value;

            return dir;
        }

        /// <summary>
        /// Turns current direction counter clockwise.
        /// </summary>
        public static Direction operator --(Direction dir)
        {
            dir.value = dir.ShiftRightLooped(1).value;

            return dir;
        }

        /* 
         * Below are all of binary operators overloaded. This is important
         * to be sure that there is no data in left tetrad of 'value' prop
         * as it would laed to wrong calculations.
         * Don't be fooled by these simple returns of usual binary operators
         * results. The core logic is inside overloaded int -> Direction and
         * implicit operator.
         */

        public static Direction operator << (Direction dir, int n)
        {
            Direction result = Direction.None;

            if (n < 4)
                result.value = (byte)((dir.value << (byte)(n % 4)) & 0B_1111);

            return result;
        }

        public static Direction operator ~(Direction dir)
        {
            return ~dir.value;
        }

        public static Direction operator &(Direction dir1, Direction dir2)
        {
            return dir1.value & dir2.value;
        }

        public static Direction operator |(Direction dir1, Direction dir2)
        {
            return dir1.value | dir2.value;
        }

        public static Direction operator ^(Direction dir1, Direction dir2)
        {
            return dir1.value ^ dir2.value;
        }
    }
}