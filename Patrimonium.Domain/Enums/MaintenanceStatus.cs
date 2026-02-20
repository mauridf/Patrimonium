namespace Patrimonium.Domain.Enums
{
    public enum MaintenanceStatus
    {
        Open = 1,           // Aberto: O status inicial de uma solicitação de manutenção, indicando que a tarefa foi registrada, mas ainda não foi iniciada.
        InProgress = 2,     // Em Progresso: Indica que a tarefa de manutenção está atualmente sendo trabalhada, ou seja, os técnicos estão ativamente envolvidos na execução da manutenção.
        Completed = 3,      // Concluído: Indica que a tarefa de manutenção foi finalizada com sucesso, ou seja, os técnicos completaram as atividades necessárias para resolver o problema ou realizar a manutenção.
        Cancelled = 4       // Cancelado: Indica que a solicitação de manutenção foi cancelada, seja por decisão do solicitante, por mudanças nas prioridades ou por outros motivos que levaram à desistência da tarefa.
    }
}