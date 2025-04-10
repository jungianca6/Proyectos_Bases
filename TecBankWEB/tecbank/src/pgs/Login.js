import { useState } from "react";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { FaRegUser } from "react-icons/fa";
import { MdLockOutline } from "react-icons/md";
import React from "react";
import axios from "axios";
import './Login.css';

function Login({ setUser }) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [isRegistering, setIsRegistering] = useState(false);
    const [cuenta, setCuenta] = useState(null);
    const [newUser, setNewUser] = useState({
        nombre: "",
        apellido1: "",
        apellido2: "",
        cedula: "",
        direccion: "",
        telefono: "",
        ingresoMensual: "",
        tipoDeCliente: "",
        usuario: "",
        contrasena: "",
        adminRol: false
    });

    const [users, setUsers] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        // Cargar usuarios guardados al iniciar
        const savedUsers = JSON.parse(localStorage.getItem("usuarios")) || [];
        setUsers(savedUsers);

        const cuentaGuardada = JSON.parse(localStorage.getItem("cuenta_actual"));
        if (cuentaGuardada) {
            setCuenta(cuentaGuardada);
        }
    }, []);

    const handleLogin = async (e) => {
        e.preventDefault();
    
        // Verificar usuarios predefinidos
        if (username === "admin" && password === "admin123") {
            const adminUser = {
                nombre: "Admin",
                apellido1: "",
                apellido2: "",
                cedula: "",
                direccion: "",
                telefono: "",
                ingresoMensual: "",
                tipoDeCliente: "Administrador",
                usuario: "admin",
                contrasena: "admin123",
                adminRol: true
            };
            setUser("admin");
            localStorage.setItem("usuario_actual", JSON.stringify(adminUser));
            navigate("/admin");
            return;
        }
    
        try {
            const response = await axios.post("https://localhost:7190/MenuInicio/Login", {
                usuario: username,   // Cambiar 'username' a 'usuario'
                contrasena: password // Cambiar 'password' a 'contrasena'
            });
        
            if (response.data.success) {
                const usuario = response.data.usuario_actual;
                const cuenta = response.data.cuenta_actual;
                console.log("Datos de la cuenta despues de guardar:", cuenta);
                console.log("Datos de la cuenta despues de guardar:", usuario);

                setUser(usuario); // Aquí guardas el usuario completo
                localStorage.setItem("usuario_actual", JSON.stringify(usuario)); // Guarda el usuario actualmente loggeado en la web.
                
                setCuenta(cuenta);
                localStorage.setItem("cuenta_actual", JSON.stringify(cuenta));
                console.log("Cuenta guardada en localStorage:", localStorage.getItem("cuenta_actual"));
        
                alert("Logeo exitoso");
        
                // Verificar si adminRol es verdadero
                if (usuario.adminRol === true) {
                    console.log("Redirigiendo a admin");
                    navigate("/admin");
                } else {
                    console.log("Redirigiendo a cliente");
                    navigate("/cliente");
                }
            } else {
                alert(response.data.message);
            }
        } catch (error) {
            console.error(error);
            alert("Error al conectar con el servidor");
        }
    };

    const handleRegister = async (e) => {
        e.preventDefault();
        // Enviar datos de registro al backend

        try {
            const response = await axios.post("https://localhost:7190/MenuInicio/Registro", newUser);
            if (response.data.success) {
                alert("Usuario registrado exitosamente");
                setIsRegistering(false);
            } else {
                alert("Error al registrar usuario");
            }
        } catch (error) {
            console.error(error);
            alert("Error al conectar con el servidor");
        }
    };

    return (
        <div className="login-wrapper">
            <div className="wrapper">
                {isRegistering ? (
                    <form onSubmit={handleRegister}>
                        <h1>Registro</h1>

                        <input type="text" placeholder="Nombre" required onChange={(e) => setNewUser({ ...newUser, nombre: e.target.value })} />
                        <input type="text" placeholder="Primer Apellido" required onChange={(e) => setNewUser({ ...newUser, apellido1: e.target.value })} />
                        <input type="text" placeholder="Segundo Apellido" required onChange={(e) => setNewUser({ ...newUser, apellido2: e.target.value })} />
                        <input type="text" placeholder="Cédula" required onChange={(e) => setNewUser({ ...newUser, cedula: e.target.value })} />
                        <input type="text" placeholder="Dirección" required onChange={(e) => setNewUser({ ...newUser, direccion: e.target.value })} />
                        <input type="text" placeholder="Teléfono" required onChange={(e) => setNewUser({ ...newUser, telefono: e.target.value })} />
                        <input type="number" placeholder="Ingreso Mensual" required onChange={(e) => setNewUser({ ...newUser, ingresoMensual: e.target.value })} />

                        <select required onChange={(e) => setNewUser({ ...newUser, tipoDeCliente: e.target.value })}>
                        <option value="">Selecciona tipo de cliente</option>
                        <option value="Fisico">Físico</option>
                        <option value="Juridico">Jurídico</option>
                        </select>

                        <input type="text" placeholder="Usuario" required onChange={(e) => setNewUser({ ...newUser, usuario: e.target.value })} />
                        <input type="password" placeholder="Contraseña" required onChange={(e) => setNewUser({ ...newUser, contrasena: e.target.value })} />

                        <button type="submit">Registrarse</button>
                        <p className="register-link">
                            ¿Ya tienes una cuenta?{" "}
                            <span onClick={() => setIsRegistering(false)} style={{ cursor: 'pointer', color: 'white' }}>Inicia sesión</span>
                        </p>
                    </form>
                ) : (
                    <form onSubmit={handleLogin}>
                        <h1>TecBank</h1>

                        <div className="input-box">
                            <input type="text" placeholder="Usuario" value={username} onChange={(e) => setUsername(e.target.value)} required />
                            <FaRegUser className="icon" />
                        </div>

                        <div className="input-box">
                            <input type="password" placeholder="Contraseña" value={password} onChange={(e) => setPassword(e.target.value)} required />
                            <MdLockOutline className="icon" />
                        </div>

                        <button type="submit">Ingresar</button>

                        <div className="register-link">
                            <p><span onClick={() => setIsRegistering(true)} style={{ cursor: 'pointer', color: 'white' }}>Registrarse</span></p>
                        </div>
                    </form>
                )}
            </div>
        </div>
    );
}

export default Login;