import React, { useEffect, useState } from 'react';
import styles from './AdminPg.module.css';
import axios from "axios";

function AdminPG() {
  const [cuenta, setCuenta] = useState(null);
  const [usuario, setUsuarioG] = useState(null);


  const [numeroTarjeta, setNumeroTarjeta] = useState('');
  const [numeroTarjetaE, setNumeroTarjetaE] = useState('');
  const [tipoTarjeta, setTipoTarjeta] = useState('');
  const [fechaExp, setFechaExp] = useState('');
  const [codigoSeg, setCodigoSeg] = useState('');
  const [saldoDisponible, setSaldoDisponible] = useState(0);
  const [numeroCuentaTarjeta, setnumeroCuentaTarjeta] = useState(0);

  const [cedulaCliente, setcedulaCliente] = useState('');
  const [cedulaECliente, setcedulaECliente] = useState('');
  const [direccionCliente, setdireccionCliente] = useState('');
  const [telefonoCliente, settelefonoCliente] = useState('');
  const [ingresoCliente, setingresoCliente] = useState('');
  const [nombreCliente, setnombreCliente] = useState('');
  const [apellido1Cliente, setapellido1Cliente] = useState('');
  const [apellido2Cliente, setapellido2Cliente] = useState('');
  const [tipoCliente, settipoCliente] = useState('');
  const [usuarioCliente, setusuarioCliente] = useState('');
  const [contrasenaCliente, setcontrasenaCliente] = useState('');

  const [numeroCuenta, setNumeroDeCuenta] = useState('');
  const [numeroECuenta, setNumeroEDeCuenta] = useState('');
  const [descripcionCuenta, setDescripcion] = useState('');
  const [usuarioCuenta, setUsuario] = useState('');
  const [monedaCuenta, setMoneda] = useState('');
  const [tipoDeCuenta, setTipoDeCuenta] = useState('');
  const [nombreCuenta, setNombre] = useState('');

  const [numeroCuentaDR, setNumeroDeCuentaDR] = useState('');
  const [montoDR, setmontoDR] = useState('');

  const [cedulaEmpleado, setcedulaEmpleado] = useState('');
  const [cedulaEEmpleado, setcedulaEEmpleado] = useState('');
  const [ingresoEmpleado, setingresoEmpleado] = useState('');
  const [nombreEmpleado, setnombreEmpleado] = useState('');
  const [apellido1Empleado, setapellido1Empleado] = useState('');
  const [apellido2Empleado, setapellido2Empleado] = useState('');
  const [rolEmpleado, setrolEmpleado] = useState('');
  const [rolDescEmpleado, setrolDescEmpleado] = useState('');
  const [fechaEmpleado, setfechaEmpleado] = useState('');
  const [usuarioEmpleado, setusuarioEmpleado] = useState('');
  const [contrasenaEmpleado, setcontrasenaEmpleado] = useState('');

  const [cedulaAsesor, setcedulaAsesor] = useState('');
  const [cedulaEAsesor, setcedulaEAsesor] = useState('');
  const [ingresoAsesor, setingresoAsesor] = useState('');
  const [nombreAsesor, setnombreAsesor] = useState('');
  const [apellido1Asesor, setapellido1Asesor] = useState('');
  const [apellido2Asesor, setapellido2Asesor] = useState('');
  const [rolDescAsesor, setrolDescAsesor] = useState('');
  const [fechaAsesor, setfechaAsesor] = useState('');
  const [usuarioAsesor, setusuarioAsesor] = useState('');
  const [contrasenaAsesor, setcontrasenaAsesor] = useState('');
  const [metaAsesor, setmetaAsesor] = useState('');

  const [montoPrestamo, setmontoPrestamo] = useState('');
  const [cedulaPrestamo, setcedulaPrestamo] = useState('');
  const [tasaPrestamo, settasaPrestamo] = useState('');
  const [fechaPrestamo, setfechaPrestamo] = useState('');

  const [idPP, setidPP] = useState('');
  const [montoPP, setmontoPP] = useState('');

  useEffect(() => {
      const cuentaGuardada = localStorage.getItem("cuenta_actual");
      if (cuentaGuardada) setCuenta(JSON.parse(cuentaGuardada));
  
      const usuarioGuardado = localStorage.getItem("usuario_actual");
      if (usuarioGuardado) setUsuarioG(JSON.parse(usuarioGuardado));
    }, []);

if (!cuenta) {
  return <div>Cargando información...</div>; // Mostrar mensaje de carga
}

const handleSubmitDR = async (e, accionp) => {
  e.preventDefault();

  console.log('Acción seleccionada:', accionp);

  try {
    if (accionp === 'depositar') {
      const drDataDeposito = {
        Nombre: usuario.nombre,
        Apellido1: usuario.apellido1,
        Apellido2: usuario.apellido2,
        Cuenta_Emisora: "0", 
        Cuenta_Receptora: numeroCuentaDR,
        Moneda: "Colones",
        Monto: parseFloat(montoDR)
      };

      console.log('Datos de depósito a enviar:', drDataDeposito);

      const response = await axios.post('https://localhost:7190/Movimiento/TransferenciaAdmin', drDataDeposito);
      console.log('Depósito ingresado con éxito:', response.data);
      alert("Depósito realizado con éxito");

    } else if (accionp === 'retirar') {
      const drDataRetiro = {
        Nombre: usuario.nombre,
        Apellido1: usuario.apellido1,
        Apellido2: usuario.apellido2,
        ID: "0",
        CuentaARetirar: numeroCuentaDR,
        Moneda: "Colones",
        Monto: parseFloat(montoDR)
      };

      console.log('Datos de retiro a enviar:', drDataRetiro);

      const response = await axios.post('https://localhost:7190/Movimiento/Retiro', drDataRetiro);
      console.log('Retiro realizado con éxito:', response.data);
      alert("Retiro realizado con éxito");
    }
  } catch (error) {
    console.error("Error al realizar la operación:", error);
  
    if (error.response) {
      console.error("Código de estado HTTP:", error.response.status);
  
      if (error.response.data) {
        console.error("Detalles del error:", error.response.data);
  
        // Mostrar errores de validación si existen
        if (error.response.data.errors) {
          console.error("Errores de validación:");
          for (const campo in error.response.data.errors) {
            console.error(`${campo}: ${error.response.data.errors[campo].join(", ")}`);
          }
        }
      }
    } else {
      console.error("Error sin respuesta del servidor:", error.message);
    }
  }
};

const handleSubmitCuenta = async (e, accionp) => {
  e.preventDefault();

  console.log('Acción seleccionada:', accionp);

  const cuentaData = {
    NumeroDeCuenta: numeroCuenta,
    Descripcion: descripcionCuenta,
    Usuario: usuarioCuenta,
    Moneda: monedaCuenta,
    TipoDeCuenta: tipoDeCuenta,
    Nombre: nombreCuenta
  };

  console.log('Datos a enviar:', cuentaData);

  try {
    if (accionp === 'ingresar') {
      // Enviar para agregar la tarjeta
      const response = await axios.post('https://localhost:7190/MenuGestionCuentas/AgregarCuenta', cuentaData);
      console.log('Cliente ingresado con éxito:', response.data);
      alert("Cuenta agregada con éxito");
    } else if (accionp === 'modificar') {
      // Enviar para modificar la tarjeta
      const response = await axios.post('https://localhost:7190/MenuGestionCuentas/ModificarCuenta', cuentaData);
      console.log('Cliente modificado con éxito:', response.data);
      alert("Cuenta modificada con éxito");
    }
  } catch (error) {
    console.error('Error al realizar la operación:', error);
    alert("No se pudo realizar el cambio");
  }
};

const handleSubmitEliminarCuenta = async (e) => {
  e.preventDefault();

  // Prepare the data to send to the backend 
  const cuentaEData = {
    NumeroDeCuenta: String(numeroECuenta), 
  };

  try {
    // Send data to backend using Axios 
    const response = await axios.post('https://localhost:7190/MenuGestionCuentas/EliminarCuenta', cuentaEData);
      console.log('Cuenta eliminada con éxito:', response.data);
      alert("Cuenta eliminada con éxito");
  } catch (error) {
    console.error('Error al eliminar la cuenta:', error);
    alert("No se pudo realizar el cambio");
  }
};

const handleSubmitCliente = async (e, accionp) => {
  e.preventDefault();

  console.log('Acción seleccionada:', accionp);

  const clienteData = {
        Cedula: cedulaCliente,
        Direccion: direccionCliente,
        Telefono: telefonoCliente,
        IngresoMensual: parseFloat(ingresoCliente),
        Nombre: nombreCliente,
        Apellido1: apellido1Cliente,
        Apellido2: apellido2Cliente,
        TipoDeCliente: tipoCliente,
        Usuario: usuarioCliente,
        Contrasena: contrasenaCliente,

        AdminRol: false
  };

  console.log('Datos a enviar:', clienteData);

  try {
    if (accionp === 'ingresar') {
      const response = await axios.post('https://localhost:7190/MenuGestionCliente/AgregarCliente', clienteData);
      console.log('Cliente ingresado con éxito:', response.data);
      alert("Cliente ingresado con éxito");
      
    } else if (accionp === 'modificar') {
      const response = await axios.post('https://localhost:7190/MenuGestionCliente/ModificarCliente', clienteData);
      console.log('Cliente modificado con éxito:', response.data);
      alert("Cliente modificado con éxito");
    }
  } catch (error) {
    console.error('Error al realizar la operación:', error);
    alert("No se pudo realizar el cambio");
  }
};

const handleSubmitEliminarCliente = async (e) => {
  e.preventDefault();

  // Prepare the data to send to the backend 
  const clienteEData = {
    Cedula: String(cedulaECliente), 
  };

  try {
    // Send data to backend using Axios 
    const response = await axios.post('https://localhost:7190/MenuGestionCliente/EliminarCliente', clienteEData);
    console.log('Tarjeta eliminada con éxito:', response.data);
    alert("Cliente eliminado con éxito");
  } catch (error) {
    alert("No se pudo realizar el cambio", error);
  }
};

const handleSubmitEmpleado = async (e, accionp) => {
  e.preventDefault();

  console.log('Acción seleccionada:', accionp);

  const empleadoData = {
        Nombre: nombreEmpleado,
        Rol: rolEmpleado,
        DescripcionDeRol: rolDescEmpleado,
        Apellido1: apellido1Empleado,
        Apellido2: apellido2Empleado,
        Cedula: cedulaEmpleado,
        AdminRol: true,
        FechaDeNacimiento: fechaEmpleado,
        Usuario: usuarioEmpleado,
        Contrasena: contrasenaEmpleado,
        IngresoMensual: ingresoEmpleado
  };

  console.log('Datos a enviar:', empleadoData);

  try {
    if (accionp === 'ingresar') {
      const response = await axios.post('https://localhost:7190/MenuGestionEmpleados/AgregarEmpleado', empleadoData);
      console.log('Empleado ingresado con éxito:', response.data);
      alert("Empleado ingresado con éxito");
      
    } else if (accionp === 'modificar') {
      const response = await axios.post('https://localhost:7190/MenuGestionEmpleados/ModificarEmpleado', empleadoData);
      console.log('Empleado modificado con éxito:', response.data);
      alert("Empleado modificado con éxito");
    }
  } catch (error) {
    console.error('Error al realizar la operación:', error);
    alert("No se pudo realizar el cambio");
  }
};

const handleSubmitEliminarEmpleado = async (e) => {
  e.preventDefault();

  // Prepare the data to send to the backend 
  const empleadoEData = {
    Cedula: cedulaEEmpleado, 
  };

  try {
    // Send data to backend using Axios (endpoint para eliminar tarjeta)
    const response = await axios.post('https://localhost:7190/MenuGestionEmpleados/EliminarEmpleado', empleadoEData);
    console.log('Tarjeta eliminada con éxito:', response.data);
    alert("Empleado eliminado con éxito");
  } catch (error) {
    alert("No se pudo realizar el cambio", error);
  }
};

const handleSubmitTarjeta = async (e, accionp) => {
  e.preventDefault();

  console.log('Acción seleccionada:', accionp);

  const tarjetaData = {
    numeroDeTarjeta: String(numeroTarjeta),
    tipoDeTarjeta: tipoTarjeta,
    fechaDeExpiracion: fechaExp,
    CCV: codigoSeg,
    saldo: parseFloat(saldoDisponible),
    numeroDeCuenta: numeroCuentaTarjeta
  };

  console.log('Datos a enviar:', tarjetaData);

  try {
    if (accionp === 'ingresar') {
      // Enviar para agregar la tarjeta
      const response = await axios.post('https://localhost:7190/MenuGestionTarjetas/AgregarTarjeta', tarjetaData);
      console.log('Tarjeta ingresada con éxito:', response.data);
      alert("Tarjeta agregada con éxito");
    } else if (accionp === 'modificar') {
      // Enviar para modificar la tarjeta
      const response = await axios.post('https://localhost:7190/MenuGestionTarjetas/ModificarTarjeta', tarjetaData);
      console.log('Tarjeta modificada con éxito:', response.data);
      alert("Tarjeta modificada");
    }
  } catch (error) {
    console.error('Error al realizar la operación:', error);
    alert("No se pudo realizar el cambio");
  }
};

  const handleSubmitEliminarTarjeta = async (e) => {
    e.preventDefault();
  
    // Prepare the data to send to the backend (solo número de tarjeta)
    const tarjetaEData = {
      numeroDeTarjeta: String(numeroTarjetaE), // Solo el número de la tarjeta
    };
  
    try {
      // Send data to backend using Axios (endpoint para eliminar tarjeta)
      const response = await axios.post('https://localhost:7190/MenuGestionTarjetas/EliminarTarjeta', tarjetaEData);
      console.log('Tarjeta eliminada con éxito:', response.data);
      alert("Tarjeta eliminada con éxito");
    } catch (error) {
      alert("No se pudo realizar el cambio", error);
    }
  };

  const handleSubmitAsesor = async (e, accionp) => {
    e.preventDefault();
  
    console.log('Acción seleccionada:', accionp);
  
    const asesorData = {
          Nombre: nombreAsesor,
          Rol: "Asesor de credito",
          DescripcionDeRol: rolDescAsesor,
          Apellido1: apellido1Asesor,
          Apellido2: apellido2Asesor,
          Cedula: cedulaAsesor,
          AdminRol: true,
          FechaDeNacimiento: fechaAsesor,
          Usuario: usuarioAsesor,
          Contrasena: contrasenaAsesor,
          IngresoMensual: ingresoAsesor,
          Meta_Colones: metaAsesor,
          Meta_Creditos: []
    };
  
    console.log('Datos a enviar:', asesorData);
  
    try {
      if (accionp === 'ingresar') {
        const response = await axios.post('https://localhost:7190/MenuGestionEmpleados/AgregarAsesorDeCredito', asesorData);
        console.log('Empleado ingresado con éxito:', response.data);
        alert("Asesor ingresado con éxito");
        
      } else if (accionp === 'modificar') {
        const response = await axios.post('https://localhost:7190/MenuGestionEmpleados/EditarAsesorDeCredito', asesorData);
        console.log('Empleado modificado con éxito:', response.data);
        alert("Asesor modificado con éxito");
      }
    } catch (error) {
      console.error('Error al realizar la operación:', error);
      alert("No se pudo realizar el cambio");
    }
  };

  const handleSubmitEliminarAsesor = async (e) => {
    e.preventDefault();
  
    // Prepare the data to send to the backend 
    const asesorEData = {
      Cedula: cedulaEAsesor, 
    };
  
    try {
      // Send data to backend using Axios (endpoint para eliminar tarjeta)
      const response = await axios.post('https://localhost:7190/MenuGestionEmpleados/EliminarAsesorDeCredito', asesorEData);
      console.log('Tarjeta eliminada con éxito:', response.data);
      alert("Asesor eliminado con éxito");
    } catch (error) {
      alert("No se pudo realizar el cambio", error);
    }
  };
  
  const handleSubmitPrestamos = async (e) => {
    e.preventDefault();
  
    // Prepare the data to send to the backend 
    const presData = {
      Monto_Original: montoPrestamo,
      Cedula_Cliente: cedulaPrestamo,
      Tasa_De_Interes: tasaPrestamo,
      FechaVencimiento: fechaPrestamo
    };
  
    try {
      // Send data to backend using Axios (endpoint para eliminar tarjeta)
      const response = await axios.post('https://localhost:7190/Prestamo/AgregarPrestamo', presData);
      console.log('Préstamo realizado con éxito:', response.data);
      alert("Préstamo agregado con éxito");
    } catch (error) {
      alert("No se pudo realizar el préstamo", error);
    }
  };

  const handleSubmitPP = async (e) => {
    e.preventDefault();
  
    // Prepare the data to send to the backend 
    const ppData = {
      Nombre: usuario.nombre,
      Apellido1: usuario.apellido1,
      Apellido2: usuario.apellido2,
      IdPrestamo: idPP,
      Moneda: "Colones",
      Monto: montoPP,
      NumeroDeCuenta: cuenta.numeroDeCuenta
    };
  
    try {
      console.log('Datos enviados (ppData):', ppData);
      const response = await axios.post('https://localhost:7190/Movimiento/PagoPrestamo', ppData);
      console.log('Pago de préstamo realizado con éxito:', response.data);
      alert("El préstamo elegido ha sido pagado con éxito");
    } catch (error) {
      alert("No se pudo realizar el pago", error);
    }
  };


  return (
    <div className="admin">
    <div className={styles.body}>
        <div className={styles.container}>
      <h1>TecBank</h1>
      <br />

      <div className="datos-en-linea2">
      <p style={{ color: '#808080' }}><strong>Usuario:</strong> {cuenta.usuario}</p>
      <p style={{ color: '#808080' }}><strong>Número de Cuenta:</strong> {cuenta.numeroDeCuenta}</p>
      </div>

      <br />
      <hr />
      <br />

      <h2>Clientes</h2>
      <br />

      <h3>Ingreso y modificación de clientes</h3>
      <form onSubmit={handleSubmitCliente}>
      <br />
        <label> Nombre: </label>
        <input 
        type="text" 
        name="nombreCliente" 
        className="form-control" 
        value={nombreCliente} 
        onChange={(e) => setnombreCliente(e.target.value)} 
      />

      <br />
        <label> Apellido 1: </label>
        <input 
        type="text" 
        name="apellido1Cliente" 
        className="form-control" 
        value={apellido1Cliente} 
        onChange={(e) => setapellido1Cliente(e.target.value)} 
      />
      <br />
        <label> Apellido 2: </label>
        <input 
        type="text" 
        name="apellido2Cliente" 
        className="form-control" 
        value={apellido2Cliente} 
        onChange={(e) => setapellido2Cliente(e.target.value)} 
      />
      <br />
      
        <label> Cédula: </label>
        <input 
        type="number" 
        name="cedulaCliente" 
        className="form-control" 
        value={cedulaCliente} 
        onChange={(e) => setcedulaCliente(e.target.value)} 
      />
      <br />
        <label> Dirección: </label>
        <input 
        type="text" 
        name="direccionCliente" 
        className="form-control" 
        value={direccionCliente} 
        onChange={(e) => setdireccionCliente(e.target.value)} 
      />
      <br />
    <label> Teléfono: </label>
    <input 
      type="number" 
      name="telefonoCliente" 
      className="form-control" 
      value={telefonoCliente} 
      onChange={(e) => settelefonoCliente(e.target.value)} 
    />
      <br />
        <label> Ingreso mensual (colones): </label>
        <input 
        type="number" 
        name="ingresoCliente" 
        className="form-control" 
        value={ingresoCliente} 
        onChange={(e) => setingresoCliente(e.target.value)} 
      />
      <br />
        <label> Tipo de cliente: </label>
        <select
        name="tipoCliente"
        className="form-control"
        value={tipoCliente}
        onChange={(e) => settipoCliente(e.target.value)}
      >
        <option value="">Selecciona tipo de cliente</option>
        <option value="Fisico">Físico</option>
        <option value="Juridico">Jurídico</option>
      </select>
        <br />
        <label> Usuario: </label>
        <input 
        type="text" 
        name="usuarioCliente" 
        className="form-control" 
        value={usuarioCliente} 
        onChange={(e) => setusuarioCliente(e.target.value)} 
      />
      <br />
        <label> Contraseña: </label>
        <input 
        type="password" 
        name="contrasenaCliente" 
        className="form-control" 
        value={contrasenaCliente} 
        onChange={(e) => setcontrasenaCliente(e.target.value)} 
      />

    <br />
        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-primary" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitCliente(e, 'ingresar');  // Pasa el evento correctamente
          }}
        >
          Ingresar
          </button>
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-primary" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitCliente(e, 'modificar');  // Pasa el evento correctamente
          }}
        >
          Modificar
          </button>
        </div>
        </form>
        <br />

        <h3>Eliminación de clientes</h3>
        <br />
        <form onSubmit={handleSubmitEliminarCliente}>
        <label> Cédula: </label>
        <input 
        type="number" 
        name="cedulaCliente" 
        className="form-control" 
        value={cedulaECliente} 
        onChange={(e) => setcedulaECliente(e.target.value)} 
      />
      
      <br />
        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-danger" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitEliminarCliente(e);  // Pasa el evento correctamente
          }}
        >
          Eliminar
          </button>
        </div>
        </form>
      <br />
      <hr />
      <br />

      <h2>Empleados</h2>
      <br />

      <h3>Ingreso y modificación de empleados</h3>
      <form onSubmit={handleSubmitEmpleado}>
        <br />
        <label>Nombre:</label>
        <input 
          type="text" 
          name="nombreEmpleado" 
          className="form-control" 
          value={nombreEmpleado} 
          onChange={(e) => setnombreEmpleado(e.target.value)} 
        />
        <br />

        <label>Apellido 1:</label>
        <input 
          type="text" 
          name="apellido1Empleado" 
          className="form-control" 
          value={apellido1Empleado} 
          onChange={(e) => setapellido1Empleado(e.target.value)} 
        />
        <br />

        <label>Apellido 2:</label>
        <input 
          type="text" 
          name="apellido2Empleado" 
          className="form-control" 
          value={apellido2Empleado} 
          onChange={(e) => setapellido2Empleado(e.target.value)} 
        />
        <br />

        <label>Cédula:</label>
        <input 
          type="number" 
          name="cedulaEmpleado" 
          className="form-control" 
          value={cedulaEmpleado} 
          onChange={(e) => setcedulaEmpleado(e.target.value)} 
        />
        <br />

        <label>Fecha de nacimiento:</label>
        <input 
          type="date" 
          name="fechaEmpleado" 
          className="form-control" 
          value={fechaEmpleado} 
          onChange={(e) => setfechaEmpleado(e.target.value)} 
        />
        <br />

        <label>Ingreso mensual (colones):</label>
        <input 
          type="number" 
          name="ingresoEmpleado" 
          className="form-control" 
          value={ingresoEmpleado} 
          onChange={(e) => setingresoEmpleado(e.target.value)} 
        />
        <br />

        <label>Rol:</label>
        <select 
        name="rolEmpleado" 
        className="form-control" 
        value={rolEmpleado} 
        onChange={(e) => setrolEmpleado(e.target.value)}
      >
        <option value="">Selecciona un rol</option>
        <option value="Asesor de credito">Asesores de crédito</option>
        <option value="Cajero">Cajero</option>
        <option value="Ejecutivo de cuentas">Ejecutivo de cuentas</option>
        <option value="Gerente de sucursal">Gerente de sucursal</option>
        <option value="Analista financiero">Analista financiero</option>
        <option value="Oficial de cumplimiento">Oficial de cumplimiento</option>
        <option value="Auditor interno">Auditor interno</option>
        <option value="Asistente administrativo">Asistente administrativo</option>
      </select>
        <br />

        <label>Descripción del Rol:</label>
        <input 
          type="text" 
          name="rolDescEmpleado" 
          className="form-control" 
          value={rolDescEmpleado} 
          onChange={(e) => setrolDescEmpleado(e.target.value)} 
        />
        <br />

        <label>Usuario:</label>
        <input 
          type="text" 
          name="usuarioEmpleado" 
          className="form-control" 
          value={usuarioEmpleado} 
          onChange={(e) => setusuarioEmpleado(e.target.value)} 
        />
        <br />

        <label>Contraseña:</label>
        <input 
          type="password" 
          name="contrasenaEmpleado" 
          className="form-control" 
          value={contrasenaEmpleado} 
          onChange={(e) => setcontrasenaEmpleado(e.target.value)} 
        />
        <br />

        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-primary" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitEmpleado(e, 'ingresar');  // Pasa el evento correctamente
          }}
        >
          Ingresar
          </button>
        </div>
        <br />

        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-primary" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitEmpleado(e, 'modificar');  // Pasa el evento correctamente
          }}
        >
          Modificar
          </button>
        </div>
      </form>
      <br />

      <h3>Eliminación de empleados</h3>
      <br />
      <form onSubmit={handleSubmitEliminarEmpleado}>
        <label>Cédula:</label>
        <input 
          type="number" 
          name="cedulaEEmpleado" 
          className="form-control" 
          value={cedulaEEmpleado} 
          onChange={(e) => setcedulaEEmpleado(e.target.value)} 
        />
        <br />

        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-danger" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitEliminarEmpleado(e);  // Pasa el evento correctamente
          }}
        >
          Eliminar
          </button>
        </div>
      </form>
      <br />
      <hr />
      <br />

      <h2>Cuentas</h2>
      <br />

      <h3>Ingreso y modificación de cuentas</h3>
      <br />
      <form onSubmit={handleSubmitCuenta}>
      <label> Número de cuenta: </label>
      <input 
        type="text" 
        name="numeroCuenta" 
        className="form-control" 
        value={numeroCuenta} 
        onChange={(e) => setNumeroDeCuenta(e.target.value)} 
      />
        <br />

        <label> Descripción: </label>
        <input 
        type="text" 
        name="descripcionCuenta" 
        className="form-control" 
        value={descripcionCuenta} 
        onChange={(e) => setDescripcion(e.target.value)} 
      />
        <br />

        <label> Usuario: </label>
        <input 
        type="text" 
        name="usuarioCuenta" 
        className="form-control" 
        value={usuarioCuenta} 
        onChange={(e) => setUsuario(e.target.value)} 
      />
        <br />

        <label> Moneda: </label>
        <select
        name="monedaCuenta"
        className="form-control"
        value={monedaCuenta}
        onChange={(e) => setMoneda(e.target.value)}
      >
        <option value="">Selecciona una moneda</option>
        <option value="Colones">Colones</option>
        <option value="Dolares">Dólares</option>
        <option value="Euros">Euros</option>
      </select>
        <br />

        <label> Tipo de cuenta: </label>
        <select
        name="tipoDeCuenta"
        className="form-control"
        value={tipoDeCuenta}
        onChange={(e) => setTipoDeCuenta(e.target.value)}
      >
        <option value="">Selecciona el tipo de cuenta</option>
        <option value="Ahorros">Ahorros</option>
        <option value="Corriente">Corriente</option>
      </select>
        <br />

        <label> Nombre del cliente: </label>
        <input 
          type="text" 
          name="nombreCuenta" 
          className="form-control" 
          value={nombreCuenta} 
          onChange={(e) => setNombre(e.target.value)} 
        />
        <br />

        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-primary" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitCuenta(e, 'ingresar');  // Pasa el evento correctamente
          }}
        >
          Ingresar
          </button>
        </div>
        <br />

        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-primary" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitCuenta(e, 'modificar');  // Pasa el evento correctamente
          }}
        >
          Modificar
          </button>
        </div>
        </form>
        <br />

        <h3>Eliminación de cuentas</h3>
        <br />
        <form onSubmit={handleSubmitEliminarCuenta}>
        <label> Número de cuenta: </label>
        <input 
        type="number" 
        name="numeroECuenta" 
        className="form-control" 
        value={numeroECuenta} 
        onChange={(e) => setNumeroEDeCuenta(e.target.value)} 
      />
      <br />
        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-danger" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitEliminarCuenta(e);  // Pasa el evento correctamente
          }}
        >
          Eliminar
          </button>
        </div>
        </form>
      <br />

      <h3>Depósito/Retiro de efectivo a cuenta</h3>
        <br />
        <form onSubmit={handleSubmitDR}>
        <label> Número de cuenta: </label>
        <input 
        type="number" 
        name="numeroCuentaDR" 
        className="form-control" 
        value={numeroCuentaDR} 
        onChange={(e) => setNumeroDeCuentaDR(e.target.value)} 
      />
        <br />
        <label> Monto: </label>
        <input 
          type="number" 
          name="montoDR" 
          className="form-control" 
          value={montoDR} 
          onChange={(e) => setmontoDR(e.target.value)} 
        />
        <br />
        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-warning" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitDR(e, 'depositar');  // Pasa el evento correctamente
          }}
        >
          Depositar
          </button>
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-warning" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitDR(e, 'retirar');  // Pasa el evento correctamente
          }}
        >
          Retirar
          </button>
        </div>
      </form>
      <br />
      <hr />
      <br />

      <h2>Tarjetas</h2>
      <br />

      <h3>Ingreso y modificación de tarjetas</h3>
        <br />
        <form onSubmit={handleSubmitTarjeta}>
          <label> Número de tarjeta: </label>
          <input 
            type="number" 
            name="numeroTarjeta" 
            className="form-control" 
            value={numeroTarjeta} 
            onChange={(e) => setNumeroTarjeta(e.target.value)} 
          />
          <br />
          <label> Tipo de tarjeta: </label>
          <select 
            name="tipoTarjeta" 
            className="form-control" 
            value={tipoTarjeta} 
            onChange={(e) => setTipoTarjeta(e.target.value)}
          >
            <option value="">Seleccione...</option>
            <option value="Debito">Debito</option>
            <option value="Credito">Credito</option>
          </select>
          <br />
          <div className="col-md-4">
            <label htmlFor="fechaExp" className="form-label">Fecha de expiración:</label>
            <input 
              type="date" 
              id="fechaExp" 
              className="form-control" 
              value={fechaExp} 
              onChange={(e) => setFechaExp(e.target.value)} 
            />
          </div>
          <br />
          <label> Código de seguridad: </label>
          <input 
            type="number" 
            min="0" 
            max="999" 
            name="codigoSeg" 
            className="form-control" 
            value={codigoSeg} 
            onChange={(e) => setCodigoSeg(e.target.value)} 
          />
          <br />
          <label> Saldo disponible/monto de crédito disponible: </label>
          <input 
            type="number" 
            name="saldoDisponible" 
            className="form-control" 
            value={saldoDisponible} 
            onChange={(e) => setSaldoDisponible(e.target.value)} 
          />
          <br />
          <label> Número de cuenta: </label>
          <input 
            type="number" 
            name="numeroCuentaTarjeta" 
            className="form-control" 
            value={numeroCuentaTarjeta} 
            onChange={(e) => setnumeroCuentaTarjeta(e.target.value)} 
          />
          <br />
          <div className="col-md-4 d-flex align-items-end">
          <button 
          type="button" 
          className="btn btn-primary" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitTarjeta(e, 'ingresar');  // Pasa el evento correctamente
          }}
        >
          Ingresar
          </button>
          </div>
          <br />
          <div className="col-md-4 d-flex align-items-end">
          <button 
          type="button" 
          className="btn btn-warning" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitTarjeta(e, 'modificar');  // Pasa el evento correctamente
          }}
        >
          Modificar
          </button>
          </div>
          <br />
        </form>

        <h3>Eliminación de tarjetas</h3>
        <br />
        <form onSubmit={handleSubmitEliminarTarjeta}>
        <label> Número de tarjeta: </label>
        <input 
            type="number" 
            name="numeroTarjetaE" 
            className="form-control" 
            value={numeroTarjetaE} 
            onChange={(e) => setNumeroTarjetaE(e.target.value)} 
          />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
        <button 
          type="button" 
          className="btn btn-danger" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitEliminarTarjeta(e);  // Pasa el evento correctamente
          }}
        >
          Eliminar
          </button>
        </div>
      <br />
      <hr />
      <br />

      <h2>Asesores de crédito</h2>
      <br />

      <h3>Ingreso y modificación de asesores de crédito</h3>
      <form onSubmit={handleSubmitAsesor}>
        <br />

        <label>Nombre:</label>
        <input 
          type="text" 
          name="nombreAsesor" 
          className="form-control" 
          value={nombreAsesor} 
          onChange={(e) => setnombreAsesor(e.target.value)} 
        />
        <br />

        <label>Apellido 1:</label>
        <input 
          type="text" 
          name="apellido1Asesor" 
          className="form-control" 
          value={apellido1Asesor} 
          onChange={(e) => setapellido1Asesor(e.target.value)} 
        />
        <br />

        <label>Apellido 2:</label>
        <input 
          type="text" 
          name="apellido2Asesor" 
          className="form-control" 
          value={apellido2Asesor} 
          onChange={(e) => setapellido2Asesor(e.target.value)} 
        />
        <br />

        <label>Cédula:</label>
        <input 
          type="number" 
          name="cedulaAsesor" 
          className="form-control" 
          value={cedulaAsesor} 
          onChange={(e) => setcedulaAsesor(e.target.value)} 
        />
        <br />

        <label>Fecha de nacimiento:</label>
        <input 
          type="date" 
          name="fechaAsesor" 
          className="form-control" 
          value={fechaAsesor} 
          onChange={(e) => setfechaAsesor(e.target.value)} 
        />
        <br />

        <label>Ingreso mensual (colones):</label>
        <input 
          type="number" 
          name="ingresoAsesor" 
          className="form-control" 
          value={ingresoAsesor} 
          onChange={(e) => setingresoAsesor(e.target.value)} 
        />
        <br />

        <label>Descripción de rol:</label>
        <input 
          type="text" 
          name="rolDescEmpleado" 
          className="form-control" 
          value={rolDescAsesor} 
          onChange={(e) => setrolDescAsesor(e.target.value)} 
        />
        <br />

        <label>Meta:</label>
        <input 
          type="number" 
          name="metaAsesor" 
          className="form-control" 
          value={metaAsesor} 
          onChange={(e) => setmetaAsesor(e.target.value)} 
        />
        <br />

        <label>Usuario:</label>
        <input 
          type="text" 
          name="usuarioAsesor" 
          className="form-control" 
          value={usuarioAsesor} 
          onChange={(e) => setusuarioAsesor(e.target.value)} 
        />
        <br />

        <label>Contraseña:</label>
        <input 
          type="password" 
          name="contrasenaAsesor" 
          className="form-control" 
          value={contrasenaAsesor} 
          onChange={(e) => setcontrasenaAsesor(e.target.value)} 
        />
        <br />

        <div className="col-md-4 d-flex align-items-end">
          <button 
            type="button" 
            className="btn btn-primary" 
            onClick={(e) => {
              e.preventDefault();
              handleSubmitAsesor(e, 'ingresar');
            }}
          >
            Ingresar
          </button>
        </div>
        <br />

        <div className="col-md-4 d-flex align-items-end">
          <button 
            type="button" 
            className="btn btn-primary" 
            onClick={(e) => {
              e.preventDefault();
              handleSubmitAsesor(e, 'modificar');
            }}
          >
            Modificar
          </button>
        </div>
      </form>
      <br />

      <h3>Eliminación de asesores de crédito</h3>
      <br />
      <form onSubmit={handleSubmitEliminarAsesor}>
        <label>Cédula:</label>
        <input 
          type="number" 
          name="cedulaEAsesor" 
          className="form-control" 
          value={cedulaEAsesor} 
          onChange={(e) => setcedulaEAsesor(e.target.value)} 
        />
        <br />

        <div className="col-md-4 d-flex align-items-end">
          <button 
            type="button" 
            className="btn btn-danger" 
            onClick={(e) => {
              e.preventDefault();
              handleSubmitEliminarAsesor(e);
            }}
          >
            Eliminar
          </button>
        </div>
      </form>
      <br />

        <h3>Reporte sobre asesor</h3>
      <br />
      <form>
        <label> Cédula: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Generar reporte</button>
        </div>
      <br />
      <hr />
      <br />

      <h2>Préstamos</h2>
      <br />

      <h3>Ingreso de préstamos</h3>
      <br />
      <form onSubmit={handleSubmitPrestamos}>
      <label> Monto original del préstamo: </label>
      <input 
        type="number" 
        name="montoPrestamo" 
        className="form-control" 
        value={montoPrestamo} 
        onChange={(e) => setmontoPrestamo(e.target.value)} 
      />
      <br />

      <label> Fecha de vencimiento: </label>
      <input
            type="date"
            className="form-control"
            onChange={(e) => {
              const [año, mes, dia] = e.target.value.split("-");
              setfechaPrestamo(`${dia}/${mes}/${año}`);
            }}
      />
      <br />

      <label> Cédula del cliente que solicitó el préstamo: </label>
      <input 
        type="number" 
        name="cedulaPrestamo" 
        className="form-control" 
        value={cedulaPrestamo} 
        onChange={(e) => setcedulaPrestamo(e.target.value)} 
      />
      <br />

      <label> Tasa de interés (%): </label>
      <input 
        type="number" 
        name="tasaPrestamo" 
        className="form-control" 
        value={tasaPrestamo} 
        onChange={(e) => settasaPrestamo(e.target.value)} 
        min="0" 
        max="100" 
        step="0.01" 
        required 
      />
      <br />

      <div className="col-md-4 d-flex align-items-end">
      
          <button 
            type="button" 
            className="btn btn-primary" 
            onClick={(e) => {
              e.preventDefault();
              handleSubmitPrestamos(e);
            }}
          >
            Ingresar
          </button>
      </div>
    </form>
        <br />

        <h3>Pago de préstamos</h3>
        <br />

        <form onSubmit={handleSubmitPP}>
        <label> ID del préstamo a pagar: </label>
        <input 
          type="text" 
          name="idPP" 
          className="form-control" 
          value={idPP} 
          onChange={(e) => setidPP(e.target.value)} 
        />

        <br />

        <label> Monto a pagar del préstamo: </label>
        <input 
          type="number" 
          name="montoPP" 
          className="form-control" 
          value={montoPP} 
          onChange={(e) => setmontoPP(e.target.value)} 
        />

        <br />
        <div className="col-md-4 d-flex align-items-end">
      
          <button 
            type="button" 
            className="btn btn-primary" 
            onClick={(e) => {
              e.preventDefault();
              handleSubmitPP(e);
            }}
          >
            Pagar
          </button>
      </div>
        </form>

      <hr />
      <br />

      <h2>Gestión de Mora</h2>
      <br />

      <h3>Reporte sobre mora</h3>
      <br />
      <form>
        <label> Cédula: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
      <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Generar reporte</button>
        </div>
      <br />


    </div>
    </div>
    </div>
  );
}

export default AdminPG;