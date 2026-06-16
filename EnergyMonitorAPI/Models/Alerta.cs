namespace EnergyMonitorAPI.Models;

public class Alerta
{
    public int Id { get; set; }
    public int EquipamentoId { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public double ConsumoRegistrado { get; set; }
    public bool Resolvido { get; set; } = false;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public Equipamento Equipamento { get; set; } = null!;
}