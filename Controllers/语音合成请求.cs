using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using 语音合成API.Mods;

namespace 语音合成API.Controllers
{
    [Route("api/语音合成请求")]
    [ApiController]
    public class 语音合成请求 : ControllerBase
    {
        [Produces("audio/mpeg")]
        [HttpGet(Name = "语音合成请求")]
        public Stream Get(string text,string name= "zh-CN-XiaoxiaoNeural", string style= "General", string rate="0",string pitch="0")
        {
            long 当前Ticks = DateTime.Now.Ticks;
            string 当前路径 = System.IO.Directory.GetCurrentDirectory();
            string Data文件夹路径 = 当前路径 + "/Data";
            string 工具文件夹路径 = Data文件夹路径 + "/Tool";
            string 语音文件夹路径 = Data文件夹路径 + "/Voice";

            text = text.Replace(" ", ",");//短占解决不能输入空白的方案

            语音文件参数 请求文件 = new 语音文件参数();
            //请求文件.传入(运行.TransferExe2(工具文件夹路径 + "/tts语音合成.exe", "--md5 " + text));
            if (文本处理.搜索文件夹回传是否(语音文件夹路径, name))
            {
                if (文本处理.搜索文件(语音文件夹路径 + "/" + name, name + "_" + style + "_" + rate + "_" + pitch + "_" + 文本处理.字符串MD5_2(text) + "_" + ".mp3"))//注意算法MD5加密后是否相同
                {
                    Console.WriteLine("["+ DateTime.Now.ToString("yyyy-MM-dd") + " "+ DateTime.Now.ToLongTimeString().ToString() + "]"+"处理时长:" + (DateTime.Now.Ticks - 当前Ticks) / 10000 + "ms "+"处理文本内容:"+text);
                    return System.IO.File.OpenRead(语音文件夹路径 + "/" + name + "/"+name + "_" + style + "_" + rate + "_" + pitch + "_" + 运行.TransferExe2(工具文件夹路径 + "/tts语音合成.exe", "--md5 " + text).Replace("\n", "").Replace("\t", "").Replace("\r", "") + "_" + ".mp3");
                }
            }

            请求文件.传入(运行.TransferExe2(工具文件夹路径 + "/tts语音合成.exe", "--text " + text, "--name " + name, "--style " + style, "--rate " + rate, "--pitch " + pitch));
            if (!文本处理.搜索文件夹回传是否(语音文件夹路径, 请求文件.声音))
            {
                System.IO.Directory.CreateDirectory(语音文件夹路径 + "/" + 请求文件.声音);    //创建语音分类文件资料夹
            }
            System.IO.File.Move(当前路径 + "/" + 请求文件.文件全名, 语音文件夹路径 + "/" + 请求文件.声音 + "/" + 请求文件.文件全名, true);//移动文件到分类资料夹

            // 直接返回文件流
            Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString().ToString() + "]" + "处理时长:" + (DateTime.Now.Ticks - 当前Ticks) / 10000 + "ms " + "处理文本内容:" + text);
            return System.IO.File.OpenRead(语音文件夹路径 + "/" + 请求文件.声音 + "/" + 请求文件.文件全名);
        }
    }
    public struct 语音文件参数
    {
        public string 文件全名;
        public string 声音;
        public string 风格;
        public string 语速;
        public string 音调;
        public string 文本内容;
        public string 文件格式;
        public void 传入(string 文件名)
        {
            文件全名 = 文件名.Replace("\n", "").Replace("\t", "").Replace("\r", "");
            string[] sArray = 文件全名.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
            
            声音 = sArray[0];
            风格 = sArray[1];
            语速 = sArray[2];
            音调 = sArray[3];
            文本内容 = sArray[4];
            文件格式 = sArray[5];
        }
    }
}
