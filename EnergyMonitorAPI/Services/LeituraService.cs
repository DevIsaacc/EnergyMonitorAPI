using EnergyMonitorAPI.Data;
using EnergyMonitorAPI.Models;
using EnergyMonitorAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EnergyMonitorAPI.Services;

public class LeituraService
{
    private readonly AppDbContext _db;
    public LeituraService(AppDbContext db) => _db = db;

    public async Task<LeituraConsumo> RegistrarAsync(LeituraRequest req)
    {
        var leitura = new LeituraConsumo
        {
            EquipamentoId = req.EquipamentoId,
            ConsumoKWh = req.ConsumoKWh,
            FonteSensor = req.FonteSensor
        };
        _db.Leituras.Add(leitura);

        var equipamento = await _db.Equipamentos.FindAsync(req.EquipamentoId);
        if (equipamento != null && req.ConsumoKWh > equipamento.LimiteConsumoKWh)
        {
            _db.Alertas.Add(new Alerta
            {
                EquipamentoId = req.EquipamentoId,
                Mensagem = $"Consumo {req.ConsumoKWh} kWh ultrapassa o limite de {equipamento.LimiteConsumoKWh} kWh",
                ConsumoRegistrado = req.ConsumoKWh
            });
        }

        await _db.SaveChangesAsync();
        return leitura;
    }

    public async Task<List<RelatorioResponse>> GerarRelatorioAsync(DateTime inicio, DateTime fim)
    {
        return await _db.Equipamentos
            .Where(e => e.Ativo)
            .Select(e => new RelatorioResponse
            {
                Equipamento = e.Nome,
                Setor = e.Setor,
                TotalConsumoKWh = e.Leituras
                    .Where(l => l.DataLeitura >= inicio && l.DataLeitura <= fim)
                    .Sum(l => l.ConsumoKWh),
                MediaDiariaKWh = e.Leituras
                    .Where(l => l.DataLeitura >= inicio && l.DataLeitura <= fim)
                    .Average(l => (double?)l.ConsumoKWh) ?? 0,
                TotalAlertas = e.Alertas.Count(a => a.CriadoEm >= inicio && a.CriadoEm <= fim),
                StatusEficiencia = e.Leituras
                    .Where(l => l.DataLeitura >= inicio && l.DataLeitura <= fim)
                    .Average(l => (double?)l.ConsumoKWh) <= e.LimiteConsumoKWh ? "Eficiente" : "Atenção"
            }).ToListAsync();
    }
}