namespace Patrimonium.Domain.Enums
{
    public enum MaintenancePriority
    {
        Low = 1,        // Baixa: A tarefa de manutenção tem baixa prioridade e pode ser agendada para um momento futuro, sem impacto imediato nos processos ou operações.
        Medium = 2,     // Média: A tarefa de manutenção tem prioridade moderada e deve ser abordada em um prazo razoável para evitar possíveis problemas ou atrasos, mas não é crítica no momento.
        High = 3,       // Alta: A tarefa de manutenção tem alta prioridade e deve ser tratada com urgência para evitar impactos significativos nos processos ou operações, exigindo atenção rápida dos técnicos ou equipes responsáveis.
        Critical = 4    // Crítica: A tarefa de manutenção é de extrema importância e requer ação imediata para evitar falhas graves, interrupções significativas ou riscos à segurança, demandando a máxima atenção e recursos para sua resolução.
    }
}