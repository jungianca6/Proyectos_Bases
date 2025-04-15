package com.example.tecbank

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.activity.ComponentActivity
import okhttp3.*
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import org.json.JSONObject
import java.io.IOException


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

            obtenerDatosUsuarios(usuario, contrasena)
        }
    }

    private fun obtenerDatosUsuarios(usuario: String, contrasena: String) {
        val client = OkHttpClient()

        // Crear JSON de solicitud
        val json = JSONObject().apply {
            put("usuario", usuario)
            put("contrasena", contrasena)
        }

        val requestBody = RequestBody.create(
            "application/json; charset=utf-8".toMediaTypeOrNull(),
            json.toString()
        )

        val request = Request.Builder()
            .url("https://b4b6-201-204-89-80.ngrok-free.app/MenuInicio/Login")
            .post(requestBody)
            .build()

        client.newCall(request).enqueue(object : Callback {
            override fun onFailure(call: Call, e: IOException) {
                Log.e("MainActivity", "Fallo en la solicitud: ${e.message}")
                runOnUiThread {
                    Toast.makeText(
                        this@MainActivity,
                        "Error de red: ${e.message}",
                        Toast.LENGTH_LONG
                    ).show()
                }
            }

            override fun onResponse(call: Call, response: Response) {
                response.use {
                    if (!response.isSuccessful) {
                        Log.e("MainActivity", "Error del servidor: ${response.code}")
                        runOnUiThread {
                            Toast.makeText(
                                this@MainActivity,
                                "Error del servidor: ${response.code}",
                                Toast.LENGTH_LONG
                            ).show()
                        }
                    } else {
                        val responseBody = response.body?.string()
                        Log.d("MainActivity", "Respuesta exitosa: $responseBody")

                        try {
                            val jsonResponse = JSONObject(responseBody)
                            val success = jsonResponse.getBoolean("success")

                            runOnUiThread {
                                if (success) {
                                    val usuarioActual= jsonResponse.getJSONObject("usuario_actual")
                                    val cuentaActual= jsonResponse.getJSONObject("cuenta_actual")

                                    val intent =
                                        Intent(this@MainActivity, CuentasActivity::class.java).apply{
                                            putExtra("nombre", usuarioActual.getString("nombre"))
                                            putExtra("usuario", usuarioActual.getString("usuario"))
                                            putExtra("numeroCuenta", cuentaActual.getString("numeroDeCuenta"))
                                        }
                                    startActivity(intent)
                                } else {
                                    Toast.makeText(
                                        this@MainActivity,
                                        "Usuario o contraseña incorrectos",
                                        Toast.LENGTH_LONG
                                    ).show()
                                }
                            }
                        } catch (e: Exception) {
                            Log.e("MainActivity", "Error al parsear la respuesta: ${e.message}")
                            runOnUiThread {
                                Toast.makeText(
                                    this@MainActivity,
                                    "Respuesta inválida del servidor",
                                    Toast.LENGTH_LONG
                                ).show()
                            }
                        }
                    }
                }
            }
        })
    }
}
