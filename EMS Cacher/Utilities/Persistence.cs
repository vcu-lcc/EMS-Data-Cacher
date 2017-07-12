using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XML;

public class Persistence
{
    public static Serializable.Object config = new Serializable.Object()
        .set("URL", new Serializable.String("http://127.0.0.1/"))
        .set("Username", new Serializable.String())
        .set("Password", new Serializable.String())
        .set("OutputDirectory", new Serializable.String(Path.GetTempPath()))
        .set("MaxConsecutiveTimeouts", new Serializable.Number(10))
        .set("MaxAuxillaryErrors", new Serializable.Number(3))
        .set("Interval", new Serializable.Object()
            .set("Seconds", new Serializable.Number(0))
            .set("Minutes", new Serializable.Number(0))
            .set("Hours", new Serializable.Number(1))
            .set("Days", new Serializable.Number(0))
            .set("Weeks", new Serializable.Number(0))
            .set("Month", new Serializable.Number(0))
            .set("Years", new Serializable.Number(0))
        )
        .set("Timeout", new Serializable.Object()
            /*
             * @FIXME: Collision with nested object variables
            */
            .set("Seconds", new Serializable.Number(0))
            .set("Minutes", new Serializable.Number(5))
            .set("Hours", new Serializable.Number(0))
            .set("Days", new Serializable.Number(0))
            .set("Weeks", new Serializable.Number(0))
            .set("Month", new Serializable.Number(0))
            .set("Years", new Serializable.Number(0))
        )
        .set("University", new Serializable.Object()
            .set("Name", new Serializable.String(""))
            .set("Acronym", new Serializable.String(""))
            .set("Department", new Serializable.String(""))
        )
        .set("LogLevel", new Serializable.String("Log"))
        .set("Edit", new Serializable.Object());

    public class Config
    {
        public static void init(params string[] filePaths)
        {
            foreach (string filePath in filePaths)
            {
                string fileContents = File.ReadAllText(filePath);
                if (filePath.ToLower().EndsWith(".xml"))
                {
                    XMLDocument xml = XMLDocument.inflate(fileContents);
                    Serializable.Object obj = (Serializable.Object)Transformations.fromXML(xml);
                    config.apply(obj);
                    console.info("Loaded " + filePath);
                }
                else if (filePath.ToLower().EndsWith(".json"))
                {
                    console.info("Loaded " + filePath);
                    console.error("JSON functionality not implemented yet...");
                }
                else
                {
                    console.warn(
                        "Unable to determine MIME type of " + filePath + '.',
                        "File not loaded."
                    );
                }
            }
            console.init();
        }
        public static TimeSpan toTimeSpan(Serializable.Object timeObj)
        {
            TimeSpan span = new TimeSpan();
            if (timeObj == null)
            {
                return span;
            }
            try
            {
                if (timeObj.getType("Years") == "number")
                {
                    span += TimeSpan.FromDays(365 * timeObj.getNumber("Years"));
                }
                if (timeObj.getType("Months") == "number")
                {
                    span += TimeSpan.FromDays(30 * timeObj.getNumber("Months"));
                }
                if (timeObj.getType("Weeks") == "number")
                {
                    span += TimeSpan.FromDays(7 * timeObj.getNumber("Weeks"));
                }
                if (timeObj.getType("Days") == "number")
                {
                    span += TimeSpan.FromDays(timeObj.getNumber("Days"));
                }
                if (timeObj.getType("Hours") == "number")
                {
                    span += TimeSpan.FromHours(timeObj.getNumber("Hours"));
                }
                if (timeObj.getType("Minutes") == "number")
                {
                    span += TimeSpan.FromMinutes(timeObj.getNumber("Minutes"));
                }
                if (timeObj.getType("Seconds") == "number")
                {
                    span += TimeSpan.FromSeconds(timeObj.getNumber("Seconds"));
                }
            }
            catch (OverflowException e)
            {
                console.error(e, "The specified time resulted in an overflow.");
                return TimeSpan.MaxValue;
            }
            return span;
        }
    }
    public class console
    {
        private static int logLevel = int.MaxValue;
        private static Dictionary<string, List<string>> pendingMessages = new Dictionary<string, List<string>>()
        {
            {"verbose", new List<string>()},
            {"log", new List<string>()},
            {"info", new List<string>()},
            {"warn", new List<string>()},
            {"error", new List<string>()},
            {"fatal", new List<string>()}
        };
        private static int getLogLevel()
        {
            switch (config.getString("LogLevel"))
            {
                case "None":
                    return 0;
                case "Fatal":
                    return 1;
                case "Error":
                    return 2;
                case "Warn":
                    return 3;
                case "Info":
                    return 4;
                case "Log":
                    return 5;
                case "Verbose":
                    return 6;
                case "All":
                    return 7;
                default:
                    {
                        console.error("Unknown LogLevel " + config.getString("LogLevel"));
                        return 7;
                    }
            }
        }
        public static void init()
        {
            if (pendingMessages == null)
            {
                console.warn("Subsequent console.init() calls are not respected.");
            }
            else
            {
                console.clear();
                logLevel = getLogLevel();
                Dictionary<string, List<string>> currentMessages = pendingMessages;
                pendingMessages = null;
                foreach (var pending in currentMessages)
                {
                    switch (pending.Key)
                    {
                        case "verbose":
                            {
                                console.verbose(pending.Value.ToArray());
                                break;
                            }
                        case "log":
                            {
                                console.log(pending.Value.ToArray());
                                break;
                            }
                        case "info":
                            {
                                console.info(pending.Value.ToArray());
                                break;
                            }
                        case "warn":
                            {
                                console.warn(pending.Value.ToArray());
                                break;
                            }
                        case "error":
                            {
                                console.error(pending.Value.ToArray());
                                break;
                            }
                        case "fatal":
                            {
                                console.fatal(pending.Value.ToArray());
                                break;
                            }
                    }
                }
            }
        }
        private static void _log(string[] messages, char level)
        {
            for (int i = 0; i != messages.Length; i++)
            {
                messages[i] = level + "| " + string.Join(
                    Environment.NewLine + level + "| ",
                    messages[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                );
            }
            File.AppendAllLines(config.getString("OutputDirectory") + @"\log.txt", messages);
        }
        public static void verbose(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["verbose"].AddRange(messages);
                return;
            }
            if (logLevel > 5)
                _log(messages, 'V');
        }
        public static void log(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["log"].AddRange(messages);
                return;
            }
            if (logLevel > 4)
                _log(messages, 'L');
        }
        public static void info(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["info"].AddRange(messages);
                return;
            }
            if (logLevel > 3)
                _log(messages, 'I');
        }
        public static void warn(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["warn"].AddRange(messages);
                return;
            }
            if (logLevel > 2)
                _log(messages, 'W');
        }
        public static void error(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["error"].AddRange(messages);
                return;
            }
            if (logLevel > 1)
                _log(messages, 'E');
        }
        public static void error(Exception e, params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["error"].AddRange(messages);
                console.error(
                    "Details: " + e.Message,
                    "Stack Trace: ", e.StackTrace
                );
                return;
            }
            if (logLevel > 1)
            {
                _log(messages, 'E');
                console.error(
                    "Details: " + e.Message,
                    "Stack Trace: ", e.StackTrace
                );
            }
        }
        public static void fatal(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["fatal"].AddRange(messages);
                return;
            }
            if (logLevel > 0)
                _log(messages, 'F');
        }
        public static void fatal(Exception e, params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["fatal"].AddRange(messages);
                console.fatal(
                    "Details: " + e.Message,
                    "Stack Trace: ", e.StackTrace
                );
                return;
            }
            if (logLevel > 0)
            {
                _log(messages, 'F');
                console.fatal(
                    "Details: " + e.Message,
                    "Stack Trace: ", e.StackTrace
                );
            }
        }
        public static void clear()
        {
            File.Delete(config.getString("OutputDirectory") + @"\log.txt");
        }
    }
    public static void saveFile(string fileName, string data)
    {
        File.WriteAllText(config.getString("OutputDirectory") + '\\' + fileName, data);
        console.info("Written to: " + config.getString("OutputDirectory") + '\\' + fileName);
    }
}
