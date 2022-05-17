using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.IO;

namespace TNSWREISAPI.Model
{
    public class ManagePDFGeneration
    {
        Font NormalFont = FontFactory.GetFont("Courier New", 8, Font.NORMAL, BaseColor.Black);
        Font FSSAIFont = FontFactory.GetFont("Courier New", 10, Font.NORMAL, BaseColor.Black);
        #region
        public Tuple<bool, string> GeneratePDF(StudentEntity entity = null)
        {
            string fPath = string.Empty, subF_Path = string.Empty, fileName = string.Empty, filePath = string.Empty;
            try
            {
                fileName = entity.AadharNo + "_" + entity.StudentId;
                fPath = GlobalVariable.FolderPath + "Reports";
                CreateFolderIfnotExists(fPath); // create a new folder if not exists
                subF_Path = fPath + "//" + entity.HostelID; //ManageReport.GetDateForFolder();
                CreateFolderIfnotExists(subF_Path);
                //delete file if exists
                filePath = subF_Path + "//" + fileName + ".pdf";
                DeleteFileIfExists(filePath);

                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 5f, 5f, 5f, 5f);
                //Create PDF Table  
                FileStream fs = new FileStream(filePath, FileMode.Create);
                //Create a PDF file in specific path  
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                // Add meta information to the document  
                document.AddAuthor("Dulasiayya from BonTon");
                document.AddCreator("Acknolowdgement for particuar document");
                document.AddKeywords("TNCSC- Webcopy ");
                document.AddSubject("Document subject - Ack Web Copy ");
                document.AddTitle("The document title - PDF creation for Receipt Document");

                //Open the PDF document  
                document.Open();
                string imagePath = GlobalVariable.FolderPath + "layout\\images\\dashboard\\tncsc-logo.PNG";
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                img.Alignment = Element.ALIGN_CENTER;
                img.ScaleToFit(80f, 60f);

                //imagePath = GlobalVariable.FolderPath + "layout\\images\\dashboard\\watermark.PNG";
                //iTextSharp.text.Image imgWaterMark = iTextSharp.text.Image.GetInstance(imagePath);
                //imgWaterMark.ScaleToFit(300, 450);
                //imgWaterMark.Alignment = iTextSharp.text.Image.UNDERLYING;
                //imgWaterMark.SetAbsolutePosition(120, 450);
                //document.Add(imgWaterMark);
                //|----------------------------------------------------------------------------------------------------------|
                //Create the table 
                PdfPTable table = new PdfPTable(2);
                table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                //table.setBorder(Border.NO_BORDER);
                //set overall width
                table.WidthPercentage = 100f;
                //set column widths
                int[] firstTablecellwidth = { 20, 80 };
                table.SetWidths(firstTablecellwidth);
                //iTextSharp.text.Font fontTable = FontFactory.GetFont("Arial", "16", iTextSharp.text.Font.NORMAL);
                PdfPCell cell = new PdfPCell(img);
                cell.Rowspan = 3;
                cell.BorderWidth = 0;
                // cell.Border = (Border.NO_BORDER);
                table.AddCell(cell);
                PdfPCell cell1 = new PdfPCell(new Phrase("TAMILNADU ADI DRAVIDAR WELFARE DEPARTMENT"));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);

                document.Add(table);
                Paragraph heading = new Paragraph("           STUDENT REGISTRATION ACKNOWLEDGMENT         WEB COPY");
                heading.Alignment = Element.ALIGN_CENTER;
                document.Add(heading);
                AddSpace(document);
                AddHRLine(document);
                //add header values
                AddheaderValues(document, entity);
                AddSpace(document);
                
                //Add border to page
                PdfContentByte content = writer.DirectContent;
                Rectangle rectangle = new Rectangle(document.PageSize);
                rectangle.Left += document.LeftMargin;
                rectangle.Right -= document.RightMargin;
                rectangle.Top -= document.TopMargin;
                rectangle.Bottom += document.BottomMargin;
                content.SetColorStroke(BaseColor.Black);
                content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
                content.Stroke();
                // Close the document  
                document.Close();
                // Close the writer instance  
                writer.Close();
                // Always close open filehandles explicity  
                fs.Close();

            }
            catch (Exception ex)
            {
                AuditLog.WriteError(" GeneratePDF :  " + ex.Message + " " + ex.StackTrace);
                return new Tuple<bool, string>(false, "Please Contact system Admin");
            }
            return new Tuple<bool, string>(true, "Print Generated Successfully");
        }
        public void AddHRLine(iTextSharp.text.Document doc)
        {
            LineSeparator line = new LineSeparator(1f, 100f, BaseColor.Black, Element.ALIGN_LEFT, 1);
            doc.Add(line);
        }

        public void AddSpace(iTextSharp.text.Document doc)
        {
            Paragraph heading = new Paragraph("");
            heading.Alignment = Element.ALIGN_CENTER;
            heading.SpacingAfter = 7f;
            doc.Add(heading);
        }

        public void AddheaderValues(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("Student Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.StudentName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Emis No:", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("DATE:", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            //cell = new PdfPCell(new Phrase(report.FormatDate(stockReceipt.OrderDate.ToString()), NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.BorderWidth = 0;
            //table.AddCell(cell);

            //cell = new PdfPCell(new Phrase("GATE PASS:", NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.BorderWidth = 0;
            //table.AddCell(cell);

            //cell = new PdfPCell(new Phrase("", NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.BorderWidth = 0;
            //table.AddCell(cell);

            //cell = new PdfPCell(new Phrase("PERIOD OF ALLOTMENT:", NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.BorderWidth = 0;
            //table.AddCell(cell);

            //cell = new PdfPCell(new Phrase(report.FormatDate(stockReceipt.SRDate.ToString()), NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.BorderWidth = 0;
            //table.AddCell(cell);

            //cell = new PdfPCell(new Phrase("Transaction Type:", NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.BorderWidth = 0;
            //table.AddCell(cell);

            //cell = new PdfPCell(new Phrase(stockReceipt.TransactionName, NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.BorderWidth = 0;
            //cell.Colspan = 3;
            //table.AddCell(cell);

            //cell = new PdfPCell(new Phrase("RECEIVING GODOWN:", NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.BorderWidth = 0;
            //table.AddCell(cell);

            //cell = new PdfPCell(new Phrase(stockReceipt.GodownName, NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.BorderWidth = 0;
            //table.AddCell(cell);

            //cell = new PdfPCell(new Phrase("DEPOSITOR'S NAME:", NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.BorderWidth = 0;
            //table.AddCell(cell);

            //cell = new PdfPCell(new Phrase(stockReceipt.DepositorName, NormalFont));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.BorderWidth = 0;
            //cell.Colspan = 3;
            //table.AddCell(cell);

            doc.Add(table);

        }

        #endregion
        private Tuple<bool, string> GetImageName(string GCode)
        {
            try
            {
                DataSet ds = new DataSet();
                ManageSQLConnection manageSQLConnection = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@GodownCode", GCode));
                ds = manageSQLConnection.GetDataSetValues("GetGodownProfile", sqlParameters);
                if (CheckDataAvailable(ds))
                {
                    return new Tuple<bool, string>(true, Convert.ToString(ds.Tables[0].Rows[0]["ImageName"]));
                }
                return new Tuple<bool, string>(false, "");

            }
            catch (Exception ex)
            {
                AuditLog.WriteError("GetImageName : " + ex.Message);
                return new Tuple<bool, string>(false, "");

            }
        }
        /// <summary>
        /// Change the decimal format for given values.
        /// </summary>
        /// <param name="sValues"></param>
        /// <returns></returns>
        public string DecimalformatForWeight(string sValues)
        {
            string sFormattedValue = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(sValues))
                {
                    if (sValues.IndexOf(".") > 0)
                    {
                        string[] split = sValues.Split('.');
                        int length = Convert.ToString(split[1]).Length;
                        if (length == 1)
                        {
                            sFormattedValue = sValues + "00";
                        }
                        else if (length == 2)
                        {
                            sFormattedValue = sValues + "0";
                        }
                        else
                        {
                            sFormattedValue = sValues;
                        }
                    }
                    else
                    {
                        sFormattedValue = sValues + ".000";
                    }
                }
                else
                {
                    sFormattedValue = "0.000";
                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(" DecimalformatForWeight : " + ex.Message);
            }
            return sFormattedValue;
        }

        /// <summary>
        /// Create a new folder 
        /// </summary>
        /// <param name="Path">Folder path</param>
        public void CreateFolderIfnotExists(string Path)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

        /// <summary>
        /// Check the Data availability
        /// </summary>
        /// <param name="ds">dataset value</param>
        /// <returns></returns>
        public bool CheckDataAvailable(DataSet ds)
        {
            bool isAvailable = false;

            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        isAvailable = true;
                    }
                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError("CheckData : " + ex.Message + " : " + ex.StackTrace);
            }
            return isAvailable;
        }

        /// <summary>
        /// Check the Data availability
        /// </summary>
        /// <param name="dt">Data Table</param>
        /// <returns></returns>
        public bool CheckDataAvailable(DataTable dt)
        {
            bool isAvailable = false;

            try
            {
                if (dt.Rows.Count > 0)
                {
                    isAvailable = true;
                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError("CheckData : " + ex.Message + " : " + ex.StackTrace);
            }
            return isAvailable;
        }
        /// <summary>
        /// Delete file if exists.
        /// </summary>
        /// <param name="Path"></param>
        public void DeleteFileIfExists(string Path)
        {
            try
            {
                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError("DeleteFileIfExists " + ex.Message);
            }

        }
    }
}
