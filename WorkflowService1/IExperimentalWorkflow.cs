using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WorkflowService1
{
    [ServiceContract(Name = "IExperimentalWorkflow")]
    public interface IExperimentalWorkflow
    {
        [OperationContract(Name = "Receive")]
        int Receive(int requestVar);

    }
}
