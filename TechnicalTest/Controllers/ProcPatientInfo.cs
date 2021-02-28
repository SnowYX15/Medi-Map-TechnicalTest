using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTest.Models;

namespace TechnicalTest.Controllers
{
    public class ProcPatientInfo
    {
        public ProcPatientInfo() { }

        /// <summary>
        /// Process the Patient Information
        /// </summary>
        /// <param name="patientList"> List of patients' information</param>
        /// <returns>Returned messages </returns>
        public string ProcessingPatientInfo(List<PatientDetails> patientList) 
        {
            string result = "";
            foreach (PatientDetails patient in patientList) 
            {
                if (!IsExistPatientInfo(patient.PatientID)) 
                {
                    CreatePatientInfo(patient);
                } 
                
                CalBMIAndInsertDB(patient);

                if(IsExistAllergyInfo(patient.PatientID,patient.MedicationID))
                {
                    result += String.Format("{0} {1} has severe allergy to medication({2}). ",patient.FirstName,patient.LastName,patient.MedicationID);
                }
            }
            return result;
        }
        /// <summary>
        /// Check patient's information is in DB or not.
        /// </summary>
        /// <param name="patientID"> Patient ID</param>
        /// <returns>Returned True or False </returns>
        public bool IsExistPatientInfo(string patientID)
        {           
                string CheckSql = String.Format("select * from Patient where PatientID={0}", patientID);
                return DBHelperSQL.Exists(CheckSql);           
        }

        /// <summary>
        /// Check the patient's allergy history.
        /// </summary>
        /// <param name="patientID"> Patient ID</param>
        /// <param name="medicationID"> Medication ID</param>
        /// <returns>Returned True or False </returns>
        public bool IsExistAllergyInfo(string patientID,string medicationID)
        {
            string CheckSql = String.Format("select * from AllergyInfo where PatientID={0} AND MedicationID={1}", patientID,medicationID);
            return DBHelperSQL.Exists(CheckSql);
        }

        /// <summary>
        /// Insert patient's information into DB.
        /// </summary>
        /// <param name="value"> Patient's information</param>
        /// <returns> The return value is 1 for success </returns>
        public int CreatePatientInfo(PatientDetails value) 
        {
            try
            {
                string InsertSql = String.Format(@"INSERT INTO dbo.Patient(PatientID,FirstName,LastName,Gender,DOB,HeightCms,WeightKgs)
                                    VALUES({0},'{1}','{2}','{3}','{4}',{5},{6})", value.PatientID, value.FirstName, value.LastName, value.Gender, value.DOB, value.HeightCms, value.WeightKgs);
                return DBHelperSQL.ExecuteSql(InsertSql);
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Calculation BMI and Insert into DB.
        /// </summary>
        /// <param name="value"> Patient's information</param>
        /// <returns> The return value is 1 for success </returns>
        public int CalBMIAndInsertDB(PatientDetails value)
        {
            try
            {
                //Check data valid or not
                if (double.Parse(value.HeightCms) > 0 && double.Parse(value.WeightKgs) > 0)
                {
                    string InsertSql = String.Format(@"INSERT INTO dbo.MedicationAdministration(PatientID,Created,BMI)
                                    VALUES({0},GETDATE(),{1}/({2}*0.01*{2}*0.01))", value.PatientID, value.WeightKgs, value.HeightCms);
                    return DBHelperSQL.ExecuteSql(InsertSql);
                }
                else
                {
                    string result = String.Format("The patient's height or weight information is wrong. PatientID:{0}", value.PatientID);
                    ErrorLog(result);
                    return 0;
                }

            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Log error messages
        /// </summary>
        /// <param name="error"> Error Messages</param>
        private void ErrorLog(string error)
        {         
            string InsertSql = String.Format(@"INSERT INTO dbo.ErrorLog(ErrorMessage)VALUES ('{0}')", error.Replace("'", "''"));
            DBHelperSQL.ExecuteSql(InsertSql);
        }
    }
}
