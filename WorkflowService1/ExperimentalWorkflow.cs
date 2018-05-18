using System.Activities;
using System.Activities.Statements;
using System.ServiceModel.Activities;
using System.Xml.Linq;

namespace WorkflowService1
{
    public class ExperimentalWorkflow : WorkflowService
    {
        public ExperimentalWorkflow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ConfigurationName = "ExperimentalWorkflow";

            var requestVar = new Variable<int>() { Name = "request" };

            this.Body = new Sequence()
            {
                Variables =
                {
                  requestVar
                },
                Activities =
                {
                  new Receive()
                  {
                    DisplayName = "ExperimentalWorkflow_Receive",
                    OperationName = "Receive",
                    ServiceContractName =
                      (XName)
                      "{http://www.acme.com/}IExperimentalWorkflow",
                    Action =
                      "http://www.acme.com/IExperimentalWorkflow/Receive",
                    CanCreateInstance = true,
                    Content = new ReceiveParametersContent()
                    {
                      Parameters =
                      {
                        { "Request", new OutArgument<int>(requestVar) }
                      }
                    }
                  },
                  new SendReply()
                  {
                    DisplayName = "ExperimentalWorkflow_SendReply",
                    Content = new SendParametersContent()
                    {
                        Parameters =
                        {
                            { "Response", new InArgument<int>(requestVar) }
                        }
                    }
                  }
                }
            };
        }
    }
}