using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAdminCommonLib;
using Hik.Communication.ScsServices.Service;

namespace DataAdmin.DataNetLogger
{
    public class DataNetLogService : ScsService, IDataNetLogService
    {


        #region EVENTS

        public delegate void RaiseStartedOperationLog(object sender, MessageFactory.LogMessage msg);
        public delegate void RaiseFinishedOperationLog(object sender, MessageFactory.LogMessage msg);
        public delegate void RaiseAbortedOperationLog(object sender, MessageFactory.LogMessage msg);

        public delegate void RaiseSimpleLog(object sender, MessageFactory.LogMessage msg);

        public event RaiseStartedOperationLog startedOperation;
        public event RaiseFinishedOperationLog finishedOperation;
        public event RaiseAbortedOperationLog abortedOperation;
        public event RaiseSimpleLog simpleMessage;
        #endregion

        public DataNetLogService()
        {
            
        }
        public void SendStartedOperationLog(MessageFactory.LogMessage msg)
        {
            if (startedOperation != null)
                startedOperation(this, msg);
        }

        public void SendFinishedOperationLog(MessageFactory.LogMessage msg)
        {
            if (msg.OperationStatus == MessageFactory.LogMessage.Status.Finished)
            {
                if (finishedOperation != null)
                    finishedOperation(this, msg);
            }
            else
            {
                if (abortedOperation != null)
                    abortedOperation(this, msg);
            }
        }

        public void SendSimpleLog(MessageFactory.LogMessage msg)
        {
            if (simpleMessage != null)
                simpleMessage(this, msg);
        }
    }
}
