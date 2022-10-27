using �����ϳ�API.Mods;
using static �����ϳ�API.Mods.�����ļ���д;

//------------------------------(����ȫ�ֲ���)------------------------------
string ��ǰ·�� = System.IO.Directory.GetCurrentDirectory();
string Data�ļ���·�� = ��ǰ·�� + "/Data";
string �����ļ���·�� = Data�ļ���·�� + "/Tool";
string �����ļ���·�� = Data�ļ���·�� + "/Voice";
string �����ļ�·�� = Data�ļ���·�� + "/Config.ini";
bool �������� = true;
//------------------------------(����ȫ�ֲ���)------------------------------

//------------------------------(Ŀ¼����)------------------------------
System.IO.Directory.CreateDirectory(Data�ļ���·��);    //����Data���ϼ�
System.IO.Directory.CreateDirectory(�����ļ���·��);    //�����������ϼ�
System.IO.Directory.CreateDirectory(�����ļ���·��);    //���������ļ����ϼ�
//------------------------------(Ŀ¼����)------------------------------

//------------------------------(ǰ�ù���)------------------------------
if (!�ı�����.�����ļ�(Data�ļ���·��, "Config.ini"))//���Config.ini�ļ����ж��Ƿ�Ϊ��һ������
{
    �������� = true;
    IniHelper.SetValue("��������", "��ǰ·��", ��ǰ·��, �����ļ�·��);//���浱ǰ·���������ļ���
    //-------------(API����)-------------
    IniHelper.SetValue("API����", "ip", "����дAPI��IP �Ƽ���д*", �����ļ�·��);
    IniHelper.SetValue("API����", "port", "����дAPI�󶨶˿�", �����ļ�·��);
    //-------------(API����)-------------

    Console.WriteLine("��⵽����Ϊ��һ������,�����������ļ���ǰ��:");
    Console.WriteLine(�����ļ�·�� + " (�Ѿ��Զ������)");
    Console.WriteLine("�������ļ������޸�,�޸����ٿ��������");
    System.Diagnostics.Process.Start("notepad.exe", �����ļ�·��);//�ü��±��������ļ�
    Console.WriteLine("��������˳�....");
    Console.ReadKey(true);
    System.Environment.Exit(0);
}
else
{
    �������� = false;
    if (IniHelper.GetValue("��������", "��ǰ·��", ��ǰ·��, �����ļ�·��) == ��ǰ·��)//�ж��ļ����Ƿ��ƶ���
    {
        //Console.WriteLine("û�б��ƶ���");
    }
    //-------------(API������֤)-------------
    if ("����дAPI��IP �Ƽ���д*".Equals(IniHelper.GetValue("API����", "ip", "", �����ļ�·��)) == true)
    {
        Console.WriteLine("API IP���ó��ִ���������ΪĬ��*");
        IniHelper.SetValue("API����", "ip", "*", �����ļ�·��);
    }
    if ("����дAPI�󶨶˿�".Equals(IniHelper.GetValue("API����", "port", "", �����ļ�·��)) == true)
    {
        Console.WriteLine("API �˿����ó��ִ���������ΪĬ��:5000");
        IniHelper.SetValue("API����", "port", "5000", �����ļ�·��);
    }
    Console.WriteLine("API����:" + "http://" + IniHelper.GetValue("API����", "ip", "", �����ļ�·��) + ":" + IniHelper.GetValue("API����", "port", "", �����ļ�·��) + "/swagger/index.html");
    //-------------(API������֤)-------------
}
//------------------------------(ǰ�ù���)------------------------------

//------------------------------(Web API������)------------------------------
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
app.Run("http://" + IniHelper.GetValue("API����", "ip", "", �����ļ�·��) + ":" + IniHelper.GetValue("API����", "port", "", �����ļ�·��));
//------------------------------(Web API������)------------------------------