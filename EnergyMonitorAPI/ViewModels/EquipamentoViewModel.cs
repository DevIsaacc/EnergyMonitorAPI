namespace EnergyMonitorAPI.ViewModels;

public class EquipamentoRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public double LimiteConsumoKWh { get; set; }
}

public class EquipamentoResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public double LimiteConsumoKWh { get; set; }
    public bool Ativo { get; set; }
}

public class PaginadoResponse<T>
{
    public int Pagina { get; set; }
    public int TotalPaginas { get; set; }
    public int TotalItens { get; set; }
    public List<T> Dados { get; set; } = new();
}