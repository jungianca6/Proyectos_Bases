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
    options.AddPolicy("AllowAnyOriginPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var ClienteA = new ClienteModel
{
    Cedula = "1243",
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
app.UseCors("AllowAnyOriginPolicy");

app.UseHttpsRedirection();
app.UseAuthorization(); // Aquí se autoriza el acceso
app.MapControllers(); // Mapea los controladores

// 🟩 Ejecutar pruebas de lectura y escritura de clientes
Console.WriteLine("\n=========== 📝 PRUEBA DE ESCRITURA DE CLIENTES ===========");
PruebaEscrituraClientes pruebaEscrituraClientes = new PruebaEscrituraClientes();
pruebaEscrituraClientes.Ejecutar(ClienteA);

Console.WriteLine("=========== 🔍 FILTRO POR NOMBRE ===========");
PruebaLecturaClientes pruebaLecturaClientes = new PruebaLecturaClientes();

pruebaLecturaClientes.Ejecutar("Nombre", "jorge1");

Console.WriteLine("=========== 🔍 FILTRO POR NOMBRE ===========");
var listaPorNombre = pruebaLecturaClientes.Ejecutar("Nombre", "jorge1");

if (listaPorNombre.Count > 0)
{
    Console.WriteLine("========= Detalles de los Clientes Encontrados por Nombre =========");
    foreach (var cliente in listaPorNombre)
    {
        // Imprimir todos los detalles del cliente
        Console.WriteLine($"Nombre Completo: {cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}");
        Console.WriteLine($"Cédula: {cliente.Cedula}");
        Console.WriteLine($"Teléfono: {cliente.Telefono}");
        Console.WriteLine($"Dirección: {cliente.Direccion}");
        Console.WriteLine($"Ingreso Mensual: {cliente.IngresoMensual}");
        Console.WriteLine($"Tipo de Cliente: {cliente.TipoDeCliente}");
        Console.WriteLine($"Usuario: {cliente.Usuario}");
        Console.WriteLine($"Contraseña: {cliente.Contrasena}");
        Console.WriteLine("------------------------------------------------------------");
    }
}
else
{
    Console.WriteLine("⚠️ No se encontraron clientes con ese nombre.");
}

Console.WriteLine("=========== 🔍 FILTRO POR CÉDULA ===========");
// Filtrar clientes por cédula "1233"
var listaPorCedula = pruebaLecturaClientes.Ejecutar("Cedula", "1233");

if (listaPorCedula.Count > 0)
{
    Console.WriteLine("========= Detalles de los Clientes Encontrados por Cédula =========");
    foreach (var cliente in listaPorCedula)
    {
        // Imprimir todos los detalles del cliente
        Console.WriteLine($"Nombre Completo: {cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}");
        Console.WriteLine($"Cédula: {cliente.Cedula}");
        Console.WriteLine($"Teléfono: {cliente.Telefono}");
        Console.WriteLine($"Dirección: {cliente.Direccion}");
        Console.WriteLine($"Ingreso Mensual: {cliente.IngresoMensual}");
        Console.WriteLine($"Tipo de Cliente: {cliente.TipoDeCliente}");
        Console.WriteLine($"Usuario: {cliente.Usuario}");
        Console.WriteLine($"Contraseña: {cliente.Contrasena}");
        Console.WriteLine("------------------------------------------------------------");
    }
}
else
{
    Console.WriteLine("⚠️ No se encontró un cliente con esa cédula.");
}
Console.WriteLine("===========  🔍 sin filtro ===========");

var listaTodos = pruebaLecturaClientes.Ejecutar();

if (listaTodos.Count > 0)
{
    Console.WriteLine("========= Detalles de Todos los Clientes =========");
    foreach (var cliente in listaTodos)
    {
        // Imprimir todos los detalles del cliente
        Console.WriteLine($"Nombre Completo: {cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}");
        Console.WriteLine($"Cédula: {cliente.Cedula}");
        Console.WriteLine($"Teléfono: {cliente.Telefono}");
        Console.WriteLine($"Dirección: {cliente.Direccion}");
        Console.WriteLine($"Ingreso Mensual: {cliente.IngresoMensual}");
        Console.WriteLine($"Tipo de Cliente: {cliente.TipoDeCliente}");
        Console.WriteLine($"Usuario: {cliente.Usuario}");
        Console.WriteLine($"Contraseña: {cliente.Contrasena}");
        Console.WriteLine("------------------------------------------------------------");
    }
}
else
{
    Console.WriteLine("⚠️ No se encontraron clientes.");
}
Console.WriteLine("=========== ✏️ CAMBIO PARCIAL DE CLIENTE ===========");

PruebaEditarClientes pruebaEditarClientes = new PruebaEditarClientes();

var cambiosParciales = new ClienteModel
{
    Telefono = "6000-0000",
    Direccion = "Nueva Casa en Cartago"
    // Los demás campos los dejamos vacíos
};

pruebaEditarClientes.EditarClienteParcial("1233", cambiosParciales);


Console.WriteLine("=========== 🔍 SIN FILTRO (TODOS) ===========");
pruebaLecturaClientes.Ejecutar();

Console.WriteLine("=========== ❌ PRUEBA DE ELIMINACIÓN ===========");
PruebaEliminacionClientes pruebaEliminacionClientes = new PruebaEliminacionClientes();
pruebaEliminacionClientes.EliminarPorCedula("12332");

Console.WriteLine("=========== 🔍 SIN FILTRO (TODOS) ===========");
pruebaLecturaClientes.Ejecutar();


app.Run();
