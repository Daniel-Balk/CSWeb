using System;

public static class Logger
{
    public static string Time
    {
        get
        {
            return "[" + DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second + "] ";
        }
    }

    public static string InfoPrefix
    {
        get
        {
            return "[INFO] " + Time;
        }
    }

    public static ConsoleColor InfoColor
    {
        get
        {
            return ConsoleColor.Blue;
        }
    }

    public static string DebugPrefix
    {
        get
        {
            return "[DEBUG] " + Time;
        }
    }

    public static ConsoleColor DebugColor
    {
        get
        {
            return ConsoleColor.Green;
        }
    }

    public static string WarnPrefix
    {
        get
        {
            return "[WARN] " + Time;
        }
    }

    public static ConsoleColor WarnColor
    {
        get
        {
            return ConsoleColor.Yellow;
        }
    }

    public static string ErrorPrefix
    {
        get
        {
            return "[ERROR] " + Time;
        }
    }

    public static ConsoleColor ErrorColor
    {
        get
        {
            return ConsoleColor.Red;
        }
    }

    public static void Info(string message)
    {
        ColorOutput(InfoPrefix + message, InfoColor);
    }

    public static void Debug(string message)
    {
        ColorOutput(DebugPrefix + message, DebugColor);
    }

    public static void Warn(string message)
    {
        ColorOutput(WarnPrefix + message, WarnColor);
    }

    public static void Error(string message)
    {
        ColorOutput(ErrorPrefix + message, ErrorColor);
    }

    private static void ColorOutput(string p, ConsoleColor color)
    {
        var conc = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(p);
        Console.ForegroundColor = conc;
    }

    public static void Info(object message)
    {
        Info(message.ToString());
    }

    public static void Debug(object message)
    {
        Debug(message.ToString());
    }

    public static void Warn(object message)
    {
        Warn(message.ToString());
    }

    public static void Error(object message)
    {
        Error(message.ToString());
    }
}