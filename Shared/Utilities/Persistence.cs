using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XML;
using Templates;

public class Persistence
{
    public static Serializable.Object config = null;
    public static Serializable.Object configDetails = new Serializable.Object()
        .set("Description", "Configure settings for the EMS Cacher.")
        .set("Value", new Serializable.Object()
            .set("Enabled", new Serializable.Object()
                .set("Value", false)
                .set("Description", "Enables or disables caching.")
            )
            .set("URL", new Serializable.Object()
                .set("Value", "http://127.0.0.1")
                .set("Description", "The location of EMS api.")
            )
            .set("Username", new Serializable.Object()
                .set("Value", string.Empty)
                .set("Description", "The EMS API username.")
            )
            .set("Password", new Serializable.Object()
                .set("Value", string.Empty)
                .set("Description", "The EMS API password.")
            )
            .set("OutputDirectory", new Serializable.Object()
                .set("Value", Path.GetTempPath())
                .set("Description", "The location to put logs and generated building structure files.")
            )
            .set("Interval", new Serializable.Object()
                .set("Description", "The amount of time to wait to rebuild building structure files.")
                .set("Value", new Serializable.Object()
                    .set("Seconds", new Serializable.Object()
                        .set("Value", 0)
                    )
                    .set("Minutes", new Serializable.Object()
                        .set("Value", 0)
                    )
                    .set("Hours", new Serializable.Object()
                        .set("Value", 1)
                    )
                    .set("Days", new Serializable.Object()
                        .set("Value", 0)
                    )
                    .set("Weeks", new Serializable.Object()
                        .set("Value", 0)
                    )
                    .set("Month", new Serializable.Object()
                        .set("Value", 0)
                    )
                    .set("Years", new Serializable.Object()
                        .set("Value", 0)
                    )
                )
                .set("AddPrimitive", false)
                .set("AddObject", false)
            )
            .set("Timeout", new Serializable.Object()
                .set("Description", "The maximum amount of time allocated to rebuild building structure files.")
                .set("Value", new Serializable.Object()
                    .set("Seconds", new Serializable.Object()
                        .set("Value", 0)
                    )
                    .set("Minutes", new Serializable.Object()
                        .set("Value", 5)
                    )
                    .set("Hours", new Serializable.Object()
                        .set("Value", 0)
                    )
                    .set("Days", new Serializable.Object()
                        .set("Value", 0)
                    )
                    .set("Weeks", new Serializable.Object()
                        .set("Value", 0)
                    )
                    .set("Month", new Serializable.Object()
                        .set("Value", 0)
                    )
                    .set("Years", new Serializable.Object()
                        .set("Value", 0)
                    )
                )
                .set("AddPrimitive", false)
                .set("AddObject", false)
            )
            .set("MaxConsecutiveTimeouts", new Serializable.Object()
                .set("Value", 5)
                .set("Description", "The maximum amount of timeouts before logging a fatal message and raising an exception.")
            )
            .set("MaxAuxillaryErrors", new Serializable.Object()
                .set("Value", 3)
                .set("Description", "The maximum amount of raised errors before logging a fatal message and quiting the program.")
            )
            .set("University", new Serializable.Object()
                .set("Description", "Details of the university.")
                .set("Value", new Serializable.Object()
                    .set("Name", new Serializable.Object()
                        .set("Value", string.Empty)
                        .set("Description", "The full name of the university.")
                    )
                    .set("Acronym", new Serializable.Object()
                        .set("Value", string.Empty)
                        .set("Description", "An abbreviation of the university.")
                    )
                    .set("Department", new Serializable.Object()
                        .set("Value", string.Empty)
                        .set("Description", "The current department that is providing this service.")
                    )
                )
                .set("AddPrimitive", true)
                .set("AddObject", false)
            )
            .set("LogLevel", new Serializable.Object()
                .set("Description", "Control the information that gets put in EMS cacher's logs.")
                .set("Value", new Serializable.Object()
                    .set("Verbose", new Serializable.Object()
                        .set("Value", false)
                    )
                    .set("Log", new Serializable.Object()
                        .set("Value", true)
                    )
                    .set("Info", new Serializable.Object()
                        .set("Value", true)
                    )
                    .set("Warn", new Serializable.Object()
                        .set("Value", true)
                    )
                    .set("Error", new Serializable.Object()
                        .set("Value", true)
                    )
                    .set("Fatal", new Serializable.Object()
                        .set("Value", true)
                    )
                )
                .set("AddPrimitive", false)
                .set("AddObject", false)
            )
            .set("Aliases", new Serializable.Object()
                .set("Value", new AliasEditor())
                .set("Description", "Configure aliases and edits when writing to disk.")
            )
        )
        .set("AddPrimitive", false)
        .set("AddObject", true);

    public class Config
    {
        public static Serializable.DataType slimify(Serializable.DataType fatConfig)
        {
            if (fatConfig is Serializable.Object && fatConfig.toObject().get("Value") != null)
            {
                Serializable.DataType defaultValue = fatConfig.toObject().get("Value");
                if (defaultValue is Serializable.DataType.Custom)
                {
                    return defaultValue.serialize();
                }
                if (defaultValue is Serializable.Object)
                {
                    Serializable.Object slimObj = new Serializable.Object();
                    foreach (var i in defaultValue.getChildren())
                    {
                        slimObj.set(i.Item1, slimify(i.Item2));
                    }
                    return slimObj;
                }
                else if (defaultValue is Serializable.Array)
                {
                    Serializable.Array slimArr = new Serializable.Array();
                    foreach (var i in defaultValue.getChildren())
                    {
                        slimArr.add(slimify(i.Item2));
                    }
                    return slimArr;
                }
                return defaultValue;
            }
            return fatConfig;
        }
        public static Serializable.DataType fatten(Serializable.DataType obj)
        {
            if (obj is Serializable.DataType.Custom)
            {
                obj = ((Serializable.DataType.Custom)obj).serialize();
            }
            if (obj is Serializable.Object)
            {
                Serializable.Object configObj = (Serializable.Object)obj;
                var children = configObj.getChildren();
                for (int i = children.Count - 1; i >= 0; i--)
                {
                    configObj.set(children[i].Item1, fatten(children[i].Item2));
                }
            }
            else if (obj is Serializable.Array)
            {
                var children = obj.getChildren();
                Serializable.Array arr = (Serializable.Array)obj;
                for (int i = children.Count - 1; i >= 0; i--)
                {
                    arr.add(fatten(children[i].Item2));
                    arr.removeAt(i);
                }
            }
            return new Serializable.Object().set("Value", obj);
        }
        static Config()
        {
            config = slimify(configDetails) as Serializable.Object;
        }
        public static void load(params string[] filePaths)
        {
            foreach (string filePath in filePaths)
            {
                try
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
                catch (IOException)
                {
                    console.warn("Unable to load file " + filePath + '.');
                }
            }
        }
        public static void init()
        {
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
                if (timeObj.get("Years") is Serializable.Number)
                {
                    span += TimeSpan.FromDays(365 * timeObj.getNumber("Years"));
                }
                if (timeObj.get("Months") is Serializable.Number)
                {
                    span += TimeSpan.FromDays(30 * timeObj.getNumber("Months"));
                }
                if (timeObj.get("Weeks") is Serializable.Number)
                {
                    span += TimeSpan.FromDays(7 * timeObj.getNumber("Weeks"));
                }
                if (timeObj.get("Days") is Serializable.Number)
                {
                    span += TimeSpan.FromDays(timeObj.getNumber("Days"));
                }
                if (timeObj.get("Hours") is Serializable.Number)
                {
                    span += TimeSpan.FromHours(timeObj.getNumber("Hours"));
                }
                if (timeObj.get("Minutes") is Serializable.Number)
                {
                    span += TimeSpan.FromMinutes(timeObj.getNumber("Minutes"));
                }
                if (timeObj.get("Seconds") is Serializable.Number)
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
        private static Dictionary<string, List<string>> pendingMessages = new Dictionary<string, List<string>>()
        {
            {"verbose", new List<string>()},
            {"log", new List<string>()},
            {"info", new List<string>()},
            {"warn", new List<string>()},
            {"error", new List<string>()},
            {"fatal", new List<string>()}
        };

        public static void init()
        {
            if (pendingMessages == null)
            {
                console.warn("Subsequent console.init() calls are not respected.");
            }
            else
            {
                console.clear();
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
        public static void verbose(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["verbose"].AddRange(messages);
                return;
            }
            if (config.getObject("LogLevel").getBoolean("Verbose"))
                _log(messages, 'V');
        }
        public static void log(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["log"].AddRange(messages);
                return;
            }
            if (config.getObject("LogLevel").getBoolean("Log"))
                _log(messages, 'L');
        }
        public static void info(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["info"].AddRange(messages);
                return;
            }
            if (config.getObject("LogLevel").getBoolean("Info"))
                _log(messages, 'I');
        }
        public static void warn(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["warn"].AddRange(messages);
                return;
            }
            if (config.getObject("LogLevel").getBoolean("Warn"))
                _log(messages, 'W');
        }
        public static void error(params string[] messages)
        {
            if (pendingMessages != null)
            {
                pendingMessages["error"].AddRange(messages);
                return;
            }
            if (config.getObject("LogLevel").getBoolean("Error"))
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
            if (config.getObject("LogLevel").getBoolean("Error"))
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
            if (config.getObject("LogLevel").getBoolean("Fatal"))
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
            if (config.getObject("LogLevel").getBoolean("Fatal"))
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
    }
    public static void saveFile(string fileName, string data)
    {
        File.WriteAllText(config.getString("OutputDirectory") + '\\' + fileName, data);
        console.info("Written to: " + config.getString("OutputDirectory") + '\\' + fileName);
    }
}
