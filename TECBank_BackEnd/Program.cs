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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 🟩 Ejecutar pruebas de lectura y escritura de clientes


Console.WriteLine("\n=========== 📝 PRUEBA DE ESCRITURA DE CLIENTES ===========");
PruebaEscrituraClientes.Ejecutar(ClienteA);
Console.WriteLine("=========== 📖 PRUEBA DE LECTURA DE CLIENTES ===========");
Console.WriteLine("=========== 🔍 FILTRO POR NOMBRE ===========");
PruebaLecturaClientes.Ejecutar("Nombre", "jorge1");

Console.WriteLine("=========== 🔍 FILTRO POR CÉDULA ===========");
PruebaLecturaClientes.Ejecutar("Cedula", "123");

Console.WriteLine("=========== 🔍 SIN FILTRO (TODOS) ===========");
PruebaLecturaClientes.Ejecutar();

app.Run();
