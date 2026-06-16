namespace EnergyMonitorAPI.ViewModels;

public class LeituraRequest
{
    public int EquipamentoId { get; set; }
    public double ConsumoKWh { get; set; }
    public string FonteSensor { get; set; } = "IoT";
}

public class RelatorioResponse
{
    public string Equipamento { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public double TotalConsumoKWh { get; set; }
    public double MediaDiariaKWh { get; set; }
    public int TotalAlertas { get; set; }
    public string StatusEficiencia { get; set; } = string.Empty;
}