namespace Patrimonium.Domain.Enums
{
    public enum ClosingStatus
    {
        Open = 1,       // Abertura inicial, sem fechamento
        Generated = 2,  // Fechamento gerado, mas ainda pode ser editado
        Locked = 3,     // Fechamento finalizado, não pode mais ser editado
        Reopened = 4    // Fechamento reaberto para ajustes, mas ainda não finalizado
    }
}
