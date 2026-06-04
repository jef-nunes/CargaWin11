namespace MonitorWin11.Models;

public class MonitorStats
{
    // CPU
    public double CpuLoadPercent { get; set; }

    // RAM
    public double RamUsedMb { get; set; }
    public double RamFreeMb { get; set; }
    public double RamUsedPercent { get; set; }

    // DISK
    public double DiskUsedMb { get; set; }
    public double DiskFreeMb { get; set; }
    public double DiskUsedPercent { get; set; }

    // NETWORK
    public double UploadKb { get; set; }
    public double DownloadKb { get; set; }
    public int TcpConnections { get; set; }
}