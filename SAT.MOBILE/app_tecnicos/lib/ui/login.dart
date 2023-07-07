import 'dart:convert';

import 'package:app_tecnicos/constants/constants.dart';
import 'package:app_tecnicos/ui/home.dart';
import 'package:http/http.dart' as http;
import 'package:app_tecnicos/types/login.types.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:localstorage/localstorage.dart';
import '../main.dart';

class LoginFormScreen extends State<LoginForm> {
  final _formKey = GlobalKey<FormState>();
  final dio = Dio();

  Future<bool> login(String codUsuario, String senha) async {
    final response = await http.post(
      Uri.parse('${Constants.apiUrl}/Usuario/Login'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(
          <String, String>{'codUsuario': codUsuario, 'senha': senha}),
    );

    if (response.statusCode == 200) {
      final jsonDecoded = jsonDecode(response.body);
      final retorno = UsuarioRetornoModel.fromJSON(jsonDecoded);

      if (retorno.usuario.codUsuario.isNotEmpty) {
        final LocalStorage storage = LocalStorage(Constants.appName);

        storage.setItem('usuario', retorno.usuario);
        storage.setItem('token', retorno.token);

        return true;
      } else {
        return false;
      }
    } else {
      return false;
    }
  }

  @override
  Widget build(BuildContext context) {
    TextEditingController codUsuarioController = TextEditingController();
    TextEditingController senhaController = TextEditingController();

    return Form(
      key: _formKey,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Align(
              alignment: Alignment.bottomCenter,
              child: Image.asset(
                'assets/images/logo.png',
                width: 80,
                height: 80,
              ),
            ),
          ),
          Padding(
            padding: const EdgeInsets.symmetric(vertical: 6, horizontal: 32),
            child: TextFormField(
              controller: codUsuarioController,
              decoration: const InputDecoration(
                icon: Icon(Icons.person),
                hintText: 'Código de usuário',
                labelText: 'Usuário *',
              ),
              onSaved: (String? value) {},
              validator: (String? value) {
                return (value == "") ? 'favor preencher este campo' : null;
              },
            ),
          ),
          Padding(
            padding: const EdgeInsets.symmetric(vertical: 0, horizontal: 32),
            child: TextFormField(
              controller: senhaController,
              decoration: const InputDecoration(
                icon: Icon(Icons.lock),
                hintText: 'Senha',
                labelText: 'Senha *',
              ),
              onSaved: (String? value) {},
              validator: (String? value) {
                return (value == "") ? 'favor preencher este campo' : null;
              },
            ),
          ),
          Align(
            alignment: Alignment.centerRight,
            child: Padding(
              padding: const EdgeInsets.all(32.0),
              child: ElevatedButton(
                onPressed: () {
                  if (_formKey.currentState!.validate()) {
                    ScaffoldMessenger.of(context).showSnackBar(
                      const SnackBar(content: Text('Autenticando...')),
                    );

                    // bool isLogado =
                    //     login(codUsuarioController.text, senhaController.text)
                    //         as bool;

                    if (true) {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => const HomeScreen()),
                      );
                    } else {
                      ScaffoldMessenger.of(context).showSnackBar(
                        const SnackBar(
                            content: Text('Usuário ou senha incorretos...')),
                      );
                    }
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
