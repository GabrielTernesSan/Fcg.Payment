namespace Fcg.Payment.Common.Responses
{
    public interface IResponse
    {
        List<string> Erros { get; }
        bool HasErrors { get; }
    }
}