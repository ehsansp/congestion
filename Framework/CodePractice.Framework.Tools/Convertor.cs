using System.Security.Cryptography;
using System.Text;

namespace CodePractice.Framework.Tools;

public static partial class Convertor
{
    private static readonly string[] pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
    private static readonly string[] en = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    public static readonly Dictionary<string, string?> ArabicToPersianDictionary = new(){
            {"ك" ,"ک"},
            {"دِ", "د"},
            {"بِ", "ب"},
            {"زِ", "ز"},
            {"ذِ", "ذ"},
            {"شِ", "ش"},
            {"سِ", "س"},
            {"ى" ,"ی"},
            {"ي" ,"ی"},
            {"١" ,"۱"},
            {"٢" ,"۲"},
            {"٣" ,"۳"},
            {"٤" ,"۴"},
            {"٥" ,"۵"},
            {"٦" ,"۶"},
            {"٧" ,"۷"},
            {"٨" ,"۸"},
            {"٩" ,"۹"},
            {"٠" ,"۰" },
        };

    public static string PersianNumbersToEnglish(this string? arabicChars)
    {
        StringBuilder st = new StringBuilder();
        foreach (var arabicChar in arabicChars)
        {
            var index = pn.Select((x, index) => new { Element = x, index }).FirstOrDefault(x => arabicChar.ToString() == x.Element);
            if (index != null)
            {
                st.Append(en[index.index]);
            }
            else
            {
                st.Append(arabicChar);
            }
        }
        return st.ToString().Replace("\"", "").Trim();
    }
    public static string ArabicToPersian(this string? arabicChars)
    {
        StringBuilder st = new StringBuilder();
        foreach (var arabicChar in arabicChars)
        {
            ArabicToPersianDictionary.TryGetValue(arabicChar.ToString(), out string? translated);
            if (translated != null)
            {
                st.Append(translated);
            }
            else
            {
                st.Append(arabicChar);
            }
        }

        return st.ToString();
    }
    public static string ToPersianNumber(this string strNum)
    {
        if (!string.IsNullOrEmpty(strNum))
        {
            string chash = strNum;
            for (int i = 0; i < 10; i++)
                chash = chash.Replace(en[i], pn[i]);
            return chash;
        }
        else
            return string.Empty;

    }
    public static string ToPersianNumber(this int intNum)
    {
        string chash = intNum.ToString();
        for (int i = 0; i < 10; i++)
            chash = chash.Replace(en[i], pn[i]);
        return chash;

    }

    public static string ToEnglishNumber(this string strNum)
    {
        if (!string.IsNullOrEmpty(strNum))
        {
            string chash = strNum;
            for (int i = 0; i < 10; i++)

                chash = chash.Replace(pn[i], en[i]);
            return chash;
        }
        else
            return strNum;

    }
    public static string ToEnglishNumber(this int intNum)
    {
        string chash = intNum.ToString();
        for (int i = 0; i < 10; i++)

            chash = chash.Replace(pn[i], en[i]);
        return chash;

    }

    public static int ToUnixTimestamp(this DateTime value)
    {
        return (int)Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
    }
    public static string ToHiddenX(this string strNum)
    {
        string chash = strNum;
        if (!string.IsNullOrEmpty(chash))
        {
            try
            {
                StringBuilder sb = new StringBuilder(chash);
                sb[5] = 'x';
                sb[6] = 'x';
                sb[7] = 'x';
                return sb.ToString();

            }
            catch
            {
                return chash;
            }

        }
        else
        {
            return chash;
        }

    }

    public static bool isEven(this int number)
    {
        var d = number % 2 == 0;
        return d;
    }
    public static bool isOdd(this int number)
    {
        var d = number % 2 != 0;
        return d;
    }

    //public static byte[] GetBytefromImage(HttpPostedFileBase Image)
    //{
    //    var content = new byte[Image.ContentLength];
    //    Image.InputStream.Read(content, 0, Image.ContentLength);
    //    return content;
    //}
    //public static System.Drawing.Image GetImagefromHttpPostedFileBase(HttpPostedFileBase image)
    //{
    //    System.Drawing.Image sourceimage = System.Drawing.Image.FromStream(image.InputStream);
    //    return sourceimage;

    //}

    public static string Encrypt(this string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public static string Decrypt(this string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public static string ByteArrayToString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

    //public static byte[] imageToByteArray(System.Drawing.Image imageIn)
    //{
    //    MemoryStream ms = new MemoryStream();
    //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
    //    return ms.ToArray();
    //}

    //public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
    //{
    //    MemoryStream ms = new MemoryStream(byteArrayIn);
    //    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
    //    return returnImage;
    //}
    public static string ToHiddenPhone(this string phone)
    {
        if (string.IsNullOrEmpty(phone))
            return null;
        try
        {
            return phone.Substring(0, 5) + "****" + phone.Substring(phone.Length - 4, 4);
        }
        catch
        {
            return null;
        }


    }
    public static string ToHiddenEmail(this string email)
    {
        if (string.IsNullOrEmpty(email))
            return null;
        try
        {
            return email.Substring(0, 3) + "****" + email.Substring(email.Length - 3, 3);
        }
        catch
        {
            return null;
        }


    }

    public static string ConvertToFinglish(string sourceText)
    {
        string temp = null;

        for (int counter = 0; counter < sourceText.Length; counter++)
        {
            switch (sourceText[counter])
            {
                case 'a':
                    if (counter != 0 && sourceText[counter - 1] == 'a')
                        temp = temp.Substring(0, temp.Length - 1) + 'ا';
                    else
                        temp += "";
                    break;
                case 'A':
                    temp += "آ";
                    break;
                case 'b':
                    temp += "ب";
                    break;
                case 'c':
                    temp += "ث";
                    break;
                case 'd':
                    temp += "د";
                    break;
                case 'e':
                    if (counter != 0 && sourceText[counter - 1] == 'e')
                        temp = temp.Substring(0, temp.Length - 1) + 'ه';
                    else
                        temp += "";
                    break;
                case 'E':
                    temp += "ع";
                    break;
                case 'f':
                    temp += "ف";
                    break;
                case 'g':
                    temp += 'گ';
                    break;
                case 'h':
                    if (counter != 0 && sourceText[counter - 1] == 'c')
                        temp = temp.Substring(0, temp.Length - 1) + 'چ';
                    else if (counter != 0 && sourceText[counter - 1] == 'g')
                        temp = temp.Substring(0, temp.Length - 1) + 'ق';
                    else if (counter != 0 && sourceText[counter - 1] == 'G')
                        temp = temp.Substring(0, temp.Length - 1) + 'غ';
                    else if (counter != 0 && sourceText[counter - 1] == 'k')
                        temp = temp.Substring(0, temp.Length - 1) + 'خ';
                    else if (counter != 0 && sourceText[counter - 1] == 'j')
                        temp = temp.Substring(0, temp.Length - 1) + 'ژ';
                    else if (counter != 0 && sourceText[counter - 1] == 's')
                        temp = temp.Substring(0, temp.Length - 1) + 'ش';
                    else
                        temp += 'ه';
                    break;
                case 'H':
                    temp += 'ح';
                    break;
                case 'i':
                    if (counter != 0 && sourceText[counter - 1] == 'e')
                        temp = temp.Substring(0, temp.Length - 1) + 'ئ';
                    else
                        temp += 'ی';
                    break;
                case 'I':
                    temp += "ای";
                    break;
                case 'j':
                    temp += "j";
                    break;
                case 'k':
                    temp += "ک";
                    break;
                case 'l':
                    temp += "ل";
                    break;
                case 'm':
                    temp += "م";
                    break;
                case 'n':
                    temp += "ن";
                    break;
                case 'o':
                    if (counter != 0 && sourceText[counter - 1] == 'o')
                        temp = temp.Substring(0, temp.Length - 1) + 'و';
                    else
                        temp += "";
                    break;
                case 'p':
                    temp += "پ";
                    break;
                case 'r':
                    temp += "ر";
                    break;
                case 's':
                    temp += "س";
                    break;
                case 'S':
                    temp += "ص";
                    break;
                case 't':
                    temp += "ت";
                    break;
                case 'T':
                    temp += "ط";
                    break;
                case 'u':
                    temp += "و";
                    break;
                case 'v':
                    temp += "و";
                    break;
                case 'w':
                    temp += "";
                    break;
                case 'x':
                    temp += "س";
                    break;
                case 'y':
                    temp += "ی";
                    break;
                case 'z':
                    temp += "ز";
                    break;
                case 'Z':
                    temp += "ذ";
                    break;
                default:
                    temp += sourceText[counter];
                    break;
            }
        }

        return temp;
    }

    public static string ToGiberish(this string giberish)
    {
        StringBuilder temp = new StringBuilder();
        for (int counter = 0; counter < giberish.Length; counter++)
        {
            switch (giberish[counter])
            {
                case 'q':
                    temp.Append("ض");
                    break;
                case 'w':
                    temp.Append("ص");
                    break;
                case 'e':
                    temp.Append("ث");
                    break;
                case 'r':
                    temp.Append("ق");
                    break;
                case 't':
                    temp.Append("ف");
                    break;
                case 'y':
                    temp.Append("غ");
                    break;
                case 'u':
                    temp.Append("ع");
                    break;
                case 'i':
                    temp.Append("ه");
                    break;
                case 'o':
                    temp.Append("خ");
                    break;
                case 'p':
                    temp.Append("ح");
                    break;
                case 'a':
                    temp.Append("ش");
                    break;
                case 's':
                    temp.Append("س");
                    break;
                case 'd':
                    temp.Append("ی");
                    break;
                case 'f':
                    temp.Append("ب");
                    break;
                case 'g':
                    temp.Append("ل");
                    break;
                case 'h':
                    temp.Append("ا");
                    break;
                case 'j':
                    temp.Append("ت");
                    break;
                case 'k':
                    temp.Append("ن");
                    break;
                case 'l':
                    temp.Append("م");
                    break;
                case ';':
                    temp.Append("ک");
                    break;
                case '\'':
                    temp.Append("گ");
                    break;
                case '\\' or 'C':
                    temp.Append("ژ");
                    break;
                case '[':
                    temp.Append("ج");
                    break;
                case ']':
                    temp.Append("چ");
                    break;
                case '`':
                    temp.Append("پ");
                    break;
                case 'z':
                    temp.Append("ظ");
                    break;
                case 'x':
                    temp.Append("ط");
                    break;
                case 'c':
                    temp.Append("ز");
                    break;
                case 'v':
                    temp.Append("ر");
                    break;
                case 'b':
                    temp.Append("ذ");
                    break;
                case 'n':
                    temp.Append("د");
                    break;
                case 'm':
                    temp.Append("ئ");
                    break;
                case ',':
                    temp.Append("و");
                    break;
                case 'ض':
                    temp.Append("a");
                    break;
                case 'ص':
                    temp.Append("w");
                    break;
                case 'ث':
                    temp.Append("e");
                    break;
                case 'ق':
                    temp.Append("r");
                    break;
                case 'ف':
                    temp.Append("t");
                    break;
                case 'غ':
                    temp.Append("y");
                    break;
                case 'ع':
                    temp.Append("u");
                    break;
                case 'ه':
                    temp.Append("i");
                    break;
                case 'خ':
                    temp.Append("o");
                    break;
                case 'ح':
                    temp.Append("p");
                    break;
                case 'ج':
                    temp.Append("[");
                    break;
                case 'چ':
                    temp.Append("]");
                    break;
                case 'ش':
                    temp.Append("a");
                    break;
                case 'س':
                    temp.Append("s");
                    break;
                case 'ی':
                    temp.Append("d");
                    break;
                case 'ب':
                    temp.Append("f");
                    break;
                case 'ل':
                    temp.Append("g");
                    break;
                case 'ا':
                    temp.Append("h");
                    break;
                case 'ت':
                    temp.Append("j");
                    break;
                case 'ن':
                    temp.Append("k");
                    break;
                case 'م':
                    temp.Append("l");
                    break;
                case 'ک':
                    temp.Append(";");
                    break;
                case 'گ':
                    temp.Append("'");
                    break;
                case 'پ':
                    temp.Append("`");
                    break;
                case '÷':
                    temp.Append("`");
                    break;
                case 'ظ':
                    temp.Append("z");
                    break;
                case 'ط':
                    temp.Append("x");
                    break;
                case 'ز':
                    temp.Append("c");
                    break;
                case 'ر':
                    temp.Append("v");
                    break;
                case 'ذ':
                    temp.Append("b");
                    break;
                case 'د':
                    temp.Append("n");
                    break;
                case 'ئ':
                    temp.Append("m");
                    break;
                case 'و':
                    temp.Append(",");
                    break;
                case '.':
                    temp.Append(".");
                    break;
                case '/':
                    temp.Append("/");
                    break;
                default:
                    temp.Append(giberish[counter]);
                    break;

            }
        }

        return temp.ToString();
    }
}
