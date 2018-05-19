using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WorkflowService1
{
    [ServiceContract(Name = nameof(IExperimentalWorkflow))]
    public interface IExperimentalWorkflow
    {
        [OperationContract(Name = nameof(IExperimentalWorkflow.Square))]
        int Square(int requestVar);

    }
}
