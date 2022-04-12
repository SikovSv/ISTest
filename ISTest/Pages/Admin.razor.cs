namespace ISTest.Pages;

public partial class Admin
{
    [Parameter]
    public string AdminKey { get; set; }

    [Inject]
    protected IConfiguration Configuration { get; set; }

    protected bool IsAccess { get; set; }

    protected override void OnParametersSet()
    {
        IsAccess = Configuration["AdminKey"] == AdminKey;
    }    
}
