// See https://aka.ms/new-console-template for more information
using DataAccess.DAL;
using DataAccess.Model;
using Snowflake.Net;

/// <summary>
/// 将string转换为decimal
/// </summary>
/// <param name="str">要转换的字符串</param>
/// <returns>转换后的int类型结果</returns>
 double StrToDouble(string str)
{
    double dt = 0;
    if (string.IsNullOrEmpty(str))
        return dt;
    if (!double.TryParse(str, out dt))
        return 0;
    return dt;
}

/// <summary>
/// 将[时:分:秒]转换为秒钟
/// </summary>
/// <param name="durationStr"></param>
/// <returns></returns>
double ConvertDuration(string durationStr)
{
    double result = 0;
    string[] durationAry = durationStr.Split(':');
    if (durationAry == null || durationAry.Length <= 0 || durationAry.Length < 3)
        return 0;
    double hourSecord = StrToDouble(durationAry[0]) * 3600;
    double minute = StrToDouble(durationAry[1]) * 60;
    double Second = StrToDouble(durationAry[2]);
    result = hourSecord + minute + Second;
    return result;
}

/// <summary>
/// 将秒钟转换为(时:分:秒)
/// </summary>
/// <param name="duration">秒钟</param>
/// <returns>转换后的字符串</returns>
string formatDuration(double duration)
{
    int hour = Convert.ToInt32(decimal.Truncate(Convert.ToDecimal(duration / 3600)));
    int minute = Convert.ToInt32(decimal.Truncate(Convert.ToDecimal(duration % 3600 / 60)));
    int second = Convert.ToInt32(decimal.Truncate(Convert.ToDecimal(duration % 60)));
    return $"{hour.ToString().PadLeft(2, '0')}:{minute.ToString().PadLeft(2, '0')}:{second.ToString().PadLeft(2, '0')}";
}

void Main() 
{
    //List<MP3FileInfo> list = new List<MP3FileInfo>();
    //IdWorker snowId = new IdWorker(1, 1);
    //for (int i = 0; i < 100; i++)
    //{
    //    list.Add(new MP3FileInfo() 
    //    {
    //        FileId=snowId.NextId(),
    //        FileName=$"测试添加",
    //        FileSize=100,
    //        CreateTime=DateTime.Now.AddSeconds(i),
    //    });
    //}
    //string message = string.Empty;
    //string SQLiteFullFilePath = @"E:\WPFProject\WpfNetMusic\WpfNetMusicDB.db";
    //MP3FileInfo_DAL dal = new MP3FileInfo_DAL(SQLiteFullFilePath);
    //if(!string.IsNullOrEmpty(message)) 
    //{
    //    Console.WriteLine(message);
    //    return;
    //}
    //int result = dal.Insert(list, out message);
    //Console.WriteLine($"result={result}");
    //Console.WriteLine($"message={message}");
    //string test = Console.ReadLine();
    //Console.WriteLine(ConvertDuration(test));
    //Console.WriteLine(formatDuration(ConvertDuration(test)));
}

Main();
Console.ReadLine();
