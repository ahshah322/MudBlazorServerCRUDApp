using MudBlazor;
using MudBlazorServerCRUD.Model;
using MudBlazorServerCRUD.Pages.Components;

namespace MudBlazorServerCRUD.Pages
{
    public partial class Index
    {
        DataGridFilterCaseSensitivity _caseSensitivity = DataGridFilterCaseSensitivity.CaseInsensitive;
        DataGridEditMode _filterMode = DataGridEditMode.Form;
        private readonly DialogOptions _studentDialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, BackdropClick = false };
        private string _searchString;
        private string _nameSearchFilter = string.Empty;
        private string _emailSearchFilter = string.Empty;
        private Gender _genderSearchFilter;
        private int _marksSearchFilter;
        private bool _sortNameByLength;
        private bool _readOnly = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadStudents();
        }

        private async Task AddStudentAsync(Student student)
        {
            var parameters = new DialogParameters<StudentDialog> { { x => x.Student, student } };

            var dialog = await DialogService.ShowAsync<StudentDialog>("Add Student", parameters, _studentDialogOptions);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await LoadStudents();
            }
        }

        private async Task DeleteStudentAsync(Student student)
        {
            await StudentService.DeleteStudent(student.Id);
            Snackbar.Add("Student deleted successfuly!", Severity.Success);
            await LoadStudents();
        }

        private async Task EditStudentAsync(Student student)
        {
            var parameters = new DialogParameters<StudentDialog> { { x => x.Student, student } };

            var dialog = await DialogService.ShowAsync<StudentDialog>("Edit Student", parameters, _studentDialogOptions);
            var result = await dialog.Result;

            if (!result.Canceled)
            {

                await LoadStudents();
            }
        }

        // custom sort by name length
        private Func<Student, object> _sortBy => x =>
        {
            if (_sortNameByLength)
                return x.Name.Length;
            else
                return x.Name;
        };

        // quick filter - filter globally across multiple columns with the same input
        private Func<Student, bool> _quickFilterStudent => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Email.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if ($"{x.DateOfBirth}".Contains(_searchString))
                return true;

            return false;
        };

        // Filtered items after the custom row filter applied as input
        private IEnumerable<Student> FilteredStudents => StudentService.Students
            .Where(x => string.IsNullOrEmpty(_nameSearchFilter) || x.Name.Contains(_nameSearchFilter, StringComparison.OrdinalIgnoreCase))
            .Where(x => string.IsNullOrEmpty(_emailSearchFilter) || x.Email.Contains(_emailSearchFilter, StringComparison.OrdinalIgnoreCase))
            .Where(x => x.Gender == _genderSearchFilter || _genderSearchFilter == null)
             .Where(x => x.Marks == _marksSearchFilter || _marksSearchFilter == 0);

        private async Task LoadStudents()
        {
            await StudentService.GetStudents();
            await GenderService.GetGenders();
        }
    }
}