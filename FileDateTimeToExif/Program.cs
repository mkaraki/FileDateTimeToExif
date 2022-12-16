using ExifLibrary;

Console.WriteLine($"Input: {args[0]}");

DateTime createTime = File.GetCreationTime(args[0]);
DateTime modTime = File.GetLastWriteTime(args[0]);

DateTime createTimeUtc = File.GetCreationTimeUtc(args[0]);

TimeSpan tz = createTime - createTimeUtc;

Console.WriteLine($"CreatedTime {createTime}");
Console.WriteLine($"Mod    Time {modTime}");

DateTime newTime;

if (createTime.Ticks < modTime.Ticks)
    newTime = createTime;
else
    newTime = modTime;

Console.WriteLine($"New Time: {newTime}");

var imgfile = ImageFile.FromFile(args[0]);

imgfile.Properties.Set(ExifTag.DateTime, newTime);
imgfile.Properties.Set(ExifTag.DateTimeDigitized, newTime);
imgfile.Properties.Set(ExifTag.DateTimeOriginal, newTime);

imgfile.Properties.Set((ExifTag)236880, $"{(tz.TotalSeconds >= 0 ? '+' : '-')}{tz.Hours}:{tz.Minutes}");
imgfile.Properties.Set((ExifTag)236881, $"{(tz.TotalSeconds >= 0 ? '+' : '-')}{tz.Hours}:{tz.Minutes}");
imgfile.Properties.Set((ExifTag)236882, $"{(tz.TotalSeconds >= 0 ? '+' : '-')}{tz.Hours}:{tz.Minutes}");

imgfile.Save(args[0]);

