import 'package:app_tecnicos/screens/home.dart';
import 'package:app_tecnicos/services/local-storage.service.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import '../constants/constants.dart';
import '../main.dart';

class LoginFormScreen extends State<LoginForm> {
  final _formKey = GlobalKey<FormState>();
  final dio = Dio();

  login(String codUsuario, String senha) async {
    Response<String> response = await dio.post(
        '${Constants.API_URL}/Usuario/Login',
        data: {'codUsuario': codUsuario, 'senha': senha});

    if (response.statusCode == 200) {
      LocalStorageService.save('usuario', response.data!);
    }

    // ignore: use_build_context_synchronously
    Navigator.of(context)
        .push(MaterialPageRoute(builder: (context) => const HomeScreen()));
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
