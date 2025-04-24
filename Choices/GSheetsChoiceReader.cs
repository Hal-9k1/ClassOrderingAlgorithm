using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace Choices
{
    internal class GSheetsChoiceReader : IChoiceReader
    {
        private List<Student> students;
        private Dictionary<Student, StudentChoices> choices;

        internal GSheetsChoiceReader()
        {
            GoogleCredential credential = GoogleCredential.FromFile(Constants.CLIENT_SECRET_PATH).CreateScoped([SheetsService.Scope.Spreadsheets]);
            var sheets = new SheetsService(new BaseClientService.Initializer
            {
                ApplicationName = Constants.GOOGLE_APPLICATION_NAME,
                HttpClientInitializer = credential
            }).Spreadsheets;
            var request = sheets.Get(Constants.SPREADSHEET_ID);
            request.Ranges = new([Constants.SPREADSHEET_INPUT_RANGE]);
            var data = request.Execute();
            var sheet = data.Sheets[0];
            var grid = sheet.Data[0];
            var rows = grid.RowData;
            Console.WriteLine(rows[0].Values[0].EffectiveValue.StringValue);
        }

        public List<Student> GetStudents()
        {
            return students;
        }

        public StudentChoices GetChoices(Student student)
        {
            return choices[student];
        }
    }
}
