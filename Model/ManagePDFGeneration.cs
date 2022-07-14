using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using iTextSharp.text;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Drawing.Drawing2D;

namespace TNSWREISAPI.Model
{
    public class ManagePDFGeneration
    {
        iTextSharp.text.Font NormalFont = FontFactory.GetFont("Courier New", 8, iTextSharp.text.Font.NORMAL, BaseColor.Black);
        iTextSharp.text.Font FSSAIFont = FontFactory.GetFont("Courier New", 8, iTextSharp.text.Font.NORMAL, BaseColor.Black);
        iTextSharp.text.Font Header = FontFactory.GetFont("Times New Roman", 8, iTextSharp.text.Font.UNDERLINE, BaseColor.Black);
        iTextSharp.text.Font Smallfont = FontFactory.GetFont("Times New Roman", 6, BaseColor.Black);
        //iTextSharp.text.Font tamilFont = GetFont("TSCu_SaiIndira", "TSCU_SAIINDIRA.TTF");
        //int totalfonts = FontFactory.RegisterDirectory("C://WINDOWS//Fonts");
        //new Font(Font.fon.TIMES_ROMAN, 11f, Font.UNDERLINE, BaseColor.Black);
        // header.SetStyle(Font.UNDERLINE);


        //public static iTextSharp.text.Font GetFont(string fontName, string filename)
        //{
        //    if (!FontFactory.IsRegistered(fontName))
        //    {
        //        var fontPath = "C://USERS//DULASIAYYA_4723//APPDATA//LOCAL//MICROSOFT//WINDOWS//FONTS//"+ filename; // Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\" + filename;
        //        FontFactory.Register(fontPath);
        //    }
        //    iTextSharp.text.Font f=  FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED,8); //,BaseColor.Black);

        //    return f;
        //}

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

                try
                {
                    //Open the PDF document  
                    document.Open();
                    AddSpace(document);
                    string imagePath = GlobalVariable.FolderPath + "images//TN_Logo.PNG";
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                    img.Alignment = Element.ALIGN_CENTER;
                    img.ScaleToFit(60f, 60f);

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
                    cell.Rowspan = 4;
                    cell.BorderWidth = 0;
                    // cell.Border = (Border.NO_BORDER);
                    table.AddCell(cell);
                    AddSpace(document);

                    cell = new PdfPCell(new Phrase("TAMILNADU ADI DRAVIDAR WELFARE DEPARTMENT"));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" "));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    // cell.Rowspan = 1;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" STUDENT ONLINE REGISTRATION ACKNOWLEDGEMENT"));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 0;

                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" "));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 0;

                    table.AddCell(cell);
                    AddSpace(document);
                    document.Add(table);

                    Paragraph heading = new Paragraph("");
                    Paragraph topic = new Paragraph("", Header);
                    //heading.Alignment = Element.ALIGN_CENTER;
                    //document.Add(heading); 
                    AddHRLine(document);
                    AddStudentInfo(document, entity);
                    AddSpace(document);



                    // Text to image convert. 
                    #region Text to image convert
                    //string text = "விடுதி  மாணாக்கர் உறுதிமொழி";
                    //Bitmap bitmap = new Bitmap(1, 1);
                    //System.Drawing.Font font = new System.Drawing.Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Pixel);
                    //Graphics graphics = Graphics.FromImage(bitmap);
                    //int width = (int)graphics.MeasureString(text, font).Width;
                    //int height = (int)graphics.MeasureString(text, font).Height;
                    //bitmap = new Bitmap(bitmap, new Size(width, height));
                    //graphics = Graphics.FromImage(bitmap);
                    //graphics.Clear(Color.White);
                    //graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    //graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    //graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(255, 0, 0)), 0, 0);
                    //graphics.Flush();
                    //graphics.Dispose();
                    //iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(bitmap, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //pdfImage.Alignment = Element.ALIGN_CENTER;
                    //document.Add(pdfImage);
                    #endregion



                    // heading = new Paragraph ("Name of the hostel விடுதி  மாணாக்கர் உறுதிமொழி", tamilFont);
                    heading = new Paragraph(" IDENTIFICATION DETAILS", Header);
                    heading.Alignment = Element.ALIGN_LEFT;
                    //     AddSpace(document);
                    document.Add(heading);

                    AddSpace(document);
                    AddIdentificationDetails(document, entity);
                    AddSpace(document);
                    topic = new Paragraph(" INSTITUTION DETAILS", Header);
                    topic.Alignment = Element.ALIGN_LEFT;
                    //   AddSpace(document);
                    document.Add(topic);
                    AddSpace(document);
                    AddInstituteDetails(document, entity);
                    AddSpace(document);
                    heading = new Paragraph(" HOSTEL DETAILS", Header);
                    heading.Alignment = Element.ALIGN_LEFT;
                    document.Add(heading);
                    AddSpace(document);
                    AddHostelDetails(document, entity);
                    AddSpace(document);
                    heading = new Paragraph(" DISABILITY DETAILS", Header);
                    heading.Alignment = Element.ALIGN_LEFT;
                    document.Add(heading);
                    AddSpace(document);
                    AddDisabilityDetails(document, entity);
                    AddSpace(document);
                    heading = new Paragraph(" DISTANCE DETAILS(in Kms)", Header);
                    heading.Alignment = Element.ALIGN_LEFT;
                    document.Add(heading);
                    AddSpace(document);
                    AddDistanceDetails(document, entity);
                    AddSpace(document);
                    heading = new Paragraph(" LAST STUDIED INSTITUTION DETAILS", Header);
                    heading.Alignment = Element.ALIGN_LEFT;
                    document.Add(heading);
                    AddSpace(document);
                    AddLastStudiedDetails(document, entity);
                    AddSpace(document);
                    heading = new Paragraph(" NATIVE ADDRESS", Header);
                    heading.Alignment = Element.ALIGN_LEFT;
                    document.Add(heading);
                    AddSpace(document);
                    AddAddressDetails(document, entity);
                    AddSpace(document);
                    heading = new Paragraph(" BANK DETAILS", Header);
                    heading.Alignment = Element.ALIGN_LEFT;
                    document.Add(heading);
                    AddSpace(document);
                    AddBankDetails(document, entity);
                    AddSpace(document);
                    heading = new Paragraph(" PARENT'S INFO", Header);
                    heading.Alignment = Element.ALIGN_LEFT;
                    document.Add(heading);
                    AddSpace(document);
                    AddParentDetails(document, entity);
                    AddSpace(document);
                    heading = new Paragraph(" GUARDIAN'S INFO", Header);
                    heading.Alignment = Element.ALIGN_LEFT;
                    document.Add(heading);
                    AddSpace(document);
                    AddGuardianDetails(document, entity);
                    AddSpace(document);
                    heading = new Paragraph(" DOCUMENT UPLOADED DETAILS", Header);
                    heading.Alignment = Element.ALIGN_LEFT;
                    document.Add(heading);
                    AddSpace(document);
                    AddDocumentDetails(document, entity);
                    topic = new Paragraph("Document generated:", Smallfont);
                    topic.Alignment = Element.ALIGN_RIGHT;
                    document.Add(topic);
                    heading = new Paragraph(DateTime.UtcNow.AddHours(5.5).ToString("yyyy-MM-dd THH:mm: ss"), Smallfont);
                    heading.Alignment = Element.ALIGN_RIGHT;
                    document.Add(heading);

                    //Add border to page
                    PdfContentByte content = writer.DirectContent;
                    iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(document.PageSize);
                    rectangle.Left += document.LeftMargin;
                    rectangle.Right -= document.RightMargin;
                    rectangle.Top -= document.TopMargin;
                    rectangle.Bottom += document.BottomMargin;
                    content.SetColorStroke(BaseColor.Black);
                    content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
                    content.Stroke();
                    // Close the document  

                    // Close the writer instance  

                    // Merge the PDF files
                    if (Convert.ToInt32(entity.DistrictApproval) == 1)
                    {
                        AddDeclaration(document, writer);
                    }

                    // Create a footer.

                }
                catch (Exception ex)
                {

                    AuditLog.WriteError(" GeneratePDF :  " + ex.Message + " " + ex.StackTrace);

                }
                finally
                {
                    document.Close();
                    writer.Close();
                    // Always close open filehandles explicity  
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(" GeneratePDF :  " + ex.Message + " " + ex.StackTrace);
                return new Tuple<bool, string>(false, "Please Contact system Admin");
            }
            finally
            {
            }
            return new Tuple<bool, string>(true, "Print Generated Successfully");
        }
        public void AddHRLine(iTextSharp.text.Document doc)
        {
            LineSeparator line = new LineSeparator(1f, 100f, BaseColor.Black, Element.ALIGN_LEFT, 1);
            doc.Add(line);
        }

        public void AddDeclaration(iTextSharp.text.Document document, PdfWriter writer)
        {
            document.NewPage();
            string declarationPath = GlobalVariable.FolderPath + "images//DeclarationForm.pdf";
            PdfReader reader = new PdfReader(declarationPath);
            reader.ConsolidateNamedDestinations();
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                PdfImportedPage page = writer.GetImportedPage(reader, i);
                document.Add(iTextSharp.text.Image.GetInstance(page));
            }
            reader.Close();
        }

        public void AddSpace(iTextSharp.text.Document doc)
        {
            Paragraph heading = new Paragraph("");
            heading.Alignment = Element.ALIGN_CENTER;
            heading.SpacingAfter = 7f;
            doc.Add(heading);
        }

        public void AddStudentInfo(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            Paragraph topic = new Paragraph(" PERSONAL DETAILS", Header);
            topic.Alignment = Element.ALIGN_LEFT;
            doc.Add(topic);
            AddSpace(doc);
            string Path = string.Empty;
            //add header values  no_image.PNG
            if (_studentEntity.StudentFilename == "dulasi")
            {
                Path = GlobalVariable.FolderPath + "images/no_image.PNG";
            }
            else
            {
                try
                {
                    Path = GlobalVariable.FolderPath + _studentEntity.HostelID + "/" + "Documents/" + _studentEntity.StudentFilename;
                }
                catch (Exception ex)
                {
                    Path = GlobalVariable.FolderPath + "images/no_image.PNG";
                    AuditLog.WriteError(ex.Message);
                }
                
            }

            iTextSharp.text.Image imge = iTextSharp.text.Image.GetInstance(Path);
            imge.Alignment = Element.ALIGN_LEFT;
            imge.ScaleToFit(80f, 60f);



            PdfPCell cell = new PdfPCell(new Phrase("Online Application No.", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("உறுதிமொழி" + _studentEntity.StudentId, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            PdfPCell cell1 = new PdfPCell(imge);
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.Rowspan = 5;
            cell1.Colspan = 2;
            cell1.PaddingRight = 2;
            cell1.BorderWidth = 0;
            table.AddCell(cell1);

            cell = new PdfPCell(new Phrase("Student Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.StudentName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            cell.Colspan = 2;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("Admission No.", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.AdmissionNo, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            cell.Colspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date of Birth", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Dob, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 2;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Age", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Age, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            cell.Colspan = 4;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Blood Group", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.BloodgroupName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            //    cell.Colspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Gender", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.GenderName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Caste", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.CasteName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Sub-Caste", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.SubcasteName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Religion", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.ReligionName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);



            cell = new PdfPCell(new Phrase("Mother Tongue", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.MothertongueName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mobile Number", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.MobileNo, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Alternate Mobile Number", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.AltMobNo, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("Emis No:", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Emisno, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Annual Income", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.TotalYearlyIncome, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            doc.Add(table);

        }

        public void AddInstituteDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            table.DefaultCell.Padding = 10;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("School/College Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.InstituteName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Medium", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.MediumName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Class/Course", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Class, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Course Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.CourseTitle, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Year", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.CourseYear, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Scholarship No.", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.ScholarshipId, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);


            doc.Add(table);

        }
        public void AddHostelDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("Hostel District", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.HostelDName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hostel Taluk", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.HostelTName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hostel Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.HostelName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            doc.Add(table);

        }
        public void AddDistanceDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("From Hostel to Home", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.DistanceFromHostelToHome, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("From Hostel to Institute", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.DistanceFromHostelToInstitute, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            doc.Add(table);

        }
        public void AddLastStudiedDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("Institution Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.LastInstitutionName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Institution Address", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.LastInstitutionAddress, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            doc.Add(table);
        }
        public void AddAddressDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("Door No.", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Address1, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Street/Area Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Address2, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Landmark", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Landmark, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Village/Town", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Village, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("District", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Districtname, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Taluk", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Talukname, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);



            cell = new PdfPCell(new Phrase("Pincode", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.Pincode, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            doc.Add(table);

        }

        public void AddBankDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("Bank Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.BankName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Account Number", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.BankAccNo, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("IFSC Code", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.IfscCode, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Branch Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.BranchName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("MICR No.", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.MICRNO, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);
            doc.Add(table);
        }

        public void AddParentDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("Father's Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.FatherName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mother's Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.MotherName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Father's Qualification", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.FatherQualification, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);



            cell = new PdfPCell(new Phrase("Mother's Qualification", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.MotherQualification, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Father's Occupation", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.FatherOccupation, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mother's Occupation", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.MotherOccupation, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Father's Contact", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.FatherMoileNo, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mother's Contact", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.MotherMoileNo, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            doc.Add(table);

        }

        public void AddGuardianDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("Guardian's Name", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.GuardianName, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Guardian's Qualification", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.GuardianQualification, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Guardian's Occupation", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.GuardianOccupation, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Guardian's Contact", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.GuardianMoileNo, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);
            doc.Add(table);

        }

        public void AddDisabilityDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("Disability", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.DisabilityType, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            doc.Add(table);

        }
        public void AddIdentificationDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("Aadhaar Number", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);
            int length = string.IsNullOrEmpty(_studentEntity.AadharNo) ? 0 : _studentEntity.AadharNo.Length;
            string aadhar = length == 0 ? "********" : "********" + _studentEntity.AadharNo.Substring(length - 4, 4);
            cell = new PdfPCell(new Phrase(aadhar, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ration Card Number", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.RationCardrNo, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);
            doc.Add(table);

        }
        public void AddDocumentDetails(iTextSharp.text.Document doc, StudentEntity _studentEntity)
        {
            PdfPTable table = new PdfPTable(6);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //table.setBorder(Border.NO_BORDER);
            //set overall width
            table.WidthPercentage = 100f;
            //set column widths
            int[] firstTablecellwidth = { 20, 5, 25, 20, 5, 25 };
            table.SetWidths(firstTablecellwidth);

            PdfPCell cell = new PdfPCell(new Phrase("Income Certificate", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.IncomeCertificateFilename, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Transfer/Bonafide Certificate", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.TcFilename, NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Bank Passbook", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.BankPassbookFilename
                , NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Declaration form", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(_studentEntity.DeclarationFilename
                , NormalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            doc.Add(table);
            // DateTime.UtcNow.ToString("yyyy-MM-dd");


            DateTime thisDate1 = new DateTime(2011, 6, 10);
            Console.WriteLine("Today is " + thisDate1.ToString("MMMM dd, yyyy") + ".");

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
