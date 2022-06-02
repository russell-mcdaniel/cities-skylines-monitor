using System;

namespace Insights.Utilities
{
    public static class GuidHelper
    {
        /// <summary>
        /// Converts the string representation of a GUID to the equivalent Guid structure.
        /// </summary>
        /// <remarks>
        /// This method approximates the functionality of the Guid.TryParse() method
        /// added in the .NET Framework 4.0.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "TryParse methods do not throw exceptions.")]
        public static bool TryParse(string input, out Guid? result)
        {
            try
            {
                result = new Guid(input);

                return true;
            }
            catch (Exception)
            {
                result = null;

                return false;
            }
        }
    }
}
