﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
      [HttpPost("{id}")]
      public bool Post(StudentEntity entity)
        {
            ManageRegistration manageRegistration = new ManageRegistration();
            var result = manageRegistration.InsertStudentDetails(entity);
            return result;
        }

        [HttpGet("{id}")]
        public string Get(int DCode, int TCode, int HCode)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HCode)));
            ds = manageSQL.GetDataSetValues("GetStudentDetails", sqlParameters);
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

    public class StudentEntity
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
        public string subcaste { get; set; }
        public string studentFilename { get; set; }
        public string instituteName { get; set; }
        public string medium { get; set; }
        public int classId { get; set; }
        public string courseTitle { get; set; }
        public string lastStudiedInstituteName { get; set; }
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

        //Bank
        public int bankId { get; set; }
        public string bankName { get; set; }
        public string bankAccNo { get; set; }
        public string ifscCode { get; set; }
        public string branchName { get; set; }
        public string micrNo { get; set; }

        //Parent
        public int parentId { get; set; }
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

        //Document
        public int documentId { get; set; }
        public string incomeCertificateFilename { get; set; }
        public string tcFilename { get; set; }
        public string bankPassbookFilename { get; set; }
        public string declarationFilename { get; set; }
    }

    public class StudentBankEntity
    {
        public int bankId { get; set; }
        public Int64 studentId { get; set; } 
        public string bankName { get; set; }
        public string bankAccNo { get; set; }
        public string ifscCode { get; set; }
        public string branchName { get; set; }
    }
    public class StudentParentInfoEntity
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

    public class StudentDocumentEntity
    {
        public int documentId { get; set; }
        public Int64 studentId { get; set; }
        public string incomeCertificateFilename { get; set; }
        public string tcFilename { get; set; }
        public string bankPassbookFilename { get; set; }
        public string declarationFilename { get; set; }

    }
}