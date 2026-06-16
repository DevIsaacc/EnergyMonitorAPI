namespace EnergyMonitorAPI.Models;

public class LeituraConsumo
{
    public int Id { get; set; }
    public int EquipamentoId { get; set; }
    public double ConsumoKWh { get; set; }
    public DateTime DataLeitura { get; set; } = DateTime.UtcNow;
    public string FonteSensor { get; set; } = "IoT";

    public Equipamento Equipamento { get; set; } = null!;
}