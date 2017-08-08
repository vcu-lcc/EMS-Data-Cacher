using System;
using System.Collections.Generic;
using XML;
using Soap;
using EducationalInstitution;
using Data;
using System.Threading;
using System.ServiceProcess;
using System.IO;
using static Persistence;
using System.Diagnostics;

namespace EMS_Cacher
{
    class EMSCacher
    {
        private static int maxConsecutiveTimeouts = int.MaxValue;
        private static int consecutiveTimeouts = 0;
        private static int maxAuxillaryErrors = int.MaxValue;
        private static int auxillaryErrors = 0;
        private static bool _continue = true;

        private static void executeOperation()
        {
            try
            {
                console.info("Updating all files...");
                console.log("Obtaining Buildings...");
                University university = EmsMapper.mapUniversity();
                Serializable.Object config = new Serializable.Object()
                .set("Universities", new Serializable.Array()
                    .add(university)
                );
                console.info("Successfully compiled Universities.");
                console.log("Applying aliases...");
                AliasHandler handler = new AliasHandler(Persistence.config.getArray("Aliases"));
                handler.applyTransformations(config.getArray("Universities"));
                string xmlConfig = Transformations.toXML(config).outerXML();
                string JSONConfig = Transformations.toJSON(config).ToString();
                saveFile("vcu.xml", xmlConfig);
                saveFile("vcu.json", JSONConfig);
                consecutiveTimeouts = 0;
            }
            catch (ThreadAbortException e)
            {
                console.error(e, "Auxillary thread timeout.");
                if (++consecutiveTimeouts > maxConsecutiveTimeouts)
                {
                    console.fatal(e, "Auxillary thread timed out an exceeding amount of times.");
                    if (++auxillaryErrors > maxAuxillaryErrors)
                    {
                        throw e;
                    }
                }
            }/*
            catch (Exception e)
            {
                console.error(e, "Uncaught Exception on auxilary thread");
                if (++auxillaryErrors > maxAuxillaryErrors)
                {
                    console.fatal(e, "Auxillary thread threw exceptions an unacceptable amount of times.", "Program Halted.");
                    _continue = false;
                }
            }*/
        }
        public static void start(string[] extraPaths)
        {
            try
            {
                console.log("Starting EMSCacher - " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt"));
                string[] defaultPaths =
                {
                    @".\settings.xml"
                };
                string[] paths = new string[extraPaths.Length + defaultPaths.Length];
                defaultPaths.CopyTo(paths, 0);
                extraPaths.CopyTo(paths, defaultPaths.Length);
                Config.load(paths);
                if (config.getBoolean("Enabled") == true) {
                    Config.init();
                    maxConsecutiveTimeouts = (int)config.getNumber("MaxConsecutiveTimeouts");
                    maxAuxillaryErrors = (int)config.getNumber("MaxAuxillaryErrors");
                    while (_continue)
                    {
                        Thread t = new Thread(new ThreadStart(executeOperation));
                        t.Start();
                        if (!t.Join(Config.toTimeSpan(config.getObject("Timeout"))))
                        {
                            console.error(
                                "The maximum specified timeout exceeded!",
                                "Killing thread..."
                            );
                            t.Abort();
                        }
                        if (_continue)
                        {
                            console.info(
                                "Sleeping for " + Config.toTimeSpan(config.getObject("Interval"))
                                    .ToString("d'd 'h'h 'm'm 's's'"),
                                "Next scheduled wakeup at " + (DateTime.Now + Config.toTimeSpan(config.getObject("Interval")))
                                    .ToString("MMMM dd, yyyy hh:mm:ss tt"));
                            Thread.Sleep(Config.toTimeSpan(config.getObject("Interval")));
                        }
                    }
                }
            }
            catch (ThreadAbortException)
            {
                console.info("EMSCacher Halted - " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt"));
            }
        }
        public static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                EmsCachingService.start();
            }
            else
            {
                ServiceBase.Run(new EmsCachingService());
            }
        }
    }
}
