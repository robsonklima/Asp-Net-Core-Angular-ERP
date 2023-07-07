import 'dart:convert';
import 'package:app_tecnicos/constants/constants.dart';
import 'package:app_tecnicos/types/ordem-servico.types.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({Key? key}) : super(key: key);

  Future<List<OrdemServicoModel>> fetchOrdensServico() async {
    final List<OrdemServicoModel> chamados;
    final response =
        await http.get(Uri.parse('${Constants.apiUrl}/OrdemServico'));

    if (response.statusCode == 200) {
      List jsonResponse = json.decode(response.body);

      chamados =
          jsonResponse.map((data) => OrdemServicoModel.fromJSON(data)).toList();
    } else {
      throw Exception('Failed to load data');
    }

    return chamados;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.teal,
        title: const Text('Lista de Chamados'),
      ),
      body: Column(
        children: [
          Expanded(
            child: FutureBuilder<List<OrdemServicoModel>>(
              future: fetchOrdensServico(),
              builder: (context, snapshot) {
                if (snapshot.hasError) {
                  return Center(
                    child: Text(
                      snapshot.error.toString(),
                    ),
                  );
                }

                if (!snapshot.hasData) {
                  return const Center(
                    child: CircularProgressIndicator(),
                  );
                }

                List<OrdemServicoModel>? chamados = snapshot.data;

                return chamados != null && chamados.isNotEmpty
                    ? ListView.builder(
                        itemCount: chamados.length,
                        itemBuilder: (_, i) {
                          return Text(chamados[i].codOS!);
                        },
                      )
                    : const Center(
                        child: Text('No data'),
                      );
              },
            ),
          ),
        ],
      ),
    );
  }
}
