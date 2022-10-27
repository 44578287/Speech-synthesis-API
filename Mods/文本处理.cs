using System.Security.Cryptography;
using System.Text;

namespace 语音合成API.Mods
{
    public class 文本处理
    {
        public static string 读取(string 文本地址)
        {
            string text = System.IO.File.ReadAllText(文本地址);
            return text;
        }
        public static string[] 字串关键字分割(string 字串, string 分割符)
        {
            string[] sArray = 字串.Split(分割符);
            return sArray;
        }
        public static int 搜索字串组位置(string[] 字串组, string 搜索内容)
        {
            return Array.IndexOf(字串组, 搜索内容);
        }
        public static bool 搜索文件(string 目标路径, string 文件名)
        {
            DirectoryInfo di = new DirectoryInfo(目标路径);
            foreach (var fi in di.GetFiles(文件名))
            {
                //Console.WriteLine(fi.Name);
                return true;
            }
            return false;
        }
        public static bool 搜索文件夹回传是否(string 目标路径, string 文件夹名)
        {
            string[] dirs = Directory.GetDirectories(目标路径, 文件夹名, SearchOption.TopDirectoryOnly);
            //Console.WriteLine("The number of directories starting with p is {0}.", dirs.Length);
            foreach (string dir in dirs)
            {
                //Console.WriteLine(dir);
                return true;
            }
            return false;
        }
        public static string 搜索文件夹回传位置(string 目标路径, string 文件夹名)
        {
            string[] dirs = Directory.GetDirectories(目标路径, 文件夹名, SearchOption.TopDirectoryOnly);
            //Console.WriteLine("The number of directories starting with p is {0}.", dirs.Length);
            foreach (string dir in dirs)
            {
                //Console.WriteLine(dir);
                return dir;
            }
            return "";
        }
        public static string 字符串MD5(string input)
        {
            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }
        public static string 字符串MD5_2(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);

            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }

        public static string 文件MD5(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString().ToLower();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
    }
}