using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public partial class DU : Node
{
#nullable enable
    public static void Log(string what,
        [CallerMemberName] string caller = "",
        [CallerFilePath] string sourceFile = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        var frame = Engine.GetFramesDrawn();

        var lastSlashIndex = sourceFile.LastIndexOf("\\");
        var className = sourceFile.Substring(lastSlashIndex + 1);
        className = className.Remove(className.Length - 3);

        GD.Print(frame, ": ", className, ":", lineNumber, ":", caller, ": ", what);
    }

    public static void Log(object[] what,
        [CallerMemberName] string caller = "",
        [CallerFilePath] string sourceFile = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        var _what = ToString(what);
        
        var frame = Engine.GetFramesDrawn();

        var lastSlashIndex = sourceFile.LastIndexOf("\\");
        var className = sourceFile.Substring(lastSlashIndex + 1);
        className = className.Remove(className.Length - 3);

        GD.Print(frame, ": ", className, ":", lineNumber, ":", caller, ": ", _what);
    }

    public static void Log(object what,
        [CallerMemberName] string caller = "",
        [CallerFilePath] string sourceFile = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        var _what = ToString(what);

        var frame = Engine.GetFramesDrawn();

        var lastSlashIndex = sourceFile.LastIndexOf("\\");
        var className = sourceFile.Substring(lastSlashIndex + 1);
        className = className.Remove(className.Length - 3);

        GD.Print(frame, ": ", className, ":", lineNumber, ":", caller, ": ", _what);
    }


    public static string ToString(params object[] args)
    {
        var output = "";

        List<string> subStrings = new List<string>();

        foreach (object arg in args)
        {
            var subString = arg.ToString();
            subStrings.Add(subString ?? "null");
        }

        output = String.Join(" ", subStrings);
        return output;
    }
}
