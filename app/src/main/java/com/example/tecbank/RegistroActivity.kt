package com.example.tecbank

import android.os.Bundle
import android.content.Intent
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.activity.ComponentActivity
import org.json.JSONObject
import okhttp3.*
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import java.io.IOException

class RegistroActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        val client = OkHttpClient()
        setContentView(R.layout.activity_registro)

        val registrarButton: Button = findViewById(R.id.registrar_button)

        registrarButton.setOnClickListener {
            // Captura los datos ingresados por el usuario al presionar el botón
            val nombre = findViewById<EditText>(R.id.nombre).text.toString()
            val apellido1 = findViewById<EditText>(R.id.apellido1).text.toString()
            val apellido2 = findViewById<EditText>(R.id.apellido2).text.toString()
            val cedula = findViewById<EditText>(R.id.cedula).text.toString()
            val direccion = findViewById<EditText>(R.id.direccion).text.toString()
            val telefono = findViewById<EditText>(R.id.telefono).text.toString()
            val ingresoMensual = findViewById<EditText>(R.id.ingresoMensual).text.toString().toIntOrNull() ?: 0
            val usuario = findViewById<EditText>(R.id.usuario).text.toString()
            val contrasena = findViewById<EditText>(R.id.contrasena).text.toString()

            // Crear el cuerpo del JSON con los campos requeridos
            val json = JSONObject().apply {
                put("cedula", cedula)
                put("direccion", direccion)
                put("telefono", telefono)
                put("ingresoMensual", ingresoMensual)
                put("nombre", nombre)
                put("apellido1", apellido1)
                put("apellido2", apellido2)
                put("tipoDeCliente", "ola")
                put("usuario", usuario)
                put("contrasena", contrasena)
                put("adminRol", false)
            }

            // Log para verificar el JSON
            Log.e("RegistroActivity", "$json")

            // Verificar que todos los campos estén llenos
            if (nombre.isNotEmpty() && apellido1.isNotEmpty() && apellido2.isNotEmpty() &&
                cedula.isNotEmpty() && direccion.isNotEmpty() && telefono.isNotEmpty() &&
                ingresoMensual > 0 && usuario.isNotEmpty() && contrasena.isNotEmpty()) {

                // Enviar la solicitud al backend
                registrarUsuarioEnBackend(json, client)
            } else {
                Toast.makeText(this, "Por favor, completa todos los campos", Toast.LENGTH_SHORT).show()
            }
        }
    }

    private fun registrarUsuarioEnBackend(json: JSONObject, client: OkHttpClient) {
        val requestBody = RequestBody.create(
            "application/json; charset=utf-8".toMediaTypeOrNull(),
            json.toString()
        )


        val request = Request.Builder()
            .url("https://b973-201-202-14-53.ngrok-free.app/MenuInicio/Registro")
            .post(requestBody)
            .build()

        client.newCall(request).enqueue(object : Callback {
            override fun onFailure(call: Call, e: IOException) {
                runOnUiThread {
                    Toast.makeText(this@RegistroActivity, "Error de conexión", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onResponse(call: Call, response: Response) {
                runOnUiThread {
                    if (response.isSuccessful) {
                        try {
                            val responseBody = response.body?.string()
                            val jsonResponse = JSONObject(responseBody)
                            Log.e("MainActivity", "$jsonResponse")

                            // Verificar si la respuesta contiene "success": true
                            if (jsonResponse.optBoolean("success", false)) {
                                Toast.makeText(this@RegistroActivity, "Usuario registrado con éxito", Toast.LENGTH_SHORT).show()
                                startActivity(Intent(this@RegistroActivity, MainActivity::class.java))
                                finish()
                            } else {
                                Toast.makeText(this@RegistroActivity, "Error al registrar: ${jsonResponse.optString("message")}", Toast.LENGTH_SHORT).show()
                            }
                        } catch (e: Exception) {
                            Toast.makeText(this@RegistroActivity, "Error al procesar la respuesta del servidor", Toast.LENGTH_SHORT).show()
                        }
                    } else {
                        Toast.makeText(this@RegistroActivity, "Error al registrar: ${response.code}", Toast.LENGTH_SHORT).show()
                    }
                }
            }
        })
    }

}