using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class JasonLectura
    {
        public List<ClienteModel> Ejecutar(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();

            return filtro switch
            {
                "Nombre" => clientes.Where(c => c.Nombre == valor).ToList(),
                "Cedula" => clientes.Where(c => c.Cedula == valor).ToList(),
                "Apellido1" => clientes.Where(c => c.Apellido1 == valor).ToList(),
                "Apellido2" => clientes.Where(c => c.Apellido2 == valor).ToList(),
                "Direccion" => clientes.Where(c => c.Direccion == valor).ToList(),
                "Telefono" => clientes.Where(c => c.Telefono == valor).ToList(),
                "IngresoMensual" => clientes.Where(c => c.IngresoMensual.ToString() == valor).ToList(),
                "TipoDeCliente" => clientes.Where(c => c.TipoDeCliente == valor).ToList(),
                "Usuario" => clientes.Where(c => c.Usuario == valor).ToList(),
                "Contrasena" => clientes.Where(c => c.Contrasena == valor).ToList(),
                "AdminRol" =>
                    bool.TryParse(valor, out var parsedValor)
                        ? clientes.Where(c => c.AdminRol == parsedValor).ToList()
                        : new List<ClienteModel>(),

                _ => clientes
            };
        }

        public List<CuentaModel> LeerCuentas(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();

            return filtro switch
            {
                "Nombre" => cuentas.Where(c => c.Nombre == valor).ToList(),
                "Monto" => int.TryParse(valor, out int monto) ? cuentas.Where(c => c.Monto == monto).ToList() : new List<CuentaModel>(),
                "NumeroDeCuenta" => cuentas.Where(c => c.NumeroDeCuenta == valor).ToList(),
                "Descripcion" => cuentas.Where(c => c.Descripcion == valor).ToList(),
                "Usuario" => cuentas.Where(c => c.Usuario == valor).ToList(),
                "Moneda" => cuentas.Where(c => c.Moneda == valor).ToList(),
                "TipoDeCuenta" => cuentas.Where(c => c.TipoDeCuenta == valor).ToList(),
                _ => cuentas
            };
        }

        public List<TarjetaModel> LeerTarjetas(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var tarjetas = json.LeerTarjetas();

            return filtro switch
            {
                "Numero" => tarjetas.Where(t => t.Numero == valor).ToList(),
                "NumeroDeCuenta" => tarjetas.Where(t => t.NumeroDeCuenta == valor).ToList(),
                "TipoDeTarjeta" => tarjetas.Where(t => t.TipoDeTarjeta == valor).ToList(),
                "CCV" => tarjetas.Where(t => t.CCV == valor).ToList(),
                "SaldoDisponible" => tarjetas.Where(t => t.SaldoDisponible.ToString() == valor).ToList(),
                "FechaDeExpiracion" => tarjetas.Where(t => t.FechaDeExpiracion == valor).ToList(),
                _ => tarjetas
            };
        }



        public List<EmpleadoModel> LeerEmpleados(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var empleados = json.LeerEmpleados();

            return filtro switch
            {
                "Cedula" => empleados.Where(e => e.Cedula == valor).ToList(),
                "Nombre" => empleados.Where(e => e.Nombre == valor).ToList(),
                "Apellido1" => empleados.Where(e => e.Apellido1 == valor).ToList(),
                "Apellido2" => empleados.Where(e => e.Apellido2 == valor).ToList(),
                "Rol" => empleados.Where(e => e.Rol == valor).ToList(),
                "DescripcionDeRol" => empleados.Where(e => e.DescripcionDeRol == valor).ToList(),
                "IngresoMensual" => empleados.Where(c => c.IngresoMensual.ToString() == valor).ToList(),

                "FechaDeNacimiento" => empleados.Where(e => e.FechaDeNacimiento.ToString() == valor).ToList(),
                "Usuario" => empleados.Where(e => e.Usuario == valor).ToList(),
                "Contrasena" => empleados.Where(e => e.Contrasena == valor).ToList(),
                "AdminRol" =>
                    bool.TryParse(valor, out var parsedValor)
                        ? empleados.Where(c => c.AdminRol == parsedValor).ToList()
                        : new List<EmpleadoModel>(),
                _ => empleados
            };
        }

        public List<PagoModel> LeerPagos(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var pagos = json.LeerPagos();

            return filtro switch
            {
                "ID" => pagos.Where(p => p.ID == valor).ToList(),
                "Apellido1" => pagos.Where(p => p.Apellido1 == valor).ToList(),
                "Apellido2" => pagos.Where(p => p.Apellido2 == valor).ToList(),
                "Monto" => pagos.Where(p => p.Monto.ToString() == valor).ToList(),
                "Moneda" => pagos.Where(p => p.Moneda == valor).ToList(),
                "Fecha" => pagos.Where(p => p.Fecha == valor).ToList(),
                "Cuenta_Emisora" => pagos.Where(p => p.Cuenta_Emisora == valor).ToList(),
                "Numero_de_Tarjeta" => pagos.Where(p => p.Numero_de_Tarjeta == valor).ToList(),
                _ => pagos
            };
        }

        public List<RetiroModel> LeerRetiros(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var retiros = json.LeerRetiros();

            return filtro switch
            {
                "ID" => retiros.Where(r => r.ID == valor).ToList(),
                "CuentaARetirar" => retiros.Where(r => r.CuentaARetirar == valor).ToList(),
                "Apellido1" => retiros.Where(r => r.Apellido1 == valor).ToList(),
                "Apellido2" => retiros.Where(r => r.Apellido2 == valor).ToList(),
                "Monto" => int.TryParse(valor, out int monto)
                            ? retiros.Where(r => r.Monto == monto).ToList()
                            : new List<RetiroModel>(), // si no se puede convertir, devuelve vacío
                "Moneda" => retiros.Where(r => r.Moneda == valor).ToList(),
                "Fecha" => retiros.Where(r => r.Fecha == valor).ToList(),
                _ => retiros
            };
        }
        public List<TransferenciaModel> LeerTransferencias(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var transferencias = json.LeerTransferencias();

            return filtro switch
            {
                "ID" => transferencias.Where(t => t.ID == valor).ToList(),
                "Cuenta_Emisora" => transferencias.Where(t => t.Cuenta_Emisora == valor).ToList(),
                "Cuenta_Receptora" => transferencias.Where(t => t.Cuenta_Receptora == valor).ToList(),
                "Monto" => int.TryParse(valor, out int monto)
                            ? transferencias.Where(t => t.Monto == monto).ToList()
                            : new List<TransferenciaModel>(), // Si no es un valor numérico, devuelve vacío
                "Moneda" => transferencias.Where(t => t.Moneda == valor).ToList(),
                "Fecha" => transferencias.Where(t => t.Fecha == valor).ToList(),
                "Apellido1" => transferencias.Where(t => t.Apellido1 == valor).ToList(),
                "Apellido2" => transferencias.Where(t => t.Apellido2 == valor).ToList(),
                _ => transferencias
            };
        }

        public List<PrestamoModel> LeerPrestamos(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var prestamos = json.LeerPrestamos();

            return filtro switch
            {
                "Monto_Original" => int.TryParse(valor, out int montoOriginal)
                    ? prestamos.Where(p => p.Monto_Original == montoOriginal).ToList()
                    : new List<PrestamoModel>(),

                "Saldo_Pendiente" => int.TryParse(valor, out int saldoPendiente)
                    ? prestamos.Where(p => p.Saldo_Pendiente == saldoPendiente).ToList()
                    : new List<PrestamoModel>(),

                "Cedula_Cliete" => prestamos.Where(p => p.Cedula_Cliete == valor).ToList(),

                "Tasa_De_Interes" => decimal.TryParse(valor, out decimal tasa)
                    ? prestamos.Where(p => p.Tasa_De_Interes == tasa).ToList()
                    : new List<PrestamoModel>(),

                "ID_Prestamos" => prestamos.Where(p => p.ID_Prestamos == valor).ToList(),


                "FechaVencimiento" => prestamos.Where(p => p.FechaVencimiento == valor).ToList(),

                _ => prestamos
            };
        }

        public List<PagoPrestamoModel> LeerPagosPrestamo(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var pagosPrestamo = json.LeerPagosPrestamo();

            return filtro switch
            {
                "ID" => pagosPrestamo.Where(p => p.ID == valor).ToList(),
                "Apellido1" => pagosPrestamo.Where(p => p.Apellido1 == valor).ToList(),
                "Apellido2" => pagosPrestamo.Where(p => p.Apellido2 == valor).ToList(),
                "Monto" => pagosPrestamo.Where(p => p.Monto.ToString() == valor).ToList(),
                "Moneda" => pagosPrestamo.Where(p => p.Moneda == valor).ToList(),
                "Fecha" => pagosPrestamo.Where(p => p.Fecha == valor).ToList(),
                "CuentaEmisora" => pagosPrestamo.Where(p => p.CuentaEmisora == valor).ToList(),
                "IdPrestamo" => pagosPrestamo.Where(p => p.IdPrestamo == valor).ToList(),
                _ => pagosPrestamo
            };
        }



        public List<CalendarioPagoModel> LeerCalendariosPago(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var calendarios = json.LeerCalendarioPagos();

            return filtro switch
            {
                "ID_Prestamo" => calendarios.Where(c => c.ID_Prestamo == valor).ToList(),
                "FechaVencimiento" => calendarios.Where(c => c.FechaVencimiento == valor).ToList(),
                "SaldoPendiente" => int.TryParse(valor, out int saldo)
                                        ? calendarios.Where(c => c.SaldoPendiente == saldo).ToList()
                                        : new List<CalendarioPagoModel>(),

                // Filtro especial por mes dentro de cuotas
                "Mes" => calendarios
                            .Where(c => c.CuotasMensuales.Any(cm => cm.Mes == valor))
                            .ToList(),

                // Filtro especial por año
                "Anio" => int.TryParse(valor, out int anio)
                            ? calendarios.Where(c => c.CuotasMensuales.Any(cm => cm.Anio == anio)).ToList()
                            : new List<CalendarioPagoModel>(),

                // Filtro especial por fecha de pago
                "FechaPago" => calendarios
                                .Where(c => c.CuotasMensuales.Any(cm => cm.FechaPago == valor))
                                .ToList(),

                // Filtro por monto a pagar
                "MontoAPagar" => int.TryParse(valor, out int monto)
                                    ? calendarios.Where(c => c.CuotasMensuales.Any(cm => cm.MontoAPagar == monto)).ToList()
                                    : new List<CalendarioPagoModel>(),
                "Pagado" => bool.TryParse(valor, out bool pagado)
                ? calendarios.Where(c => c.CuotasMensuales.Any(cm => cm.Pagado == pagado)).ToList()
                : new List<CalendarioPagoModel>(),






                _ => calendarios
            };
        }


        public ClienteModel? BuscarPorUsuario(string usuario)
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();

            return clientes.FirstOrDefault(c =>
                c.Usuario?.Equals(usuario, StringComparison.OrdinalIgnoreCase) == true
            );
        }

        public ClienteModel? BuscarPorCedula(string cedula)
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();

            return clientes.FirstOrDefault(c =>
                c.Cedula?.Equals(cedula, StringComparison.OrdinalIgnoreCase) == true
            );
        }
        public EmpleadoModel? BuscarEmpleadoPorCedula(string cedula)
        {
            Jason json = new Jason();
            var empleados = json.LeerEmpleados();

            return empleados.FirstOrDefault(c =>
                c.Cedula?.Equals(cedula, StringComparison.OrdinalIgnoreCase) == true
            );
        }

        public CuentaModel? BuscarCuentaPorUsuario(string usuario)
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();

            return cuentas.FirstOrDefault(c =>
                c.Usuario?.Equals(usuario, StringComparison.OrdinalIgnoreCase) == true
            );
        }

        public CuentaModel? BuscarCuentaPorNumero(string numero_de_cuenta)
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();

            return cuentas.FirstOrDefault(c =>
                c.NumeroDeCuenta?.Equals(numero_de_cuenta, StringComparison.OrdinalIgnoreCase) == true
            );
        }

        public TarjetaModel? BuscarTarjetaPorNumero(string numero_tarjeta)
        {
            Jason json = new Jason();
            var tarjetas = json.LeerTarjetas();

            return tarjetas.FirstOrDefault(t =>
                t.Numero?.Equals(numero_tarjeta, StringComparison.OrdinalIgnoreCase) == true
            );
        }

        public PrestamoModel? BuscarPrestamoPorId(string id_prestamo)
        {
            Jason json = new Jason();
            var tarjetas = json.LeerPrestamos();

            return tarjetas.FirstOrDefault(t =>
                t.ID_Prestamos?.Equals(id_prestamo, StringComparison.OrdinalIgnoreCase) == true
            );
        }


        public List<PagoModel> LeerPagosPorFechaYCuenta(string fechaInicio, string fechaFin, string numeroCuenta)
        {
            Jason json = new Jason();
            var pagos = json.LeerPagos();

            // Intentamos convertir las fechas string a DateTime
            if (!DateTime.TryParseExact(fechaInicio, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime inicio) ||
                !DateTime.TryParseExact(fechaFin, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime fin))
            {
                // Si hay error en la conversión, devolvemos la lista vacía
                return new List<PagoModel>();
            }

            // Filtramos los pagos que cumplan ambas condiciones
            return pagos.Where(p =>
            {
                bool fechaValida = DateTime.TryParseExact(p.Fecha, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime fechaPago);
                return fechaValida &&
                       fechaPago >= inicio &&
                       fechaPago <= fin &&
                       p.Cuenta_Emisora == numeroCuenta;
            }).ToList();
        }

        public List<PagoPrestamoModel> LeerPagosPrestamosPorFechaYCuenta(string fechaInicio, string fechaFin, string numeroCuenta)
        {
            Jason json = new Jason();
            var pagos = json.LeerPagosPrestamo();

            // Intentamos convertir las fechas string a DateTime
            if (!DateTime.TryParseExact(fechaInicio, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime inicio) ||
                !DateTime.TryParseExact(fechaFin, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime fin))
            {
                // Si hay error en la conversión, devolvemos la lista vacía
                return new List<PagoPrestamoModel>();
            }

            // Filtramos los pagos que cumplan ambas condiciones
            return pagos.Where(p =>
            {
                bool fechaValida = DateTime.TryParseExact(p.Fecha, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime fechaPago);
                return fechaValida &&
                       fechaPago >= inicio &&
                       fechaPago <= fin &&
                       p.CuentaEmisora == numeroCuenta;
            }).ToList();
        }

        public List<TransferenciaModel> LeerTransferenciasPorFechaYCuenta(string fechaInicio, string fechaFin, string numeroCuenta)
        {
            Jason json = new Jason();
            var transferencias = json.LeerTransferencias();

            if (!DateTime.TryParseExact(fechaInicio, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime inicio) ||
                !DateTime.TryParseExact(fechaFin, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime fin))
            {
                return new List<TransferenciaModel>();
            }

            return transferencias.Where(t =>
            {
                bool fechaValida = DateTime.TryParseExact(t.Fecha, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime fechaTrans);
                return fechaValida &&
                       fechaTrans >= inicio &&
                       fechaTrans <= fin &&
                        (t.Cuenta_Emisora == numeroCuenta || t.Cuenta_Receptora == numeroCuenta);
            }).ToList();
        }

        public List<RetiroModel> LeerRetirosPorFechaYCuenta(string fechaInicio, string fechaFin, string numeroCuenta)
        {
            Jason json = new Jason();
            var retiros = json.LeerRetiros();

            if (!DateTime.TryParseExact(fechaInicio, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime inicio) ||
                !DateTime.TryParseExact(fechaFin, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime fin))
            {
                return new List<RetiroModel>();
            }

            return retiros.Where(r =>
            {
                bool fechaValida = DateTime.TryParseExact(r.Fecha, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime fechaRetiro);
                return fechaValida &&
                       fechaRetiro >= inicio &&
                       fechaRetiro <= fin &&
                       r.CuentaARetirar == numeroCuenta;
            }).ToList();
        }

        public List<AsesorCreditoModel> LeerAsesoresCredito(string filtro = "", string valor = "")
        {
            Jason json = new Jason();
            var asesores = json.LeerAsesoresCredito();

            return filtro switch
            {

                "Cedula" => asesores.Where(a => a.Cedula == valor).ToList(),

                "Meta_Colones" => int.TryParse(valor, out int colones) ? asesores.Where(a => a.Meta_Colones == colones).ToList() : new List<AsesorCreditoModel>(),
                "Meta_Creditos" => int.TryParse(valor, out int metaCredito) ?
                                    asesores.Where(a => a.Meta_Creditos.Contains(metaCredito)).ToList() :
                                    new List<AsesorCreditoModel>(),
                _ => asesores
            };
        }





    }
}
