using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Web;
using Lucidity.Utilities.Contracts.Logging;
using Lucidity.Utilities.Logging;


namespace BusinessSafe.Infrastructure.Logging
{
    /// <summary>
    ///   Class for adding to event log.
    /// </summary>
    /// <remarks>
    ///   Author : Paul Davies Date : 10th March 2012
    /// </remarks>
    [LogIgnore]
    public abstract class LogBase : ILog
    {
        /// <summary>
        ///   Adds a record to the log.
        /// </summary>
        /// <param name="level"> The level of the message (0 = Info, 1 = Warning, 2 = Error). </param>
        /// <param name="message"> The message body. </param>
        /// <param name="ex"> The exception raised. </param>
        /// <param name="objects"> objects to seiralise to XML and store. </param>
        /// <returns> Log event Id. </returns>
        public string Add(int level, string message, Exception ex, object objects)
        {
            var logEvent = new LogEvent
            {
                Level = level,
                Message = message,
                Exception = ex,
                Objects = objects,
                Date = DateTime.Now,
                ExecutingMachine = Environment.MachineName
            };

            //if (OperationContext.Current != null
            //    && OperationContext.Current.IncomingMessageHeaders != null
            //    && OperationContext.Current.IncomingMessageHeaders.FindHeader("Username", "Peninsula.Common") > -1
            //    && OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("Username", "Peninsula.Common") != null)
            //{
            //    logEvent.User = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("Username", "Peninsula.Common");
            //}
            if (HttpContext.Current != null
                && HttpContext.Current.User != null
                    && HttpContext.Current.User.Identity.Name != null)
            {
                logEvent.User = HttpContext.Current.User.Identity.Name;
            }
            else if (WindowsIdentity.GetCurrent() != null)
            {
                logEvent.User = WindowsIdentity.GetCurrent().Name;
            }

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items["ContextGuid"] == null)
                {
                    HttpContext.Current.Items["ContextGuid"] = Guid.NewGuid();
                }

                logEvent.ContextGuid = (Guid)HttpContext.Current.Items["ContextGuid"];
            }
            //else if (OperationContext.Current != null)
            //{
            //    var contextGuidExtension =
            //        OperationContext.Current.Extensions.Find<ContextGuidExtension>();

            //    if (contextGuidExtension == null)
            //    {
            //        contextGuidExtension = new ContextGuidExtension {ContextGuid = Guid.NewGuid()};
            //        OperationContext.Current.Extensions.Add(contextGuidExtension);
            //    }

            //    logEvent.ContextGuid = contextGuidExtension.ContextGuid;
            //}

            var stackTrace = new StackTrace();
            var stackFrames = stackTrace.GetFrames();

            foreach (StackFrame stackFrame in stackFrames)
            {
                if (stackFrame.GetMethod().DeclaringType != null)
                {
                    var declaringType = stackFrame.GetMethod().DeclaringType;
                    if (declaringType != null)
                    {
                        var logIgnoreAttribute = (LogIgnoreAttribute)
                            Attribute.GetCustomAttribute(declaringType, typeof (LogIgnoreAttribute));

                        if (logIgnoreAttribute == null)
                        {
                            logEvent.CallingMethod = stackFrame.GetMethod().Name;
                            logEvent.CallingClass = declaringType.Name;
                            logEvent.CallingAssembly = declaringType.Assembly.GetName().Name;
                            break;
                        }
                    }
                }
            }

            return WriteEvent(logEvent);
        }

        /// <summary>
        ///   Adds a record to the log.
        /// </summary>
        /// <param name="level"> The level of the message (0 = Info, 1 = Warning, 2 = Error). </param>
        /// <param name="message"> The message body. </param>
        /// <returns> Log event Id. </returns>
        public string Add(int level, string message)
        {
            return Add(level, message, null, null);
        }

        /// <summary>
        ///   Adds a record to the log.
        /// </summary>
        /// <param name="level"> The level of the message (0 = Info, 1 = Warning, 2 = Error). </param>
        /// <param name="message"> The message body. </param>
        /// <param name="ex"> The exception raised. </param>
        /// <returns> Log event Id. </returns>
        public string Add(int level, string message, Exception ex)
        {
            return Add(level, message, ex, null);
        }

        /// <summary>
        ///   Adds a record to the log.
        /// </summary>
        /// <param name="level"> The level of the message (0 = Info, 1 = Warning, 2 = Error). </param>
        /// <param name="message"> The message body. </param>
        /// <param name="objects"> objects to seiralise to XML and store. </param>
        /// <returns> Log event Id. </returns>
        public string Add(int level, string message, object objects)
        {
            return Add(level, message, null, objects);
        }

        /// <summary>
        ///   Adds an exception record to the log.
        /// </summary>
        /// <param name="ex"> The exception raised. </param>
        /// <returns> Log event Id. </returns>
        public string Add(Exception ex)
        {
            return Add(2, ex.Message, ex, null);
        }

        /// <summary>
        ///   Adds an info record to the log.
        /// </summary>
        /// <param name="message"> The message body. </param>
        /// <returns> Log event Id. </returns>
        public string Add(string message)
        {
            return Add(0, message, null, null);
        }

        /// <summary>
        ///   Adds an info record to the log.
        /// </summary>
        /// <param name="message"> The message body. </param>
        /// <param name="objects"> objects to seiralise to XML and store. </param>
        /// <returns> Log event Id. </returns>
        public string Add(string message, object objects)
        {
            return Add(0, message, null, objects);
        }

        /// <summary>
        ///   Adds an info record to the log.
        /// </summary>
        /// <param name="objects"> objects to serialise to XML and store. </param>
        /// <returns> Log event Id. </returns>
        public string Add(object objects)
        {
            return Add(0, null, null, objects);
        }

        /// <summary>
        ///   Adds an info record to the log.
        /// </summary>
        /// <returns> Log event Id. </returns>
        public string Add()
        {
            return Add(0, null, null, null);
        }

        protected abstract string WriteEvent(LogEvent logEvent);
    }
}