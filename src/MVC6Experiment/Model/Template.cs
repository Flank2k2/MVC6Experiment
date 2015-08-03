using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6Experiment.Model
{
    public enum Permission
    {
        UNKNOWN = 0,
        GLOBAL = 1,
        USER = 2,
    }

    public class Template
    {
        //METADATA
        public String Id { get; set; }
        public String Author { get; set; }
        public DateTime CreationTime { get; set; }
        public Permission Permission { get; set; }

        //DATA
        public String Title { get; set; }
        public Section Header { get; set; }
        public Section ChangeDescription { get; set; }
        public Section Monitoring { get; set; }

        public static Template DEFAULT
        {
            get
            {
                return new Template()
                {
                    Title = "New Template",
                    Header = new Section()
                    {
                        Title = "Client Information",
                        Fields = new List<Field>()
                        {
                            new Field(){
                                DisplayName =  "Client(s)",
                                Name = "head_client",
                                Value ="",
                            },
                            new Field(){
                                DisplayName =  "Server(s)",
                                Name = "head_server",
                                Value ="",
                            },
                            new Field(){
                                DisplayName =  "Proposed Date",
                                Name = "head_proposeddate",
                                Value =DateTime.Now.ToString("yyyy.MM.dd"),
                            },
                            new Field(){
                                DisplayName =  "Start Time",
                                Name = "head_starttime",
                                Value ="23:00",
                            },
                            new Field(){
                                DisplayName =  "Requestor",
                                Name = "head_requestor",
                                Value ="",
                            }                           ,
                            new Field(){
                                DisplayName =  "Email",
                                Name = "head_email",
                                Value ="",
                            }                           ,
                            new Field(){
                                DisplayName =  "Type",
                                Name = "head_type",
                                Value ="",
                            }                           ,
                            new Field(){
                                DisplayName =  "Severity",
                                Name = "head_severity",
                                Value ="",
                            }                           ,
                            new Field(){
                                DisplayName =  "Change reason",
                                Name = "head_changereason",
                                Value ="",
                            }                           ,
                        }
                    },
                    ChangeDescription = new Section()
                    {
                        Title = "Change Description",
                        Fields = new List<Field>()
                        {
                            new Field(){
                                DisplayName =  "Summary of change",
                                Name = "txt_summary",
                                Value ="",
                                },
                            new Field()
                            {
                                DisplayName = "Package location",
                                Name = "txt_packagelocation",
                                Value = "",
                            },
                            new Field()
                            {
                                DisplayName = "Deployment Instructions",
                                Name = "txt_instructions",
                                Value = "",
                            }                        ,
                            new Field()
                            {
                                DisplayName = "Confirmation Procedure",
                                Name = "txt_confirmationprocedure",
                                Value = "",
                            }                        ,
                            new Field()
                            {
                                DisplayName = "Roll back plan",
                                Name = "txt_rollbackplan",
                                Value = "",
                            }                    
                        }
                    },
                    Monitoring = new Section()
                    {
                        Title = "Monitoring",
                        Fields = new List<Field>()
                        {
                            new Field() {
                                DisplayName = "Responsible parties",
                                Name = "txt_responsibleparties",
                                Value = "",
                            }                        ,
                            new Field()
                            {
                                DisplayName = "Standard monitoring",
                                Name = "txt_standardmonitoring",
                                Value = "",
                            },
                            new Field()
                            {
                                DisplayName = "Specific monitoring",
                                Name = "txt_specificmonitoring",
                                Value = "",
                            },
                            new Field()
                            {
                                DisplayName = "Escalation plan",
                                Name = "txt_escalationplan",
                                Value = "",
                            }
                        }
                    }
                };
            }
        }
    }
}