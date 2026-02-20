namespace Patrimonium.Domain.Enums
{
    public enum InspectionType
    {
        Entry = 1,      // Entrada: Inspeção realizada no início do contrato, geralmente durante a entrega do imóvel ao inquilino. O objetivo é documentar o estado do imóvel, identificando quaisquer danos ou problemas existentes antes que o inquilino ocupe o imóvel.
        Exit = 2,       // Saída: Inspeção realizada no final do contrato, geralmente durante a desocupação do imóvel pelo inquilino. O objetivo é comparar o estado do imóvel com a inspeção de entrada para identificar quaisquer danos ou problemas causados durante a ocupação.
        Periodic = 3,   // Periódica: Inspeção realizada em intervalos regulares durante a vigência do contrato, independentemente da entrada ou saída do inquilino. O objetivo é monitorar o estado do imóvel ao longo do tempo, identificando quaisquer problemas ou necessidades de manutenção que possam surgir.
        Technical = 4   // Técnica: Inspeção realizada por um profissional técnico, como um engenheiro ou arquiteto, para avaliar aspectos específicos do imóvel, como estrutura, instalações elétricas, hidráulicas, entre outros. O objetivo é garantir a segurança e a conformidade do imóvel com as normas técnicas.
    }
}