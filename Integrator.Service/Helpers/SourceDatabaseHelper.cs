using NLog;
using System;

namespace Integrator.Service.Helpers
{
    public class SourceDatabaseHelper
    {
        private static int incomingCounter;
        private static int outgoingCounter;
        private static int imagesCounter;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public void FirstWorkHelper ()
        {
            try
            {
                incomingCounter++;
                Console.WriteLine("IncomingDocumentsHelper is working   :   " + incomingCounter);
                
            }
            catch (Exception ex)
            {
                logger.Trace(ex, "Error Occured in IncomingDocumentsHelper");
            }

           
        }

        public void SecondWorkHelper ()
        {
            try
            {
                outgoingCounter++;
                Console.WriteLine("OutgoingDocumentsHelper is working   :   " + outgoingCounter);
            }
            catch (Exception ex)
            {

                logger.Trace(ex, "Error Occured in OutgoingDocumentsHelper");
            }
        }

        public void ThirdWorkHelper ()
        {
            try
            {
                imagesCounter++;
                Console.WriteLine("ImageHelper is working   :   " + imagesCounter);
            }
            catch (Exception ex)
            {
                logger.Trace(ex, "Error Occured in ImageHelper");
            }
        }

    }
}
