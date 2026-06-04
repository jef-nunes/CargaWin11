using System.IO;
using System.Text.Json;

namespace MonitorWin11.Models;

// Classe que auxilia na personalização do app pelo usuário
public class AppSettings
{
    private static readonly string SettingsFilePath =
        Path.Combine(
            AppContext.BaseDirectory,
            "appsettings.json"
        );

    public int MonitorUpdateIntervalMs { get; set; } = 1000;

    public int CurrentThemeIndex { get; set; } = 0;

    public int CurrentFontIndex { get; set; } = 0;

    public bool DisplayCpuCard { get; set; } = true;

    public bool DisplayRamCard { get; set; } = true;

    public bool DisplayStorageCard { get; set; } = true;

    public bool DisplaySpecsCard { get; set; } = true;

    public bool DisplayNetCard { get; set; } = true;
    
    public bool IsDebugMode { get; set; } = false;

    public void PersistSettings()
    {
        string json = JsonSerializer.Serialize(
            this,
            new JsonSerializerOptions
            {
                WriteIndented = true
            }
        );

        File.WriteAllText(SettingsFilePath, json);
    }

    public void LoadSettings()
    {
        // Se o arquivo não exisitr por algum motivo
        if (!File.Exists(SettingsFilePath))
        {
            PersistSettings();
            return;
        }

        try
        {
            string json = File.ReadAllText(SettingsFilePath);

            AppSettings? loaded =
                JsonSerializer.Deserialize<AppSettings>(json);

            if (loaded == null)
            {
                return;
            }

            IsDebugMode = loaded.IsDebugMode;
            
            MonitorUpdateIntervalMs = loaded.MonitorUpdateIntervalMs;
            CurrentThemeIndex = loaded.CurrentThemeIndex;
            CurrentFontIndex = loaded.CurrentFontIndex;

            DisplayCpuCard = loaded.DisplayCpuCard;
            DisplayRamCard = loaded.DisplayRamCard;
            DisplayStorageCard = loaded.DisplayStorageCard;
            DisplaySpecsCard = loaded.DisplaySpecsCard;
            DisplayNetCard = loaded.DisplayNetCard;
        }
        catch (JsonException)
        {
            ResetSettings();
        }
    }

    public void ResetSettings()
    {
        IsDebugMode = false;
        
        MonitorUpdateIntervalMs = 1000;
        CurrentThemeIndex = 0;
        CurrentFontIndex = 0;

        DisplayCpuCard = true;
        DisplayRamCard = true;
        DisplayStorageCard = true;
        DisplaySpecsCard = true;
        DisplayNetCard = true;

        PersistSettings();
    }
}