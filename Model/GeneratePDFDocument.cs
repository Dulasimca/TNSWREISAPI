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
                _studentEntity.StudentId = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.HostelID = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.StudentName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Age = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Dob = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.BloodGroup = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Gender = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.MotherTongue = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.MobileNo = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.AltMobNo = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Religion = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Caste = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Subcaste = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.StudentFilename = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.InstituteName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Medium = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.ClassName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.CourseYearId = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.CourseTitle = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.LastInstitutionName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.LastInstitutionAddress = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.DistanceFromHostelToHome = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.DistanceFromHostelToInstitute = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.DisabilityType = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Address1 = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Address2 = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Landmark = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Village = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Pincode = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.AadharNo = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.RationCardrNo = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Emisno = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.TalukApproval = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.DistrictApproval = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.CreatedDate = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Flag = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.BankId = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.BankName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.BankAccNo = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.IfscCode = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.BranchName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.MICRNO = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.ParentId = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.FatherName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.FatherMoileNo = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.FatherOccupation = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.FatherQualification = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.FatherYIncome = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.MotherMoileNo = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.MotherName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.MotherOccupation = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.MotherQualification = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.MotherYIncome = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.GuardianMoileNo = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.GuardianName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.GuardianOccupation = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.GuardianQualification = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.TotalYearlyIncome = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.DocumentId = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.IncomeCertificateFilename = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.TcFilename = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.BankPassbookFilename = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.DeclarationFilename = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.GenderName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.MothertongueName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.BloodgroupName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.ReligionName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.CasteName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Class = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.CourseYear = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.MediumName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.SubcasteName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Districtname = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Talukname = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.HostelDName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.HostelTName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.HostelName = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.ScholarshipId = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Remarks = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.AdmissionNo = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.AcademicYear = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Address = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
                _studentEntity.Course = Convert.ToString(studentDs.Tables[0].Rows[0][""]);
            }
            return _studentEntity;
        }

    }
}
