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
            }
            catch (ThreadAbortException e)
            {
                console.error(e, "Auxillary thread killed by main thread.");
            }
            catch (Exception e)
            {
                console.error(e, "Uncaught Exception on auxilary thread");
            }
        }
        static void Main(string[] args)
        {
            console.log("Starting EMSCacher on " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt"));
            Config.init(@".\settings.xml");
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
    }
}
