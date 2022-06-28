using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.Controllers.Forms;

namespace TNSWREISAPI.ManageSQL
{
    public class ManageOnlineRegistration
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();
     //   StudentAccountingYearController studentAccountingYearController = new StudentAccountingYearController();

        public bool InsertOnlineStudentDetails(onlineStudentEntity onlineStudentEntity)
        {
            SqlTransaction objTrans = null;
            int StudentId;
            using (sqlConnection = new SqlConnection(GlobalVariable.ConnectionString))
            {
                DataSet ds = new DataSet();

                sqlCommand = new SqlCommand();
                try
                {
                    if (sqlConnection.State == 0)
                    {
                        sqlConnection.Open();
                    }
                    objTrans = sqlConnection.BeginTransaction();
                    sqlCommand.Transaction = objTrans;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "InsertOnlineIntoStudent";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", onlineStudentEntity.studentId);
                    sqlCommand.Parameters.AddWithValue("@HostelId", onlineStudentEntity.hostelId);
                    sqlCommand.Parameters.AddWithValue("@StudentName", onlineStudentEntity.studentName);
                    sqlCommand.Parameters.AddWithValue("@Age", onlineStudentEntity.age);
                    sqlCommand.Parameters.AddWithValue("@DOB", onlineStudentEntity.dob);
                    sqlCommand.Parameters.AddWithValue("@BloodGroup", onlineStudentEntity.bloodGroup);
                    sqlCommand.Parameters.AddWithValue("@Gender", onlineStudentEntity.gender);
                    sqlCommand.Parameters.AddWithValue("@MotherTongue", onlineStudentEntity.motherTongue);
                    sqlCommand.Parameters.AddWithValue("@MobileNo", onlineStudentEntity.mobileNo);
                    sqlCommand.Parameters.AddWithValue("@AltMobNo", onlineStudentEntity.altMobNo);
                    sqlCommand.Parameters.AddWithValue("@Religion", onlineStudentEntity.religion);
                    sqlCommand.Parameters.AddWithValue("@Caste", onlineStudentEntity.caste);
                    sqlCommand.Parameters.AddWithValue("@Subcaste", onlineStudentEntity.subcaste);
                    sqlCommand.Parameters.AddWithValue("@StudentFilename", onlineStudentEntity.studentFilename);
                    sqlCommand.Parameters.AddWithValue("@InstituteName", onlineStudentEntity.instituteName);
                    sqlCommand.Parameters.AddWithValue("@Medium", onlineStudentEntity.medium);
                    sqlCommand.Parameters.AddWithValue("@ClassId", onlineStudentEntity.classId);
                    sqlCommand.Parameters.AddWithValue("@CourseTitle", onlineStudentEntity.courseTitle);
                    sqlCommand.Parameters.AddWithValue("@CourseYearId", onlineStudentEntity.courseYearId);
                    sqlCommand.Parameters.AddWithValue("@LastInstituteName", onlineStudentEntity.lastStudiedInstituteCode);
                    sqlCommand.Parameters.AddWithValue("@LastInstituteAddress", onlineStudentEntity.lastStudiedInstituteAddress);
                    sqlCommand.Parameters.AddWithValue("@DistanceToHome", onlineStudentEntity.distanceFromHostelToHome);
                    sqlCommand.Parameters.AddWithValue("@DistanceToInstitute", onlineStudentEntity.distanceFromHostelToInstitute);
                    sqlCommand.Parameters.AddWithValue("@DisabilityType", onlineStudentEntity.disabilityType);
                    sqlCommand.Parameters.AddWithValue("@Address1", onlineStudentEntity.address1);
                    sqlCommand.Parameters.AddWithValue("@Address2", onlineStudentEntity.address2);
                    sqlCommand.Parameters.AddWithValue("@Landmark", onlineStudentEntity.landmark);
                    sqlCommand.Parameters.AddWithValue("@DistrictCode", onlineStudentEntity.distrctCode);
                    sqlCommand.Parameters.AddWithValue("@TalukCode", onlineStudentEntity.talukCode);
                    sqlCommand.Parameters.AddWithValue("@Village", onlineStudentEntity.village);
                    sqlCommand.Parameters.AddWithValue("@Pincode", onlineStudentEntity.pincode);
                    sqlCommand.Parameters.AddWithValue("@AadharNo", onlineStudentEntity.aadharNo);
                    sqlCommand.Parameters.AddWithValue("@RationCardNo", onlineStudentEntity.rationCardrNo);
                    sqlCommand.Parameters.AddWithValue("@EMISNo", onlineStudentEntity.emisno);
                    sqlCommand.Parameters.AddWithValue("@TalukApproval", onlineStudentEntity.talukApproval);
                    sqlCommand.Parameters.AddWithValue("@DistrictApproval", onlineStudentEntity.districtApproval);
                    sqlCommand.Parameters.AddWithValue("@ScholarshipId", onlineStudentEntity.scholarshipId);
                    sqlCommand.Parameters.AddWithValue("@AdmissionNo", onlineStudentEntity.admissionNo);
                    sqlCommand.Parameters.AddWithValue("@Remarks", onlineStudentEntity.remarks);
                    sqlCommand.Parameters.AddWithValue("@RefugeeId", onlineStudentEntity.refugeeId);
                    sqlCommand.Parameters.AddWithValue("@isRefugee", onlineStudentEntity.refugeeSelectedType);
                    sqlCommand.Parameters.AddWithValue("@isOrphan", onlineStudentEntity.orphanageSelectedType);
                    sqlCommand.Parameters.AddWithValue("@CommunityCertificateNo", onlineStudentEntity.communityCertificateNo);
                    sqlCommand.Parameters.AddWithValue("@isNewStudent", onlineStudentEntity.isNewStudent);
                    sqlCommand.Parameters.AddWithValue("@CurrentInstituteId", onlineStudentEntity.currentInstituteId);
                    sqlCommand.Parameters.AddWithValue("@ReasonForDisApprove", onlineStudentEntity.ReasonForDisApprove);            
                    sqlCommand.Parameters.Add("@StudentId", SqlDbType.BigInt, 13);
                    sqlCommand.Parameters["@StudentId"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();

                    StudentId = (int)(long)(sqlCommand.Parameters["@StudentId"].Value);
                    onlineStudentEntity.studentId = StudentId;
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();

                    //StudentBankEntity bankEntity = new StudentBankEntity();
                    sqlCommand = new SqlCommand();
                    sqlCommand.Transaction = objTrans;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "InsertOnlineStudentBankDetails";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@StudentId", StudentId);
                    sqlCommand.Parameters.AddWithValue("@BankName", onlineStudentEntity.bankName);
                    sqlCommand.Parameters.AddWithValue("@BankAccNo", onlineStudentEntity.bankAccNo);
                    sqlCommand.Parameters.AddWithValue("@IFSCCode", onlineStudentEntity.ifscCode);
                    sqlCommand.Parameters.AddWithValue("@BranchName", onlineStudentEntity.branchName);
                    sqlCommand.Parameters.AddWithValue("@MICRNo", onlineStudentEntity.micrNo);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();

                   // StudentParentInfoEntity parentInfoEntity = new StudentParentInfoEntity();
                    sqlCommand = new SqlCommand();
                    sqlCommand.Transaction = objTrans;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "InsertOnlineStudentParentInfo";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@StudentId", StudentId);
                    sqlCommand.Parameters.AddWithValue("@FatherTitle", onlineStudentEntity.fatherTitle);
                    sqlCommand.Parameters.AddWithValue("@FatherName", onlineStudentEntity.fatherName);
                    sqlCommand.Parameters.AddWithValue("@FatherOccupation", onlineStudentEntity.fatherOccupation);
                    sqlCommand.Parameters.AddWithValue("@FatherMobileNo", onlineStudentEntity.fatherMoileNo);
                    sqlCommand.Parameters.AddWithValue("@FatherQualification", onlineStudentEntity.fatherQualification);
                    sqlCommand.Parameters.AddWithValue("@FatherYIncome", onlineStudentEntity.fatherYIncome);
                    sqlCommand.Parameters.AddWithValue("@MotherTitle", onlineStudentEntity.motherTitle);
                    sqlCommand.Parameters.AddWithValue("@MotherName", onlineStudentEntity.motherName);
                    sqlCommand.Parameters.AddWithValue("@MotherOccupation", onlineStudentEntity.motherOccupation);
                    sqlCommand.Parameters.AddWithValue("@MotherQualification", onlineStudentEntity.motherQualification);
                    sqlCommand.Parameters.AddWithValue("@MotherMobileNo", onlineStudentEntity.motherMoileNo);
                    sqlCommand.Parameters.AddWithValue("@MotherYIncome", onlineStudentEntity.motherYIncome);
                    sqlCommand.Parameters.AddWithValue("@GuardianTitle", onlineStudentEntity.guardianTitle);
                    sqlCommand.Parameters.AddWithValue("@GuardianName", onlineStudentEntity.guardianName);
                    sqlCommand.Parameters.AddWithValue("@GuardianOccupation", onlineStudentEntity.guardianOccupation);
                    sqlCommand.Parameters.AddWithValue("@GuardianMobileNo", onlineStudentEntity.guardianMobileNo);
                    sqlCommand.Parameters.AddWithValue("@GuardianQualification", onlineStudentEntity.guardianQualification);
                    sqlCommand.Parameters.AddWithValue("@TotalYIncome", onlineStudentEntity.totalYIncome);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();

                   // StudentDocumentEntity documentEntity = new StudentDocumentEntity();
                    sqlCommand = new SqlCommand();
                    sqlCommand.Transaction = objTrans;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "InsertOnlineStudentDocuments";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@StudentId", StudentId);
                    sqlCommand.Parameters.AddWithValue("@ICFilename", onlineStudentEntity.incomeCertificateFilename);
                    sqlCommand.Parameters.AddWithValue("@TCFilename", onlineStudentEntity.tcFilename);
                    sqlCommand.Parameters.AddWithValue("@BPFilename", onlineStudentEntity.bankPassbookFilename);
                    sqlCommand.Parameters.AddWithValue("@DCFilename", onlineStudentEntity.declarationFilename);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();

                    ///
                    InsertStudentAccountingYear(onlineStudentEntity, sqlCommand, objTrans);

                    objTrans.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    AuditLog.WriteError(ex.Message + " : " + ex.StackTrace);
                    objTrans.Rollback();
                    return false;
                }
                finally
                {
                    sqlConnection.Close();
                    sqlCommand.Dispose();
                    ds.Dispose();
                }
            }
        }
        public bool InsertStudentAccountingYear(onlineStudentEntity entity, SqlCommand sql, SqlTransaction objTrans)
        {
            try
            {
                sqlCommand = new SqlCommand();
                sqlCommand.Transaction = objTrans;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "InsertStudentAccountingYear";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", Convert.ToString(entity.studentAccId));
                sqlCommand.Parameters.AddWithValue("@DCode", Convert.ToString(entity.distrctCode));
                sqlCommand.Parameters.AddWithValue("@TCode", Convert.ToString(entity.talukCode));
                sqlCommand.Parameters.AddWithValue("@HCode", Convert.ToString(entity.hostelId));
                sqlCommand.Parameters.AddWithValue("@StudentId", Convert.ToString(entity.studentId));
                sqlCommand.Parameters.AddWithValue("@AccYearId", Convert.ToString(entity.accYearId));
                sqlCommand.Parameters.AddWithValue("@Flag", "1");
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                sqlCommand.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }
        }
    }
}

