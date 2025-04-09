package com.example.tecbank

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.activity.ComponentActivity
import org.json.JSONArray
import org.json.JSONObject
import java.io.*
import java.io.InputStream
import java.io.InputStreamReader

class MainActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val usuarioEditText: EditText = findViewById(R.id.usuario)
        val contrasenaEditText: EditText = findViewById(R.id.contrasena)
        val ingresarButton: Button = findViewById(R.id.button)
        val registrarButton: Button = findViewById(R.id.btnRegistrar)

        registrarButton.setOnClickListener {
            val intent = Intent(this, RegistroActivity::class.java)
            startActivity(intent)
        }

        ingresarButton.setOnClickListener {
            val usuario = usuarioEditText.text.toString()
            val contrasena = contrasenaEditText.text.toString()

            // Verificar si el usuario y la contraseña son correctos
            if (verificarUsuario(usuario, contrasena)) {
                // Si es válido, mostrar un mensaje de éxito y navegar a la pantalla de cuentas
                Toast.makeText(this, "Ingreso exitoso", Toast.LENGTH_SHORT).show()

                // Intent para ir a la nueva actividad
                val intent = Intent(this, CuentasActivity::class.java)
                startActivity(intent)
            } else {
                // Si no es válido, mostrar un mensaje de error
                Toast.makeText(this, "Usuario o contraseña incorrectos", Toast.LENGTH_SHORT).show()
            }
        }

    }

    // Función para verificar si el usuario y contraseña existen en el archivo JSON
    private fun verificarUsuario(usuario: String, contrasena: String): Boolean {
        try {
            val archivo = File(filesDir, "usuarios.json")
            val jsonString = if (archivo.exists()) {
                archivo.readText()
            } else {
                assets.open("usuarios.json").bufferedReader().use { it.readText() }
            }

            // Convertir el JSON en un array
            val jsonArray = JSONArray(jsonString)

            // Iterar sobre los usuarios y verificar si coinciden
            for (i in 0 until jsonArray.length()) {
                val jsonObject: JSONObject = jsonArray.getJSONObject(i)
                val usuarioJson = jsonObject.getString("usuario")
                val contrasenaJson = jsonObject.getString("contrasena")

                // Si el usuario y la contraseña coinciden, retornamos true
                if (usuario == usuarioJson && contrasena == contrasenaJson) {
                    return true
                }
            }
        } catch (e: Exception) {
            e.printStackTrace() // Imprimir el error en el Logcat
            Toast.makeText(this, "Error al leer el archivo JSON", Toast.LENGTH_SHORT).show()
        }

        // Si no encontramos una coincidencia, retornamos false
        return false
    }
}