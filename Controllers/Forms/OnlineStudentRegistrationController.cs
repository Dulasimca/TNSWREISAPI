using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineStudentRegistrationController : Controller
    {
        [HttpPost("{id}")]
        public Tuple<bool,string> Post(onlineStudentEntity entity)
        {
            bool result = false;

            if (entity.studentId == 0)
            {
                DataSet ds = new DataSet();
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(entity.hostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@isNewStudent", Convert.ToInt32(entity.isNewStudent) == 0 ? "2" : entity.isNewStudent));
                ds = manageSQL.GetDataSetValues("OnlineApplicationControlForHO", sqlParameters);
                ManagePDFGeneration manage = new ManagePDFGeneration();
                if (!manage.CheckDataAvailable(ds))
                {
                    return new Tuple<bool, string>(false, " Online Registration has been clossed, Please check with TNADWHO ");
                }
                // Need to check
            }
            
                ManageOnlineRegistration ManageOnlineRegistration = new ManageOnlineRegistration();
                result = ManageOnlineRegistration.InsertOnlineStudentDetails(entity);
                //Generate the PDF file. 
                GeneratePDFDocument generatePDF = new GeneratePDFDocument();
                generatePDF.Generate(entity.aadharNo, entity.mobileNo, entity.dob);
            
          //  }
           
            return new Tuple<bool,string>(result,"Registered Successfuly");
        }

        [HttpGet("{id}")]
        public string Get(string AadharNo, string MobileNo, string Dob)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@AadharNo",(AadharNo))); 
            sqlParameters.Add(new KeyValuePair<string, string>("@MobileNo", (MobileNo)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Dob", (Dob)));
            ds = manageSQL.GetDataSetValues("GetOnlineRegistrationById", sqlParameters);
            GeneratePDFDocument generatePDF = new GeneratePDFDocument();
            generatePDF.Generate(ds);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

        [HttpPut("{id}")]
        public bool Put(StudentEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", Convert.ToString(entity.studentId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DApproval", Convert.ToString(entity.districtApproval)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TApproval", Convert.ToString(entity.talukApproval)));
                var result = manageSQL.UpdateValues("StudentApproval", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }

    }

    public class onlineStudentEntity
    {
        public Int64 studentId { get; set; }
        public int hostelId { get; set; }
        public string studentName { get; set; }
        public int age { get; set; }
        public string dob { get; set; }
        public int bloodGroup { get; set; }
        public int gender { get; set; }
        public int motherTongue { get; set; }
        public string mobileNo { get; set; }
        public string altMobNo { get; set; }
        public int religion { get; set; }
        public int caste { get; set; }
        public string subCaste { get; set; }
        public string studentFilename { get; set; }
        public string instituteName { get; set; }
        public string currentInstituteId { get; set; }
        public string medium { get; set; }
        public int classId { get; set; }
        public int courseYearId { get; set; }
        public string courseTitle { get; set; }
        public string lastStudiedInstituteCode { get; set; }
        public string lastStudiedInstituteAddress { get; set; }
        public string distanceFromHostelToHome { get; set; }
        public string distanceFromHostelToInstitute { get; set; }
        public string disabilityType { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string landmark { get; set; }
        public string distrctCode { get; set; }
        public string talukCode { get; set; }
        public string village { get; set; }
        public string pincode { get; set; }
        public string aadharNo { get; set; }
        public string rationCardrNo { get; set; }
        public string emisno { get; set; }
        public string talukApproval { get; set; }
        public string districtApproval { get; set; }
        public string admissionNo { get; set; }
        public string remarks { get; set; }
        public string scholarshipId { get; set; }
        public string wardenApproval { get; set; }
        public string ReasonForDisApprove { get; set; }
        public string refugeeSelectedType { get; set; }
        public string refugeeId { get; set; }
        public string orphanageSelectedType { get; set; }
        public string communityCertificateNo { get; set; }
        public string isNewStudent { get; set; }
        //Bank
        public int bankId { get; set; }
        public string bankName { get; set; }
        public string bankAccNo { get; set; }
        public string ifscCode { get; set; }
        public string branchName { get; set; }
        public string micrNo { get; set; }

        //Parent
        public int parentId { get; set; }
        public int fnTitleCode { get; set; }
        public string fatherName { get; set; }
        public string fatherOccupation { get; set; }
        public string fatherMoileNo { get; set; }
        public string fatherQualification { get; set; }
        public string fatherYIncome { get; set; }
        public int mnTitleCode { get; set; }
        public string motherName { get; set; }
        public string motherOccupation { get; set; }
        public string motherMoileNo { get; set; }
        public string motherQualification { get; set; }
        public string motherYIncome { get; set; }
        public int gnTitleCode { get; set; }
        public string guardianName { get; set; }
        public string guardianOccupation { get; set; }
        public string guardianMobileNo { get; set; }
        public string guardianQualification { get; set; }
        public string totalYIncome { get; set; }

        //Document
        public int documentId { get; set; }
        public string incomeCertificateFilename { get; set; }
        public string tcFilename { get; set; }
        public string bankPassbookFilename { get; set; }
        public string declarationFilename { get; set; }

        //Common
        public int Type { get; set; }
        public int flag { get; set; }
        public int accYearId { get; set; }
        public string studentAccId { get; set; }

        public int roleId { get; set; }

    }

    public class OnlineStudentBankEntity
    {
        public int bankId { get; set; }
        public Int64 studentId { get; set; }
        public string bankName { get; set; }
        public string bankAccNo { get; set; }
        public string ifscCode { get; set; }
        public string branchName { get; set; }
    }
    public class OnlineStudentParentInfoEntity
    {
        public int parentId { get; set; }
        public Int64 studentId { get; set; }
        public string fatherName { get; set; }
        public string fatherOccupation { get; set; }
        public string fatherMoileNo { get; set; }
        public string fatherQualification { get; set; }
        public string fatherYIncome { get; set; }
        public string motherName { get; set; }
        public string motherOccupation { get; set; }
        public string motherMoileNo { get; set; }
        public string motherQualification { get; set; }
        public string motherYIncome { get; set; }
        public string guardianName { get; set; }
        public string guardianOccupation { get; set; }
        public string guardianMobileNo { get; set; }
        public string guardianQualification { get; set; }
        public string totalYIncome { get; set; }
    }

    public class OnlineStudentDocumentEntity
    {
        public int documentId { get; set; }
        public Int64 studentId { get; set; }
        public string incomeCertificateFilename { get; set; }
        public string tcFilename { get; set; }
        public string bankPassbookFilename { get; set; }
        public string declarationFilename { get; set; }

    }

     
}