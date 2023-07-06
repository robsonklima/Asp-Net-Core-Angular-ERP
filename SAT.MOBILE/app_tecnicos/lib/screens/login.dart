import 'dart:convert';

import 'package:app_tecnicos/constants/constants.dart';
import 'package:app_tecnicos/screens/home.dart';
import 'package:app_tecnicos/services/local-storage.service.dart';
import 'package:http/http.dart' as http;
import 'package:app_tecnicos/types/login.types.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import '../main.dart';

class LoginFormScreen extends State<LoginForm> {
  final _formKey = GlobalKey<FormState>();
  final dio = Dio();

  void login(String codUsuario, String senha) async {
    final response = await http.post(
      Uri.parse('${Constants.API_URL}/Usuario/Login'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(
          <String, String>{'codUsuario': codUsuario, 'senha': senha}),
    );

    if (response.statusCode == 200) {
      final jsonDecoded = jsonDecode(response.body);
      final retorno = UsuarioRetornoModel.fromJSON(jsonDecoded);
      LocalStorageService.save('usuario', retorno.usuario);
      // ignore: use_build_context_synchronously
      Navigator.push(
        context,
        MaterialPageRoute(builder: (context) => const HomeScreen()),
      );
    } else {
      throw Exception('Failed to load album');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Form(
      key: _formKey,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // ignore: prefer_const_constructors
          Padding(
            padding: const EdgeInsets.symmetric(vertical: 12, horizontal: 32),
            child: const Text(
              'Entre com seu usuário e senha do SAT Web',
              textAlign: TextAlign.center,
              overflow: TextOverflow.ellipsis,
              style: TextStyle(fontWeight: FontWeight.bold),
            ),
          ),
          Padding(
            padding: const EdgeInsets.symmetric(vertical: 6, horizontal: 32),
            child: TextFormField(
              decoration: const InputDecoration(
                icon: Icon(Icons.person),
                hintText: 'Código de usuário',
                labelText: 'Usuário *',
              ),
              onSaved: (String? value) {},
              validator: (String? value) {
                return (value == null) ? 'favor preencher este campo' : null;
              },
            ),
          ),
          Padding(
            padding: const EdgeInsets.symmetric(vertical: 0, horizontal: 32),
            child: TextFormField(
              decoration: const InputDecoration(
                icon: Icon(Icons.lock),
                hintText: 'Senha',
                labelText: 'Senha *',
              ),
              onSaved: (String? value) {},
              validator: (String? value) {
                return (value == null) ? 'favor preencher este campo' : null;
              },
            ),
          ),
          Align(
            alignment: Alignment.centerRight,
            child: Padding(
              padding: const EdgeInsets.symmetric(vertical: 32, horizontal: 16),
              child: ElevatedButton(
                onPressed: () {
                  if (_formKey.currentState!.validate()) {
                    login("ealmanca", "Eroa@608");

                    ScaffoldMessenger.of(context).showSnackBar(
                      const SnackBar(content: Text('Autenticando...')),
                    );
                  }
                },
                child: const Text('Entrar'),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
