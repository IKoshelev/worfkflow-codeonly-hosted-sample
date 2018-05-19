using System;
using System.Activities;
using System.Activities.Statements;
using System.ServiceModel.Activities;
using System.Xml.Linq;

namespace WorkflowService1
{
    public class ExperimentalWorkflow : Activity
    {
        protected override Func<Activity> Implementation
        {
            get
            {
                return () => {
                    var requestVar = new Variable<int>()
                    {
                        Name = "request"
                    };

                    var receiveActivity = new Receive()
                    {
                        DisplayName = "ExperimentalWorkflow_Receive",
                        OperationName = "Receive",
                        ServiceContractName = (XName)"{http://www.acme.com/}IExperimentalWorkflow",
                        Action = "http://www.acme.com/IExperimentalWorkflow/Receive",
                        CanCreateInstance = true,
                        Content = new ReceiveParametersContent()
                        {
                            Parameters = {
                                        {
                                            "Request",
                                            new OutArgument<int>(requestVar)
                                        }
                                    }
                        }
                    };

                    var sequence = new Sequence()
                    {
                        Variables = {
                            requestVar
                        },
                        Activities = {
                            receiveActivity,
                            new SendReply() {
                                DisplayName = "ExperimentalWorkflow_SendReply",
                                Request = receiveActivity,
                                Content = new SendParametersContent() {
                                    Parameters = {
                                        {
                                            "Response",
                                            new InArgument<int>(requestVar)
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