import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';

import '../types/album.types.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({Key? key}) : super(key: key);

  Future<List<Album>> fetchAlbuns() async {
    final response = await http
        .get(Uri.parse('https://jsonplaceholder.typicode.com/albums'));

    if (response.statusCode == 200) {
      // final teste = Album.fromJson(jsonDecode(response.body));
      // final teste2 = 123;

      List jsonResponse = json.decode(response.body);
      final albuns = jsonResponse.map((data) => Album.fromJson(data)).toList();
      final teste = 12;
    } else {
      throw Exception('Failed to load album');
    }

    return [];
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.blue,
        title: const Text('Lista de Chamados'),
        automaticallyImplyLeading: false,
      ),
      body: ListView(
        children: <Widget>[
          ListTile(
            title: Text('97229380'),
            onTap: () {
              print('Sun');
            },
          ),
          ListTile(
            title: Text('97229312'),
            onTap: () {
              print('Moon');
            },
          ),
          ListTile(
            title: Text('97223307'),
            onTap: () {
              print('Star');
            },
          ),
        ],
      ),
    );
  }
}
