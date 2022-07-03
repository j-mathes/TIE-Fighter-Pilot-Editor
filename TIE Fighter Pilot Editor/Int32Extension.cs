using System;

namespace TIE_Fighter_Pilot_Editor
{
    public static class Int32Extension
    {
        /// <summary>
        /// Copies an int to a byte array: Byte order and sift order are inverted.
        /// </summary>
        /// <param name="source">The integer to convert.</param>
        /// <param name="destination">An arbitrary array of bytes.</param>
        /// <param name="offset">Offset into the desination array.</param>
        public static void CopyToByteArray(this int source, byte[] destination, int offset)
        {
            if (destination == null)
                throw new ArgumentException("Destination array cannot be null");

            // check if there is enough space for all the 4 bytes we will copy
            if (destination.Length < offset + sizeof(int))
                throw new ArgumentException("Not enough room in the destination array");

            for (int i = 0, j = sizeof(int) - 1; i < sizeof(int); i++, --j)
            {
                destination[offset + i] = (byte)(source >> (8 * j));
            }
        }

        /// <summary>
        /// Copies an int to a to byte array Little Endian: Byte order and sift order are the same.
        /// </summary>
        /// <param name="source">The integer to convert.</param>
        /// <param name="destination">An arbitrary array of bytes.</param>
        /// <param name="offset">Offset into the desination array.</param>
        public static void CopyToByteArrayLE(this int source, byte[] destination, int offset)
        {
            if (destination == null)
                throw new ArgumentException("Destination array cannot be null");

            // check if there is enough space for all the 4 bytes we will copy
            if (destination.Length < offset + sizeof(int))
                throw new ArgumentException("Not enough room in the destination array");

            for (int i = 0, j = 0; i < sizeof(int); i++, j++)
            {
                destination[offset + i] = (byte)(source >> (8 * j));
            }
        }

        /// <summary>
        /// Copies a uint to a byte array: Byte order and sift order are inverted.
        /// </summary>
        /// <param name="source">The integer to convert.</param>
        /// <param name="destination">An arbitrary array of bytes.</param>
        /// <param name="offset">Offset into the desination array.</param>
        public static void CopyToByteArray(this uint source, byte[] destination, int offset)
        {
            if (destination == null)
                throw new ArgumentException("Destination array cannot be null");

            // check if there is enough space for all the 4 bytes we will copy
            if (destination.Length < offset + sizeof(uint))
                throw new ArgumentException("Not enough room in the destination array");

            for (int i = 0, j = sizeof(uint) - 1; i < sizeof(uint); i++, --j)
            {
                destination[offset + i] = (byte)(source >> (8 * j));
            }
        }

        /// <summary>
        /// Copies a uint to a to byte array Little Endian: Byte order and sift order are the same.
        /// </summary>
        /// <param name="source">The integer to convert.</param>
        /// <param name="destination">An arbitrary array of bytes.</param>
        /// <param name="offset">Offset into the desination array.</param>
        public static void CopyToByteArrayLE(this uint source, byte[] destination, int offset)
        {
            if (destination == null)
                throw new ArgumentException("Destination array cannot be null");

            // check if there is enough space for all the 4 bytes we will copy
            if (destination.Length < offset + sizeof(uint))
                throw new ArgumentException("Not enough room in the destination array");

            for (int i = 0, j = 0; i < sizeof(uint); i++, j++)
            {
                destination[offset + i] = (byte)(source >> (8 * j));
            }
        }

        /// <summary>
        /// Copies a ushort to a to byte array Little Endian: Byte order and sift order are the same.
        /// </summary>
        /// <param name="source">The integer to convert.</param>
        /// <param name="destination">An arbitrary array of bytes.</param>
        /// <param name="offset">Offset into the desination array.</param>
        public static void CopyToByteArrayLE(this ushort source, byte[] destination, int offset)
        {
            if (destination == null)
                throw new ArgumentException("Destination array cannot be null");

            // check if there is enough space for all the 4 bytes we will copy
            if (destination.Length < offset + sizeof(ushort))
                throw new ArgumentException("Not enough room in the destination array");

            for (int i = 0, j = 0; i < sizeof(ushort); i++, j++)
            {
                destination[offset + i] = (byte)(source >> (8 * j));
            }
        }
    }
}
