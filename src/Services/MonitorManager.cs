using MonitorWin11.Models;
using MonitorWin11.Monitors;
using MonitorWin11.Util;

namespace MonitorWin11.Services;

public class MonitorManager
{
    private readonly CpuMonitor _cpuMonitor = new();
    private readonly RamMonitor _ramMonitor = new();
    private readonly StorageMonitor _storageMonitor = new();
    private readonly NetMonitor _netMonitor = new();
    
    public MonitorStats Stats { get; } = new();

    public event Action? StatsChanged;

    public void Update()
    {
        _cpuMonitor.Update();
        _ramMonitor.Update();
        _storageMonitor.Update();
        _netMonitor.Update();

        Stats.CpuLoadPercent =
            Math.Round(_cpuMonitor.Load);

        Stats.RamUsedMb =
            ByteConverter.BytesToMb(_ramMonitor.UsedMem);

        Stats.RamFreeMb =
            ByteConverter.BytesToMb(_ramMonitor.FreeMem);

        Stats.RamUsedPercent =
            Math.Round(_ramMonitor.UsedMemPercent);

        Stats.DiskUsedMb =
            ByteConverter.BytesToMb(_storageMonitor.UsedDisk);

        Stats.DiskFreeMb =
            ByteConverter.BytesToMb(_storageMonitor.FreeDisk);

        Stats.DiskUsedPercent =
            Math.Round(_storageMonitor.UsedDiskPercent);

        Stats.TcpConnections = _netMonitor.TcpConnections;

        Stats.UploadKb = Math.Round(_netMonitor.UploadKb);

        Stats.DownloadKb = Math.Round(_netMonitor.DownloadKb);
        
        StatsChanged?.Invoke();
    }
}