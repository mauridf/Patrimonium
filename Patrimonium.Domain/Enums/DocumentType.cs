namespace Patrimonium.Domain.Enums
{
    public enum DocumentType
    {
        Contract = 1,       // Contrato de locação, compra e venda, etc.
        Inspection = 2,     // Laudo de vistoria, relatório de inspeção, etc.
        Invoice = 3,        // Faturas, recibos, notas fiscais, etc.
        Tax = 4,            // Documentos relacionados a impostos, como IPTU, ITBI, etc.
        PropertyDeed = 5,   // Escritura do imóvel, certidão de ônus reais, etc.
        Other = 99          // Outros tipos de documentos que não se encaixam nas categorias acima
    }
}