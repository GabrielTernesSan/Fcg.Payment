namespace Fcg.Payment.Common.Responses
{
    public class Response : IResponse
    {
        public List<string> Erros { get; private set; } = [];
        public bool HasErrors => Erros.Count > 0;

        public object? Result { get; private set; }

        public Response SetResult(object result)
        {
            Result = result;
            return this;
        }

        public Response AddError(string erro)
        {
            if (!string.IsNullOrWhiteSpace(erro) && !Erros.Contains(erro))
                Erros.Add(erro);

            return this;
        }

        public Response Append(IResponse response)
        {
            ArgumentNullException.ThrowIfNull(response);

            foreach (var erro in response.Erros)
                AddError(erro);

            return this;
        }

        public override string ToString() => string.Join("; ", Erros);
    }

    public class Response<T> : Response
    {
        public T Result { get; set; }

        public Response() { }

        public Response(T result)
        {
            Result = result;
        }

        public Response<T> SetResult(T result)
        {
            Result = result;
            return this;
        }
    }
}