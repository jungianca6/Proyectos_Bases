package com.example.tecbank

data class Cuenta(
    val numeroCuenta: String,
    val tipo: String,
    val moneda: String,
    var saldo: Double
)

