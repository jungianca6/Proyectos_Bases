import React from "react";
import styles from './ClientePg.module.css';

function ClientePG() {
  
  const [cuenta, setCuenta] = useState(null);

  useEffect(() => {
      // Cargar cuenta actual desde localStorage
      const cuentaGuardada = localStorage.getItem("cuenta_actual");
      if (cuentaGuardada) {
          setCuenta(JSON.parse(cuentaGuardada));
      }
  }, []);

  return (
    <div className="cliente">
    <div className={styles.body}>
      <div className={styles.container}>
      <h1>TecBank</h1>
      <br />

      <div className="datos-en-linea">
      <p><strong>Usuario:</strong> {cuenta.Usuario}</p>
      <p><strong>Número de Cuenta:</strong> {cuenta.NumeroDeCuenta}</p>
      </div>

      <br />
      <hr />
      <br />

      <h2>Cuentas</h2>
      <br />
      <h3>Mis movimientos</h3>
      
      <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ver mis movimientos</button>
        </div>
        <br />

      <h3>Transferencias</h3>
      <br />
      <form>
        <label> Número de cuenta: </label>
        <input type="number" min="100000" name="transferencia" className="form-control" />
      </form>
      <br />
      <form>
        <label> Monto a transferir (colones): </label>
        <input type="number" min="0" name="transferencia" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Transferir</button>
        </div>
      <br />
      <hr />
      <br />

      <h2>Tarjetas</h2>
      <br />

      <h3>Pago de tarjetas de crédito</h3>
      <br />
      <form>
        <label> Número de tarjeta: </label>
        <input type="number" min="100000" name="transferencia" className="form-control" />
      </form>
      <br />
      <form>
        <label> Monto a pagar (colones): </label>
        <input type="number" min="0" name="transferencia" className="form-control" />
      </form>
      <br />
      <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Pagar</button>
        </div>
      <br />

      <h3>Listado de compras</h3>
      <br />
      <form className="row g-3">
        <div className="col-md-4">
          <label htmlFor="fechaInicio" className="form-label">Fecha de inicio:</label>
          <input type="date" id="fechaInicio" className="form-control" />
        </div>
        <br />
        <div className="col-md-4">
          <label htmlFor="fechaFin" className="form-label">Fecha de fin:</label>
          <input type="date" id="fechaFin" className="form-control" />
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Buscar</button>
        </div>
      </form>
      <br />
      <hr />
      <br />

      <h2>Préstamos</h2>
      <br />

      <h3>Pagos normales</h3>
      <br />

      <h3>Pagos extraordinarios</h3>
    </div>
    </div>
    </div>
  );
}

export default ClientePG;
