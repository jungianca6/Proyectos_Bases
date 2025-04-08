using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;
using TECBank_BackEnd.Pruebas; // 👈 Asegúrate de tener este namespace si guardaste las clases de prueba aquí
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:3000")  // Ajusta esto según el dominio de tu frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var ClienteA = new ClienteModel
{
    Cedula = "123",
    Nombre = "jorge1",
    Apellido1 = "e",
    Apellido2 = "s",
    Direccion = "S2e",
    Telefono = "8828-8888",
    IngresoMensual = 1210,
    TipoDeCliente = "Pr2mium",
    Usuario = "sapo",
    Contrasena = "32"
};

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuración de CORS (debe ir antes de Authorization)
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthorization(); // Aquí se autoriza el acceso
app.MapControllers(); // Mapea los controladores

// 🟩 Ejecutar pruebas de lectura y escritura de clientes
Console.WriteLine("\n=========== 📝 PRUEBA DE ESCRITURA DE CLIENTES ===========");
PruebaEscrituraClientes pruebaEscrituraClientes = new PruebaEscrituraClientes();
pruebaEscrituraClientes.Ejecutar(ClienteA);

Console.WriteLine("=========== 📖 PRUEBA DE LECTURA DE CLIENTES ===========");
Console.WriteLine("=========== 🔍 FILTRO POR NOMBRE ===========");
PruebaLecturaClientes pruebaLecturaClientes = new PruebaLecturaClientes();
pruebaLecturaClientes.Ejecutar("Nombre", "jorge1");

Console.WriteLine("=========== 🔍 FILTRO POR CÉDULA ===========");
pruebaLecturaClientes.Ejecutar("Cedula", "123");

Console.WriteLine("=========== 🔍 SIN FILTRO (TODOS) ===========");
pruebaLecturaClientes.Ejecutar();

app.Run();
