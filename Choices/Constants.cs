using System.Data;
using Choices;

namespace Choices
{
    internal static class Constants
    {
        internal static readonly string SPREADSHEET_INPUT_RANGE = "responses!B2:AR208";
        internal static readonly string SPREADSHEET_OUTPUT_RANGE = "A1:A1";
        internal static readonly int PERIODS = 6;
        internal static readonly int GREEDY_ALGORITHM_ITERATIONS = 10000;
        internal static readonly string CLASS_DATA_PATH = "choiceData.json";
        internal static readonly string SCIENCE_RECOVERY_LOWER_PATTERN = "science focus";
        internal static readonly string MATH_RECOVERY_LOWER_PATTERN = "math focus";
    }
}