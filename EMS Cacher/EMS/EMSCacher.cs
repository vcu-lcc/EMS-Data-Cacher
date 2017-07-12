using System;
using System.Collections.Generic;
using XML;
using Soap;
using EducationalInstitution;
using Data;
using System.Threading;
using System.IO;
using static Persistence;

namespace EMS_Cacher
{
    class EMSCacher
    {
        private static int maxConsecutiveTimeouts = int.MaxValue;
        private static int consecutiveTimeouts = 0;
        private static int maxAuxillaryErrors = int.MaxValue;
        private static int auxillaryErrors = 0;

        private static void executeOperation()
        {
            try
            {
                console.info("Updating all files...");
                console.log("Obtaining Buildings...");
                University vcu = EmsMapper.mapUniversity();
                string xmlConfig = Transformations.toXML(vcu).outerXML();
                string JSONConfig = Transformations.toJSON(vcu).ToString();
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
            }
            catch (Exception e)
            {
                console.error(e, "Uncaught Exception on auxilary thread");
                if (++auxillaryErrors > maxAuxillaryErrors)
                {
                    throw e;
                }
            }
        }
        static void Main(string[] args)
        {
            try
            {
                console.log("Starting EMSCacher on " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt"));
                Config.init(@".\settings.xml");
                maxConsecutiveTimeouts = (int)config.getNumber("MaxConsecutiveTimeouts");
                maxAuxillaryErrors = (int)config.getNumber("");
                while (true)
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
                    Thread.Sleep(Config.toTimeSpan(config.getObject("Interval")));
                }
            }
            catch (Exception e)
            {
                console.fatal(e, "Auxillary thread threw exceptions an exceeding amount of times.");
            }
        }
    }
}
