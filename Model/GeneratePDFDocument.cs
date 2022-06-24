using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Model
{
    public class GeneratePDFDocument
    {
        ManagePDFGeneration managePDFGeneration = new ManagePDFGeneration();
        public bool Generate(string AadharNo, string MobileNo, string Dob)
        {
            bool isGenerate = false;
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                DataSet ds = new DataSet();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@AadharNo", (AadharNo)));
                sqlParameters.Add(new KeyValuePair<string, string>("@MobileNo", (MobileNo)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Dob", (Dob)));
                ds = manageSQL.GetDataSetValues("GetOnlineRegistrationById", sqlParameters);
                var studentEntity = ManagePDFEntity(ds);
                managePDFGeneration.GeneratePDF(studentEntity);
                isGenerate = true;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return isGenerate;
        }
        public bool Generate(DataSet nds)
        {
            bool isGenerate = false;
            try
            {
                var studentEntity = ManagePDFEntity(nds);
                managePDFGeneration.GeneratePDF(studentEntity);
                isGenerate = true;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return isGenerate;
        }
        public StudentEntity ManagePDFEntity(DataSet studentDs)
        {
            StudentEntity _studentEntity = new StudentEntity();
            if (managePDFGeneration.CheckDataAvailable(studentDs))
            {
                _studentEntity.StudentId = Convert.ToString(studentDs.Tables[0].Rows[0]["studentId"]);
                _studentEntity.HostelID = Convert.ToString(studentDs.Tables[0].Rows[0]["hostelId"]);
                _studentEntity.StudentName = Convert.ToString(studentDs.Tables[0].Rows[0]["studentName"]);
                _studentEntity.Age = Convert.ToString(studentDs.Tables[0].Rows[0]["age"]);
                _studentEntity.Dob = Convert.ToString(studentDs.Tables[0].Rows[0]["DateofBirth"]);
                _studentEntity.BloodGroup = Convert.ToString(studentDs.Tables[0].Rows[0]["bloodGroup"]);
                _studentEntity.Gender = Convert.ToString(studentDs.Tables[0].Rows[0]["gender"]);
                _studentEntity.MotherTongue = Convert.ToString(studentDs.Tables[0].Rows[0]["motherTongue"]);
                _studentEntity.MobileNo = Convert.ToString(studentDs.Tables[0].Rows[0]["mobileNo"]);
                _studentEntity.AltMobNo = Convert.ToString(studentDs.Tables[0].Rows[0]["altMobNo"]);
                _studentEntity.Religion = Convert.ToString(studentDs.Tables[0].Rows[0]["religion"]);
                _studentEntity.Caste = Convert.ToString(studentDs.Tables[0].Rows[0]["caste"]);
                _studentEntity.Subcaste = Convert.ToString(studentDs.Tables[0].Rows[0]["subCaste"]);
                _studentEntity.StudentFilename = Convert.ToString(studentDs.Tables[0].Rows[0]["studentFilename"]);
                _studentEntity.InstituteName = Convert.ToString(studentDs.Tables[0].Rows[0]["instituteName"]);
                _studentEntity.Medium = Convert.ToString(studentDs.Tables[0].Rows[0]["medium"]);
                _studentEntity.ClassId = Convert.ToString(studentDs.Tables[0].Rows[0]["classId"]);
                _studentEntity.CourseYearId = Convert.ToString(studentDs.Tables[0].Rows[0]["courseYearId"]);
                _studentEntity.CourseTitle = Convert.ToString(studentDs.Tables[0].Rows[0]["courseTitle"]);
                _studentEntity.LastInstitutionName = Convert.ToString(studentDs.Tables[0].Rows[0]["lastStudiedInstituteName"]);
                _studentEntity.LastInstitutionAddress = Convert.ToString(studentDs.Tables[0].Rows[0]["lastStudiedInstituteAddress"]);
                _studentEntity.DistanceFromHostelToHome = Convert.ToString(studentDs.Tables[0].Rows[0]["distanceFromHostelToHome"]);
                _studentEntity.DistanceFromHostelToInstitute = Convert.ToString(studentDs.Tables[0].Rows[0]["distanceFromHostelToInstitute"]);
                _studentEntity.DisabilityType = Convert.ToString(studentDs.Tables[0].Rows[0]["disabilityType"]);
                _studentEntity.Address1 = Convert.ToString(studentDs.Tables[0].Rows[0]["address1"]);
                _studentEntity.Address2 = Convert.ToString(studentDs.Tables[0].Rows[0]["address2"]);
                _studentEntity.Landmark = Convert.ToString(studentDs.Tables[0].Rows[0]["landmark"]);
                _studentEntity.Village = Convert.ToString(studentDs.Tables[0].Rows[0]["village"]);
                _studentEntity.Pincode = Convert.ToString(studentDs.Tables[0].Rows[0]["pincode"]);
                _studentEntity.AadharNo = Convert.ToString(studentDs.Tables[0].Rows[0]["aadharNo"]);
                _studentEntity.RationCardrNo = Convert.ToString(studentDs.Tables[0].Rows[0]["rationCardrNo"]);
                _studentEntity.Emisno = Convert.ToString(studentDs.Tables[0].Rows[0]["emisno"]);
                _studentEntity.TalukApproval = Convert.ToString(studentDs.Tables[0].Rows[0]["talukApproval"]);
                _studentEntity.DistrictApproval = Convert.ToString(studentDs.Tables[0].Rows[0]["districtApproval"]);
                _studentEntity.CreatedDate = Convert.ToString(studentDs.Tables[0].Rows[0]["CreatedDate"]);
                _studentEntity.Flag = Convert.ToString(studentDs.Tables[0].Rows[0]["Flag"]);
                _studentEntity.BankId = Convert.ToString(studentDs.Tables[0].Rows[0]["bankId"]);
                _studentEntity.BankName = Convert.ToString(studentDs.Tables[0].Rows[0]["bankName"]);
                _studentEntity.BankAccNo = Convert.ToString(studentDs.Tables[0].Rows[0]["bankAccNo"]);
                _studentEntity.IfscCode = Convert.ToString(studentDs.Tables[0].Rows[0]["ifscCode"]);
                _studentEntity.BranchName = Convert.ToString(studentDs.Tables[0].Rows[0]["branchName"]);
                _studentEntity.MICRNO = Convert.ToString(studentDs.Tables[0].Rows[0]["micrNo"]);
                _studentEntity.ParentId = Convert.ToString(studentDs.Tables[0].Rows[0]["parentId"]);
                _studentEntity.FatherName = Convert.ToString(studentDs.Tables[0].Rows[0]["fatherName"]);
                _studentEntity.FatherMoileNo = Convert.ToString(studentDs.Tables[0].Rows[0]["fatherMoileNo"]);
                _studentEntity.FatherOccupation = Convert.ToString(studentDs.Tables[0].Rows[0]["fatherOccupation"]);
                _studentEntity.FatherQualification = Convert.ToString(studentDs.Tables[0].Rows[0]["fatherQualification"]);
                _studentEntity.FatherYIncome = Convert.ToString(studentDs.Tables[0].Rows[0]["fatherYIncome"]);
                _studentEntity.MotherMoileNo = Convert.ToString(studentDs.Tables[0].Rows[0]["motherMoileNo"]);
                _studentEntity.MotherName = Convert.ToString(studentDs.Tables[0].Rows[0]["motherName"]);
                _studentEntity.MotherOccupation = Convert.ToString(studentDs.Tables[0].Rows[0]["motherOccupation"]);
                _studentEntity.MotherQualification = Convert.ToString(studentDs.Tables[0].Rows[0]["motherQualification"]);
                _studentEntity.MotherYIncome = Convert.ToString(studentDs.Tables[0].Rows[0]["motherYIncome"]);
                _studentEntity.GuardianMoileNo = Convert.ToString(studentDs.Tables[0].Rows[0]["guardianMobileNo"]);
                _studentEntity.GuardianName = Convert.ToString(studentDs.Tables[0].Rows[0]["guardianName"]);
                _studentEntity.GuardianOccupation = Convert.ToString(studentDs.Tables[0].Rows[0]["guardianOccupation"]);
                _studentEntity.GuardianQualification = Convert.ToString(studentDs.Tables[0].Rows[0]["guardianMobileNo"]);
                _studentEntity.TotalYearlyIncome = Convert.ToString(studentDs.Tables[0].Rows[0]["totalYIncome"]);
                _studentEntity.DocumentId = Convert.ToString(studentDs.Tables[0].Rows[0]["documentId"]);
                _studentEntity.IncomeCertificateFilename = Convert.ToString(studentDs.Tables[0].Rows[0]["incomeCertificateFilename"]);
                _studentEntity.TcFilename = Convert.ToString(studentDs.Tables[0].Rows[0]["tcFilename"]);
                _studentEntity.BankPassbookFilename = Convert.ToString(studentDs.Tables[0].Rows[0]["bankPassbookFilename"]);
                _studentEntity.DeclarationFilename = Convert.ToString(studentDs.Tables[0].Rows[0]["declarationFilename"]);
                _studentEntity.GenderName = Convert.ToString(studentDs.Tables[0].Rows[0]["genderName"]);
                _studentEntity.MothertongueName = Convert.ToString(studentDs.Tables[0].Rows[0]["mothertongueName"]);
                _studentEntity.BloodgroupName = Convert.ToString(studentDs.Tables[0].Rows[0]["bloodgroupName"]);
                _studentEntity.ReligionName = Convert.ToString(studentDs.Tables[0].Rows[0]["religionName"]);
                _studentEntity.CasteName = Convert.ToString(studentDs.Tables[0].Rows[0]["casteName"]);
                _studentEntity.Class = Convert.ToString(studentDs.Tables[0].Rows[0]["class"]);
                _studentEntity.CourseYear = Convert.ToString(studentDs.Tables[0].Rows[0]["courseYear"]);
                _studentEntity.MediumName = Convert.ToString(studentDs.Tables[0].Rows[0]["mediumName"]);
                _studentEntity.SubcasteName = Convert.ToString(studentDs.Tables[0].Rows[0]["subcasteName"]);
                _studentEntity.Districtname = Convert.ToString(studentDs.Tables[0].Rows[0]["Districtname"]);
                _studentEntity.Talukname = Convert.ToString(studentDs.Tables[0].Rows[0]["Talukname"]);
                _studentEntity.HostelDName = Convert.ToString(studentDs.Tables[0].Rows[0]["hostelDName"]);
                _studentEntity.HostelTName = Convert.ToString(studentDs.Tables[0].Rows[0]["hostelTName"]);
                _studentEntity.HostelName = Convert.ToString(studentDs.Tables[0].Rows[0]["HostelName"]);
                _studentEntity.ScholarshipId = Convert.ToString(studentDs.Tables[0].Rows[0]["scholarshipId"]);
                _studentEntity.Remarks = Convert.ToString(studentDs.Tables[0].Rows[0]["remarks"]);
                _studentEntity.AdmissionNo = Convert.ToString(studentDs.Tables[0].Rows[0]["admissionNo"]);
                _studentEntity.AcademicYear = Convert.ToString(studentDs.Tables[0].Rows[0]["AcademicYear"]);
                _studentEntity.Address = Convert.ToString(studentDs.Tables[0].Rows[0]["Address"]);
                _studentEntity.Course = Convert.ToString(studentDs.Tables[0].Rows[0]["Course"]);
            }


            return CheckValues(_studentEntity);
        }

        public StudentEntity CheckValues(StudentEntity _studentEntity)
        {
            //StudentEntity studentEntity = new StudentEntity
            //{
            _studentEntity.DeclarationFilename = !string.IsNullOrEmpty(_studentEntity.DeclarationFilename) || _studentEntity.DeclarationFilename != "" ? "Yes" : "No";
            _studentEntity.BankPassbookFilename = !string.IsNullOrEmpty(_studentEntity.BankPassbookFilename) || _studentEntity.BankPassbookFilename != "" ? "Yes" : "No";
            _studentEntity.IncomeCertificateFilename = !string.IsNullOrEmpty(_studentEntity.IncomeCertificateFilename) || _studentEntity.IncomeCertificateFilename != "" ? "Yes" : "No";
            _studentEntity.TcFilename = !string.IsNullOrEmpty(_studentEntity.TcFilename) || _studentEntity.TcFilename != "" ? "Yes" : "No";
            _studentEntity.MICRNO = !string.IsNullOrEmpty(_studentEntity.MICRNO) || _studentEntity.MICRNO != "" ? _studentEntity.MICRNO : "-";
            _studentEntity.DisabilityType = !string.IsNullOrEmpty(_studentEntity.DisabilityType) || _studentEntity.DisabilityType == "0" ? "No" : _studentEntity.DisabilityType;
            _studentEntity.StudentFilename = !string.IsNullOrEmpty(_studentEntity.StudentFilename) || _studentEntity.StudentFilename != "" ? _studentEntity.StudentFilename : "dulasi";
            //};
            return _studentEntity;
        }
    }
}
