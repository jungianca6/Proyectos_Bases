import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { FaRegUser } from "react-icons/fa";
import { MdLockOutline } from "react-icons/md";
import React from "react";
import './Login.css';

function Login({ setUser }) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleLogin = (e) => {
        e.preventDefault();

        if (username === "admin" && password === "admin123") {
            setUser("admin");
            navigate("/admin");
        } else if (username === "cliente" && password === "cliente123") {
            setUser("cliente");
            navigate("/cliente");
        } else {
            alert("Credenciales incorrectas");
        }
    };

    return (
        <div className="login-wrapper">
        <div className="wrapper">
            <form onSubmit={handleLogin}>

                <h1>TecBank</h1>

                <div className="input-box">
                    <input type="text" placeholder="Usuario" value={username} onChange={(e) => setUsername(e.target.value)} required />
                    <FaRegUser className="icon"/>
                </div>

                <div className="input-box">
                    <input type="password" placeholder="Contraseña" value={password} onChange={(e) => setPassword(e.target.value)} required />
                    <MdLockOutline className="icon"/>
                </div>

                <button type="submit">Ingresar</button>

                <div className="register-link">
                    <p><a href="#">Registrarse</a></p>
                </div>

            </form>
        </div>
        </div>
    );
}

export default Login;