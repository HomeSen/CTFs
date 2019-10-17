using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace BMPHIDE
{
    class Program
    {
        public static int yy = 20;

        public static string ww = "1F7D";

        public static string zz = "MTgwMw==";

        private static void Init()
        {
            yy *= 136;
            Type typeFromHandle = typeof(A);
            ww += "14";
            MethodInfo[] methods = typeFromHandle.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            MethodInfo[] array = methods;
            foreach (MethodInfo methodInfo in array)
            {
                RuntimeHelpers.PrepareMethod(methodInfo.MethodHandle);
            }
            A.CalculateStack();
            ww += "82";
            MethodInfo m = null;
            MethodInfo m2 = null;
            MethodInfo m3 = null;
            MethodInfo m4 = null;
            zz = "MzQxOTk=";
            MethodInfo[] methods2 = typeof(Program).GetMethods();
            foreach (MethodInfo methodInfo2 in methods2)
            {
                if (methodInfo2.GetMethodBody() != null)
                {
                    byte[] iLAsByteArray = methodInfo2.GetMethodBody().GetILAsByteArray();
                    if (iLAsByteArray.Length > 8)
                    {
                        byte[] array2 = new byte[iLAsByteArray.Length - 2];
                        Buffer.BlockCopy(iLAsByteArray, 2, array2, 0, iLAsByteArray.Length - 2);
                        D d = new D();
                        switch (d.a(array2))
                        {
                            case 3472577156u:
                                m = methodInfo2;
                                break;
                            case 2689456752u:
                                m2 = methodInfo2;
                                break;
                            case 3785258436u: // 3040029055u:
                                m3 = methodInfo2;
                                break;
                            case 2663056498u:
                                m4 = methodInfo2;
                                break;
                        }
                    }
                }
            }
            A.VerifySignature(m, m2);   // swap a() and b()
            A.VerifySignature(m3, m4);  // swap c() and d()
        }

        public static byte a(byte b, int r)
        {
            return (byte)((b + r ^ r) & 0xFF);
        }

        public static byte b(byte b, int r)
        {
            for (int i = 0; i < r; i++)
            {
                byte b2 = (byte)((b & 0x80) / 128);
                b = (byte)((b * 2 & 0xFF) + b2);
            }
            return b;
        }

        public static byte c(byte b, int r)
        {
            byte b2 = 1;
            for (int i = 0; i < 8; i++)
            {
                b2 = (((b & 1) != 1) ? ((byte)(b2 - 1 & 0xFF)) : ((byte)(b2 * 2 + 1 & 0xFF)));
            }
            return b2;
        }

        public static byte d(byte b, int r)
        {
            for (int i = 0; i < r; i++)
            {
                byte b2 = (byte)((b & 1) * 128);
                b = (byte)(((int)b / 2 & 0xFF) + b2);
            }
            return b;
        }

        public static byte e(byte b, byte k)
        {
            for (int i = 0; i < 8; i++)
            {
                b = (((b >> i & 1) != (k >> i & 1)) ? ((byte)(b | (1 << i & 0xFF))) : ((byte)(b & ~(1 << i) & 0xFF)));
            }
            return b;
        }

        public static byte f(int idx)
        {
            int num = 0;
            int num2 = 0;
            byte result = 0;
            int num3 = 0;
            int[] array = new int[256] { 121, 255, 214, 60, 106, 216, 149, 89, 96, 29, 81, 123, 182, 24, 167, 252, 88, 212, 43, 85, 181, 86, 108, 213, 50, 78, 247, 83, 193, 35, 135, 217, 0, 64, 45, 236, 134, 102, 76,
            74, 153, 34, 39, 10, 192, 202, 71, 183, 185, 175, 84, 118, 9, 158, 66, 128, 116, 117, 4, 13, 46, 227, 132, 240, 122, 11, 18, 186, 30, 157, 1, 154, 144, 124, 152, 187, 32, 87, 141, 103, 189, 12, 53, 222, 206,
            91, 20, 174, 49, 223, 155, 250, 95, 31, 98, 151, 179, 101, 47, 17, 207, 142, 199, 3, 205, 163, 146, 48, 165, 225, 62, 33, 119, 52, 241, 228, 162, 90, 140, 232, 129, 114, 75, 82, 190, 65, 2, 21, 14, 111, 115,
            36, 107, 67, 126, 80, 110, 23, 44, 226, 56, 7, 172, 221, 239, 161, 61, 93, 94, 99, 171, 97, 38, 40, 28, 166, 209, 229, 136, 130, 164, 194, 243, 220, 25, 169, 105, 238, 245, 215, 195, 203, 170, 16, 109, 176,
            27, 184, 148, 131, 210, 231, 125, 177, 26, 246, 127, 198, 254, 6, 69, 237, 197, 54, 59, 137, 79, 178, 139, 235, 249, 230, 233, 204, 196, 113, 120, 173, 224, 55, 92, 211, 112, 219, 208, 77, 191, 242, 133, 244,
            168, 188, 138, 251, 70, 150, 145, 248, 180, 218, 42, 15, 159, 104, 22, 37, 72, 63, 234, 147, 200, 253, 100, 19, 73, 5, 57, 201, 51, 156, 41, 143, 68, 8, 160, 58 };
            for (int i = 0; i <= idx; i++)
            {
                num++;
                num %= 256;
                num2 += array[num];
                num2 %= 256;
                num3 = array[num];
                array[num] = array[num2];
                array[num2] = num3;
                result = (byte)array[(array[num] + array[num2]) % 256];
            }
            return result;
        }

        public static byte g(int idx)
        {
            byte b = (byte)((idx + 1) * 3988292384u);   // changed to 197
            byte k = (byte)((idx + 2) * 1669101435);    // changed to 125
            return e(b, k);
        }

        public static byte[] h(byte[] data)
        {
            byte[] array = new byte[data.Length];
            int num = 0;
            for (int i = 0; i < data.Length; i++)
            {
                int num3 = f(num++);    // rewritten to g(num++)
                int num4 = data[i];
                num4 = e((byte)num4, (byte)num3);
                num4 = a((byte)num4, 7);    // rewritten to b(num4, 7)
                int num6 = f(num++);
                num4 = e((byte)num4, (byte)num6);
                num4 = c((byte)num4, 3);    // rewritten to d(num4, 3)
                array[i] = (byte)num4;
            }
            return array;
        }

        public static byte[] reverse_h(byte[] data)
        {
            return reverse_h(data, data.Length);
        }

        public static byte[] reverse_h(byte[] data, int len)
        {
            byte[] array = new byte[len];
            int num = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (i >= len)
                    break;

                num += 2;
                int num4 = data[i];
                num4 = find_c((byte)num4, 3);
                int num6 = g(num - 1);
                num4 = find_e((byte)num4, (byte)num6);
                num4 = find_a((byte)num4, 7);
                int num3 = g(num - 2);
                array[i] = find_e((byte)num4, (byte)num3);
            }

            return array;
        }

        private static byte find_c(byte target, int r)
        {
            byte i = 0;
            for (i = 0; i <= 255; i++)
                if (target == c(i, r))
                    break;

            return i;
        }

        private static byte find_e(byte target, byte k)
        {
            byte i = 0;
            for (i = 0; i <= 255; i++)
                if (target == e(i, k))
                    break;

            return i;
        }

        private static byte find_a(byte target, int r)
        {
            byte i = 0;
            for (i = 0; i <= 255; i++)
                if (target == a(i, r))
                    break;

            return i;
        }

        public static void i(Bitmap bm, byte[] data)
        {
            int num = Program.j(103);
            for (int i = Program.j(103); i < bm.Width; i++)
            {
                for (int j = Program.j(103); j < bm.Height; j++)
                {
                    if (num > data.Length - Program.j(231))
                    {
                        break;
                    }
                    Color pixel = bm.GetPixel(i, j);
                    int red = (pixel.R & Program.j(27)) | (data[num] & Program.j(228));
                    int green = (pixel.G & Program.j(27)) | (data[num] >> Program.j(230) & Program.j(228));
                    int blue = (pixel.B & Program.j(25)) | (data[num] >> Program.j(100) & Program.j(230));
                    Color color = Color.FromArgb(Program.j(103), red, green, blue);
                    bm.SetPixel(i, j, color);
                    num += Program.j(231);
                }
            }
        }

        public static byte[] reverse_i(Bitmap bm)
        {
            return reverse_i(bm, bm.Width * bm.Height);
        }

        public static byte[] reverse_i(Bitmap bm, int len)
        {
            byte[] data = new byte[len];
            int num = Program.j(103);
            for (int i = Program.j(103); i < bm.Width; i++)
            {
                for (int j = Program.j(103); j < bm.Height; j++)
                {
                    if (num >= len)
                        break;
                    Color pixel = bm.GetPixel(i, j);
                    //if (pixel.A != 0)
                    //    break;
                    int red = pixel.R & Program.j(228);
                    int green = (pixel.G & Program.j(228)) << Program.j(230);
                    int blue = (pixel.B & Program.j(230)) << Program.j(100);
                    data[num] = (byte)(red + green + blue);
                    num += Program.j(231);
                }
                if (num >= len)
                    break;
            }

            return data;
        }

        public static int j(byte z)
        {
            byte b = 5;
            uint num = 0u;
            string value = "";
            byte[] bytes = new byte[8];
            while (true)
            {
                switch (b)
                {
                    case 1:
                        num += 4;
                        b = (byte)(b + 2);
                        break;
                    case 2:
                        num = (uint)(num * yy);
                        b = (byte)(b + 8);
                        break;
                    case 3:
                        num += f(6);
                        b = (byte)(b + 1);
                        break;
                    case 4:
                        z = Program.b(z, 1);
                        b = (byte)(b + 2);
                        break;
                    case 5:
                        num = Convert.ToUInt32(ww, 16);
                        b = (byte)(b - 3);
                        break;
                    case 6:
                        return e(z, (byte)num);
                    case 7:
                        num += Convert.ToUInt32(value);
                        b = (byte)(b - 6);
                        break;
                    case 10:
                        bytes = Convert.FromBase64String(zz);
                        b = (byte)(b + 4);
                        break;
                    case 14:
                        value = Encoding.Default.GetString(bytes);
                        b = (byte)(b - 7);
                        break;
                }
            }

            /*
             * num = Convert.ToUInt32(ww, 16);
             * num = (uint)(num * yy);
             * bytes = Convert.FromBase64String(zz);
             * value = Encoding.Default.GetString(bytes);
             * num += Convert.ToUInt32(value);
             * num += 4;
             * num += f(6);     --> num += 207
             * z = Program.b(z, 1);
             * return e(z, (byte)num);
             */
        }

        static void Main(string[] args)
        {
            Init();
            yy += 18;
            string fullPath = Path.GetFullPath(args[0]);
            Bitmap bitmap = new Bitmap(fullPath);
            //byte[] data = reverse_i(bitmap);
            byte[] data = reverse_i(bitmap);
            //byte[] data2 = reverse_h(data);
            byte[] data2 = reverse_h(data);
            string result = new UTF8Encoding().GetString(data2);
            FileStream f = File.Open(Path.GetFullPath(args[1]), FileMode.Create);
            f.Write(data2, 0, data2.Length);
            f.Flush();
            f.Close();
            //Console.WriteLine(result);
        }
    }
}
