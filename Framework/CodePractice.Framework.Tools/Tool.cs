using System.Text.RegularExpressions;
using CodePractice.Framework.Functions;

namespace CodePractice.Framework.Tools;

public static class Tool
{
    private static Random random;
    public static Boolean IsValidNationalCode(this String nationalCode)
    {
        //در صورتی که کد ملی وارد شده تهی باشد

        if (String.IsNullOrEmpty(nationalCode))
            return false;


        //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
        if (nationalCode.Length != 10)
            return false;

        //در صورتی که کد ملی ده رقم عددی نباشد
        var regex = new Regex(@"\d{10}");
        if (!regex.IsMatch(nationalCode))
            return false;

        //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
        var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
        if (allDigitEqual.Contains(nationalCode)) return false;


        //عملیات شرح داده شده در بالا
        var chArray = nationalCode.ToCharArray();
        var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
        var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
        var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
        var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
        var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
        var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
        var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
        var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
        var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
        var a = Convert.ToInt32(chArray[9].ToString());

        var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
        var c = b % 11;

        return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
    }

    private static void Init()
    {
        if (random == null) random = new Random();
    }

    public static int Random(int min = 100000000, int max = 999999999)
    {
        Init();
        return random.Next(min, max);
    }

    public static long LongRandom(long min = 100000000000, long max = 999999999999)
    {

        return LRandom(min, max);
    }
    private static long LRandom(long min, long max)
    {
        Init();
        long result = random.Next((Int32)(min >> 32), (Int32)(max >> 32));
        result = (result << 32);
        result = result | (long)random.Next((Int32)min, (Int32)max);
        return result;
    }
    private static readonly Random _rng = new Random();
    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwzyz1234567890";
    public static string RandomString(int size = 16)
    {
        char[] buffer = new char[size];

        for (int i = 0; i < size; i++)
        {
            buffer[i] = _chars[_rng.Next(_chars.Length)];
        }
        return new string(buffer);
    }
    //public static Bitmap cropAtRect(this Bitmap b, Rectangle r)
    //{
    //    Bitmap nb = new Bitmap(r.Width, r.Height);
    //    using (Graphics g = Graphics.FromImage(nb))
    //    {
    //        g.DrawImage(b, -r.X, -r.Y);
    //        return nb;
    //    }
    //}
    //public static Image cropImage(Image img, Rectangle cropArea)
    //{
    //    Bitmap bmpImage = new Bitmap(img);
    //    return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
    //}




    //public static byte[] Resize2Max50Kbytes(byte[] byteImageIn)
    //{
    //    byte[] currentByteImageArray = byteImageIn;
    //    double scale = 1f;



    //    MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
    //    Image fullsizeImage = Image.FromStream(inputMemoryStream);

    //    while (currentByteImageArray.Length > 155000)
    //    {
    //        Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
    //        MemoryStream resultStream = new MemoryStream();

    //        fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

    //        currentByteImageArray = resultStream.ToArray();
    //        resultStream.Dispose();
    //        resultStream.Close();

    //        scale -= 0.05f;
    //    }

    //    return currentByteImageArray;
    //}

    public static bool? CheckImageSame(byte[] image1Bytes, byte[] image2Bytes)
    {


        var image164 = Convert.ToBase64String(image1Bytes);
        var image264 = Convert.ToBase64String(image2Bytes);

        return string.Equals(image164, image264);
    }

    public static int Replace<T>(this IList<T> source, T oldValue, T newValue)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var index = source.IndexOf(oldValue);
        if (index != -1)
            source[index] = newValue;
        return index;
    }

    public static void ReplaceAll<T>(this IList<T> source, T oldValue, T newValue)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        int index = -1;
        do
        {
            index = source.IndexOf(oldValue);
            if (index != -1)
                source[index] = newValue;
        } while (index != -1);
    }


    public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, T oldValue, T newValue)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return source.Select(x => EqualityComparer<T>.Default.Equals(x, oldValue) ? newValue : x);
    }

    public static bool ContainsAny(this string str, params string[] values)
    {
        if (!string.IsNullOrEmpty(str) || values.Length > 0)
        {
            foreach (string value in values)
            {
                if (str.Contains(value))
                    return true;
            }
        }

        return false;
    }


    public static string ImageUrlBuilder(long? id, string ex)
    {
        if (id != null)
            return StaticParams.ArvanUrl + "/" + id + "." + ex;
        else
            return string.Empty;
    }

    public static string VideoUrlBuilder(long? id, string ex)
    {
        if (id != null)
            return StaticParams.ArvanUrlVideo + id + "." + ex;
        else
            return string.Empty;
    }



}
