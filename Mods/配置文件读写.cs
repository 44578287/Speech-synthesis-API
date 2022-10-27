using System.Runtime.InteropServices;
using System.Text;

namespace 语音合成API.Mods
{
    public class 配置文件读写
    {
        public class IniHelper
        {
            [DllImport("kernel32")]//返回0表示失败，非0为成功
            private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
            [DllImport("kernel32")]//返回取得字符串缓冲区的长度
            private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

            /// <summary>
            /// 读取ini文件
            /// </summary>
            /// <param name="Section">名称</param>
            /// <param name="Key">关键字</param>
            /// <param name="defaultText">默认值</param>
            /// <param name="iniFilePath">ini文件地址</param>
            /// <returns></returns>
            public static string GetValue(string Section/*分区名*/, string Key/*值名*/, string defaultText/*留空*/, string iniFilePath/*配置文件位置*/)
            {
                if (File.Exists(iniFilePath))
                {
                    StringBuilder temp = new StringBuilder(1024);
                    GetPrivateProfileString(Section, Key, defaultText, temp, 1024, iniFilePath);
                    return temp.ToString();
                }
                else
                {
                    return defaultText;
                }
            }

            /// <summary>
            /// 写入ini文件
            /// </summary>
            /// <param name="Section">名称</param>
            /// <param name="Key">关键字</param>
            /// <param name="defaultText">默认值</param>
            /// <param name="iniFilePath">ini文件地址</param>
            /// <returns></returns>
            public static bool SetValue(string Section/*分区名*/, string Key/*值名*/, string Value/*值*/, string iniFilePath/*配置文件位置*/)
            {
                var pat = Path.GetDirectoryName(iniFilePath);
                if (Directory.Exists(pat) == false)
                {
                    Directory.CreateDirectory(pat);
                }
                if (File.Exists(iniFilePath) == false)
                {
                    File.Create(iniFilePath).Close();
                }
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
