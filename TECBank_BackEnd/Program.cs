using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// 🟩 Leer clientes desde el archivo JSON
var clientes = Holas.LeerClientes();

// 🟨 Mostrar en consola los clientes leídos
Console.WriteLine("📄 Clientes leídos desde el archivo JSON:");
if (clientes.Count == 0)
{
    Console.WriteLine("⚠️ No se encontraron clientes en el archivo JSON.");
}
else
{
    foreach (var cliente in clientes)
    {
        Console.WriteLine($"➡️ {cliente.Nombre} {cliente.Apellido1} | Usuario: {cliente.Usuario}");
    }
}

// 🟥 Agregar cliente si no existe aún (para no duplicar)
// Leer clientes desde el archivo

// Verificar si el cliente con la cédula no existe
// Verificar si el cliente con la cédula ya existe en la lista
string cedulaNueva = "11111111111";  // La cédula del cliente que deseas agregar

if (!clientes.Any(c => c.Cedula == cedulaNueva))  // Verifica si ya existe la cédula
{
    // Agregar un nuevo cliente con datos diferentes
    var nuevoCliente = new ClienteModel
    {
        Cedula = cedulaNueva,  // Cédula diferente
        Nombre = "perra",
        Apellido1 = "e",
        Apellido2 = "s",
        Direccion = "S2e",
        Telefono = "8828-8888",
        IngresoMensual = 1210,
        TipoDeCliente = "Pr2mium",
        Usuario = "sapo",
        Contrasena = "32"
    };

    // Añadir al listado de clientes
    clientes.Add(nuevoCliente);

    // Guardar los cambios en el archivo JSON
    Holas.GuardarClientes(clientes);

    // Confirmar que el cliente se agregó
    Console.WriteLine("✅ Cliente agregado y guardado en el archivo JSON.");
}
else
{
    // Si el cliente ya existe, mostrar un mensaje
    Console.WriteLine("⚠️ Ya existe un cliente con esta cédula en el archivo JSON.");
}


app.Run();
