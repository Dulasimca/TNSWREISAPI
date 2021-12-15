using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.Controllers.Forms;

namespace TNSWREISAPI.ManageSQL
{
    public class ManageRegistration
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();
        public bool InsertStudentDetails(StudentEntity studentEntity)
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
                    sqlCommand.CommandText = "InsertIntoStudent";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Id", studentEntity.studentId);
                    sqlCommand.Parameters.AddWithValue("@HostelId", studentEntity.hostelId);
                    sqlCommand.Parameters.AddWithValue("@StudentName", studentEntity.studentName);
                    sqlCommand.Parameters.AddWithValue("@Age", studentEntity.age);
                    sqlCommand.Parameters.AddWithValue("@DOB", studentEntity.dob);
                    sqlCommand.Parameters.AddWithValue("@BloodGroup", studentEntity.bloodGroup);
                    sqlCommand.Parameters.AddWithValue("@Gender", studentEntity.gender);
                    sqlCommand.Parameters.AddWithValue("@MotherTongue", studentEntity.motherTongue);
                    sqlCommand.Parameters.AddWithValue("@MobileNo", studentEntity.mobileNo);
                    sqlCommand.Parameters.AddWithValue("@AltMobNo", studentEntity.altMobNo);
                    sqlCommand.Parameters.AddWithValue("@Religion", studentEntity.religion);
                    sqlCommand.Parameters.AddWithValue("@Caste", studentEntity.caste);
                    sqlCommand.Parameters.AddWithValue("@Subcaste", studentEntity.subcaste);
                    sqlCommand.Parameters.AddWithValue("@StudentFilename", studentEntity.studentFilename);
                    sqlCommand.Parameters.AddWithValue("@InstituteName", studentEntity.instituteName);
                    sqlCommand.Parameters.AddWithValue("@Medium", studentEntity.medium);
                    sqlCommand.Parameters.AddWithValue("@ClassId", studentEntity.classId);
                    sqlCommand.Parameters.AddWithValue("@CourseTitle", studentEntity.courseTitle);
                    sqlCommand.Parameters.AddWithValue("@LastInstituteName", studentEntity.lastStudiedInstituteName);
                    sqlCommand.Parameters.AddWithValue("@LastInstituteAddress", studentEntity.lastStudiedInstituteAddress);
                    sqlCommand.Parameters.AddWithValue("@DistanceToHome", studentEntity.distanceFromHostelToHome);
                    sqlCommand.Parameters.AddWithValue("@DistanceToInstitute", studentEntity.distanceFromHostelToInstitute);
                    sqlCommand.Parameters.AddWithValue("@DisabilityType", studentEntity.disabilityType);
                    sqlCommand.Parameters.AddWithValue("@Address1", studentEntity.address1);
                    sqlCommand.Parameters.AddWithValue("@Address2", studentEntity.address2);
                    sqlCommand.Parameters.AddWithValue("@Landmark", studentEntity.landmark);
                    sqlCommand.Parameters.AddWithValue("@DistrictCode", studentEntity.distrctCode);
                    sqlCommand.Parameters.AddWithValue("@TalukCode", studentEntity.talukCode);
                    sqlCommand.Parameters.AddWithValue("@Village", studentEntity.village);
                    sqlCommand.Parameters.AddWithValue("@Pincode", studentEntity.pincode);
                    sqlCommand.Parameters.AddWithValue("@AadharNo", studentEntity.aadharNo);
                    sqlCommand.Parameters.AddWithValue("@RationCardNo", studentEntity.rationCardrNo);
                    sqlCommand.Parameters.AddWithValue("@EMISNo", studentEntity.emisno);
                    sqlCommand.Parameters.AddWithValue("@TalukApproval", studentEntity.talukApproval);
                    sqlCommand.Parameters.AddWithValue("@DistrictApproval", studentEntity.districtApproval);
                    sqlCommand.Parameters.AddWithValue("@ScholarshipId", studentEntity.scholarshipId);
                    sqlCommand.Parameters.AddWithValue("@AdmissionNo", studentEntity.admissionNo);
                    sqlCommand.Parameters.AddWithValue("@Remarks", studentEntity.remarks);
                    sqlCommand.Parameters.Add("@StudentId", SqlDbType.BigInt, 13);
                    sqlCommand.Parameters["@StudentId"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();

                    StudentId = (int)(long)(sqlCommand.Parameters["@StudentId"].Value);
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();

                    //StudentBankEntity bankEntity = new StudentBankEntity();
                    sqlCommand = new SqlCommand();
                    sqlCommand.Transaction = objTrans;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "InsertIntoStudentBankDetails";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@StudentId", StudentId);
                    sqlCommand.Parameters.AddWithValue("@BankName", studentEntity.bankName);
                    sqlCommand.Parameters.AddWithValue("@BankAccNo", studentEntity.bankAccNo);
                    sqlCommand.Parameters.AddWithValue("@IFSCCode", studentEntity.ifscCode);
                    sqlCommand.Parameters.AddWithValue("@BranchName", studentEntity.branchName);
                    sqlCommand.Parameters.AddWithValue("@MICRNo", studentEntity.micrNo);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();

                   // StudentParentInfoEntity parentInfoEntity = new StudentParentInfoEntity();
                    sqlCommand = new SqlCommand();
                    sqlCommand.Transaction = objTrans;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "InsertIntoStudentParentInfo";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@StudentId", StudentId);
                    sqlCommand.Parameters.AddWithValue("@FatherName", studentEntity.fatherName);
                    sqlCommand.Parameters.AddWithValue("@FatherOccupation", studentEntity.fatherOccupation);
                    sqlCommand.Parameters.AddWithValue("@FatherMobileNo", studentEntity.fatherMoileNo);
                    sqlCommand.Parameters.AddWithValue("@FatherQualification", studentEntity.fatherQualification);
                    sqlCommand.Parameters.AddWithValue("@FatherYIncome", studentEntity.fatherYIncome);
                    sqlCommand.Parameters.AddWithValue("@MotherName", studentEntity.motherName);
                    sqlCommand.Parameters.AddWithValue("@MotherOccupation", studentEntity.motherOccupation);
                    sqlCommand.Parameters.AddWithValue("@MotherQualification", studentEntity.motherQualification);
                    sqlCommand.Parameters.AddWithValue("@MotherMobileNo", studentEntity.motherMoileNo);
                    sqlCommand.Parameters.AddWithValue("@MotherYIncome", studentEntity.motherYIncome);
                    sqlCommand.Parameters.AddWithValue("@GuardianName", studentEntity.guardianName);
                    sqlCommand.Parameters.AddWithValue("@GuardianOccupation", studentEntity.guardianOccupation);
                    sqlCommand.Parameters.AddWithValue("@GuardianMobileNo", studentEntity.guardianMobileNo);
                    sqlCommand.Parameters.AddWithValue("@GuardianQualification", studentEntity.guardianQualification);
                    sqlCommand.Parameters.AddWithValue("@TotalYIncome", studentEntity.totalYIncome);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();

                   // StudentDocumentEntity documentEntity = new StudentDocumentEntity();
                    sqlCommand = new SqlCommand();
                    sqlCommand.Transaction = objTrans;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "InsertIntoStudentDocuments";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@StudentId", StudentId);
                    sqlCommand.Parameters.AddWithValue("@ICFilename", studentEntity.incomeCertificateFilename);
                    sqlCommand.Parameters.AddWithValue("@TCFilename", studentEntity.tcFilename);
                    sqlCommand.Parameters.AddWithValue("@BPFilename", studentEntity.bankPassbookFilename);
                    sqlCommand.Parameters.AddWithValue("@DCFilename", studentEntity.declarationFilename);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Dispose();
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
    }
}
