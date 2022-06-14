using Portafolio.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Configuramos la clase Reposit... en el sistema de inyeccion de dependencias
builder.Services.AddTransient<IRepositorioProyectos, RepositorioProyectos>();
builder.Services.AddTransient<IServicioCorreo, ServicioContacto>();

/*En este caso cuando la clase que instanciamos en el sistema de inyeccion
de dependencias no necesita compartir datos con otras instancias usamos 
AddTransient, cuando necesitamos compartir datos con otras instancias en
diferentes solicitudes HTTP usamos AddSingelton y si necesitamos compartir 
los datos con otras instancias pero dentro de la misma peticion HTTP
usamos AddScoped*/

/*Ademas, cuando utilizamos varios servivicos transitorios dentro de la misma
inyecccion de dependencias, cada uno de estos servicios va a tener su propia
instanciacion y por ende todos van a ser diferentes cuando recarguemos y 
aunque estemos en la misma peticion HTTP*/

/*Cuando usamos servicios delimitados al refrescar el proyecto se va a generar
una nueva instancia del servicio, pero siempre va a ser la misma para todos
los servicios delimitados dentro de la misma peticion HTTP*/

/*Cuando usamos servicios unicos, siempre se va a usar la misma instancia
para todos los servicios sin importar la peticion HTTP y esta instancia
no va a cambiar aunque refresquemos el proyecto*/

/*builder.Services.AddTransient<ServicioTransitorio>();
builder.Services.AddScoped<ServicioDelimitado>();
builder.Services.AddSingleton<ServicioUnico>();*/

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Este es el routeo convencional
app.MapControllerRoute (
    name: "default", // Ruta por defecto de nuestra aplicacion
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();