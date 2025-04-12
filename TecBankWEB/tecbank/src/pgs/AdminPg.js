import React, { useEffect, useState } from 'react';
import styles from './AdminPg.module.css';
import axios from "axios";

function AdminPG() {
  const [cuenta, setCuenta] = useState(null);
  
  const [numeroTarjeta, setNumeroTarjeta] = useState('');
  const [numeroTarjetaE, setNumeroTarjetaE] = useState('');
  const [tipoTarjeta, setTipoTarjeta] = useState('');
  const [fechaExp, setFechaExp] = useState('');
  const [codigoSeg, setCodigoSeg] = useState('');
  const [saldoDisponible, setSaldoDisponible] = useState(0);

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

  useEffect(() => {
    // Cargar cuenta actual desde localStorage
    const cuentaGuardada = localStorage.getItem("cuenta_actual");
    if (cuentaGuardada) {
        setCuenta(JSON.parse(cuentaGuardada));
    }
}, []);

if (!cuenta) {
  return <div>Cargando información...</div>; // Mostrar mensaje de carga
}

const handleSubmitCuenta = async (e, accionp) => {
  e.preventDefault();

  console.log('Acción seleccionada:', accionp);

  const cuentaData = {
    numeroCuenta: numeroCuenta,
    descripcion: descripcionCuenta,
    usuario: usuarioCuenta,
    moneda: monedaCuenta,
    tipoDeCuenta: tipoDeCuenta,
    nombre: nombreCuenta
  };

  console.log('Datos a enviar:', cuentaData);

  try {
    if (accionp === 'ingresar') {
      // Enviar para agregar la tarjeta
      const response = await axios.post('https://localhost:7190/cuenta/MenuGestion/AgregarTarjeta', cuentaData);
      console.log('Cliente ingresado con éxito:', response.data);
    } else if (accionp === 'modificar') {
      // Enviar para modificar la tarjeta
      const response = await axios.post('https://localhost:7190/cuenta/MenuGestion/ModificarTarjeta', cuentaData);
      console.log('Cliente modificado con éxito:', response.data);
    }
  } catch (error) {
    console.error('Error al realizar la operación:', error);
  }
};

const handleSubmitEliminarCuenta = async (e) => {
  e.preventDefault();

  // Prepare the data to send to the backend 
  const cuentaEData = {
    cedula: String(numeroECuenta), 
  };

  try {
    // Send data to backend using Axios 
    const response = await axios.delete(`https://localhost:7190/cuenta/MenuGestion/EliminarTarjeta/${numeroECuenta}`, { data: cuentaEData });
    console.log('Tarjeta eliminada con éxito:', response.data);
  } catch (error) {
    console.error('Error al eliminar la tarjeta:', error);
  }
};

const handleSubmitCliente = async (e, accionp) => {
  e.preventDefault();

  console.log('Acción seleccionada:', accionp);

  const clienteData = {
        nombre: nombreCliente,
        apellido1: apellido1Cliente,
        apellido2: apellido2Cliente,
        cedula: cedulaCliente,
        direccion: direccionCliente,
        telefono: telefonoCliente,
        ingresoMensual: ingresoCliente,
        tipoDeCliente: tipoCliente,
        usuario: usuarioCliente,
        contrasena: contrasenaCliente,
        adminRol: false
  };

  console.log('Datos a enviar:', clienteData);

  try {
    if (accionp === 'ingresar') {
      // Enviar para agregar la tarjeta
      const response = await axios.post('https://localhost:7190/cuenta/MenuGestion/AgregarTarjeta', clienteData);
      console.log('Cliente ingresado con éxito:', response.data);
    } else if (accionp === 'modificar') {
      // Enviar para modificar la tarjeta
      const response = await axios.post('https://localhost:7190/cuenta/MenuGestion/ModificarTarjeta', clienteData);
      console.log('Cliente modificado con éxito:', response.data);
    }
  } catch (error) {
    console.error('Error al realizar la operación:', error);
  }
};

const handleSubmitEliminarCliente = async (e) => {
  e.preventDefault();

  // Prepare the data to send to the backend 
  const clienteEData = {
    cedula: String(cedulaECliente), 
  };

  try {
    // Send data to backend using Axios 
    const response = await axios.delete(`https://localhost:7190/cuenta/MenuGestion/EliminarTarjeta/${cedulaECliente}`, { data: clienteEData });
    console.log('Tarjeta eliminada con éxito:', response.data);
  } catch (error) {
    console.error('Error al eliminar la tarjeta:', error);
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
    numeroDeCuenta: cuenta.usuario
  };

  console.log('Datos a enviar:', tarjetaData);

  try {
    if (accionp === 'ingresar') {
      // Enviar para agregar la tarjeta
      const response = await axios.post('https://localhost:7190/cuenta/MenuGestion/AgregarTarjeta', tarjetaData);
      console.log('Tarjeta ingresada con éxito:', response.data);
    } else if (accionp === 'modificar') {
      // Enviar para modificar la tarjeta
      const response = await axios.post('https://localhost:7190/cuenta/MenuGestion/ModificarTarjeta', tarjetaData);
      console.log('Tarjeta modificada con éxito:', response.data);
    }
  } catch (error) {
    console.error('Error al realizar la operación:', error);
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
      const response = await axios.delete(`https://localhost:7190/cuenta/MenuGestion/EliminarTarjeta/${numeroTarjetaE}`, { data: tarjetaEData });
      console.log('Tarjeta eliminada con éxito:', response.data);
    } catch (error) {
      console.error('Error al eliminar la tarjeta:', error);
    }
  };
  

  return (
    <div className="admin">
    <div className={styles.body}>
        <div className={styles.container}>
      <h1>TecBank</h1>
      <br />

      <div className="datos-en-linea">
      <p><strong>Usuario:</strong> {cuenta.usuario}</p>
      <p><strong>Número de Cuenta:</strong> {cuenta.numeroDeCuenta}</p>
      </div>

      <br />
      <hr />
      <br />
      
      <h2>Roles</h2>
      <br />

      <h3>Ingreso y modificación de roles</h3>
      <br />
      <form>
        <label> Nombre: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Rol: </label>
        <input type="text" name="rol" className="form-control" />
      </form>
      <br />
      <form>
        <label> Descripción: </label>
        <input type="text" name="rol" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Modificar</button>
        </div>
        <br />

        <h3>Eliminación de roles</h3>
        <br />
        <form>
        <label> Nombre: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Eliminar</button>
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
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Modificar</button>
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
          <button type="submit" className="btn btn-primary">Eliminar</button>
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
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
        <br />

        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Modificar</button>
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
          <button type="submit" className="btn btn-primary">Eliminar</button>
        </div>
        </form>
      <br />

      <h3>Depósito/Retiro de efectivo a cuenta</h3>
        <br />
        <form>
        <label> Número de cuenta: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <form>
        <label> Monto: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Depositar</button>
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Retirar</button>
        </div>
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

      <h3>Ingreso y modificación de asesores</h3>
      <br />
      <form>
        <label> Nombre: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Apellido 1: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Apellido 2: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
        <br />
        <form>
        <label> Cédula: </label>
        <input type="number" name="rol" className="form-control" />
        </form>
    <br />
    <div className="col-md-4">
          <label htmlFor="fechaNac" className="form-label">Fecha de nacimiento:</label>
          <input type="date" id="fechaNac" className="form-control" />
        </div>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Modificar</button>
        </div>
        <br />

        <h3>Eliminación de asesores</h3>
        <br />
        <form>
        <label> Cédula: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Eliminar</button>
        </div>
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
      <form>
        <label> Monto original: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Saldo: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Número de cliente que solicitó el préstamo: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
        <br />
        <form>
        <label> Tasa de interés (%): </label>
        <input 
            type="number" 
            name="tasa" 
            className="form-control" 
            min="0" 
            max="100" 
            step="0.01" 
            required 
        />
        </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
        <br />

        <h3>Cálculo de pagos</h3>
        <br />

        <h3>Pagos</h3>
        <br />

        <h3>Pagos extraordinarios</h3>
        <br />
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