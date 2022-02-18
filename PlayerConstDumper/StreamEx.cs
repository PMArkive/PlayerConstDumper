using LordG.IO;
using Syroot.BinaryData;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing;

namespace PlayerConstDumper
{
    public static class StreamEx
    {
        public static float ReadSingle(this Stream stream)
        {
            using var reader = new EndianReader(stream, true)
            {
                Order = ByteOrder.BigEndian
            };
            return reader.ReadNumeric<float>();
        }

        public static short ReadShort(this Stream stream)
        {
            using var reader = new EndianReader(stream, true)
            {
                Order = ByteOrder.BigEndian
            };
            return reader.ReadNumeric<short>();
        }

        public static ushort ReadUShort(this Stream stream)
        {
            using var reader = new EndianReader(stream, true)
            {
                Order = ByteOrder.BigEndian
            };
            return reader.ReadNumeric<ushort>();
        }

        public static Color ReadColor(this Stream stream)
        {
            using var reader = new EndianReader(stream, true)
            {
                Order = ByteOrder.BigEndian
            };
            /// The color seems to be a ABGR pixel based off Hackio's calls to his version of this method.
            /// Reference: https://github.com/SuperHackio/SMG2MarioConstEditor/blob/master/SMG2MarioConstEditor/PlayerConst.cs#L4653
            /// P.S. System.Drawing.Color isn't considered unmanaged, thanks MS.
            var pixel = reader.ReadNumeric<Argb32>();
            return Color.FromArgb(pixel.A, pixel.B, pixel.G, pixel.R);
        }

        public static byte ReadByte(this Stream stream)
        {
            using var reader = new EndianReader(stream, true);
            return reader.ReadByte();
        }

        public static int ReadInt(this Stream stream)
        {
            using var reader = new EndianReader(stream, true)
            {
                Order = ByteOrder.BigEndian
            };
            return reader.ReadNumeric<int>();
        }

        public static void WriteSingle(this Stream stream, float num)
        {
            using var writer = new EndianWriter(stream, true)
            {
                Order = ByteOrder.BigEndian
            };
            writer.WriteNumeric(num);
        }

        public static void WriteShort(this Stream stream, short num)
        {
            using var writer = new EndianWriter(stream, true)
            {
                Order = ByteOrder.BigEndian
            };
            writer.WriteNumeric(num);
        }

        public static void WriteUShort(this Stream stream, ushort num)
        {
            using var writer = new EndianWriter(stream, true)
            {
                Order = ByteOrder.BigEndian
            };
            writer.WriteNumeric(num);
        }

        public static void WriteColor(this Stream stream, Color color)
        {
            // You know why I am using Agbr here.
            var pixel = new Argb32(color.B, color.G, color.R, color.A);
            using var writer = new EndianWriter(stream, true)
            {
                Order = ByteOrder.BigEndian
            };
            writer.WriteNumeric(pixel);
        }

        public static void WriteInt(this Stream stream, int num)
        {
            using var writer = new EndianWriter(stream, true)
            {
                Order = ByteOrder.BigEndian
            };
            writer.WriteNumeric(num);
        }
    }
}