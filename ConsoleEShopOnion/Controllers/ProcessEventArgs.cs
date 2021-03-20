using System;

namespace OA_UI.Controllers
{
    /// <summary>
    /// Class for processing events arguments.
    /// </summary>
    public class ProcessEventArgs : EventArgs
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Processing events arguments if successful.
        /// </summary>
        public ProcessEventArgs()
        {
            IsSuccessful = true;
            ErrorMessage = "None";
        }

        /// <summary>
        /// Processing events arguments if error.
        /// </summary>
        /// <param name="error">type of error.</param>
        public ProcessEventArgs(string error)
        {
            IsSuccessful = false;
            ErrorMessage = error;
        }
    }
}
