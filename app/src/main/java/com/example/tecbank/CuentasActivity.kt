package com.example.tecbank

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.LinearLayout
import android.widget.TextView
import android.widget.Toast
import android.graphics.Color
import androidx.activity.ComponentActivity
import org.json.JSONArray
import java.io.InputStream
import java.io.InputStreamReader
import java.io.IOException
import okhttp3.*
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import org.json.JSONObject



class CuentasActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_cuentas)
        val layoutMovimientos: LinearLayout = findViewById(R.id.layout_movimientos)
        val layoutCuentas: LinearLayout = findViewById(R.id.layout_cuentas)
        val textoView: TextView = findViewById(R.id.texto_cuenta)
        val transferenciaButton: Button = findViewById(R.id.transferencias_button)
        val tarjetasButton: Button = findViewById(R.id.tarjetas_button)
        val prestamosButton: Button = findViewById(R.id.prestamos_button)

        val nombre = intent.getStringExtra("nombre") ?: ""
        val usuario = intent.getStringExtra("usuario") ?: ""
        val numeroCuenta = intent.getStringExtra("numeroCuenta") ?: ""

        textoView.text = "Usuario: $usuario\nNombre: $nombre\nCuenta: $numeroCuenta"


        transferenciaButton.setOnClickListener {
            val intent = Intent(this, TransferenciaActivity::class.java)
            startActivity(intent)

        }
        tarjetasButton.setOnClickListener {
            val intent = Intent(this, TarjetasActivity::class.java)
            startActivity(intent)

        }
        prestamosButton.setOnClickListener {
            val intent = Intent(this, PrestamosActivity::class.java)
            startActivity(intent)

        }
        obtenerMovimientos(numeroCuenta)
    }

    private fun obtenerMovimientos(numeroCuenta: String) {
        val client = OkHttpClient()

        val json = JSONObject().apply {
            put("numeroDeCuenta", numeroCuenta)
        }

        val requestBody = RequestBody.create(
            "application/json; charset=utf-8".toMediaTypeOrNull(),
            json.toString()
        )

        val request = Request.Builder()
            .url("https://b4b6-201-204-89-80.ngrok-free.app/Movimiento/ListadoDeMovimientos")
            .post(requestBody)
            .build()

        client.newCall(request).enqueue(object : Callback {
            override fun onFailure(call: Call, e: IOException) {
                runOnUiThread {
                    Toast.makeText(
                        this@CuentasActivity,
                        "Error al cargar movimientos",
                        Toast.LENGTH_SHORT
                    ).show()
                }
            }

            override fun onResponse(call: Call, response: Response) {
                response.use {
                    if (response.isSuccessful) {
                        val responseBody = response.body?.string()
                        val jsonResponse = JSONObject(responseBody)
                        val retiros = jsonResponse.getJSONArray("retiros")
                        val pagosTarjetas = jsonResponse.getJSONArray("pagos_tarjetas")
                        val pagosPrestamos = jsonResponse.getJSONArray("pagos_prestamos")
                        val transferencias = jsonResponse.getJSONArray("transferencias")

                        runOnUiThread {
                            mostrarMovimientos("Retiros", retiros)
                            mostrarMovimientos("Pagos Tarjetas", pagosTarjetas)
                            mostrarMovimientos("Pagos Préstamos", pagosPrestamos)
                            mostrarMovimientos("Transferencias", transferencias)
                        }
                    } else {
                        runOnUiThread {
                            Toast.makeText(
                                this@CuentasActivity,
                                "Error del servidor",
                                Toast.LENGTH_SHORT
                            ).show()
                        }
                    }
                }
            }
        })
    }

    fun mostrarMovimientos(titulo: String, array: JSONArray) {
        val layoutMovimientos: LinearLayout = findViewById(R.id.layout_movimientos)

        val tituloText = TextView(this).apply {
            text = "$titulo (${array.length()})"
            setTextColor(Color.WHITE)
            textSize = 16f
        }
        layoutMovimientos.addView(tituloText)

        for (i in 0 until array.length()) {
            val item = array.getJSONObject(i)
            val detalle = TextView(this).apply {
                text = item.toString() // Puedes formatear aquí si conoces los campos
                setTextColor(Color.WHITE)
            }
            layoutMovimientos.addView(detalle)
        }
    }
}