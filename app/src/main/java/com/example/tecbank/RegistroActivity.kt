package com.example.tecbank

import android.os.Bundle
import android.content.Intent
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.activity.ComponentActivity
import org.json.JSONArray
import org.json.JSONObject
import java.io.*

class RegistroActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_registro)

        val nuevoUsuario: EditText = findViewById(R.id.nuevo_usuario)
        val nuevaContrasena: EditText = findViewById(R.id.nueva_contrasena)
        val registrarButton: Button = findViewById(R.id.registrar_button)

        registrarButton.setOnClickListener {
            val usuario = nuevoUsuario.text.toString()
            val contrasena = nuevaContrasena.text.toString()

            if (usuario.isNotEmpty() && contrasena.isNotEmpty()) {
                if (agregarUsuario(usuario, contrasena)) {
                    Toast.makeText(this, "Usuario registrado", Toast.LENGTH_SHORT).show()
                    startActivity(Intent(this, MainActivity::class.java))
                    finish()
                } else {
                    Toast.makeText(this, "El usuario ya existe", Toast.LENGTH_SHORT).show()
                }
            } else {
                Toast.makeText(this, "Completa todos los campos", Toast.LENGTH_SHORT).show()
            }
        }
    }

    private fun agregarUsuario(usuario: String, contrasena: String): Boolean {
        try {
            val file = File(filesDir, "usuarios.json")

            // Si no existe, lo crea con un array vacío
            if (!file.exists()) {
                file.writeText("[]")
            }

            val jsonArray = JSONArray(file.readText())

            // Verifica si el usuario ya existe
            for (i in 0 until jsonArray.length()) {
                if (jsonArray.getJSONObject(i).getString("usuario") == usuario) {
                    return false
                }
            }

            val nuevoUsuario = JSONObject()
            nuevoUsuario.put("usuario", usuario)
            nuevoUsuario.put("contrasena", contrasena)

            jsonArray.put(nuevoUsuario)

            // Escribe de vuelta el archivo
            file.writeText(jsonArray.toString())

            return true
        } catch (e: Exception) {
            e.printStackTrace()
            return false
        }
    }
}