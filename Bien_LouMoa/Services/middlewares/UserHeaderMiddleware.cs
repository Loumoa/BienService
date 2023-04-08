namespace Bien_LouMoa.Services.middlewares;

public class UserHeaderMiddleware
{
    public static readonly string ID_KEY = "userId";
    public static readonly string ROLE_KEY = "userRole";
    private readonly RequestDelegate _next;

    public UserHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(ID_KEY, out var idUtilisateur))
        {
            context.Items[ID_KEY] = idUtilisateur.ToString();
        }
        if (context.Request.Headers.TryGetValue(ROLE_KEY, out var roleUtilisateur))
        {
            if (Enum.TryParse(roleUtilisateur.ToString(), out Role role))
            {
                context.Items[ROLE_KEY] = role;
            }
        }

        await _next(context);
    }

    public enum Role
    {
        PROPRIETAIRE,
        LOCATAIRE
    }
}
