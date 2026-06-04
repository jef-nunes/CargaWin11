using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MonitorWin11.Ui.Formatting;
using MonitorWin11.Models;
using MonitorWin11.Services;

namespace MonitorWin11.Ui.Blazor.Components;

public partial class Dashboard : ComponentBase, IDisposable
{
    [Inject] protected MonitorManager MonitorManager { get; set; } = null!;

    [Inject] protected SystemSpecs SystemSpecs { get; set; } = null!;

    [Inject] protected AppSettings AppSettings { get; set; } = null!;

    [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;

    private double _lastDiskUsedPercent = 0d;
    
    // Overrides
    
    protected override async Task OnInitializedAsync()
    {
        MonitorManager.StatsChanged += OnStatsChanged;
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        await OnStart();
    }
    
    // Métodos adicionais
    
    private async Task OnStart()
    {
        Console.WriteLine("OnStart()");
        await LoadUserPreferences();
    }

    private async void OnStatsChanged()
    {
        await InvokeAsync(OnStatsChangedAsync);
    }

    private async Task OnStatsChangedAsync()
    {
        await UpdateCharts();
        if (Math.Abs(MonitorManager.Stats.DiskUsedPercent - _lastDiskUsedPercent) > 0.1)
        {
            await UpdateStorageChart();
            _lastDiskUsedPercent = MonitorManager.Stats.DiskUsedPercent;
        }
        await InvokeAsync(StateHasChanged);
    }

    private async Task UpdateStorageChart()
    {
        await JsRuntime.InvokeVoidAsync(
            "updateStorageChart",
            MonitorManager.Stats.DiskUsedPercent,
            ColorManager.CssVariableFromUsageLevel(MonitorManager.Stats.DiskUsedPercent)    
                
        );    
    }

    private async Task UpdateCharts()
    {
        await JsRuntime.InvokeVoidAsync(
            "updateChart",
            "cpu-load-chart-fill",
            ColorManager.CssVariableFromUsageLevel(MonitorManager.Stats.CpuLoadPercent),
                MonitorManager.Stats.CpuLoadPercent
        );

        await JsRuntime.InvokeVoidAsync(
            "updateChart",
            "ram-usage-chart-fill",
            ColorManager.CssVariableFromUsageLevel(MonitorManager.Stats.RamUsedPercent),
                MonitorManager.Stats.RamUsedPercent
        );
    }

    public void Dispose()
    {
        MonitorManager.StatsChanged -= OnStatsChanged;
    }

    private async Task ChangeTheme()
    {
        await JsRuntime.InvokeVoidAsync("useNextTheme");
        await UpdateStorageChart();
        await UpdateCharts();
    }

    private async Task ChangeFont()
    {
        await JsRuntime.InvokeVoidAsync("useNextFont");
        await UpdateStorageChart();
        await UpdateCharts();
    }

    private async Task CustomisationDemo()
    {
        await ChangeTheme();
        await ChangeFont();
    }

    private async Task LoadUserPreferences()
    {
        Console.WriteLine("LoadUserPreferences()");
        
        // Carrega as preferências do usuário do arquivo appsettings.json
        AppSettings.LoadSettings();

        // Aplicar as preferências no frontend
        await JsRuntime.InvokeVoidAsync(
            "applyUserPreferences",
            AppSettings.CurrentThemeIndex,
            AppSettings.CurrentFontIndex,
                AppSettings.DisplaySpecsCard,
                AppSettings.DisplayCpuCard,
                AppSettings.DisplayRamCard,
                AppSettings.DisplayStorageCard,
                AppSettings.DisplayNetCard
        );
        
        // Atualizar gráficos
        await UpdateCharts();
        await UpdateStorageChart();
        
        StateHasChanged();
    }

    private async Task SaveUserPreferences()
    {
        int currentFontIndex = await JsRuntime.InvokeAsync<int>(
            "getCurrentFontIndex"
        );
        AppSettings.CurrentFontIndex = currentFontIndex;

        int currentThemeIndex = await JsRuntime.InvokeAsync<int>(
            "getCurrentThemeIndex"
        );
        AppSettings.CurrentThemeIndex = currentThemeIndex;
        
        AppSettings.PersistSettings();
    }

    private async Task ResetUserPreferences()
    {
        AppSettings.ResetSettings();
        await LoadUserPreferences();
    }

    private async Task ToggleSpecsCard()
    {
        await JsRuntime.InvokeVoidAsync("toggleDisplaySpecsCard");
        await JsRuntime.InvokeVoidAsync("updateCardDisplaying");
    }

    private async Task ToggleCpuCard()
    {
        await JsRuntime.InvokeVoidAsync("toggleDisplayCpuCard");
        await JsRuntime.InvokeVoidAsync("updateCardDisplaying");
    }

    private async Task ToggleRamCard()
    {
        await JsRuntime.InvokeVoidAsync("toggleDisplayRamCard");
        await JsRuntime.InvokeVoidAsync("updateCardDisplaying");
    }

    private async Task ToggleStorageCard()
    {
        await JsRuntime.InvokeVoidAsync("toggleDisplayStorageCard");
        await JsRuntime.InvokeVoidAsync("updateCardDisplaying");
    }

    private async Task ToggleNetCard()
    {
        await JsRuntime.InvokeVoidAsync("toggleDisplayNetCard");
        await JsRuntime.InvokeVoidAsync("updateCardDisplaying");
    }
}