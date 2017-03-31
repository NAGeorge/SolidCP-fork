using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SolidCP.Providers
{
    public class LERegistrationResult
    {
        private bool isSuccess;
        private List<string> errorCodes;

        public bool IsSuccess
        {
            get { return isSuccess; }
            set { isSuccess = value; }
        }

        public List<string> ErrorCodes
        {
            get { return errorCodes; }
            set { errorCodes = value; }
        }

        public LERegistrationResult()
        {
            isSuccess = false;
            errorCodes = new List<string>();
        }

        public void AddError(string errorCode, Exception ex)
        {
            if (ex != null)
                errorCode += ":" + ex.Message + "; " + ex.StackTrace;

            this.ErrorCodes.Add(errorCode);
            this.IsSuccess = false;
        }
        public string signer { get; set; }
        public string registrant { get; set; }
        public string keyGenFile { get; set; }
        public string keyPemFile { get; set; }
        public string csrGenFile { get; set; }
        public string csrPemFile { get; set; }
        public string crtDerFile { get; set; }
        public string crtPemFile { get; set; }
        public string crtPfxFile { get; set; }
        public DateTime expiryDate { get; set; }
    }
}
