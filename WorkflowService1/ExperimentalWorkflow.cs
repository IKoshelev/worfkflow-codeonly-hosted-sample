using System;
using System.Activities;
using System.Activities.Statements;
using System.ServiceModel.Activities;
using System.Xml.Linq;

namespace WorkflowService1
{
    public class ExperimentalWorkflow : Activity
    {
        const string companyUrl = "http://www.acme.com/";

        protected override Func<Activity> Implementation
        {
            get
            {
                return () => 
                {
                    var requestVar = new Variable<int>()
                    {
                        Name = "request"
                    };

                    var receiveActivity = new Receive()
                    {
                        DisplayName = $"{nameof(IExperimentalWorkflow)}_{nameof(IExperimentalWorkflow.Square)}",
                        OperationName = "Square",
                        ServiceContractName = (XName)$"{{{companyUrl}}}{nameof(IExperimentalWorkflow)}",
                        Action = $"{companyUrl}{nameof(IExperimentalWorkflow)}/{nameof(IExperimentalWorkflow.Square)}",
                        CanCreateInstance = true,
                        Content = new ReceiveParametersContent()
                        {
                            Parameters =
                            {
                                {
                                    "Request", new OutArgument<int>(requestVar)
                                }
                            }
                        }
                    };

                    var sequence = new Sequence()
                    {
                        Variables =
                        {
                            requestVar
                        },
                        Activities =
                        {
                            receiveActivity,
                            new Assign<int>
                            {
                                To = new OutArgument<int>((env) => requestVar.Get(env)),
                                Value = new InArgument<int>((env) => requestVar.Get(env) * requestVar.Get(env))
                            },
                            new SendReply()
                            {
                                DisplayName =  $"{nameof(IExperimentalWorkflow)}_SendReply",
                                Request = receiveActivity,
                                Content = new SendParametersContent()
                                {
                                    Parameters =
                                    {
                                        {
                                            "Response", new InArgument<int>(requestVar)
                                        }
                                    }
                                }
                            }
                        }
                    };

                    return sequence;

                };
            }
            set
            {
                base.Implementation = value;
            }
        }
    }
}