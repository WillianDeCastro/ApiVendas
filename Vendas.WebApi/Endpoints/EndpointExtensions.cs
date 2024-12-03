namespace Vendas.WebApi.Endpoints
{
    public static class EndpointExtensions
    {
        public static void RegistrarEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapVendasEndpoints();
        }
    }
}
