using Microsoft.Framework.Configuration;
using MVC6Experiment.Model;
using Novacode;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6Experiment.Service
{
    public class DocumentService
    {
        private IConfiguration _configuration;
        public DocumentService(IConfiguration conf) //Heyyy where is my DI here !?!?!
        {
            _configuration = conf;
        }

        private void Fill_Header(Header header)
        {
            var headerText = _configuration.Get("Template:Header");

            var paragraph = header.InsertParagraph();
            paragraph.Append(headerText)
                 .Color(Color.FromArgb(31, 73, 125))
                 .FontSize(20)
                 .Bold()
                 .Font(new FontFamily("Verdana"));
            paragraph.Alignment = Alignment.center;
        }

        private void Fill_Footer(Footer footer)
        {
            var pr_date = footer.InsertParagraph();
            pr_date.Append("Created: " + DateTime.Now.ToLongDateString());

            var footPara = footer.InsertParagraph();
            footPara.Alignment = Alignment.right;
            footPara.AppendPageNumber(PageNumberFormat.normal);
            footPara.Append("/").AppendPageCount(PageNumberFormat.normal);
        }

        public Stream GenerateTemplate(Template template)
        {
            var memory_stream = new MemoryStream();
            using (DocX document = DocX.Create(memory_stream))
            {
                document.AddHeaders();
                document.AddFooters();

                Fill_Header(document.Headers.first);

                Fill_Footer(document.Footers.first);
                Fill_Footer(document.Footers.even);
                Fill_Footer(document.Footers.odd);

                Paragraph title = document.InsertParagraph();
                title.Append(template.Title)
                    .Color(Color.FromArgb(31, 73, 125))
                    .Bold()
                    .FontSize(36);

                document.InsertParagraph(" ");
                document.InsertParagraph(" ");

                /* HEADER TABLE
                ******************/

                var header_row_count = (int)Math.Ceiling(template.Header.Fields.Count / 2.0);
                Table header_table = document.InsertTable(header_row_count + 1,4 ); //Account for header row

                header_table.Design = TableDesign.LightListAccent1;
                header_table.AutoFit = AutoFit.ColumnWidth;

                //Add header:
                header_table.Rows[0].MergeCells(1, 3);
                header_table.Rows[0].Cells[0].Paragraphs[0].Append("Division:");
                header_table.Rows[0].Cells[0].FillColor = Color.FromArgb(31, 73, 125);
                //Format: <display name A><value A><display name B><value B>
                var rowIndex = 1;
                for (int i = 0; i < template.Header.Fields.Count; i += 2)
                {
                    //First collumn     <display name><value>
                    var field1 = template.Header.Fields[i];
                    header_table.Rows[rowIndex].Cells[0].Paragraphs[0]
                        .Append(field1.DisplayName + ":")
                        .Bold();

                    header_table.Rows[rowIndex].Cells[1].Paragraphs[0]
                        .Append(field1.Value);

                    //Second collumn    <display name><value>
                    if ((i + 1) < template.Header.Fields.Count)
                    {
                        var field2 = template.Header.Fields[i + 1];
                        var pr = header_table.Rows[rowIndex].Cells[2].Paragraphs[0]
                            .Append(field2.DisplayName + ":")
                            .Bold();

                        header_table.Rows[rowIndex].Cells[3].Paragraphs[0]
                            .Append(field2.Value);
                    }

                    rowIndex++;
                }

                document.InsertParagraph(" ");
                document.InsertParagraph(" ");
                document.InsertParagraph(" ");

                /* CHANGE CONTENT TABLE
                **************************/
                //Not sure how to set collumns width, so creating more collumns than needed then merge them....

                var main_table = document.InsertTable(template.ChangeDescription.Fields.Count + 1, 4);
                main_table.Design = TableDesign.MediumShading2Accent1;

                main_table.Rows[0].MergeCells(0, 3);
                main_table.Rows[0].Cells[0].Paragraphs[0].Append("Change Description (Detailed description of the change. Reference attachments if necessary)");

                for (int i = 0; i < template.ChangeDescription.Fields.Count; i++)
                {
                    main_table.Rows[(i + 1)].MergeCells(1, 3);

                    var field = template.ChangeDescription.Fields[i];
                    main_table.Rows[(i + 1)].Cells[0].Width = 150;
                    main_table.Rows[(i + 1)].Cells[0].Paragraphs[0].Append(field.DisplayName + ":");

                    
                    main_table.Rows[(i + 1)].Cells[1].Width = 600;
                    main_table.Rows[(i + 1)].Cells[1].Paragraphs[0].Append(field.Value);
                }


                document.Save();

                return memory_stream;
            }
        }
    }
}

