using 语音合成API.Mods;
using static 语音合成API.Mods.配置文件读写;

//------------------------------(设置全局参数)------------------------------
string 当前路径 = System.IO.Directory.GetCurrentDirectory();
string Data文件夹路径 = 当前路径 + "/Data";
string 工具文件夹路径 = Data文件夹路径 + "/Tool";
string 语音文件夹路径 = Data文件夹路径 + "/Voice";
string 配置文件路径 = Data文件夹路径 + "/Config.ini";
bool 初次启动 = true;
//------------------------------(设置全局参数)------------------------------

//------------------------------(目录生成)------------------------------
System.IO.Directory.CreateDirectory(Data文件夹路径);    //创建Data资料夹
System.IO.Directory.CreateDirectory(工具文件夹路径);    //创建工具资料夹
System.IO.Directory.CreateDirectory(语音文件夹路径);    //创建语音文件资料夹
//------------------------------(目录生成)------------------------------

//------------------------------(前置工作)------------------------------
if (!文本处理.搜索文件(Data文件夹路径, "Config.ini"))//检查Config.ini文件以判断是否为第一次启动
{
    初次启动 = true;
    IniHelper.SetValue("环境参数", "当前路径", 当前路径, 配置文件路径);//保存当前路径到配置文件中
    //-------------(API设置)-------------
    IniHelper.SetValue("API设置", "ip", "请填写API绑定IP 推荐填写*", 配置文件路径);
    IniHelper.SetValue("API设置", "port", "请填写API绑定端口", 配置文件路径);
    //-------------(API设置)-------------

    Console.WriteLine("检测到本次为第一次启动,已生成配置文件请前往:");
    Console.WriteLine(配置文件路径 + " (已经自动帮你打开)");
    Console.WriteLine("对配置文件进行修改,修改完再开启服务端");
    System.Diagnostics.Process.Start("notepad.exe", 配置文件路径);//用记事本打开配置文件
    Console.WriteLine("按任意键退出....");
    Console.ReadKey(true);
    System.Environment.Exit(0);
}
else
{
    初次启动 = false;
    if (IniHelper.GetValue("环境参数", "当前路径", 当前路径, 配置文件路径) == 当前路径)//判断文件夹是否被移动过
    {
        //Console.WriteLine("没有被移动过");
    }
    //-------------(API配置验证)-------------
    if ("请填写API绑定IP 推荐填写*".Equals(IniHelper.GetValue("API设置", "ip", "", 配置文件路径)) == true)
    {
        Console.WriteLine("API IP设置出现错误已设置为默认*");
        IniHelper.SetValue("API设置", "ip", "*", 配置文件路径);
    }
    if ("请填写API绑定端口".Equals(IniHelper.GetValue("API设置", "port", "", 配置文件路径)) == true)
    {
        Console.WriteLine("API 端口设置出现错误已设置为默认:5000");
        IniHelper.SetValue("API设置", "port", "5000", 配置文件路径);
    }
    Console.WriteLine("API详情:" + "http://" + IniHelper.GetValue("API设置", "ip", "", 配置文件路径) + ":" + IniHelper.GetValue("API设置", "port", "", 配置文件路径) + "/swagger/index.html");
    //-------------(API配置验证)-------------
}
//------------------------------(前置工作)------------------------------

//------------------------------(Web API主程序)------------------------------
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run("http://" + IniHelper.GetValue("API设置", "ip", "", 配置文件路径) + ":" + IniHelper.GetValue("API设置", "port", "", 配置文件路径));
//------------------------------(Web API主程序)------------------------------