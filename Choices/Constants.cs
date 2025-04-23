using System.Data;

namespace Choices
{
    internal static class Constants
    {
        private static readonly string SPREADSHEET_ID = "ID";
        private static readonly string SPREADSHEET_INPUT_RANGE = "A1:A1";
        private static readonly string SPREADSHEET_OUTPUT_RANGE = "A1:A1";

        internal static readonly int PERIODS = 6;
        internal static readonly string SHEETS_READ_ENDPOINT = $"https://sheets.googleapis.com/v4/spreadsheets/{SPREADSHEET_ID}/values/{SPREADSHEET_INPUT_RANGE}";
        internal static readonly string SHEETS_WRITE_ENDPOINT = $"https://sheets.googleapis.com/v4/spreadsheets/{SPREADSHEET_ID}/values/{SPREADSHEET_OUTPUT_RANGE}";
        internal static readonly int GREEDY_ALGORITHM_ITERATIONS = 10000;
    }
}