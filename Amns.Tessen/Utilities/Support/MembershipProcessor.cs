using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Amns.Tessen.Utilities.Support
{
    public sealed class MembershipProcessor
    {
        static object lockObject = string.Empty;
        static bool isCompiling;
        static bool abortRequest;
        static int itemNumber = 0;
        static int itemCount = 0;

        #region Static Properties

        public static bool IsCompiling
        {
            get
            {
                lock (lockObject)
                {
                    return isCompiling;
                }
            }
        }

        public static int ItemNumber
        {
            get
            {
                lock (lockObject)
                {
                    return itemNumber;
                }
            }
        }

        public static int ItemCount
        {
            get
            {
                lock (lockObject)
                {
                    return itemCount;
                }
            }
        }

        public static bool AbortRequest
        {
            get
            {
                lock (lockObject)
                {
                    return abortRequest;
                }
            }
        }

        #endregion

        public static bool Compile()
        {
            lock(lockObject)
            {
                if (!isCompiling)
                {
                    isCompiling = true;
                    abortRequest = false;
                    Thread compilerThread = new Thread(compileTask);
                    compilerThread.Start();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool Abort()
        {
            lock (lockObject)
            {
                if (isCompiling)
                {
                    abortRequest = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static void compileTask()
        {
            // Load Active Members
            // Loop Active Members
            //      * Check available membership
            //      * Renew available membership if allowed
            //      * Issue invoice to rappahanock - link lines on invoices to memberships
            //      * Trigger invoice to autopay if allowed (MemberTypeTemplate & Member)
            //      * Payment due on invoice follows MemberTypeTemplate Due Date
            //      * Auto deactivate membership over due in MemberTypeTemplate
            // Finish & Cleanup
            // Call Rappahanock InvoiceProcessor.Compile() for AutoPayment
        }
    }
}
