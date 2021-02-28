using System;
using Xunit;
using TechnicalTest;
using TechnicalTest.Controllers;

namespace Tests
{
    public class PatientTest
    {
        [Fact]
        public void IsExistPatientInfo_ReturnFalse()
        {
            ProcPatientInfo procPatientInfo = new ProcPatientInfo();
            var result = procPatientInfo.IsExistPatientInfo("10");
            Assert.False(result, "Not Exist");
        }

        [Fact]
        public void IsExistPatientInfo_ReturnTrue()
        {
            ProcPatientInfo procPatientInfo = new ProcPatientInfo();
            var result = procPatientInfo.IsExistPatientInfo("1");
            Assert.True(result, "Exist");
        }

        [Theory]
        [InlineData("5", "6")]
        [InlineData("4", "2")]
        [InlineData("3", "1")]
        public void IsExistAllergyInfo_ReturnFalse(string patientID, string medicationID)
        {
            ProcPatientInfo procPatientInfo = new ProcPatientInfo();
            var result = procPatientInfo.IsExistAllergyInfo(patientID, medicationID);
            Assert.False(result, "Not Exist");
        }
        [Fact]
        public void InsertPatientInfo()
        {
            ProcPatientInfo procPatientInfo = new ProcPatientInfo();
            PatientDetails patientinfo = new PatientDetails();
            patientinfo.PatientID = "4";
            patientinfo.FirstName = "Richard";
            patientinfo.LastName = "Elgar";
            patientinfo.DOB = "1953-12-12";
            patientinfo.Gender = "Male";
            patientinfo.HeightCms = "180";
            patientinfo.WeightKgs = "83.2";
           
            int result = procPatientInfo.CreatePatientInfo(patientinfo);
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalBMIAndInsertDB_ReturnTrue()
        {
            ProcPatientInfo procPatientInfo = new ProcPatientInfo();
            PatientDetails patientinfo = new PatientDetails();
            patientinfo.PatientID = "4";
            patientinfo.FirstName = "Richard";
            patientinfo.LastName = "Elgar";
            patientinfo.DOB = "1953-12-12";
            patientinfo.Gender = "Male";
            patientinfo.HeightCms = "180";
            patientinfo.WeightKgs = "83.2";

            int result = procPatientInfo.CalBMIAndInsertDB(patientinfo);
            Assert.Equal(1, result);
        }

        [Fact]
        public void CalBMIAndInsertDB_ReturnFalse()
        {
            ProcPatientInfo procPatientInfo = new ProcPatientInfo();
            PatientDetails patientinfo = new PatientDetails();
            patientinfo.PatientID = "4";
            patientinfo.FirstName = "Richard";
            patientinfo.LastName = "Elgar";
            patientinfo.DOB = "1953-12-12";
            patientinfo.Gender = "Male";
            patientinfo.HeightCms = "0";
            patientinfo.WeightKgs = "0";

            int result = procPatientInfo.CalBMIAndInsertDB(patientinfo);
            Assert.Equal(0, result);
        }
    }
}
